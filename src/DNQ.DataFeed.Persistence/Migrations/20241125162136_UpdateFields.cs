using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNQ.DataFeed.Persistence.Migrations
{
    public partial class UpdateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "accounts",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "site_id",
                table: "accounts",
                newName: "SiteId");

            migrationBuilder.RenameColumn(
                name: "reference_value",
                table: "accounts",
                newName: "ReferenceValue");

            migrationBuilder.RenameColumn(
                name: "platform_id",
                table: "accounts",
                newName: "PlatformId");

            migrationBuilder.RenameColumn(
                name: "internal_id",
                table: "accounts",
                newName: "InternalId");

            migrationBuilder.RenameColumn(
                name: "fin_year",
                table: "accounts",
                newName: "FinYear");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "accounts",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "accounts",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "accounts",
                newName: "site_id");

            migrationBuilder.RenameColumn(
                name: "ReferenceValue",
                table: "accounts",
                newName: "reference_value");

            migrationBuilder.RenameColumn(
                name: "PlatformId",
                table: "accounts",
                newName: "platform_id");

            migrationBuilder.RenameColumn(
                name: "InternalId",
                table: "accounts",
                newName: "internal_id");

            migrationBuilder.RenameColumn(
                name: "FinYear",
                table: "accounts",
                newName: "fin_year");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "accounts",
                newName: "end_date");
        }
    }
}
