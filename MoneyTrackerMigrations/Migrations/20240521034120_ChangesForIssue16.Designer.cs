﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240521034120_ChangesForIssue16")]
    partial class ChangesForIssue16
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("BucketModelUserModel", b =>
                {
                    b.Property<int>("BucketsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BucketsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserBuckets", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AccountModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RoutingNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FinanceAccounts", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AutoPayModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoPayActive")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AutoPayAmount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AutoPayDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("AutoPayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LoanId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LoanId");

                    b.HasIndex("UserId");

                    b.ToTable("AutoPays", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.BucketModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BucketAmount")
                        .HasColumnType("TEXT");

                    b.Property<string>("BucketName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SavingsBuckets", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.LoanModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("InterestRate")
                        .HasColumnType("REAL");

                    b.Property<double>("LoanAmount")
                        .HasColumnType("REAL");

                    b.Property<string>("LoanName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("MonthlyPayment")
                        .HasColumnType("REAL");

                    b.Property<bool>("PaidOff")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("RemainingBalance")
                        .HasColumnType("REAL");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.SettingsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBiometricEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDarkMode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocationEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNotificationsEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Settings", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.TransactionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int>("AutoPayId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BucketId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("LoanId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AutoPayId");

                    b.HasIndex("BucketId");

                    b.HasIndex("LoanId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("MFA")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Suffix")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BucketModelUserModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.BucketModel", null)
                        .WithMany()
                        .HasForeignKey("BucketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AccountModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AutoPayModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.AccountModel", "Accounts")
                        .WithMany("AutoPays")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.LoanModel", "Loan")
                        .WithMany("AutoPay")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("AutoPay")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accounts");

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.BucketModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.AccountModel", "Account")
                        .WithMany("Buckets")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.LoanModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.SettingsModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithOne("Settings")
                        .HasForeignKey("MoneyTrackerMigrations.Models.SettingsModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.TransactionModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.AccountModel", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.AutoPayModel", "AutoPay")
                        .WithMany("Transactions")
                        .HasForeignKey("AutoPayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.BucketModel", "Bucket")
                        .WithMany("Transactions")
                        .HasForeignKey("BucketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.LoanModel", "Loan")
                        .WithMany("Transactions")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AutoPay");

                    b.Navigation("Bucket");

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AccountModel", b =>
                {
                    b.Navigation("AutoPays");

                    b.Navigation("Buckets");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AutoPayModel", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.BucketModel", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.LoanModel", b =>
                {
                    b.Navigation("AutoPay");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.UserModel", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("AutoPay");

                    b.Navigation("Loans");

                    b.Navigation("Settings");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
