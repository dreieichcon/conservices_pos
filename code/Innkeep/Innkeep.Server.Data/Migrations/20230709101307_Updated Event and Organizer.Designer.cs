﻿// <auto-generated />
using System;
using Innkeep.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Innkeep.Server.Data.Migrations
{
    [DbContext(typeof(InnkeepServerContext))]
    [Migration("20230709101307_Updated Event and Organizer")]
    partial class UpdatedEventandOrganizer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("Innkeep.Server.Data.Models.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SelectedEventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SelectedOrganizerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SelectedEventId");

                    b.HasIndex("SelectedOrganizerId");

                    b.ToTable("ApplicationSettings");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Authentication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authentications");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Organizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Organizer");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Register");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PretixEventId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PretixOrderNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TseToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("PretixEventId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.ApplicationSetting", b =>
                {
                    b.HasOne("Innkeep.Server.Data.Models.Event", "SelectedEvent")
                        .WithMany()
                        .HasForeignKey("SelectedEventId");

                    b.HasOne("Innkeep.Server.Data.Models.Organizer", "SelectedOrganizer")
                        .WithMany()
                        .HasForeignKey("SelectedOrganizerId");

                    b.Navigation("SelectedEvent");

                    b.Navigation("SelectedOrganizer");
                });

            modelBuilder.Entity("Innkeep.Server.Data.Models.Transaction", b =>
                {
                    b.HasOne("Innkeep.Server.Data.Models.Register", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Innkeep.Server.Data.Models.Event", "PretixEvent")
                        .WithMany()
                        .HasForeignKey("PretixEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("PretixEvent");
                });
#pragma warning restore 612, 618
        }
    }
}