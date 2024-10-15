using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations;

public class ClientConfiguration: IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Clients");
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
        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}