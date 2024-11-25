﻿// <auto-generated />
using System;
using DNQ.DataFeed.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DNQ.DataFeed.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DNQ.DataFeed.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<uint>("FinYear")
                        .HasColumnType("int unsigned");

                    b.Property<Guid>("InternalId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ReferenceValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SiteId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("accounts", (string)null);
                });

            modelBuilder.Entity("DNQ.DataFeed.Domain.Sites.Site", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("sites", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
