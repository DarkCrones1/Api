using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Api.Domain.Entities;

namespace Api.Infrastructure.Data.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        // Don't Delete, Manual configuration
        builder.Ignore(x => x.FullName);

        // Automatic Scaffold Configuration
        builder.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC076D7D9088");

        builder.Property(e => e.CellPhone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.FirstName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.LastName)
            .HasMaxLength(200)
            .IsUnicode(false);
        builder.Property(e => e.MiddleName)
            .HasMaxLength(150)
            .IsUnicode(false);
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .IsUnicode(false);
        builder.Property(e => e.ProfilePictureUrl).HasMaxLength(250);
    }
}