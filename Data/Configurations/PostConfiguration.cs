using CompatriotsClub.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Albums).IsRequired().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }


    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.Post).WithMany(x => x.Images).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class ConmentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.DateMoodified).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.PostId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.Post).WithMany(x => x.Conments).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Conments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class FeelConfiguration : IEntityTypeConfiguration<Feel>
    {
        public void Configure(EntityTypeBuilder<Feel> builder)
        {
            builder.Property(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PostId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.Post).WithMany(x => x.Feel).HasForeignKey(x => x.PostId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Feel).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
