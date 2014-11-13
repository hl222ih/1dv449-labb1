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
        public int NumberOfCourses
        {
            get
            {
                return Courses.Count;
            }
        }

        public DateTime FinishedAt { get; set; }

        public ScrapeResult()
        {
            Courses = new List<CourseInfo>();
        }

        public void AddCourse(CourseInfo course)
        {
            var added = false;
            for (int i = 0; i < Courses.Count; i++ )
            {
                var cmpValue = Courses[i].CourseCode.CompareTo(course.CourseCode);
                if (cmpValue == 1)
                {
                    Courses.Insert(i, course);
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                Courses.Add(course);
            }
        }

        public ReadOnlyCollection<CourseInfo> GetCourses()
        {
            return Courses.AsReadOnly();
        }
    }
}