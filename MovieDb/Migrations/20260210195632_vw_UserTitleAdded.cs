using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDb.Migrations
{
    /// <inheritdoc />
    public partial class vw_UserTitleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_UserTitle AS
                SELECT u.Username AS [User], t.Name AS Title
                FROM Users u
                JOIN Titles t ON u.Id = t.UserId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_UserTitle;");
        }
    }
}
