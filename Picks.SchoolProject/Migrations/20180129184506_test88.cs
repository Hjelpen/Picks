using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Picks.SchoolProject.Migrations
{
    public partial class test88 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Categories_CategoryId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CategoryId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageIdId",
                table: "Images",
                type: "int",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CategoryId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_CategoryId",
                table: "Images",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Categories_CategoryId",
                table: "Images",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
