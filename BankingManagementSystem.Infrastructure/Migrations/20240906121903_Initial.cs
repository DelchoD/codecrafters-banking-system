﻿using System;
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
                    Id TEXT NOT NULL PRIMARY KEY,
                    FirstName TEXT NOT NULL,
                    MiddleName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    PersonalIDNumber TEXT NOT NULL,
                    DateOfBirth TEXT NOT NULL,
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
                    PhoneNumberConfirmed INTEGER NOT NULL,
                    TwoFactorEnabled INTEGER NOT NULL,
                    LockoutEnd TEXT,
                    LockoutEnabled INTEGER NOT NULL,
                    AccessFailedCount INTEGER NOT NULL
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Accounts (
                    IBAN TEXT NOT NULL PRIMARY KEY,
                    Name TEXT NOT NULL,
                    Balance TEXT NOT NULL,
                    CustomerId TEXT NOT NULL,
                    FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
                );
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Transactions (
                    Id TEXT NOT NULL PRIMARY KEY,
                    Date TEXT NOT NULL,
                    TotalAmount TEXT NOT NULL,
                    Reason TEXT NOT NULL,
                    IBANFromId TEXT NOT NULL,
                    IBANToId TEXT NOT NULL,
                    CustomerId TEXT,
                    FOREIGN KEY (IBANFromId) REFERENCES Accounts(IBAN) ON DELETE CASCADE,
                    FOREIGN KEY (IBANToId) REFERENCES Accounts(IBAN) ON DELETE CASCADE,
                    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
                );
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS IX_Accounts_CustomerId ON Accounts (CustomerId);
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS IX_Transactions_CustomerId ON Transactions (CustomerId);
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS IX_Transactions_IBANFromId ON Transactions (IBANFromId);
            ");

            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS IX_Transactions_IBANToId ON Transactions (IBANToId);
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
