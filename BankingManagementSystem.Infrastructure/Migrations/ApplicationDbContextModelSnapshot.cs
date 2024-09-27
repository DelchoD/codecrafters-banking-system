﻿// <auto-generated />
using System;
using BankingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.CreditScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<short>("Score")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("CreditScores");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PersonalIdNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.LoanApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("AmountRequested")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateApproved")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<byte>("RepaymentPeriod")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("LoanApplications");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.RiskAssessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Details")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("LoanApplicationId")
                        .HasColumnType("int");

                    b.Property<short>("RiskScore")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("LoanApplicationId")
                        .IsUnique();

                    b.ToTable("RiskAssessments");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("AccountId")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("AccountId1")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IbanFrom")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("IbanTo")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountId1");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Account", b =>
                {
                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.CreditScore", b =>
                {
                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Customer", "Customer")
                        .WithOne("CreditScore")
                        .HasForeignKey("BankingManagementSystem.Infrastructure.Data.Models.CreditScore", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.LoanApplication", b =>
                {
                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Customer", "Customer")
                        .WithMany("LoanApplications")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.RiskAssessment", b =>
                {
                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.LoanApplication", "LoanApplication")
                        .WithOne("RiskAssessment")
                        .HasForeignKey("BankingManagementSystem.Infrastructure.Data.Models.RiskAssessment", "LoanApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoanApplication");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Transaction", b =>
                {
                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Account", null)
                        .WithMany("TransactionsFrom")
                        .HasForeignKey("AccountId");

                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Account", null)
                        .WithMany("TransactionsTo")
                        .HasForeignKey("AccountId1");

                    b.HasOne("BankingManagementSystem.Infrastructure.Data.Models.Customer", null)
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Account", b =>
                {
                    b.Navigation("TransactionsFrom");

                    b.Navigation("TransactionsTo");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.Customer", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("CreditScore");

                    b.Navigation("LoanApplications");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BankingManagementSystem.Infrastructure.Data.Models.LoanApplication", b =>
                {
                    b.Navigation("RiskAssessment");
                });
#pragma warning restore 612, 618
        }
    }
}
