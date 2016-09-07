using System;

namespace Ass2.Models
{
    public class AddCourseViewModel
    {
        public string TemplateID { get; set; }
        public string Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}