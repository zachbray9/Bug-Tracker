using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class fixed_casing_on_profilePictureUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "profilePictureUrl",
                table: "AspNetUsers",
                newName: "ProfilePictureUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                newName: "profilePictureUrl");
        }
    }
}
