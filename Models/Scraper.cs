using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using System.Net;

namespace labb1._1dv449.Models
{
    public class Scraper
    {
        private string HelloWorld { get; set; }
        private List<string> links;
        private ScrapeResult result;

        public Scraper(string startUrl)
        {
            result = new ScrapeResult();
            links = new List<string>();
            links.Add(startUrl);
            Crawl();
        }

        public void Crawl()
        {
            HtmlAgilityPack.HtmlWeb htmlWeb = new HtmlWeb();

            int count = 0;
            while (links.Count > count)
            {
                HtmlDocument htmlDoc = htmlWeb.Load(links[count]);

                Uri baseUri = new Uri(links[count], UriKind.Absolute);
                var urls = GetCourseUrls(htmlDoc, baseUri);
                AddUrlsToList(urls);

                var courseInfo = GetCourseInfo(htmlDoc);
                if (courseInfo != null)
                {
                    result.AddCourse(courseInfo);
                }
                count++;
            }

        }
        public string GetDataAsJson()
        {
            //var serializer = new JavaScriptSerializer();
            //var serializedResult = serializer.Serialize(this);
            //return serializedResult;
            return HelloWorld;
        }

        private List<string> GetCourseUrls(HtmlDocument htmlDoc, Uri baseUri)
        {
            //leta upp alla länkar och kolla om de leder till kurssidor
            var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            var linkList = linkNodes.Select(node => node.GetAttributeValue("href","")).ToList();

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

        private CourseInfo GetCourseInfo(HtmlDocument htmlDoc)
        {
            var course = new CourseInfo();
            //HelloWorld = htmlDoc.DocumentNode.OuterHtml;

            //fill cousrse info if exist in htmlDoc.
            return course;
        }
    }
}