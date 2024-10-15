using BankSystemDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityConfigurations;

public class AccountConfiguration: IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.ToTable("Accounts");
        builder.Property(a => a.Amount)
            .IsRequired();
        builder.HasOne(a => a.Client)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.ClientId);
        builder.HasOne(a => a.Currency)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.CurrencyId);
    }
}