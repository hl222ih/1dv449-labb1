using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace labb1._1dv449.Models
{
    public class Scraper
    {
        private List<string> links;
        private ScrapeResult result;
        private string filename;

        public Scraper(string startUrl)
        {
            //locally
            //filename = HttpContext.Current.Server.MapPath("~/App_Data/result.json");
            //server
            filename = HttpContext.Current.Server.MapPath(@"~/data/result.json");

            result = new ScrapeResult();
            links = new List<string>();
            links.Add(startUrl);
        }

        public void Crawl(bool forceScraping = false)
        {
            //don't scrape if it's recently done.
            if (!forceScraping && IsRecentlyScraped()) return;

            HtmlWeb htmlWeb = new HtmlWeb();
            //identifying the bot when making http requests.
            htmlWeb.UserAgent += " Bot by hl222ih@student.lnu.se for educational purposes.";

            int count = 0;
            while (links.Count > count)
            {
                try
                {

                    string url = links[count];
                    if (!url.StartsWith(@"http"))
                    {
                        count++;
                        continue;
                    }

                    HtmlDocument htmlDoc = htmlWeb.Load(url);
                    
                    //only need to add urls from startUrl
                    if (count == 0)
                    {
                        Uri baseUri = new Uri(links[count], UriKind.Absolute);
                        var urls = GetCourseUrls(htmlDoc, baseUri);
                        AddUrlsToList(urls);
                    }

                    //only get course info from course web urls
                    //should be made safer to not allow sub pages, however seems to not be needed in this particular case.
                    if (url.Contains(@"//coursepress.lnu.se/kurs/") && !url.Contains(@"/?p="))
                    {
                        var courseInfo = GetCourseInfo(htmlDoc, url);
                        if (courseInfo != null)
                        {
                            result.AddCourse(courseInfo);
                        }
                    }
                }
                catch
                {
                    count++;
                    continue;
                }
                count++;
            }
            //need to add an extra hour because of localization, should check for a better solution...
            result.FinishedAt = DateTime.Now.AddHours(1);
            //save the result as Json-file
            SaveDataAsJsonFile();
        }

        private List<string> GetCourseUrls(HtmlDocument htmlDoc, Uri baseUri)
        {
            List<string> linkList = new List<string>();

            //leta upp alla länkar och kolla om de leder till kurssidor
            var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            
            if (linkNodes != null)
            {
                linkList = linkNodes.Select(node => node.GetAttributeValue("href", "")).ToList();
            }

            for (int i = 0; i < linkList.Count; i++)
            {
                Uri absoluteUri = new Uri(baseUri, linkList[i]);
                linkList[i] = absoluteUri.AbsoluteUri;
            }
            return linkList;
        }

        private void AddUrlsToList(List<string> urls)
        {
            foreach (string url in urls)
            {
                try
                {
                    var checkedUrl = new Uri(url).ToString();
                    if (!links.Contains(checkedUrl))
                    {
                        links.Add(checkedUrl);
                    }
                }
                catch
                {
                    //do nothing with malformed urls.
                }
            }
        }

        private CourseInfo GetCourseInfo(HtmlDocument htmlDoc, string url)
        {
            var course = new CourseInfo();

            //get course web url
            course.CourseWebUrl = url;

            //get course code (first attempt)
            var node = htmlDoc.DocumentNode.SelectSingleNode("//article[header[h2[span[text()='Fakta']]]]");
            if (node != null)
            {
                node = node.SelectSingleNode("em[text()='Kurskod:']/following-sibling::text()[1]");
                string courseCode = String.Empty;
                if (node != null)
                {
                    courseCode = node.InnerText.Replace(System.Environment.NewLine, " ").Trim();
                    if (!courseCode.Equals(String.Empty))
                    {
                        course.CourseCode = courseCode;
                    }
                }
            }

            //get course name
            node = htmlDoc.DocumentNode.SelectSingleNode("//head/meta[@name='description']");
            if (node != null)
            {
                var metaContent = node.GetAttributeValue("content", "");
                course.CourseName = metaContent.Split('|')[0].Trim();
            }

            node = htmlDoc.DocumentNode.SelectSingleNode("//article/div[@class='entry-content']");
            if (node != null)
            {
                //sometimes a messagebox is present in the introduction section. in case it is, remove it
                var messageBox = node.SelectSingleNode("article");
                if (messageBox != null)
                {
                    node.RemoveChild(messageBox, false);
                }
                //not sure about the purpose of this property, redisplay as html or just text? 
                //depending on the purpose, it might be needed to clean the text from carriage returns and linefeeds,
                //or change InnerText to InnerHtml, and more.
                course.Introduction = HttpUtility.HtmlDecode(node.InnerText).Trim();
            }

            node = htmlDoc.DocumentNode.SelectSingleNode("//a[contains(@href,'coursesyllabus')]");
            if (node != null)
            {
                var href = node.GetAttributeValue("href", "");
                if (!String.IsNullOrEmpty(href))
                {
                    course.SyllabusUrl = HttpUtility.HtmlDecode(href);

                    //this is second attempt to get course code
                    if (course.CourseCode.Equals("no information"))
                    {
                        var re = new Regex(@"code=\b(\w+?)\b");
                        var m = re.Match(href);
                        if (m.Success)
                        {
                            course.CourseCode = m.Groups[1].Value;
                        }
                    }
                }
            }

            //get info about latest post
            node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='latest-post']");
            if (node != null)
            {
                //get the article adjacant to post header.
                node = node.NextSibling.NextSibling;

                if (node != null)
                {
                    var nodeEntryTitle = node.SelectSingleNode("header/*[@class='entry-title']");
                    if (nodeEntryTitle != null)
                    {
                        var nodeTitle = nodeEntryTitle.FirstChild;


                        if (nodeTitle != null)
                        {
                            var title = nodeTitle.GetAttributeValue("title", "");
                            if (!String.IsNullOrEmpty(title))
                            {
                                course.LatestArticleHeading = title;
                            }
                        }
                    }

                    var nodeEntryByLine = node.SelectSingleNode("header/*[@class='entry-byline']");

                    if (nodeEntryByLine != null)
                    {
                        var about = nodeEntryByLine.InnerHtml;

                        if (!String.IsNullOrEmpty(about))
                        {
                            var reTime = new Regex(@"(\d{4}-\d{2}-\d{2} \d{2}:\d{2})");
                            var mTime = reTime.Match(about);
                            if (mTime.Success)
                            {
                                course.LatestArticleTimestamp = mTime.Groups[1].Value;
                            }

                            var reAuthor = new Regex(@"av <strong>([^<]+)</strong>");
                            var mAuthor = reAuthor.Match(about);
                            if (mAuthor.Success)
                            {
                                course.LatestArticleAuthor = mAuthor.Groups[1].Value;
                            }

                        }

                    }
                }
            }

            return course;
        }

        public string GetDataAsJson()
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this.result);
            return serializedResult;
        }

        private bool SaveDataAsJsonFile()
        {
            try
            {
                
                var json = GetDataAsJson();
                File.Delete(filename);
                File.WriteAllText(@filename, json);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool IsRecentlyScraped()
        {
            var text = File.ReadAllText(filename);
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.DeserializeObject(text);

            
            var finishedAtJson = jsonObject["FinishedAt"];
            DateTime finishedAt = (DateTime)finishedAtJson;
            if (finishedAt.Subtract(DateTime.Now.AddMinutes(-5)).TotalMilliseconds > 0)
            {
                return true;
            }
            return false;
        }
    }
}