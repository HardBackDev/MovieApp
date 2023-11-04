using FluentMigrator;

namespace MovieApp.Migrations
{
    [Migration(2)]
    public class CreatingTriggers : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            string createChangeRatingTrigger = File.ReadAllText(@"./Migrations/create_movies_triggers.txt");

            Execute.Sql(createChangeRatingTrigger);
        }
    }
}
