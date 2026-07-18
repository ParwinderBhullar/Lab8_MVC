using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentWorshopPortal.Models;

namespace StudentWorkshopPortal.Data.Configurations;

public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
{
    public void Configure(EntityTypeBuilder<Workshop> builder)
    {
        builder.ToTable("Workshops");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(w => w.Instructor)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(w => w.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(w => w.Description)
            .HasMaxLength(1000);

        builder.Property(w => w.Date)
            .IsRequired();

        builder.HasIndex(w => w.Title);

        builder.HasMany(w => w.Registrations)
            .WithOne(r => r.Workshop)
            .HasForeignKey(r => r.WorkshopId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
