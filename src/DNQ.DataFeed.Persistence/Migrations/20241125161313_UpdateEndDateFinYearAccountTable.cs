using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNQ.DataFeed.Persistence.Migrations
{
    public partial class UpdateEndDateFinYearAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "finyear",
                table: "accounts",
                newName: "fin_year");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "accounts",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date",
                table: "accounts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "start_date",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "fin_year",
                table: "accounts",
                newName: "finyear");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "accounts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "accounts",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
