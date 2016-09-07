using System.ComponentModel.DataAnnotations;

namespace Ass2.Services.Entities
{
    public class Student
    {
        [Key]
        public string SSN { get; set; }
        public string Name { get; set; }
    }
}