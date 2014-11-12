using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labb1._1dv449.Models
{
    public class CourseInfo
    {
        //kursens namn
        public string CourseName {get; set;}
        //den url kurswebbplatsen har
        public string CourseWebUrl {get; set;}
        //kursens kurskod
        public string CourseCode {get; set;}
        //url till kursplanen
        public string SyllabusUrl {get; set;}
        //den inledande texten om varje kurs
        public string Introduction {get; set;}
        //senaste inläggets rubrik
        public string LatestArticleHeading {get; set;}
        //senaste inläggets författare
        public string LatestArticleAuthor {get; set;}
        //senaste inläggets datum/klockslag (på formatet YYYY-MM-DD HH:MM)
        public string LatestArticleTimestamp {get; set;}

        public CourseInfo()
        {
            CourseName = "no information";
            CourseWebUrl = "no information";
            CourseCode = "no information";
            SyllabusUrl = "no information";
            Introduction = "no information";
            LatestArticleHeading = "no information";
            LatestArticleAuthor = "no information";
            LatestArticleTimestamp = "no information";
        }
    }
}