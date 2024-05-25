﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyTrackerMigrations;

#nullable disable

namespace MoneyTrackerMigrations.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240524193316_AddLocationToJob")]
    partial class AddLocationToJob
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

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LoanId");

                    b.HasIndex("TransactionTypeId");

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

            modelBuilder.Entity("MoneyTrackerMigrations.Models.JobModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DirectDeposit")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FirstPayDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("HourlyWage")
                        .HasColumnType("REAL");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSalary")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PayFrequencyInWeeks")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("YearlyWage")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Jobs", (string)null);
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

            modelBuilder.Entity("MoneyTrackerMigrations.Models.LocationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address2")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasMaxLength(2)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Zip")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Locations", (string)null);
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

                    b.Property<string>("OtherDescription")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AutoPayId");

                    b.HasIndex("BucketId");

                    b.HasIndex("LoanId");

                    b.HasIndex("TransactionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.TransactionTypeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Debit"
                        },
                        new
                        {
                            Id = 3,
                            Type = "Cash"
                        },
                        new
                        {
                            Id = 4,
                            Type = "Credit (Debit Card)"
                        },
                        new
                        {
                            Id = 5,
                            Type = "Credit (Credit Card)"
                        },
                        new
                        {
                            Id = 6,
                            Type = "ApplePay"
                        },
                        new
                        {
                            Id = 7,
                            Type = "Venmo"
                        },
                        new
                        {
                            Id = 8,
                            Type = "PayPal"
                        },
                        new
                        {
                            Id = 9,
                            Type = "CashApp"
                        },
                        new
                        {
                            Id = 10,
                            Type = "ACHRecurring"
                        },
                        new
                        {
                            Id = 11,
                            Type = "ACHOnce"
                        },
                        new
                        {
                            Id = 12,
                            Type = "Check"
                        },
                        new
                        {
                            Id = 13,
                            Type = "InternalTransfer"
                        },
                        new
                        {
                            Id = 14,
                            Type = "ExternalTransfer"
                        },
                        new
                        {
                            Id = 15,
                            Type = "ATM Withdrawal"
                        },
                        new
                        {
                            Id = 16,
                            Type = "ATM Deposit"
                        },
                        new
                        {
                            Id = 17,
                            Type = "Mobile Deposit"
                        },
                        new
                        {
                            Id = 18,
                            Type = "Deposit"
                        },
                        new
                        {
                            Id = 19,
                            Type = "Other"
                        });
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

                    b.HasOne("MoneyTrackerMigrations.Models.TransactionTypeModel", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("AutoPay")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accounts");

                    b.Navigation("Loan");

                    b.Navigation("TransactionType");

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

            modelBuilder.Entity("MoneyTrackerMigrations.Models.JobModel", b =>
                {
                    b.HasOne("MoneyTrackerMigrations.Models.AccountModel", "Account")
                        .WithMany("Jobs")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.LocationModel", "Location")
                        .WithOne("Job")
                        .HasForeignKey("MoneyTrackerMigrations.Models.JobModel", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyTrackerMigrations.Models.UserModel", "User")
                        .WithMany("Jobs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Location");

                    b.Navigation("User");
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

                    b.HasOne("MoneyTrackerMigrations.Models.TransactionTypeModel", "TransactionType")
                        .WithMany()
                        .HasForeignKey("TransactionTypeId")
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

                    b.Navigation("TransactionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.AccountModel", b =>
                {
                    b.Navigation("AutoPays");

                    b.Navigation("Buckets");

                    b.Navigation("Jobs");

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

            modelBuilder.Entity("MoneyTrackerMigrations.Models.LocationModel", b =>
                {
                    b.Navigation("Job");
                });

            modelBuilder.Entity("MoneyTrackerMigrations.Models.UserModel", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("AutoPay");

                    b.Navigation("Jobs");

                    b.Navigation("Loans");

                    b.Navigation("Settings");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}