using System.Collections.Generic;
using Ass2.Models;
using System.Linq;

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
            var course = new CourseLiteDTO
            {

            };
        }

    }
}