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
        private readonly ILogger _studentLogger;

        public StudentsController(IStudentService studentService, ILogger<IStudentService> studentLogger)
        {
            _studentService = studentService;
            _studentLogger = studentLogger;
        }

        // GET: api/<StudentsController>

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await _studentService.GetAsync());
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student != null)
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
        public async Task<ActionResult<Student>> Post([FromBody]Student student)
        {



            if (Request.HasJsonContentType())
            {
               await _studentService.CreateAsync(student);

                return CreatedAtAction(nameof(Get), new { student.Id }, student);
            }
            else
            {

                throw new AppException("only application/json supported");
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public async Task<ActionResult> Put(string id,[FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                var studentResult =await _studentService.GetByIdAsync(id);

                if (studentResult != null)
                {
                    _studentService.UpdateAsync(id, student);

                     return Ok($"Student with Id = {id} Updated");
                }
                else
                {
                    throw new KeyNotFoundException($"Student with Id = {id} not found");
                }
            }
            else
            {
                throw new AppException("Invalid Request Body");
            }
                
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var studentResult = await _studentService.GetByIdAsync(id);

            if (studentResult != null)
            {
                _studentService.DeleteAsync(id);
                return Ok($"Student with Id = {id} deleted");
            }
            else
            {
                throw new KeyNotFoundException($"Student with Id = {id} not found");
            }
        }
    }
}
