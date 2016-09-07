using System;
using System.Collections.Generic;

namespace Ass2.Models
{
    public class CourseDetailsDTO
    {
        public String Name { get; set; }
        public String Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StudentLiteDTO> StudentsInCourse { get; set; }
    }
}
