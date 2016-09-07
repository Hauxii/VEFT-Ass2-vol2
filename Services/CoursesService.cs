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
                orderby y.Name
                select new CourseLiteDTO
                {
                    ID = x.ID,
                    Name = y.Name,
                    Semester = x.Semester
                });
            
            if(semester != null){
                return query.Where(x => x.Semester == semester).ToList();
            }
            else{
                return query.Where(x => x.Semester == "20163").ToList();
            }
            
        }

        public CourseLiteDTO GetCourseByID(int id){
            return (from x in _db.Courses
            join y in _db.CourseTemplates on x.TemplateID equals y.ID
            where x.ID == id
            select new CourseLiteDTO{
                ID = x.ID,
                Name = y.Name,
                Semester = x.Semester
            }).SingleOrDefault();
        }
        
        public void AddCourse(AddCourseViewModel model)
        {
            //TODO: Validation
            //1. Validate that the templateid is valid and coursetemplate exists
            //2. validate that the semester is valid
            
            var course = new Course
            {
                TemplateID = model.TemplateID,
                Semester = model.Semester
            };

            _db.Courses.Add(course);
            _db.SaveChanges();
        }

    }
}