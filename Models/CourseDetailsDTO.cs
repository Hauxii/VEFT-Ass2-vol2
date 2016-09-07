using System;

namespace Ass2.Models
{
    public class CourseDetailsDTO
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
