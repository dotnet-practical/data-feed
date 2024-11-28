using DNQ.DataFeed.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DNQ.DataFeed.Persistence.Configs;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.PlatformId).IsRequired();
        builder.Property(s => s.InternalId).IsRequired();
        builder.Property(s => s.ReferenceValue).IsRequired();
        builder.Property(s => s.SiteId).IsRequired();
        builder.Property(s => s.StartDate).IsRequired();
        builder.Property(s => s.EndDate).IsRequired(false);
        builder.Property(s => s.FinYear).IsRequired();
    }
}
