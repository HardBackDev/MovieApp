using Entities.Models;
using FluentMigrator;
using MovieApp.Migrations.PopulatingMigrations;
using Shared;
using System.Globalization;
using System.Text;

namespace MovieApp.Migrations
{
    [Migration(4)]
    public class PopulatingMovieTable : Migration
    {
        public static List<Guid> MovieIds = new();

        static PopulatingMovieTable()
        {
            for (int i = 0; i < 10; i++)
            {
                MovieIds.Add(Guid.NewGuid());
            }
        }

        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable(Tables.MovieTable)
                .Row(new
                {
                    movie_id = MovieIds[8],
                    movie_title = "Terrifier",
                    movie_description = "On Halloween night, Tara Heyes finds herself as the obsession of a sadistic murderer known as Art the Clown.",
                    movie_photo_url = "https://m.media-amazon.com/images/S/pv-target-images/698f75aebd1403d6c78af2e44c587c0cb7f7ff0aa38bc6ca7befdefe7d78fd0e.jpg",
                    movie_genres = "Horror Thriller",
                    movie_release_date = DateTime.ParseExact("2016/09/23", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[0],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Terrifier.mp4")
                })
                .Row(new
                {
                    movie_id = MovieIds[0],
                    movie_title = "Terrifier 2",
                    movie_description = "After resurrecting from the dead, maniac Art goes to the laundry room, where he meets a girl who looks like him. Meanwhile, high school student Sienna prepares for Halloween by making a costume from her recently deceased father.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BNzEzOTZjYjYtMTI3MS00NWE1LWIyZTktYTRlMTBlYjlkMjZiXkEyXkFqcGdeQXVyMTUzMTg2ODkz._V1_.jpg",
                    movie_genres = "Horror Thriller",
                    movie_release_date = DateTime.ParseExact("2022/08/29", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[0],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Terrifier 2.mp4")
                })
                .Row(new
                {
                    movie_id = MovieIds[1],
                    movie_title = "The Collector",
                    movie_description = "Desperate to repay his debt to his ex-wife, an ex-con plots a heist at his new employer's country home, unaware that a second criminal has also targeted the property, and rigged it with a series of deadly traps.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BMTQ4NTU3OTE3OF5BMl5BanBnXkFtZTcwNTkxMjA3Mg@@._V1_FMjpg_UX1000_.jpg",
                    movie_genres = "Horror Thriller",
                    movie_release_date = DateTime.ParseExact("2010/08/05", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[4],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "The Collector.mp4")
                })
                .Row(new
                {
                    movie_id = MovieIds[2],
                    movie_title = "Fargo",
                    movie_description = "Minnesota car salesman Jerry Lundegaard's inept crime falls apart due to his and his henchmen's bungling and the persistent police work of the quite pregnant Marge Gunderson.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BNDJiZDgyZjctYmRjMS00ZjdkLTkwMTEtNGU1NDg3NDQ0Yzk1XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                    movie_genres = "Comedy Thriller",
                    movie_release_date = DateTime.ParseExact("1996/03/08", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[3],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Fargo.mp4")
                }).Row(new
                {
                    movie_id = MovieIds[3],
                    movie_title = "The Shawshank Redemption",
                    movie_description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BNDE3ODcxYzMtY2YzZC00NmNlLWJiNDMtZDViZWM2MzIxZDYwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg",
                    movie_genres = "Drama",
                    movie_release_date = DateTime.ParseExact("1994/09/23", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[4],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "The Shawshank Redemption.mp4")
                }).Row(new
                {
                    movie_id = MovieIds[4],
                    movie_title = "Inception",
                    movie_description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    movie_photo_url = "https://flxt.tmsimg.com/assets/p7825626_p_v8_af.jpg",
                    movie_genres = "Action Sci-Fi",
                    movie_release_date = DateTime.ParseExact("2010/07/16", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[1],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "inception.mp4")
                }).Row(new
                {
                    movie_id = MovieIds[5],
                    movie_title = "Pulp Fiction",
                    movie_description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                    movie_genres = "Crime Drama",
                    movie_release_date = DateTime.ParseExact("1994/10/14", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[1],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Pulp Fiction.mp4")
                }).Row(new
                {
                    movie_id = MovieIds[6],
                    movie_title = "Avatar",
                    movie_description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                    movie_photo_url = "https://m.media-amazon.com/images/M/MV5BYjhiNjBlODctY2ZiOC00YjVlLWFlNzAtNTVhNzM1YjI1NzMxXkEyXkFqcGdeQXVyMjQxNTE1MDA@._V1_.jpg",
                    movie_genres = "Action Adventure Sci-Fi",
                    movie_release_date = DateTime.ParseExact("2009/12/18", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[2],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Avatar.mp4")
                }).Row(new
                {
                    movie_id = MovieIds[7],
                    movie_title = "Forrest Gump",
                    movie_description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other history unfold through the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                    movie_photo_url = "https://flxt.tmsimg.com/assets/p15829_v_v13_aa.jpg",
                    movie_genres = "Drama Romance",
                    movie_release_date = DateTime.ParseExact("1994/07/06", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[2],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "FORREST GUMP.mp4")
                })
                .Row(new
                {
                    movie_id = MovieIds[9],
                    movie_title = "Titanic",
                    movie_description = "Titanic is a 1997 American disaster film directed, written, produced, and co-edited by James Cameron. Incorporating both historical and fictionalized aspects, it is based on accounts of the sinking of RMS Titanic in 1912. Kate Winslet and Leonardo DiCaprio star as members of different social classes who fall in love during the ship's maiden voyage. The film also features Billy Zane, Kathy Bates, Frances Fisher, Gloria Stuart, Bernard Hill, Jonathan Hyde, Victor Garber, David Warner, Suzy Amis and Bill Paxton.",
                    movie_photo_url = "https://upload.wikimedia.org/wikipedia/en/1/18/Titanic_%281997_film%29_poster.png",
                    movie_genres = "Drama Romance",
                    movie_release_date = DateTime.ParseExact("1997/07/06", "yyyy/MM/dd", CultureInfo.InvariantCulture),
                    movie_director_id = PopulatingDirectorTable.DirectorIds[2],
                    movie_video_path = Path.Combine(Directory.GetCurrentDirectory() + "\\wwwroot", "Titanic.mp4")
                });
            foreach (var id in MovieIds)
            {
                Update.Table(Tables.MovieTable)
                    .Set(new
                    {
                        movie_good_rating = new Random().Next(5000),
                    })
                    .Where(new
                    {
                        movie_id = id
                    });

                Update.Table(Tables.MovieTable)
                   .Set(new
                   {
                       movie_bad_rating = new Random().Next(5000)
                   })
                   .Where(new
                   {
                       movie_id = id
                   });

            }

        }
    }
}
