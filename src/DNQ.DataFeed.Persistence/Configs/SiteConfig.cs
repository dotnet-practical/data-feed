using DNQ.DataFeed.Domain.Sites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DNQ.DataFeed.Persistence.Configs;

public class SiteConfig : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable("sites");

        builder.HasKey(s => s.Id);

        builder.HasIndex(i => i.Id).IsUnique();
        builder.HasIndex(i => i.Code).IsUnique();

        builder.Property(s => s.Code)
           .IsRequired() 
           .HasMaxLength(50); 

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Deleted)
           .IsRequired(); 
    }
}
