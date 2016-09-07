using System.Collections.Generic;
using Ass2.Models;

namespace Ass2.Services
{
    public interface ICoursesService
    {
        List<CourseLiteDTO> GetCoursesBySemester(string semester);
        CourseDetailsDTO GetCourseByID(int id);
        void AddCourse(AddCourseViewModel model);
        void EditCourse(EditCourseViewModel model, int id);
        List<StudentLiteDTO> GetStudentsInCourse(int id);
        bool AddStudentToCourse(AddStudentViewModel model, int id);
        void DeleteCourse(int id);
    }
}