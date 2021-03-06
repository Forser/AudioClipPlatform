﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectUntitled.Migrations
{
    public partial class AddedDateTimeToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PostedAt",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostedAt",
                table: "Comments");
        }
    }
}
