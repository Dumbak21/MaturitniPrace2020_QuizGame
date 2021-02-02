﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("API.Models.QuestionAnswers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Answers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QA");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c82bc5e0-2aa5-4044-b501-f9187b141ea3"),
                            Answers = "1°2°3°4",
                            Area = 4,
                            Question = "How much is 2+2",
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("6c9b473e-e27a-4c47-8b6b-1378a476edeb"),
                            Answer = "1021",
                            Area = 4,
                            Question = "How much is 1000+21",
                            Type = 1
                        },
                        new
                        {
                            Id = new Guid("ead3d6f3-4634-4410-b7dd-9d69cd09093b"),
                            Answers = "c°d°3°z",
                            Area = 4,
                            Question = "Last letter in alphabet",
                            Type = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
