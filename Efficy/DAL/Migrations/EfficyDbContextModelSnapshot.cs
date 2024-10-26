﻿// <auto-generated />
using System;
using Efficy.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Efficy.DAL.Migrations
{
    [DbContext(typeof(EfficyDbContext))]
    partial class EfficyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Efficy.DAL.Counter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Steps")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TeamId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamId1");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("Efficy.DAL.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Efficy.DAL.Counter", b =>
                {
                    b.HasOne("Efficy.DAL.Team", "Team")
                        .WithMany("Counters")
                        .HasForeignKey("TeamId");

                    b.HasOne("Efficy.DAL.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamId1");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Efficy.DAL.Team", b =>
                {
                    b.Navigation("Counters");
                });
#pragma warning restore 612, 618
        }
    }
}
