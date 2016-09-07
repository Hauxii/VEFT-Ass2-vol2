using System.Collections.Generic;
using Ass2.Models;

namespace Ass2.Services
{
    public interface ICoursesService
    {
        List<CourseLiteDTO> GetCoursesBySemester(string semester);
        CourseLiteDTO GetCourseByID(int id);
        void AddCourse(AddCourseViewModel model);
    }
}