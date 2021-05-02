using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imagegram.Domain.CommentsEntity
{
    public class CommentsEntityBuilder
    {
        public CommentsEntityBuilder(EntityTypeBuilder<Comments> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Content)
                .IsRequired();

            entityBuilder.HasOne(e => e.Creator)
                .WithMany(e => e.Comments)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();

            entityBuilder.HasOne(e => e.Post)
                .WithMany(e => e.Comments)
                .HasForeignKey(k => k.PostId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.ClientCascade)
                .IsRequired();
        }
    }
}
