
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public readonly CollegeDbContext _dbContext;

        public StudentRepository(CollegeDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<int> CreateAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteByIdasync(Student student)
        {

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool noTracking = false)
        {
            if (noTracking)
            {
                return await _dbContext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();

            }
            else
            {
                return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();

            }
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbContext.Students
                    .Where(student => student.Name.Replace(" ", "").ToLower() == name)
                    .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Student student)
        {

            _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();

            return student.Id;
        }
    }
}
