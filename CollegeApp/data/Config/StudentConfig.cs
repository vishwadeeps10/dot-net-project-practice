using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Entollment_no).IsRequired();
            builder.Property(x => x.Entollment_no).HasMaxLength(100);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.Property(x => x.Fathers_name).IsRequired();
            builder.Property(x => x.Fathers_name).HasMaxLength(100);

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200);

            builder.Property(x => x.Date_of_birth).IsRequired();

            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Gender).HasMaxLength(100);

            builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.Category).HasMaxLength(100);

            builder.Property(x => x.Address).IsRequired(false).HasMaxLength(400);

        }
    }
}
