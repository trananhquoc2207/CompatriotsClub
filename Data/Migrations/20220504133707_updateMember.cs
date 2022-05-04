using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class updateMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 766, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 483, DateTimeKind.Local).AddTicks(6310));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Member",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Images",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(3198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(975));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateMoodified",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(4640),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(2504));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(4447),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(2261));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 483, DateTimeKind.Local).AddTicks(6310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 766, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Images",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(975),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateMoodified",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(2504),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(4640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 4, 12, 29, 18, 484, DateTimeKind.Local).AddTicks(2261),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 4, 20, 37, 7, 767, DateTimeKind.Local).AddTicks(4447));
        }
    }
}
