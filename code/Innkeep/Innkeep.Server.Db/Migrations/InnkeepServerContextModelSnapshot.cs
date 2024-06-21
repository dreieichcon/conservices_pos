﻿// <auto-generated />
using System;
using Innkeep.Server.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Innkeep.Server.Db.Migrations
{
    [DbContext(typeof(InnkeepServerContext))]
    partial class InnkeepServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Innkeep.Server.Db.Models.FiskalyConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<string>("ApiKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ApiSecret")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TokenValidUntil")
                        .HasColumnType("TEXT");

                    b.Property<string>("TseId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FiskalyConfigs");
                });

            modelBuilder.Entity("Innkeep.Server.Db.Models.FiskalyTseConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<string>("TseAdminPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("TseId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TsePuk")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TseConfigs");
                });

            modelBuilder.Entity("Innkeep.Server.Db.Models.PretixConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnOrder(0);

                    b.Property<string>("PretixAccessToken")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelectedEventName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelectedEventSlug")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelectedOrganizerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SelectedOrganizerSlug")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PretixConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}
