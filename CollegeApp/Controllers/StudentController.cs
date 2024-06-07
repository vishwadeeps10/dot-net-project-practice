using CollegeApp.data;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDbContext _dbContext;
        public StudentController(ILogger<StudentController> logger, CollegeDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("All", Name = "getAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            var students = _dbContext.Students.Select(n => new StudentDTO()
            {
                Id = n.Id,
                Entollment_no = n.Entollment_no,
                Name = n.Name,
                Fathers_name = n.Fathers_name,
                Email = n.Email,
                Date_of_birth = n.Date_of_birth,
                Gender = n.Gender,
                Category = n.Category,
                Address = n.Address,
                Added_On = n.Added_On,

            }).ToList();

            if (!students.Any())
            {
                return NotFound("Data is not available");
            }

            return Ok(students);
        }

        [HttpGet("{id:int}", Name = "getStuentDetailsById")]
        //[Route("{id}", Name = "getStuentDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {

            var student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (id <= 0)
            {
                return BadRequest($"You are trying to fetching the data with id {id} that are not in our scope."); //400
            }
            if (student == null)
            {
                return NotFound($"Student with this id {id} is not found."); //404 - not found
            }

            var studentsDTO = new StudentDTO
            {
                Id = student.Id,
                Entollment_no = student.Entollment_no,
                Name = student.Name,
                Fathers_name = student.Fathers_name,
                Email = student.Email,
                Date_of_birth = student.Date_of_birth,
                Gender = student.Gender,
                Category = student.Category,
                Address = student.Address,
                Added_On = student.Added_On,

            };
            return Ok(studentsDTO);
        }
        [HttpGet]
        // [Route("{name:string}", Name = "getStuentDetailsByName")]

        [Route("{name:alpha}", Name = "getStuentDetailsByName")] //alpha is used for alphabatical because string giving error
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            var studentName = _dbContext.Students.Where(n => n.Name == name).FirstOrDefault();
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"You are trying to fetching the data with id {name} that are not in our scope."); //400
            }
            if (studentName == null)
            {
                return NotFound($"Student with this id {name} is not found."); //404 - not found
            }
            var studentsDTO = new StudentDTO
            {
                Id = studentName.Id,
                Entollment_no = studentName.Entollment_no,
                Name = studentName.Name,
                Fathers_name = studentName.Fathers_name,
                Email = studentName.Email,
                Date_of_birth = studentName.Date_of_birth,
                Gender = studentName.Gender,
                Category = studentName.Category,
                Address = studentName.Address,
                Added_On = studentName.Added_On,

            };
            return Ok(studentsDTO);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Create")]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            // if (model.AddimisonDate < DateTime.Now) //here direcly added err msg to modelState. We can validate using custom attribute.
            //{
            //   ModelState.AddModelError("Addimison Error", "Addimision data should be same as today date.");
            // return BadRequest(ModelState);
            // }


            //if(ModelState.IsValid)
            //{
            //    return BadRequest(ModelState.ToString());
            //} This code is validating the input value if [ApiController] is not added.

            var emailExist = _dbContext.Students.Any(s => s.Email.Equals(model.Email));

            if (emailExist)
                return BadRequest($"{model.Email} is already existed.Please try with another email.");

            if (model == null)
                return BadRequest();

            Student student = new Student
            {
                Entollment_no = model.Entollment_no,
                Name = model.Name,
                Fathers_name = model.Fathers_name,
                Email = model.Email,
                Date_of_birth = model.Date_of_birth,
                Gender = model.Gender,
                Category = model.Category,
                Address = model.Address,
                Added_On = DateTime.Now,
            };
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            model.Id = student.Id;

            return CreatedAtRoute("getStuentDetailsById", new { id = model.Id }, model);
            // return Ok(model);

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Update")]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("You are trying to update the value that are not present.");
            }

            existingStudent.Name = model.Name;
            existingStudent.Fathers_name = model.Fathers_name;
            existingStudent.Email = model.Email;
            existingStudent.Gender = model.Gender;
            existingStudent.Category = model.Category;
            existingStudent.Address = model.Address;

            _dbContext.SaveChanges();

            return NoContent();

        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id:int}/PartialUpdate")]
        public ActionResult<StudentDTO> PartialUpdate(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("You are trying to update the value that are not present.");
            }

            var studentDTO = new StudentDTO
            {

                Name = existingStudent.Name,
                Address = existingStudent.Address,
                Email = existingStudent.Email,

            };
            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.Name = studentDTO.Name;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;

            _dbContext.SaveChanges();
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("Delete/{id}", Name = "deleteStudentById")]
        //[Route("{id:min(1):max(100)}", Name = "deleteStudentById")] we can add constaint as well
        public ActionResult<string> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var deleteStudent = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();

            if (deleteStudent == null)
            {
                return NotFound($"Student with this id {id} is not found."); //404 - not found
            }

            if (id <= 0)
            {
                return BadRequest($"You are trying to delete the data with id {id} that are not in our scope."); //400
            }
            _dbContext.Students.Remove(deleteStudent);
            _dbContext.SaveChanges();
            return Ok(true);
        }
    }
}
