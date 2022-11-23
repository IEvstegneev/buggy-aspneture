using BuggyAspneture.DataAccess.PostgreSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuggyAspneture.DataAccess.PostgreSQL.Entities
{
    public sealed class OpenLoopEntity
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}

public sealed class OpenLoopEntityConfiguration : IEntityTypeConfiguration<OpenLoopEntity>
{
    public void Configure(EntityTypeBuilder<OpenLoopEntity> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Note)
            .HasMaxLength(500);
    }
}
