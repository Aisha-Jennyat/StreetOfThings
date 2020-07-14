﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transaction.DataAccessLayer;

namespace Transaction.DataAccessLayer.Migrations
{
    [DbContext(typeof(TransactionContext))]
    partial class TransactionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Transaction.Models.Login", b =>
                {
                    b.Property<Guid>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TokenId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginId");

                    b.HasIndex("UserId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Transaction.Models.ObjectReceiving", b =>
                {
                    b.Property<Guid>("ObjectReceivingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GiverLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("HourlyCharge")
                        .HasColumnType("real");

                    b.Property<Guid>("ObjectRegistrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ObjectReturningId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReceivedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RecipientLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ObjectReceivingId");

                    b.HasIndex("GiverLoginId");

                    b.HasIndex("ObjectRegistrationId")
                        .IsUnique();

                    b.HasIndex("RecipientLoginId");

                    b.ToTable("ObjectReceivings");
                });

            modelBuilder.Entity("Transaction.Models.ObjectRegistration", b =>
                {
                    b.Property<Guid>("ObjectRegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpiresAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<Guid?>("ObjectReceivingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipientLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegisteredAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan?>("ShouldReturnItAfter")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ObjectRegistrationId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("RecipientLoginId");

                    b.ToTable("ObjectRegistrations");
                });

            modelBuilder.Entity("Transaction.Models.ObjectReturning", b =>
                {
                    b.Property<Guid>("ObjectReturningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LoaneeLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LoanerLoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ObjectReceivingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReturnedAtUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("ObjectReturningId");

                    b.HasIndex("LoaneeLoginId");

                    b.HasIndex("LoanerLoginId");

                    b.HasIndex("ObjectReceivingId")
                        .IsUnique();

                    b.ToTable("ObjectReturnings");
                });

            modelBuilder.Entity("Transaction.Models.OfferedObject", b =>
                {
                    b.Property<int>("OfferedObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("HourlyCharge")
                        .HasColumnType("real");

                    b.Property<int>("OriginalObjectId")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ShouldReturn")
                        .HasColumnType("bit");

                    b.HasKey("OfferedObjectId");

                    b.HasIndex("OwnerUserId");

                    b.ToTable("OfferedObject");
                });

            modelBuilder.Entity("Transaction.Models.TransactionToken", b =>
                {
                    b.Property<Guid>("TransactionTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("IssuedAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReceivingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RegistrationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UseAfterUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UseBeforeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionTokenId");

                    b.HasIndex("ReceivingId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("TransactionToken");
                });

            modelBuilder.Entity("Transaction.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OriginalUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Transaction.Models.Login", b =>
                {
                    b.HasOne("Transaction.Models.User", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Transaction.Models.ObjectReceiving", b =>
                {
                    b.HasOne("Transaction.Models.Login", "GiverLogin")
                        .WithMany("ObjectReceivingGivers")
                        .HasForeignKey("GiverLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Transaction.Models.ObjectRegistration", "ObjectRegistration")
                        .WithOne("ObjectReceiving")
                        .HasForeignKey("Transaction.Models.ObjectReceiving", "ObjectRegistrationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transaction.Models.Login", "RecipientLogin")
                        .WithMany("ObjectReceivingRecepiants")
                        .HasForeignKey("RecipientLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Transaction.Models.ObjectRegistration", b =>
                {
                    b.HasOne("Transaction.Models.OfferedObject", "Object")
                        .WithMany()
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Transaction.Models.Login", "RecipientLogin")
                        .WithMany("RegistrationRecepiants")
                        .HasForeignKey("RecipientLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Transaction.Models.ObjectReturning", b =>
                {
                    b.HasOne("Transaction.Models.Login", "LoaneeLogin")
                        .WithMany("ObjectReturningLoanees")
                        .HasForeignKey("LoaneeLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Transaction.Models.Login", "LoanerLogin")
                        .WithMany("ObjectReturningLoaners")
                        .HasForeignKey("LoanerLoginId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Transaction.Models.ObjectReceiving", "ObjectReceiving")
                        .WithOne("ObjectReturning")
                        .HasForeignKey("Transaction.Models.ObjectReturning", "ObjectReceivingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Transaction.Models.OfferedObject", b =>
                {
                    b.HasOne("Transaction.Models.User", "OwnerUser")
                        .WithMany()
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Transaction.Models.TransactionToken", b =>
                {
                    b.HasOne("Transaction.Models.ObjectReceiving", "Receiving")
                        .WithMany("Tokens")
                        .HasForeignKey("ReceivingId");

                    b.HasOne("Transaction.Models.ObjectRegistration", "Registration")
                        .WithMany("Tokens")
                        .HasForeignKey("RegistrationId");
                });
#pragma warning restore 612, 618
        }
    }
}
