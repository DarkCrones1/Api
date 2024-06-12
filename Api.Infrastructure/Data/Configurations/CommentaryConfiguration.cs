using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Api.Domain.Entities;

namespace Api.Infrastructure.Data.Configurations;

public class CommentaryConfiguration : IEntityTypeConfiguration<Commentary>
{
    public void Configure(EntityTypeBuilder<Commentary> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Description).HasMaxLength(10000);

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Commentary)
            .HasForeignKey(d => d.UserAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Commentary_UserAccount");

        builder.HasOne(d => d.Post)
            .WithMany(p => p.Commentary)
            .HasForeignKey(d => d.PostId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Commentary_Post");
    }
}