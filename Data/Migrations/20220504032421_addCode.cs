using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "PositionMember",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionMember_RoleId",
                table: "PositionMember",
                newName: "IX_PositionMember_PositionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(2912),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(2297));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Position",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Images",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(7430),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(6301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateMoodified",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8973),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(7662));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8712),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(7474));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "PositionMember",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PositionMember_PositionId",
                table: "PositionMember",
                newName: "IX_PositionMember_RoleId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(2297),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(2912));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Images",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(6301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(7430));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateMoodified",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(7662),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8973));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 3, 22, 28, 21, 329, DateTimeKind.Local).AddTicks(7474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 10, 24, 21, 653, DateTimeKind.Local).AddTicks(8712));
        }
    }
}
