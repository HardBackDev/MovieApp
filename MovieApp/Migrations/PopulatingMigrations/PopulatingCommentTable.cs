using Entities.Models;
using FluentMigrator;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace MovieApp.Migrations.PopulatingMigrations
{
    [Migration(9)]
    public class PopulatingCommentTable : Migration
    {
        UserManager<ApplicationUser> _userManager;
        string[] commentsArray = new[]
        {
            "good movie",
            "i like this movie",
            "bad movie",
            "i dont like this movie",
            "this movie poop",
            "i will watch this movie with my family",
            "my dauther was scared for this movie, I disliked",
            "in movie appear blood, i disliked",
            "in movie a lot of murders, i liked",
            "in movie a lot of blood, i liked"
        };

        public PopulatingCommentTable(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public static Guid[] commentIds = new Guid[]
        {
            Guid.Parse("ec3c5120-5d6d-4211-a932-fc78e51297df"),
            Guid.Parse("60bf2393-5c8a-4304-963e-9328cf8380a1"),
            Guid.Parse("23ffb636-7188-48f6-8fde-3fd3f58df7c1"),
            Guid.Parse("9def8409-9084-4594-ad0a-8dac1c3605dc"),
            Guid.Parse("85aa88d2-b421-4201-84e9-3eb7f45191e4"),
            Guid.Parse("7796dab4-606f-4a62-b00d-b082f17a2b8d"),
            Guid.Parse("322c18a1-7221-4807-9157-19bd4bfecc08"),
            Guid.Parse("a26b6b0a-332a-4242-8c4b-eaa7134fd9b7"),
            Guid.Parse("01e76d70-2d65-4a85-b5cd-a9b03d44a659"),
            Guid.Parse("1efc7264-7624-4829-98d9-2768bf7bf23a"),
        };

        public override void Down()
        {
        }

        public override void Up()
        {
            for (int i = 0; i < 4; i++)
                InsertRandomComments();
        }

        private void InsertRandomComments()
        {
            var movieIds = PopulatingMovieTable.MovieIds;


            for (int i = 0; i < movieIds.Count; i++)
            {
                Insert.IntoTable(Tables.CommentTable)
                    .Row(new 
                    {
                        comment_id = Guid.NewGuid(),
                        comment_date_added = RandomDay().AddMinutes(new Random().Next(60)),
                        comment_movie_id = movieIds[i],
                        comment_text = commentsArray[new Random().Next(10)],
                        comment_user_name = PopulatingUserTable.Tucker.UserName
                    })
                    .Row(new 
                    {
                        comment_id = Guid.NewGuid(),
                        comment_date_added = RandomDay().AddMinutes(new Random().Next(60)),
                        comment_movie_id = movieIds[i],
                        comment_text = commentsArray[new Random().Next(10)],
                        comment_user_name = PopulatingUserTable.Deil.UserName
                    })
                    .Row(new 
                    {
                        comment_id = Guid.NewGuid(),
                        comment_date_added = RandomDay().AddMinutes(new Random().Next(60)),
                        comment_movie_id = movieIds[i],
                        comment_text = commentsArray[new Random().Next(10)],
                        comment_user_name = PopulatingUserTable.Bob.UserName
                    });
            }
        }

        DateTime RandomDay()
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
