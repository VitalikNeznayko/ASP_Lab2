using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicBooking.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAppoitments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DoctorID1",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacientID1",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorID1",
                table: "Appointments",
                column: "DoctorID1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PacientID1",
                table: "Appointments",
                column: "PacientID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorID1",
                table: "Appointments",
                column: "DoctorID1",
                principalTable: "Doctors",
                principalColumn: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pacients_PacientID1",
                table: "Appointments",
                column: "PacientID1",
                principalTable: "Pacients",
                principalColumn: "PacientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorID1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pacients_PacientID1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorID1",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PacientID1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorID1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PacientID1",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
