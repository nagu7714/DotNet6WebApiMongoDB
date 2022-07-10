using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Helpers;
using StudentManagement.Models;
using StudentManagement.Services;


namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/<StudentsController>

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            return Ok(_studentService.Get());
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            var student = _studentService.GetById(id);
            if(student != null)
            {
                return Ok(student);
            }
            else
            {
                throw new KeyNotFoundException($"Student with Id = {id} not found");
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Student> Post([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                _studentService.Create(student);

                return CreatedAtAction(nameof(Get), new { student.Id }, student);
            }
            throw new AppException($"Invalid Request Body");
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public ActionResult Put(string id,[FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                var studentResult = _studentService.GetById(id);

                if (studentResult != null)
                {
                    _studentService.Update(id, student);

                     return Ok($"Student with Id = {id} Updated");
                }
                else
                {
                    throw new KeyNotFoundException($"Student with Id = {id} not found");
                }
            }
            else
            {
                throw new AppException($"Invalid Request Body");
            }
                
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var studentResult = _studentService.GetById(id);

            if (studentResult != null)
            {
                _studentService.Delete(id);
                return Ok($"Student with Id = {id} deleted");
            }
            else
            {
                throw new KeyNotFoundException($"Student with Id = {id} not found");
            }
        }
    }
}
