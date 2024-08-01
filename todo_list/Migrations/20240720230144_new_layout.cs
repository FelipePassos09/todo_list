using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list.Migrations
{
    /// <inheritdoc />
    public partial class new_layout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "notes",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "notes",
                newName: "UpdatedAt");
        }
    }
}
