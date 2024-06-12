using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

// using Api.Domain.Entities;

namespace Api.Infrastructure.Data;

public partial class ApiDbContext : DbContext
{
    public ApiDbContext()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Commentary> Commentary { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<UserAccount> UserAccount { get; set; }

    public virtual DbSet<UserInfo> UserInfo { get; set; }

    public virtual DbSet<ActiveUserAccount> ActiveUserAccount{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            option => option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).MigrationsAssembly("Api.Api")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
    }
}