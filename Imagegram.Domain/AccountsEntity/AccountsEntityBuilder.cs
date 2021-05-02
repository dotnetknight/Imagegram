using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imagegram.Domain.AccountsEntity
{
    public class AccountsEntityBuilder
    {
        public AccountsEntityBuilder(EntityTypeBuilder<Accounts> entityBuilder)
        {
            entityBuilder.HasKey(e => e.Id);

            entityBuilder.Property(e => e.Name)
                .HasMaxLength(120)
                .IsRequired();
        }
    }
}
