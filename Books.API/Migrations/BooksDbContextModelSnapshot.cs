﻿// <auto-generated />
using System;
using Books.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Books.API.Migrations
{
    [DbContext(typeof(BooksDbContext))]
    partial class BooksDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Books.API.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("WritingAwards")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateOfBirth = new DateOnly(1903, 6, 25),
                            FirstName = "George",
                            LastName = "Orwell",
                            MiddleName = "",
                            WritingAwards = new[] { "Prometheus Hall of Fame Award" }
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateOfBirth = new DateOnly(1915, 3, 18),
                            FirstName = "Richard",
                            LastName = "Condon",
                            MiddleName = "",
                            WritingAwards = new string[0]
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateOfBirth = new DateOnly(1821, 11, 11),
                            FirstName = "Fyodor",
                            LastName = "Dostoevsky",
                            MiddleName = "",
                            WritingAwards = new string[0]
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9910),
                            DateOfBirth = new DateOnly(1907, 7, 7),
                            FirstName = "Robert",
                            LastName = "Heinlein",
                            MiddleName = "A.",
                            WritingAwards = new string[0]
                        });
                });

            modelBuilder.Entity("Books.API.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DatePublished")
                        .HasColumnType("date");

                    b.Property<int>("Genre")
                        .HasColumnType("integer");

                    b.Property<int>("PageLength")
                        .HasColumnType("integer");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1949, 6, 8),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 1,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1945, 8, 17),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 1,
                            Title = "Animal Farm"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 2,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1959, 4, 27),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 2,
                            Title = "The Manchurian Candidate"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 3,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1866, 1, 1),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 3,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 3,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1871, 1, 1),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 3,
                            Title = "Demons"
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 4,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9980),
                            DatePublished = new DateOnly(1961, 6, 1),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 4,
                            Title = "Stranger in a Strange Land"
                        },
                        new
                        {
                            Id = 7,
                            AuthorId = 4,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990),
                            DatePublished = new DateOnly(1959, 11, 5),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 4,
                            Title = "Starship Troopers"
                        },
                        new
                        {
                            Id = 8,
                            AuthorId = 4,
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9990),
                            DatePublished = new DateOnly(1966, 6, 2),
                            Genre = 0,
                            PageLength = 0,
                            PublisherId = 4,
                            Title = "The Moon is a Harsh Mistress"
                        });
                });

            modelBuilder.Entity("Books.API.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateFounded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Publishers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address1 = "",
                            Address2 = "",
                            City = "",
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            DateFounded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            Name = "Penguin Books",
                            State = 0,
                            ZipCode = ""
                        },
                        new
                        {
                            Id = 2,
                            Address1 = "",
                            Address2 = "",
                            City = "",
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            DateFounded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            Name = "McGraw-Hill",
                            State = 0,
                            ZipCode = ""
                        },
                        new
                        {
                            Id = 3,
                            Address1 = "",
                            Address2 = "",
                            City = "",
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            DateFounded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9960),
                            Name = "The Russian Messenger",
                            State = 0,
                            ZipCode = ""
                        },
                        new
                        {
                            Id = 4,
                            Address1 = "",
                            Address2 = "",
                            City = "",
                            DateCreated = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970),
                            DateFounded = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(2024, 11, 14, 18, 26, 12, 546, DateTimeKind.Utc).AddTicks(9970),
                            Name = "G. P. Puntnam's Sons",
                            State = 0,
                            ZipCode = ""
                        });
                });

            modelBuilder.Entity("Books.API.Book", b =>
                {
                    b.HasOne("Books.API.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Books.API.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Publisher");
                });
#pragma warning restore 612, 618
        }
    }
}
