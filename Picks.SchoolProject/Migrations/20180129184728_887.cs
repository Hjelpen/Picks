using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Picks.SchoolProject.Migrations
{
    public partial class _887 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Images_ImageIdId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImageIdId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageIdId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageIdId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImageIdId",
                table: "Images",
                column: "ImageIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Images_ImageIdId",
                table: "Images",
                column: "ImageIdId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
