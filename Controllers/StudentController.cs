using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchollApi;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Essa configuração já vem por padrão, mas pode ser trodaca por XML por exemplo
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentController : ControllerBase
    {
        private IStudenService studenService;

        public StudentController(IStudenService studenService)
        {
            this.studenService = studenService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudent() 
        {
            //TODO: Ciar um middleware para pegar os erros de exceção
            var students = await studenService.GetStudents();
            return Ok(students);
        }

        [HttpGet("student-by-name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudentByName([FromQuery] string name)
        {
            var students = await studenService.GetStudentByName(name);
            if(students is null || students.ToList().Count() == 0)
            {
                return NotFound($"Nenhum estudante encontrado com o nome {name}");
            }

            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await studenService.GetStudent(id);
            if(student is null)
            {
                return NotFound($"Nenhum estudante encontrado com o id {id}");
            }

            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            try
            {
                var newStudent = await studenService.Create(student);
                return CreatedAtRoute(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
            }
            catch (Exception ex)
            {
                return BadRequest("Request Inválido -> " + ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> Update(int id, [FromBody] Student student)
        {
            try
            {
                if(student.Id != id)
                {
                    return BadRequest("Id do estudante não corresponde ao id informado");
                }

                var updatedStudent = await studenService.Update(student);
                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                return BadRequest("Request Inválido -> " + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await studenService.GetStudent(id);
            if(student is null)
            {
                return NotFound($"Nenhum estudante encontrado com o id {id}");
            }

            await studenService.Delete(student);
            return NoContent();
        }
    }
}
