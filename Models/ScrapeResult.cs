using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace labb1._1dv449.Models
{
    public class ScrapeResult
    {
        private List<CourseInfo> courses { get; set; }

        public ScrapeResult()
        {
            courses = new List<CourseInfo>();
        }

        public void AddCourse(CourseInfo course)
        {
            courses.Add(course);
        }

        public ReadOnlyCollection<CourseInfo> GetCourses()
        {
            return courses.AsReadOnly();
        }
    }
}