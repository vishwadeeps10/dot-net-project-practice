using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.data.Config
{
    public class AdmissionDetailsConfig : IEntityTypeConfiguration<AdmissionDetails>
    {
        public void Configure(EntityTypeBuilder<AdmissionDetails> builder)
        {
            builder.ToTable("Addmision_Details");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.Student_ID)
                .IsRequired();

            builder.Property(x => x.Class_ID)
                .IsRequired();

        }
    }
}
