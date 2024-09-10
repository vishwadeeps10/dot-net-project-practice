using AutoMapper;
using CollegeApp.data;
using CollegeApp.data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CollegeApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly CollegeDbContext _dbContext;
        //public readonly ICollegeRepository<Student> _studentRepository;
        public readonly IStudentRepository _studentRepository;
        public readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly ICache _cacheService;



        public StudentController(ILogger<StudentController> logger, 
            CollegeDbContext dbContext, 
            IStudentRepository studentRepository, 
            IMapper mapper, 
            IHttpClientFactory httpClientFactory,
            ICache cacheService
            )
        {
            _logger = logger;
            _dbContext = dbContext;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _httpClient = httpClientFactory.CreateClient("casteCertificateClient");
            _cacheService = cacheService;
        }

        [HttpGet]
        [Route("All", Name = "getAllStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudent()
        {
            try
            {

                string cacheKey = "all_data";

                var cacheItem = await _cacheService.GetData<List<StudentDTO>>(cacheKey);

                if (cacheItem != null) 
                { 
                    return Ok(cacheItem);
                }

                if (!_dbContext.Students.Any())
                {
                    return NotFound("Data is not available");
                }

                var students = await _studentRepository.GetAllAsync();

                if (students == null || !students.Any())
                {
                    return NotFound("No students found");
                }

                var studentDTOs = _mapper.Map<List<StudentDTO>>(students);

                await _cacheService.SetData<List<StudentDTO>>(cacheKey, studentDTOs, TimeSpan.FromMinutes(30));

                return Ok(studentDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now}:- An error occurred while fetching students: {ex.InnerException} ErrorMessage: {ex.Message}");


                return StatusCode(500, $"{DateTime.Now}:- Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id:int}", Name = "getStuentDetailsById")]
        //[Route("{id}", Name = "getStuentDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest($"You are trying to fetch the data with id {id} that is not in our scope.");
                }

                string cacheKey = $"Item_{id}";
                var cachedItem = await _cacheService.GetData<StudentDTO>(cacheKey);

                if (cachedItem != null)
                {
                    return Ok(cachedItem);
                }

                var student = await _studentRepository.GetByIdAsync(s => s.Id == id);

                if (student == null)
                {
                    return NotFound($"Student with this id {id} is not found.");
                }
                var studentDTO = _mapper.Map<StudentDTO>(student);

                await _cacheService.SetData(cacheKey, studentDTO, TimeSpan.FromMinutes(30));

                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching student with id {id}");

                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // [Route("{name:string}", Name = "getStuentDetailsByName")]

        //[Route("{name:alpha}", Name = "getStuentDetailsByName")] //alpha is used for alphabatical because string giving error
        [HttpGet]
        [Route("getStuentByName")] //alpha is used for alphabatical because string giving error
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByName([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest($"You are trying to fetch the data with name {name} that is not in our scope.");
                }

                var studentName = await _studentRepository.GetByNameAsync(student => student.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower());

                if (studentName == null)
                {
                    return NotFound($"Student with the name {name} is not found.");
                }

                var studentDTO = _mapper.Map<StudentDTO>(studentName);
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching student with the name {name}");

                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody] StudentDTO model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Student model is null.");

                var emailExist = await _dbContext.Students.AnyAsync(s => s.Email.Equals(model.Email));
                if (emailExist)
                    return BadRequest($"{model.Email} is already existed. Please try with another email.");

                var student = new Student
                {
                    Enrollment_no = model.Enrollment_no,
                    Name = model.Name,
                    Fathers_name = model.Fathers_name,
                    Email = model.Email,
                    Date_of_birth = model.Date_of_birth,
                    Gender = model.Gender,
                    Category = model.Category,
                    Address = model.Address,
                    Added_On = DateTime.Now
                };

                var studentdb = await _studentRepository.CreateAsync(student);

                if (model.AdmissionDetails != null)
                {
                    var admissionDetails = new AdmissionDetails
                    {
                        Student_ID = studentdb.Id,
                        Class_ID = model.AdmissionDetails.Class_ID,
                        Previous_Class_ID = model.AdmissionDetails.Previous_Class_ID,
                        Annual_Family_Income = model.AdmissionDetails.Annual_Family_Income,
                        Cast_Certificate_ID = model.AdmissionDetails.Cast_Certificate_ID,
                        Added_On = DateTime.Now,
                        Added_By = "Admin"
                    };

                    await _dbContext.Addmision_Details.AddAsync(admissionDetails);
                    await _dbContext.SaveChangesAsync();
                }

                if (model.CasteCertificateDetails != null)
                {
                    var casteDetailsDto = new CasteCertificateDetailsDTO
                    {
                        CasteCertiNo = model.CasteCertificateDetails.CasteCertiNo,
                        CasteCertiUrl = model.CasteCertificateDetails.CasteCertiUrl,
                        CasteCode = model.CasteCertificateDetails.CasteCode,
                        StudentName = model.Name,
                        RecievedOn = model.CasteCertificateDetails.RecievedOn,
                        RecievedBy = model.CasteCertificateDetails.RecievedBy,
                        StudentId = studentdb.Id
                    };

                    var response = await _httpClient.PostAsJsonAsync("api/StudentCasteCertificateDetails/Create", casteDetailsDto);

                    if (!response.IsSuccessStatusCode)
                        return StatusCode((int)response.StatusCode, "Failed to create caste certificate details");
                }

                model.Id = studentdb.Id;

                return CreatedAtRoute("getStuentDetailsById", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the student.");

                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
        public async Task<ActionResult<StudentDTO>> UpdateStudent([FromBody] StudentDTO model)
        {
            try
            {
                if (model == null || model.Id <= 0)
                    return BadRequest();

                var existingStudent = await _studentRepository.GetByIdAsync(student => student.Id == model.Id);
                if (existingStudent == null)
                {
                    return NotFound("You are trying to update the value that is not present.");
                }

                _mapper.Map(model, existingStudent);

                // Update AdmissionDetails if provided
                if (model.AdmissionDetails != null)
                {
                    if (existingStudent.AdmissionDetails == null)
                    {
                        existingStudent.AdmissionDetails = _mapper.Map<AdmissionDetails>(model.AdmissionDetails);
                    }
                    else
                    {
                        _mapper.Map(model.AdmissionDetails, existingStudent.AdmissionDetails);
                    }
                }

                existingStudent.Added_On = existingStudent.Added_On;

                bool flag = true;

                if (model.CasteCertificateDetails != null)
                {
                    var casteDetailsUpdateDto = _mapper.Map<CasteCertificateDetailsDTO>(model.CasteCertificateDetails);

                    var response = await _httpClient.PutAsJsonAsync("api/StudentCasteCertificateDetails/Update", casteDetailsUpdateDto);
                    if (!response.IsSuccessStatusCode)
                    {
                        flag = false;
                        return StatusCode((int)response.StatusCode, "Failed to update caste certificate details");
                    }
                }

                if (!flag)
                {
                    return BadRequest("Caste certificate is not updated.");
                }
                else
                {
                    await _studentRepository.UpdateAsync(existingStudent);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while updating the student.");

                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("Delete/{id}", Name = "deleteStudentById")]
        //[Route("{id:min(1):max(100)}", Name = "deleteStudentById")] we can add constaint as well
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var deleteStudent = await _studentRepository.GetByIdAsync(s => s.Id == id);

                if (deleteStudent == null)
                {
                    return NotFound($"Student with this id {id} is not found."); //404 - not found
                }

                if (id <= 0)
                {
                    return BadRequest($"You are trying to delete the data with id {id} that are not in our scope."); //400
                }
                await _studentRepository.DeleteByIdasync(deleteStudent);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the student.");

                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
