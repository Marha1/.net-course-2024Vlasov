using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations;

public class EmployConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Employee");
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.Surname)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.PassportDetails)
            .IsRequired()
            .HasMaxLength(25);
        builder.Property(c => c.BirthDate)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(),          
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)  
            );
    }
}