using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "getAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            var students = CollegeRepository.Students.Select(n => new StudentDTO()
            {
                Id = n.Id,
                StudentName = n.StudentName,
                Address = n.Address,
                Email = n.Email,

            });
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
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
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
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,

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
            var studentName = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
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
                StudentName = studentName.StudentName,
                Address = studentName.Address,
                Email = studentName.Email,

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

            var emailExist = CollegeRepository.Students.Any(s => s.Email.Equals(model.Email));

            if (emailExist)
                return BadRequest($"{model.Email} is already existed.Please try with another email.");

            if (model == null)
                return BadRequest();

            var newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
            };
            CollegeRepository.Students.Add(student);
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

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("You are trying to update the value that are not present.");
            }
            existingStudent.StudentName = model.StudentName;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

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

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound("You are trying to update the value that are not present.");
            }

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Address = existingStudent.Address,
                Email = existingStudent.Email,

            };
            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;

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
            var deleteStudent = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            if (deleteStudent == null)
            {
                return NotFound($"Student with this id {id} is not found."); //404 - not found
            }

            if (id <= 0)
            {
                return BadRequest($"You are trying to delete the data with id {id} that are not in our scope."); //400
            }
            CollegeRepository.Students.Remove(deleteStudent);
            return Ok($"Your data has been deleted of id {id}");
        }
    }
}
