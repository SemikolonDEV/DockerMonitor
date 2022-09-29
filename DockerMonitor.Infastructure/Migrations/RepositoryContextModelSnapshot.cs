﻿// <auto-generated />
using System;
using DockerMonitor.Infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DockerMonitor.Infastructure.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DockerMonitor.Domain.Entities.ContainerStat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ulong>("CPUUsage")
                        .HasColumnType("bigint unsigned");

                    b.Property<uint>("Cores")
                        .HasColumnType("int unsigned");

                    b.Property<string>("DBContainerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<ulong>("MemoryMax")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("MemoryUsage")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ReadSize")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("WriteSize")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("DBContainerId");

                    b.ToTable("ContainersStats");
                });

            modelBuilder.Entity("DockerMonitor.Domain.Entities.DBContainer", b =>
                {
                    b.Property<string>("DBContainerId")
                        .HasColumnType("varchar(255)");

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
