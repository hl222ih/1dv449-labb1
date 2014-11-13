using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using labb1._1dv449.Models;
using System.Text;

namespace labb1._1dv449.Controllers
{
    public class ScraperController : ApiController
    {
        // GET: api/Scraper
        public void Get()
        {
            //prepare scraper
            Scraper scraper = new Scraper("http://coursepress.lnu.se/kurser");

            //begin scraping
            scraper.Crawl();

            //get the result from the scraping as json data
            string jsonData = scraper.GetDataAsJson();

            //save the result as Json-file
            scraper.SaveDataAsJsonFile();

            //temporarily return the scraped json data in a response
            //var response = this.Request.CreateResponse(HttpStatusCode.OK);
            //response.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            //return response;
        }
    }
}
