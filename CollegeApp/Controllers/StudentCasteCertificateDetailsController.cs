using AutoMapper;
using CollegeApp.data;
using CollegeApp.data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentCasteCertificateDetailsController : ControllerBase
    {
        private readonly IStudentCasteCertificateDetailsRepository _CertificateRepository;
        private readonly IMapper _mapper;
        private readonly CollegeDbContext _dbContext;

        public StudentCasteCertificateDetailsController(IStudentCasteCertificateDetailsRepository repository, IMapper mapper, CollegeDbContext dbContext)
        {
            _CertificateRepository = repository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("All", Name = "GetCetificateDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<CasteCertificateDetailsDTO>>> GetStudent()
        {
            if (!_dbContext.Students.Any())
            {
                return NotFound("Data is not available");
            }
            var students = await _CertificateRepository.GetAllAsync();

            var studentCertiDTOs = _mapper.Map<List<CasteCertificateDetailsDTO>>(students);

            return Ok(studentCertiDTOs);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<CasteCertificateDetailsDTO>> Create([FromBody] CasteCertificateDetailsDTO model)
        {
            if (model == null)
                return BadRequest();

            var entity = _mapper.Map<StudentCasteCertificateDetails>(model);
            await _CertificateRepository.CreateAsync(entity);

            model.Id = entity.StudentId;
            CreatedAtRoute("getCertificateDetailsByStudentId", new { StudentID = model.Id }, model);
            return Ok(true);
        }

        [HttpGet("{StudentID:int}", Name = "getCertificateDetailsByStudentId")]
        //[Route("{id}", Name = "getStuentDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CasteCertificateDetailsDTO>> GetStudentById(int StudentID)
        {

            var student = await _CertificateRepository.GetByIdAsync(s => s.StudentId == StudentID);

            if (StudentID <= 0)
            {
                return BadRequest($"You are trying to fetching the data with id {StudentID} that are not in our scope."); //400
            }
            if (student == null)
            {
                return NotFound($"Student with this id {StudentID} is not found."); //404 - not found
            }

            var studentCertificateDTO = _mapper.Map<CasteCertificateDetailsDTO>(student);

            return Ok(studentCertificateDTO);
        }

        [HttpGet]
        [Route("getStuentCertificateByStudentName")] //alpha is used for alphabatical because string giving error
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CasteCertificateDetailsDTO>> GetStudentByName([FromQuery] string name)
        {

            var studentName = await _CertificateRepository.GetByNameAsync(s => s.StudentName.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest($"You are trying to fetching the data with id {name} that are not in our scope."); //400
            }
            if (studentName == null)
            {
                return NotFound($"Student with this id {name} is not found."); //404 - not found
            }
            var studentCertificateDTO = _mapper.Map<CasteCertificateDetailsDTO>(studentName);

            return Ok(studentCertificateDTO);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
        public async Task<ActionResult<CasteCertificateDetailsDTO>> UpdateStudent([FromBody] CasteCertificateDetailsDTO model)
        {
            if (model == null || model.StudentId <= 0)
                return BadRequest();

            var existingStudent = await _CertificateRepository.GetByIdAsync(student => student.StudentId == model.StudentId);
            if (existingStudent == null)
            {
                return NotFound("You are trying to update the value that is not present.");
            }

            _mapper.Map(model, existingStudent);

            // Update AdmissionDetails if provided
            // Ensure Added_On property remains unchanged
            existingStudent.RecievedOn = existingStudent.RecievedOn;
            existingStudent.StudentId = existingStudent.StudentId;

            await _CertificateRepository.UpdateAsync(existingStudent);
            return Ok(true);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("Delete/{studentId:int}", Name = "deleteCertificateByStudentId")]
        public async Task<ActionResult<string>> DeleteCertificate(int studentId)
        {
            if (studentId <= 0)
            {
                return BadRequest("Invalid student ID."); // 400 - Bad Request
            }

            var deleteStudent = await _CertificateRepository.GetByIdAsync(s => s.StudentId == studentId);

            if (deleteStudent == null)
            {
                return NotFound($"Student with ID {studentId} is not found."); // 404 - Not Found
            }

            await _CertificateRepository.DeleteByIdasync(deleteStudent);

            return Ok("Certificate deleted successfully."); // 200 - OK
        }
    }
}
