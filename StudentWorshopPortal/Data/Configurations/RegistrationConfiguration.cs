using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Data.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.ToTable("Registrations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.StudentFullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(r => r.StudentNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(r => r.EmailAddress)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.Comments)
            .HasMaxLength(1000);

        builder.HasIndex(r => new
        {
            r.StudentNumber,
            r.WorkshopId
        })
        .IsUnique();

        builder.HasOne(r => r.Workshop)
            .WithMany(w => w.Registrations)
            .HasForeignKey(r => r.WorkshopId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
