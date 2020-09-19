﻿// <auto-generated />
using System;
using Catalog.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace Catalog.DataAccessLayer.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20200919215705_AddTagStatus")]
    partial class AddTagStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Catalog.Models.Login", b =>
                {
                    b.Property<Guid>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("datetime2");

                    b.Property<Point>("LoginLocation")
                        .HasColumnType("geography");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginId");

                    b.HasIndex("UserId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Catalog.Models.ObjectComment", b =>
                {
                    b.Property<Guid>("ObjectCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectCommentId");

                    b.HasIndex("LoginId");

                    b.HasIndex("ObjectId");

                    b.ToTable("ObjectComment");
                });

            modelBuilder.Entity("Catalog.Models.ObjectImpression", b =>
                {
                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ViewedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectId", "LoginId", "ViewedAtUtc");

                    b.HasIndex("LoginId");

                    b.ToTable("ObjectImpressions");
                });

            modelBuilder.Entity("Catalog.Models.ObjectLike", b =>
                {
                    b.Property<Guid>("ObjectLikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LikedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.HasKey("ObjectLikeId");

                    b.HasIndex("LoginId");

                    b.HasIndex("ObjectId");

                    b.ToTable("ObjectLike");
                });

            modelBuilder.Entity("Catalog.Models.ObjectPhoto", b =>
                {
                    b.Property<int>("ObjectPhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.HasKey("ObjectPhotoId");

                    b.HasIndex("ObjectId");

                    b.ToTable("ObjectPhoto");
                });

            modelBuilder.Entity("Catalog.Models.ObjectTag", b =>
                {
                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ObjectId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ObjectTags");
                });

            modelBuilder.Entity("Catalog.Models.ObjectView", b =>
                {
                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ViewedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectId", "LoginId", "ViewedAtUtc");

                    b.HasIndex("LoginId");

                    b.ToTable("ObjectView");
                });

            modelBuilder.Entity("Catalog.Models.OfferedObject", b =>
                {
                    b.Property<int>("OfferedObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentTransactionType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndsAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PublishedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OfferedObjectId");

                    b.HasIndex("OwnerLoginId");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("Catalog.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TagStatus")
                        .HasColumnType("int");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Catalog.Models.TagPhoto", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("TagPhoto");
                });

            modelBuilder.Entity("Catalog.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<float?>("Rating")
                        .HasColumnType("real");

                    b.Property<Guid>("ReceipientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReceivedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReceivingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegisteredAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReturnId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReturnedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("ReceipientId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Catalog.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Catalog.Models.Login", b =>
                {
                    b.HasOne("Catalog.Models.User", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectComment", b =>
                {
                    b.HasOne("Catalog.Models.Login", "Login")
                        .WithMany("Comments")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Comments")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectImpression", b =>
                {
                    b.HasOne("Catalog.Models.Login", "Login")
                        .WithMany("Impressions")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Impressions")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectLike", b =>
                {
                    b.HasOne("Catalog.Models.Login", "Login")
                        .WithMany("Likes")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Likes")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectPhoto", b =>
                {
                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Photos")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectTag", b =>
                {
                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Tags")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.Tag", "Tag")
                        .WithMany("Objects")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.ObjectView", b =>
                {
                    b.HasOne("Catalog.Models.Login", "Login")
                        .WithMany("Views")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Views")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.OfferedObject", b =>
                {
                    b.HasOne("Catalog.Models.Login", "OwnerLogin")
                        .WithMany("OwnedObjects")
                        .HasForeignKey("OwnerLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.TagPhoto", b =>
                {
                    b.HasOne("Catalog.Models.Tag", "Tag")
                        .WithOne("Photo")
                        .HasForeignKey("Catalog.Models.TagPhoto", "TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Catalog.Models.Transaction", b =>
                {
                    b.HasOne("Catalog.Models.OfferedObject", "Object")
                        .WithMany("Transactions")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Models.User", "Receipient")
                        .WithMany("MeAsReceipient")
                        .HasForeignKey("ReceipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
