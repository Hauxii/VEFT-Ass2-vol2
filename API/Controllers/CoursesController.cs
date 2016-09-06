using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using Ass2.Models;
using Ass2.Services;

namespace Ass2.API.Controllers
{
    [Route("/api/courses")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _service;

        public CoursesController(ICoursesService service){
            _service = service;
        }


        [HttpGet]
        public List<CourseLiteDTO> GetCourses(String semester = null)
        {
            //TODO: return list of elements taugh in that "semester"
            //else return list of courses
            return _service.GetCoursesBySemester(semester);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCourseByID")]
        public IActionResult GetCourseByID(int id)
        {
            //TODO: Should return a more detailed object describing that course
            // return 404 if it doesn't exist   
            return Ok();
        }
        
        /*
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditCourse(int id, [FromBody]Course toEdit)
        {
            //TODO: Should allow the client of the API to modify the given course 
            //instance. The properties which should be mutable are StartDate and 
            //EndDate, others (CourseID and Semester) should be immutable.
            //404 if it doesn't exist

            var location = Url.Link("GetCourseByID", new {id = toEdit.ID});
            return Ok();
        }
        */

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCourse(int id)
        {
            //TODO: should remove the given course
            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}/students")]
        public IActionResult GetStudents()
        {
            //TODO: Should return a list of all students in that course
            //404 if course doesn't exist
            return Ok();
        }

        [HttpPost]
        [Route("{id:int}/students")]
        public IActionResult AddStudentToCourse()
        {
            //TODO: should add a new student to that course, 
            //It is assumed that the request body contains the
            return StatusCode(201);
        }
    
    
    }


}