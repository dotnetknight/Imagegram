using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imagegram.Domain.PostsEntity
{
    public class PostsEntityBuilder
    {
        public PostsEntityBuilder(EntityTypeBuilder<Posts> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.ImageUrl);

            entityBuilder.HasOne(e => e.Creator)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();

            entityBuilder.HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .HasForeignKey(k => k.PostId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.ClientCascade)
                .IsRequired();
        }
    }
}
