using CollegeApp.data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AdmissionDetails> Addmision_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //StudentTable
            modelBuilder.ApplyConfiguration(new StudentConfig());
            //student details table confing
            modelBuilder.ApplyConfiguration(new AdmissionDetailsConfig());
        }
    }
}
