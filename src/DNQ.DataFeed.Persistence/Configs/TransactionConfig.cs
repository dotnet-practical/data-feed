using DNQ.DataFeed.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNQ.DataFeed.Persistence.Configs;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        builder.HasKey(s => s.Id);

        builder.Property(t => t.PlatformId)
          .IsRequired();

        builder.Property(t => t.FileID)
            .IsRequired();

        builder.Property(t => t.SiteID)
            .IsRequired();

        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(t => t.ReferenceValue)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(t => t.EffectiveDate)
            .IsRequired();

        builder.Property(t => t.ProcessingStatus)
            .IsRequired()
            .HasMaxLength(50); 

        builder.Property(t => t.ProcessingReason)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(t => t.TransactionDate)
            .IsRequired();

        builder.Property(t => t.TransactionAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)"); 

        builder.Property(t => t.NarrationText)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(t => t.InstitutionID)
            .IsRequired();

        builder.Property(t => t.TransactionReference)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.ReversedTransactionReference)
            .HasMaxLength(100);

        builder.Property(t => t.TransactionCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.ReferenceCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.RecordOrder)
            .IsRequired();

        builder.Property(t => t.LoadedDate)
            .IsRequired();

        builder.Property(t => t.TransactionHashValue)
            .IsRequired()
            .HasMaxLength(256); 

        builder.Property(t => t.Currency)
            .IsRequired()
            .HasMaxLength(10); // USD, EUR,...

        builder.Property(t => t.ExchangeRate)
            .IsRequired()
            .HasColumnType("decimal(18,4)"); 

        builder.Property(t => t.ExchangeRateSource)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.NativeCurrencyAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.SuppliedSecurityCode)
            .IsRequired()
            .HasMaxLength(50);

        // Index
        builder.HasIndex(t => new { t.PlatformId, t.TransactionReference })
          .HasDatabaseName("ix_01")
          .IsUnique(false);
    }
}
