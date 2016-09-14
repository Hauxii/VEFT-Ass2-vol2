using System;
using System.Collections.Generic;
using Ass2.Models;
using System.Linq;
using Ass2.Services.Entities;

namespace Ass2.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly AppDataContext _db;
        public CoursesService(AppDataContext db)
        {
            _db = db;
        }

        public List<CourseLiteDTO> GetCoursesBySemester(string semester){
            
            var query = (from x in _db.Courses
                join y in _db.CourseTemplates on x.TemplateID equals y.ID
                orderby y.ID ascending
                select new CourseLiteDTO
                {
                    Name = y.ID,
                    Semester = x.Semester,
                    NumberOfStudents = (from cs in _db.CourseStudents
                        where cs.CourseID == x.ID
                        select cs).Count()
                });
            
            if(semester != null){
                return query.Where(x => x.Semester == semester).ToList();
            }
            else{
                return query.Where(x => x.Semester == "20163").ToList();
            }
            
        }

        public CourseDetailsDTO GetCourseByID(int id){
            return (from x in _db.Courses
            join y in _db.CourseTemplates on x.TemplateID equals y.ID
            where x.ID == id
            select new CourseDetailsDTO{
                Name = y.Name,
                Semester = x.Semester,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                StudentsInCourse = (from cs in _db.CourseStudents
                    join stu in _db.Students on cs.StudentSSN equals stu.SSN
                    where cs.CourseID == x.ID
                    select new StudentLiteDTO {
                        SSN = stu.SSN,
                        Name = stu.Name
                    }).ToList()
            }).SingleOrDefault();
        }
        
        public Course AddCourse(AddCourseViewModel model)
        {
            //TODO: Validation
            //1. Validate that the templateid is valid and coursetemplate exists
            //2. validate that the semester is valid

            Course course = new Course
            {
                TemplateID = model.TemplateID,
                Semester = model.Semester, 
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                MaxStudents = model.MaxStudents
            };

            _db.Courses.Add(course);
            _db.SaveChanges();

            return course;
        }

        public void EditCourse(EditCourseViewModel model, int id)
        {
            var course = _db.Courses.SingleOrDefault(x => x.ID == id);
            if(course != null){
                course.StartDate = model.StartDate;
                course.EndDate = model.EndDate;
                _db.SaveChanges();
            }
        }

        public List<StudentLiteDTO> GetStudentsInCourse(int id){
            return (from x in _db.Students
            join y in _db.CourseStudents on x.SSN equals y.StudentSSN
            join z in _db.Courses on y.CourseID equals z.ID
            where z.ID == id
            select new StudentLiteDTO{
                SSN = x.SSN,
                Name = x.Name
            }).ToList();
        }

        public bool AddStudentToCourse(AddStudentViewModel model, int id)
        {
            CourseStudent current = (from x in _db.CourseStudents
            where x.CourseID == id && x.StudentSSN == model.SSN
            select x).SingleOrDefault();

            if(current == null){
                var entry = new CourseStudent{
                    CourseID = id,
                    StudentSSN = model.SSN,
                    Active = true
                };

                _db.CourseStudents.Add(entry);
                _db.SaveChanges();
                return true;
            }
            
            else if(current.Active == false){
                current.Active = true;
                _db.SaveChanges();
                return true;
            }
            return false;           
            
        }

        public void DeleteCourse(int id)
        {
            Course temp = (from x in _db.Courses
            where x.ID == id 
            select x).SingleOrDefault();

            _db.Courses.Remove(temp);
            _db.SaveChanges();
        }

        public bool StudentInStudents(AddStudentViewModel model){
           var current = (from x in _db.Students
            where x.SSN == model.SSN
            select x).SingleOrDefault();
            
            if(current == null){
                return false;
            }
            else{
                return true;
            }
        }

        public bool AddStudentToWaitinglist(AddStudentViewModel model, int id)
        {
            StudentInWaitinglist current = (from x in _db.Waitinglist
            where x.CourseID == id && x.StudentSSN == model.SSN
            select x).SingleOrDefault();

            if(current == null){
                var entry = new StudentInWaitinglist{
                    CourseID = id,
                    StudentSSN = model.SSN,
                };

                _db.Waitinglist.Add(entry);
                _db.SaveChanges();
                return true;
            }
            
            return false; 
        }

        public List<StudentLiteDTO> GetWaitinglist(int id)
        {
            return (from x in _db.Students
            join y in _db.Waitinglist on x.SSN equals y.StudentSSN
            join z in _db.Courses on y.CourseID equals z.ID
            where z.ID == id
            select new StudentLiteDTO{
                SSN = x.SSN,
                Name = x.Name
            }).ToList();
        }

    }
}