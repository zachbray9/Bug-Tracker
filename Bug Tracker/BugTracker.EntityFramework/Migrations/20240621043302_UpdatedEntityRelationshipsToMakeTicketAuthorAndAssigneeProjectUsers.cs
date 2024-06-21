using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntityRelationshipsToMakeTicketAuthorAndAssigneeProjectUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AssigneeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_AuthorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssigneeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AuthorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeProjectId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssigneeId_AssigneeProjectId",
                table: "Tickets",
                columns: new[] { "AssigneeId", "AssigneeProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AuthorId_ProjectId",
                table: "Tickets",
                columns: new[] { "AuthorId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId_ProjectId",
                table: "Comments",
                columns: new[] { "AuthorId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ProjectUsers_AuthorId_ProjectId",
                table: "Comments",
                columns: new[] { "AuthorId", "ProjectId" },
                principalTable: "ProjectUsers",
                principalColumns: new[] { "UserId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ProjectUsers_AssigneeId_AssigneeProjectId",
                table: "Tickets",
                columns: new[] { "AssigneeId", "AssigneeProjectId" },
                principalTable: "ProjectUsers",
                principalColumns: new[] { "UserId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ProjectUsers_AuthorId_ProjectId",
                table: "Tickets",
                columns: new[] { "AuthorId", "ProjectId" },
                principalTable: "ProjectUsers",
                principalColumns: new[] { "UserId", "ProjectId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ProjectUsers_AuthorId_ProjectId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ProjectUsers_AssigneeId_AssigneeProjectId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ProjectUsers_AuthorId_ProjectId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AssigneeId_AssigneeProjectId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_AuthorId_ProjectId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AuthorId_ProjectId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AssigneeProjectId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssigneeId",
                table: "Tickets",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AuthorId",
                table: "Tickets",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AssigneeId",
                table: "Tickets",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_AuthorId",
                table: "Tickets",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
