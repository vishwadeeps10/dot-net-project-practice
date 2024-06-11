namespace CollegeApp.data.Repository
{
    public interface IStudentCasteCertificateDetailsRepository : ICollegeRepository<StudentCasteCertificateDetails>
    {

        Task<StudentCasteCertificateDetails> GetByStudentIdAsync(int studentId);
    }

}
