using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations;

public class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Currency");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Currency)
            .HasForeignKey(a => a.CurrencyId);
    }
}