using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Api.Domain.Entities;

namespace Api.Infrastructure.Data.Configurations;

public class ActiveUserAccountConfiguration : IEntityTypeConfiguration<ActiveUserAccount>
{
    public void Configure(EntityTypeBuilder<ActiveUserAccount> builder)
    {
        builder
                .HasNoKey()
                .ToView("VW_ActiveUserAccount");

        builder.Property(e => e.CellPhone).HasMaxLength(20);
        builder.Property(e => e.Email).HasMaxLength(150);
        builder.Property(e => e.FirstName).HasMaxLength(200);
        builder.Property(e => e.LastName).HasMaxLength(200);
        builder.Property(e => e.MiddleName).HasMaxLength(150);
        builder.Property(e => e.Phone).HasMaxLength(20);
        builder.Property(e => e.UserName).HasMaxLength(150);

        // Configure id don't delete
        builder.Property(e => e.Id).HasColumnName("UserAccountId");
    }
}