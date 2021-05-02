using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.CommentsEntity;
using Imagegram.Domain.PostsEntity;
using Microsoft.EntityFrameworkCore;

namespace Imagegram.Repository
{
    public class ImagegramDbContext : DbContext
    {
        public ImagegramDbContext(DbContextOptions<ImagegramDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new AccountsEntityBuilder(modelBuilder.Entity<Accounts>());
            new CommentsEntityBuilder(modelBuilder.Entity<Comments>());
            new PostsEntityBuilder(modelBuilder.Entity<Posts>());
        }
    }
}
