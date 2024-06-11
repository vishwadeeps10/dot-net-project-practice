using Microsoft.EntityFrameworkCore;

namespace CollegeApp.data.Repository
{
    public class StudentCasteCertificateDetailsRepository : CollegeRepository<StudentCasteCertificateDetails>, IStudentCasteCertificateDetailsRepository
    {
        private readonly CollegeDbContext _dbContext;

        public StudentCasteCertificateDetailsRepository(CollegeDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentCasteCertificateDetails> GetByStudentIdAsync(int studentId)
        {
            return await _dbContext.StudentCasteCertificateDetails
                .FirstOrDefaultAsync(sccd => sccd.StudentId == studentId);
        }
    }
}
