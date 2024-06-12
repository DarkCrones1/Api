using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Api.Domain.Entities;

namespace Api.Infrastructure.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .HasDefaultValueSql("(N'Admin')");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.Description).HasMaxLength(10000);
        builder.Property(e => e.LastModifiedBy).HasMaxLength(50);
        builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.PublicationDate)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime");
        builder.Property(e => e.ImagePostUrl).HasMaxLength(250);

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Post)
            .HasForeignKey(d => d.UserAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Post_UserAccount");
    }
}