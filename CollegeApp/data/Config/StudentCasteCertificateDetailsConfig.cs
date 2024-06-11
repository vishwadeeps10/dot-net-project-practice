using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.data.Config
{
    public class StudentCasteCertificateDetailsConfig : IEntityTypeConfiguration<StudentCasteCertificateDetails>
    {
        public void Configure(EntityTypeBuilder<StudentCasteCertificateDetails> builder)
        {
            builder.HasKey(sccd => sccd.Id);
            builder.Property(sccd => sccd.CasteCertiNo).HasMaxLength(50).IsRequired();
            builder.Property(sccd => sccd.CasteCertiUrl).IsRequired();
            builder.Property(sccd => sccd.CasteCode).IsRequired();
            builder.Property(sccd => sccd.StudentName).IsRequired();
            builder.Property(sccd => sccd.RecievedOn).IsRequired();
            builder.Property(sccd => sccd.RecievedBy).IsRequired();
        }
    }
}
