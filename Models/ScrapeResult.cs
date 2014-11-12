using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace labb1._1dv449.Models
{
    public class ScrapeResult
    {
        public List<CourseInfo> Courses { get; set; }
        public int NumberOfCourses { get; set; }

        public DateTime FinishedAt { get; set; }

        public ScrapeResult()
        {
            Courses = new List<CourseInfo>();
        }

        public void AddCourse(CourseInfo course)
        {
            Courses.Add(course);
        }

        public ReadOnlyCollection<CourseInfo> GetCourses()
        {
            return Courses.AsReadOnly();
        }
    }
}