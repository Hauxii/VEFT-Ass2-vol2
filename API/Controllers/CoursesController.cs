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
        public IActionResult GetCourses(String semester = null)
        {
            //TODO: return list of elements taught in that "semester"
            //else return list of courses
            return Ok(_service.GetCoursesBySemester(semester));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetCourseByID")]
        public IActionResult GetCourseByID(int id)
        {
            //TODO: Should return a more detailed object describing that course
            CourseDetailsDTO dto = _service.GetCourseByID(id);

            if(dto != null){
                 return Ok(dto);
            }
            else{
                return NotFound();
            }
        }
        
        
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditCourse([FromBody]EditCourseViewModel toEdit, int id)
        {
            //TODO: Should allow the client of the API to modify the given course 
            //instance. The properties which should be mutable are StartDate and 
            //EndDate, others (CourseID and Semester) should be immutable.
            //404 if it doesn't exist

            //var location = Url.Link("GetCourseByID", new {id = toEdit.ID});
            CourseDetailsDTO dto = _service.GetCourseByID(id);

            if(dto != null){
                 _service.EditCourse(toEdit, id);
                 return Ok();
            }
            else{
                return NotFound();
            }
            
        }
        

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCourse(int id)
        {
            //TODO: should remove the given course
            return NoContent();
        }

        [HttpGet]
        [Route("{id:int}/students", Name="GetStudents")] 
        public IActionResult GetStudents(int id)
        {
            //TODO: Should return a list of all students in that course
            //404 if course doesn't exist
            CourseDetailsDTO dto = _service.GetCourseByID(id);

            if(dto == null){
                 return NotFound();
            }
            else{
                return Ok(_service.GetStudentsInCourse(id));
            }
        }

        [HttpPost]
        [Route("{id:int}/students", Name="AddStudentToCourse")]
        public IActionResult AddStudentToCourse([FromBody]AddStudentViewModel toAdd, int id)
        {
            //TODO: should add a new student to that course, 
            //It is assumed that the request body contains the

            bool success = _service.AddStudentToCourse(toAdd, id);
            if(success){
                return StatusCode(201);
            }
            else{
                return Ok();
            }
        }
    
    
    }


}