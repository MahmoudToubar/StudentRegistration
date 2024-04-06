using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.DTO;
using StudentRegistration.Models;
using StudentRegistration.Repositories;

namespace StudentRegistration.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper) 
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetStudents();

            return Ok(mapper.Map<List<Models.Student>>(students));
          
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute] Guid studentId)
        {
            var student = await studentRepository.GetStudent(studentId);

            if (student == null) 
            {
                return NotFound();
            }

            return Ok(mapper.Map<Models.Student>(student));

        }
        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if(await studentRepository.Exists(studentId)) 
            {
               var updatedStudent = await studentRepository.UpdateStudent(studentId, mapper.Map<Models.Student>(request));

                if(updatedStudent != null)
                {
                    return Ok(mapper.Map<Models.Student>(updatedStudent));
                }
            }          
                return NotFound();          
        }
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exists(studentId))
            {
                var student = await studentRepository.DeleteStudent(studentId);
                return Ok(mapper.Map<Models.Student>(student));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudent([FromBody] AddStudentRequest request)
        {
           var student = await studentRepository.AddStudent(mapper.Map<Models.Student>(request));
           return CreatedAtAction(nameof(GetStudent), new {studentId = student.Id},
               mapper.Map<Models.Student>(student));
        }
    }

    
}
