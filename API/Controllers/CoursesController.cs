using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using Ass2.Models;
using Ass2.Services;

namespace Ass2.API.Controllers
{
    /// <summary>
    /// Represents resources for courses
    /// </summary>
    [Route("/api/courses")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService _service;

        public CoursesController(ICoursesService service){
            _service = service;
        }

        /// <summary>
        /// This method returns a list of courses taught in the given semester, current semester if no semester is given
        /// </summary>
        /// <param name="semester">The semester you wish to see courses for</param>
        /// <returns>List of courses</returns>
        [HttpGet]
        public IActionResult GetCourses(String semester = null)
        {
            //TODO: return list of elements taught in that "semester"
            //else return list of courses
            return Ok(_service.GetCoursesBySemester(semester));
        }


        /// <summary>
        /// This method returns a single course based on the ID in the url
        /// </summary>
        /// <param name="id">The unique ID of the course</param>
        /// <returns>A course</returns>
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
        
        /// <summary>
        /// This method allows the client of the API to modify the given course instance
        /// </summary>
        /// <param name="toEdit">The edited instance of the course</param>
        /// <param name="id">The unique ID of the course</param>
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditCourse([FromBody]EditCourseViewModel toEdit, int id)
        {

            CourseDetailsDTO dto = _service.GetCourseByID(id);

            if(dto != null){
                 _service.EditCourse(toEdit, id);
                 return Ok();
            }
            else{
                return NotFound();
            }
            
        }
        
        /// <summary>
        /// This method deletes an existing course
        /// </summary>
        /// <param name="id">The unique ID of the course</param>
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCourse(int id)
        {
            //TODO: should remove the given course
            return NoContent();
        }


        /// <summary>
        /// This method returns a list of students in a course
        /// </summary>
        /// <param name="id">The unique ID of the course</param>
        /// <returns>A list of students</returns>
        [HttpGet]
        [Route("{id:int}/students", Name="GetStudents")] 
        public IActionResult GetStudents(int id)
        {
            CourseDetailsDTO dto = _service.GetCourseByID(id);

            if(dto == null){
                 return NotFound();
            }
            else{
                return Ok(_service.GetStudentsInCourse(id));
            }
        }


        /// <summary>
        /// This method adds a new student to a course
        /// </summary>
        /// <param name="toAdd">New instance of student</param>
        /// <param name="id">The unique ID of the course</param>
        [HttpPost]
        [Route("{id:int}/students", Name="AddStudentToCourse")]
        public IActionResult AddStudentToCourse([FromBody]AddStudentViewModel toAdd, int id)
        {
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