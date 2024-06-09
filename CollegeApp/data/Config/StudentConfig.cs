﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(x => x.Entollment_no)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Fathers_name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(x => x.Date_of_birth)
                   .IsRequired();

            builder.Property(x => x.Gender)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Category)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Address)
                   .HasMaxLength(400);
        }
    }

}
