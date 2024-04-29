using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace balancing.Migrations
{
    /// <inheritdoc />
    public partial class deleteresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultValue",
                table: "Flows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ResultValue",
                table: "Flows",
                type: "double precision",
                nullable: true);
        }
    }
}
