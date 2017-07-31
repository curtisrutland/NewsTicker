using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NewsTicker.Entities;

namespace NewsTicker.Migrations
{
    [DbContext(typeof(NewsContext))]
    [Migration("20170731021935_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("NewsTicker.Entities.NewsEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("Group");

                    b.Property<string>("Message");

                    b.Property<int>("Severity");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });
        }
    }
}
