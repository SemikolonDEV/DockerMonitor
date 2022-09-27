﻿// <auto-generated />
using System;
using DockerMonitor.Infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DockerMonitor.Infastructure.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220927120319_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DockerMonitor.Domain.Entities.ContainerStat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CPUUsage")
                        .HasColumnType("decimal(20,0)");

                    b.Property<long>("Cores")
                        .HasColumnType("bigint");

                    b.Property<string>("DBContainerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("MemoryMax")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("MemoryUsage")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("ReadSize")
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("WriteSize")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("DBContainerId");

                    b.ToTable("ContainersStats");
                });

            modelBuilder.Entity("DockerMonitor.Domain.Entities.DBContainer", b =>
                {
                    b.Property<string>("DBContainerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DBContainerId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("DockerMonitor.Domain.Entities.ContainerStat", b =>
                {
                    b.HasOne("DockerMonitor.Domain.Entities.DBContainer", "DBContainer")
                        .WithMany("Stats")
                        .HasForeignKey("DBContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DBContainer");
                });

            modelBuilder.Entity("DockerMonitor.Domain.Entities.DBContainer", b =>
                {
                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}
