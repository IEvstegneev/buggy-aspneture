using BuggyAspneture.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuggyAspneture.DataAccess.PostgreSQL
{
    public class BuggyAspnetureDbContext : DbContext
    {
        public DbSet<OpenLoopEntity> OpenLoops { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public BuggyAspnetureDbContext(DbContextOptions<BuggyAspnetureDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuggyAspnetureDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}