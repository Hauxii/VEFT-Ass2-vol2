using System;

namespace Ass2.Services.Entities
{
    public class Course
    {
        public int ID { get; set; }

        public string TemplateID { get; set; }

        public string Semester { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}