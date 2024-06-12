using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Api.Domain.Entities;

namespace Api.Infrastructure.Data.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Password).HasMaxLength(100);
        builder.Property(e => e.UserName).HasMaxLength(150);
        builder.Property(e => e.Email)
            .HasMaxLength(150)
            .IsUnicode(false);

        builder.HasMany(d => d.UserInfo).WithMany(p => p.UserAccount)
            .UsingEntity<Dictionary<string, object>>(
                "UserAccountUserInfo",
                r => r.HasOne<UserInfo>().WithMany()
                    .HasForeignKey("UserInfoId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountUserInfo_UserInfo"),
                l => l.HasOne<UserAccount>().WithMany()
                    .HasForeignKey("UserAccountId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAccountUserInfo_UserAccount"),
                j =>
                {
                    j.HasKey("UserAccountId", "UserInfoId");
                });
    }
}