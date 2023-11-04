using FluentMigrator;
using Identity.Dapper.Postgres.Models;

namespace MovieApp.Migrations.PopulatingMigrations
{
    [Migration(7)]
    public class PopulatingRolesTables : Migration
    {
        public static Guid userRole = Guid.NewGuid();
        public static Guid adminRole = Guid.NewGuid();
        public override void Down()
        {
            Delete.FromTable("identity_roles")
            .Row(new
            {
                id = userRole,
                name = "User",
                normalized_name = "USER"
            })
            .Row(new
            {
                id = adminRole,
                name = "Admin",
                normalized_name = "ADMIN"
            });
        }
        public override void Up()
        {
            Insert.IntoTable("identity_roles")
            .Row(new
            {
                id = userRole,
                name = "User",
                normalized_name = "USER"
            })
            .Row(new
            {
                id = adminRole,
                name = "Admin",
                normalized_name = "ADMIN"
            });
        }
    }
}
