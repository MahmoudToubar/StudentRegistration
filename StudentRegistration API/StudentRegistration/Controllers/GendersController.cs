using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Models;
using StudentRegistration.Repositories;
using System.Linq;

namespace StudentRegistration.Controllers
{
    public class GendersController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public GendersController(IStudentRepository studentRepository, IMapper mapper) 
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetGenders()
        {
            var genderList = await studentRepository.GetGenders();

            if (genderList == null)
                
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<Gender>>(genderList));
        }
    }
}
