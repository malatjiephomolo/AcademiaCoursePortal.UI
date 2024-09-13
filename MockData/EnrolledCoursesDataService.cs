

// File: DummyDataGenerator.cs
using AcademiaCoursePortal.UI.Models;
using System.Collections.Generic;

namespace AcademiaCoursePortal.UI.MockData
{
    public class EnrolledCoursesDataService
    {
        public List<Course> GetDummyCourses()
        {
            return new List<Course>
            {

                new Course { Id = 2, Title = "Advanced C#", Description = "Deep dive into advanced C# topics and best practices." },
                new Course { Id = 3, Title = "Web Development with ASP.NET Core", Description = "Build modern web applications using ASP.NET Core." },
                new Course { Id = 5, Title = "Introduction to Blazor", Description = "Create interactive web UIs using Blazor and C#." }
            };
        }
    }
}

