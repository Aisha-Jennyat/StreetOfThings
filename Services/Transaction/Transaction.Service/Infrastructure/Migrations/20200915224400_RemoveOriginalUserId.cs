﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Transaction.Service.Infrastructure.Migrations
{
    public partial class RemoveOriginalUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalUserId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalUserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}