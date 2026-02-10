using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDb.Migrations
{
    /// <inheritdoc />
    public partial class CreateAddUserProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE AddUser
                    @Username NVARCHAR(100),
                    @Email NVARCHAR(100),
                    @Password NVARCHAR(100) 
                AS
                BEGIN
                    INSERT INTO Users (Username, Email)
                    VALUES (@Username, @Email);
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AddUser;");
        }
    }
}
