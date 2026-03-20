using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QCEServices.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarriageLicenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationFormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Groom_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Groom_CivilStatus = table.Column<int>(type: "int", nullable: false),
                    Groom_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_BirthPlace_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_BirthPlace_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_BirthPlace_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Father_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Father_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Father_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Father_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Father_Status = table.Column<int>(type: "int", nullable: false),
                    Groom_Parents_Father_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Father_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Father_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Father_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Father_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Mother_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Mother_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groom_Parents_Mother_Status = table.Column<int>(type: "int", nullable: false),
                    Groom_Parents_Mother_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groom_Parents_Mother_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Bride_CivilStatus = table.Column<int>(type: "int", nullable: false),
                    Bride_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_BirthPlace_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_BirthPlace_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_BirthPlace_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Father_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Father_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Father_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Father_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Father_Status = table.Column<int>(type: "int", nullable: false),
                    Bride_Parents_Father_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Father_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Father_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Father_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Father_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Mother_Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Mother_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bride_Parents_Mother_Status = table.Column<int>(type: "int", nullable: false),
                    Bride_Parents_Mother_Residence_Barangay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Residence_HouseNoOrStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Residence_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Residence_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bride_Parents_Mother_Residence_CityOrMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriageLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarriageLicenses_ApplicationForms_ApplicationFormId",
                        column: x => x.ApplicationFormId,
                        principalTable: "ApplicationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForms_Type_Status",
                table: "ApplicationForms",
                columns: new[] { "Type", "Status" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarriageLicenses_ApplicationFormId",
                table: "MarriageLicenses",
                column: "ApplicationFormId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarriageLicenses");

            migrationBuilder.DropTable(
                name: "ApplicationForms");
        }
    }
}
