using System.Collections.Generic;
using Ass2.Models;
using Ass2.Services.Entities;

namespace Ass2.Services
{
    public interface ICoursesService
    {
        List<CourseLiteDTO> GetCoursesBySemester(string semester);
        CourseDetailsDTO GetCourseByID(int id);
        Course AddCourse(AddCourseViewModel model);
        void EditCourse(EditCourseViewModel model, int id);
        List<StudentLiteDTO> GetStudentsInCourse(int id);
        List<StudentLiteDTO> GetWaitinglist(int id);
        bool AddStudentToCourse(AddStudentViewModel model, int id);
        bool AddStudentToWaitinglist(AddStudentViewModel model, int id);
        void DeleteCourse(int id);
        bool StudentInStudents(AddStudentViewModel model);
        bool isCourseFull(int id);
        bool DeleteFromCourse(int id, string ssn);
    }
}