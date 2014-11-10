using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace labb1._1dv449.Models
{
    public class Scraper
    {
        public string HelloWorld { get; set; }

        public Scraper()
        {
            HelloWorld = "Hello World!";
        }

        public string GetDataAsJson()
        {
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(this);
            return serializedResult;
        }
    }
}