using Entities.Models;
using FluentMigrator;
using MovieApp.Migrations.PopulatingMigrations;
using Shared;

namespace MovieApp.Migrations
{
    [Migration(6)]
    public class CreatingMoviesActorsRelations : Migration
    {

        public override void Down()
        {
        }

        public override void Up()
        {
            Insert.IntoTable(Tables.MoviesActorsTable)
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[7],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[0]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[7],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[1]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[7],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[2]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[0],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[3]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[0],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[4]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[3],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[5]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[3],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[6]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[8],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[7]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[8],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[8]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[6],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[9]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[6],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[10]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[6],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[11]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[1],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[12]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[1],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[13]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[1],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[14]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[2],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[15]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[2],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[16]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[2],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[17]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[4],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[18]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[4],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[19]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[4],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[20]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[4],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[21]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[9],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[18]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[9],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[11]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[5],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[20]
                })
                .Row(new 
                {
                    movies_actors_movie_id = PopulatingMovieTable.MovieIds[5],
                    movies_actors_actor_id = PopulatingActorTable.ActorIds[11]
                });
        }
    }
}
