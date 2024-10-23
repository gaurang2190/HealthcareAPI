using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class seeddataprofessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "HealthcareProfessionals",
            columns: new[] { "Id", "Name", "Specialty" },
            values: new object[,]
            {
                { 1, "Dr. Alice Brown", "Cardiologist" },
                { 2, "Dr. Bob White", "Dermatologist" }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
          table: "HealthcareProfessionals",
          keyColumn: "Id",
          keyValues: new object[] { 1, 2 });
        }
    }
}
