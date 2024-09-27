using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingManagementSystem.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                 CREATE TABLE IF NOT EXISTS Customers (
            Id VARCHAR(255) NOT NULL PRIMARY KEY,
            FirstName TEXT NOT NULL,
            MiddleName TEXT NOT NULL,
            LastName TEXT NOT NULL,
            PersonalIDNumber TEXT NOT NULL,
            DateOfBirth DATE NOT NULL,
            Address TEXT NOT NULL,
            UserName TEXT,
            NormalizedUserName TEXT,
            Email TEXT,
            NormalizedEmail TEXT,
            EmailConfirmed INTEGER NOT NULL,
            Password TEXT,
            PasswordHash TEXT,
            SecurityStamp TEXT,
            ConcurrencyStamp TEXT,
            PhoneNumber TEXT,
            PhoneNumberConfirmed INT NOT NULL,
            TwoFactorEnabled INT NOT NULL,
            LockoutEnd TEXT,
            LockoutEnabled INT NOT NULL,
            AccessFailedCount INT NOT NULL
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Accounts (
            IBAN VARCHAR(255) NOT NULL PRIMARY KEY,
            Name TEXT NOT NULL,
            Balance DECIMAL(18, 2) NOT NULL,
            CustomerId VARCHAR(255) NOT NULL,
            FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Transactions (
            Id VARCHAR(255) NOT NULL PRIMARY KEY,
            Date DATE NOT NULL,
            TotalAmount DECIMAL(18, 2) NOT NULL,
            Reason TEXT NOT NULL,
            IBANFromId VARCHAR(255) NOT NULL,
            IBANToId VARCHAR(255) NOT NULL,
            CustomerId VARCHAR(255),
            FOREIGN KEY (IBANFromId) REFERENCES Accounts(IBAN) ON DELETE CASCADE,
            FOREIGN KEY (IBANToId) REFERENCES Accounts(IBAN) ON DELETE CASCADE,
            FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                );
            ");


            migrationBuilder.Sql(@"
        CREATE INDEX IX_Accounts_CustomerId ON Accounts (CustomerId);
    ");

            migrationBuilder.Sql(@"
        CREATE INDEX IX_Transactions_CustomerId ON Transactions (CustomerId);
    ");

            migrationBuilder.Sql(@"
        CREATE INDEX IX_Transactions_IBANFromId ON Transactions (IBANFromId);
    ");

            migrationBuilder.Sql(@"
        CREATE INDEX IX_Transactions_IBANToId ON Transactions (IBANToId);
    ");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
