using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuggyAspneture.DataAccess.PostgreSQL.Entities
{
    public sealed class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public IList<OpenLoopEntity> OpenLoops { get; set; }
    }

    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login)
                .HasMaxLength(50);
            builder.HasMany(u => u.OpenLoops)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}