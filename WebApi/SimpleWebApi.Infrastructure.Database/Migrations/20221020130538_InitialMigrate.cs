using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApi.Infrastructure.Database.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    province_id = table.Column<Guid>(type: "uuid", nullable: false),
                    province_name = table.Column<string>(type: "text", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_provinces", x => x.province_id);
                    table.ForeignKey(
                        name: "fk_provinces_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    province_id = table.Column<Guid>(type: "uuid", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    password_salt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_users_provinces_province_id",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "province_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "country_id", "country_name" },
                values: new object[,]
                {
                    { new Guid("038d1341-2d32-455c-afd3-66e7f36fdf6f"), "Country 3" },
                    { new Guid("75b2129e-3b9a-4713-81f3-c7105e7939ae"), "Country 2" },
                    { new Guid("7f148b1e-f671-4017-904c-b24542922ae6"), "Country 1" }
                });

            migrationBuilder.InsertData(
                table: "provinces",
                columns: new[] { "province_id", "country_id", "province_name" },
                values: new object[,]
                {
                    { new Guid("171d3f32-307d-407d-babb-d40dcc373158"), new Guid("7f148b1e-f671-4017-904c-b24542922ae6"), "Country 1 Province 2" },
                    { new Guid("1de6b7be-ea8b-41af-b393-77e9b980ff97"), new Guid("75b2129e-3b9a-4713-81f3-c7105e7939ae"), "Country 2 Province 2" },
                    { new Guid("2254cb1d-3f92-429c-9fa4-a57e4c791a9d"), new Guid("75b2129e-3b9a-4713-81f3-c7105e7939ae"), "Country 2 Province 1" },
                    { new Guid("2b1334c5-46dd-4319-ab93-32b8f6d1cd7f"), new Guid("75b2129e-3b9a-4713-81f3-c7105e7939ae"), "Country 2 Province 3" },
                    { new Guid("3a09746a-9123-4f64-b0c0-92337aa65d21"), new Guid("7f148b1e-f671-4017-904c-b24542922ae6"), "Country 1 Province 1" },
                    { new Guid("47c7c268-8ef6-4141-afbf-b1fabd2c94b2"), new Guid("038d1341-2d32-455c-afd3-66e7f36fdf6f"), "Country 3 Province 3" },
                    { new Guid("60ecb98e-8890-4df0-8932-d3749b732ac3"), new Guid("7f148b1e-f671-4017-904c-b24542922ae6"), "Country 1 Province 3" },
                    { new Guid("c1361566-bc5f-499c-85be-2162ab6b5450"), new Guid("038d1341-2d32-455c-afd3-66e7f36fdf6f"), "Country 3 Province 1" },
                    { new Guid("c55f08d2-7b3d-44b4-8bb4-bc098d6445ea"), new Guid("038d1341-2d32-455c-afd3-66e7f36fdf6f"), "Country 3 Province 2" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_countries_country_name",
                table: "countries",
                column: "country_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_provinces_country_id_province_name",
                table: "provinces",
                columns: new[] { "country_id", "province_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_province_id",
                table: "users",
                column: "province_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
