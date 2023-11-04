using Entities.Models;
using FluentMigrator;
using Shared;
using Shared.Columns;
using System.Data;

namespace MovieApp.Migrations
{
    [Migration(1)]
    public class CreatingTables : Migration
    {
        public override void Down()
        {
            var deleteCommand = $"""
                DROP TABLE identity_user_tokens;
                DROP TABLE identity_user_roles;
                DROP TABLE identity_user_logins;
                DROP TABLE identity_user_claims;
                DROP TABLE identity_role_claims;
                DROP TABLE "{Tables.RatedMovieTable}";
                DROP TABLE "{Tables.CommentTable}";
                DROP TABLE "{Tables.MovieToWatchTable}";
                DROP TABLE identity_users;
                DROP TABLE identity_roles;
                DROP TABLE "{Tables.MoviesActorsTable}";
                DROP TABLE "{Tables.MovieTable}";
                DROP TABLE "{Tables.ActorTable}";
                DROP TABLE "{Tables.DirectorTable}";
                """;

            Execute.Sql(deleteCommand);
        }

        public override void Up()
        {
            string creatingIdendityTablesSql = File.ReadAllText(@"./Migrations/create_identity_tables.txt");
            Execute.Sql(creatingIdendityTablesSql);

            Create.Table(Tables.DirectorTable)
                .WithColumn(DirectorColumns.Id).AsGuid().PrimaryKey()
                .WithColumn(DirectorColumns.Name).AsString().NotNullable()
                .WithColumn(DirectorColumns.Age).AsInt32().NotNullable()
                .WithColumn(DirectorColumns.Bio).AsString(5000).NotNullable()
                .WithColumn(DirectorColumns.PhotoUrl).AsString(12000).NotNullable();

            Create.Table(Tables.MovieTable)
                .WithColumn(MovieColumns.Id).AsGuid().PrimaryKey()
                .WithColumn(MovieColumns.DirectorId).AsGuid().ForeignKey(Tables.DirectorTable, DirectorColumns.Id).OnDelete(Rule.Cascade)
                .WithColumn(MovieColumns.Title).AsString().NotNullable()
                .WithColumn(MovieColumns.Description).AsString(5000).NotNullable()
                .WithColumn(MovieColumns.ReleaseDate).AsDate().NotNullable()
                .WithColumn(MovieColumns.Genres).AsString().NotNullable()
                .WithColumn(MovieColumns.PhotoUrl).AsString(12000).NotNullable()
                .WithColumn(MovieColumns.VideoPath).AsString(2000).Nullable()
                .WithColumn(MovieColumns.GoodRating).AsInt32().Nullable().WithDefaultValue(0)
                .WithColumn(MovieColumns.BadRating).AsInt32().Nullable().WithDefaultValue(0);

            Create.Table(Tables.ActorTable)
                .WithColumn(ActorColumns.Id).AsGuid().PrimaryKey()
                .WithColumn(ActorColumns.Name).AsString().NotNullable()
                .WithColumn(ActorColumns.Bio).AsString(5000).NotNullable()
                .WithColumn(ActorColumns.PhotoUrl).AsString(12000).NotNullable();

            Create.Table(Tables.CommentTable)
                .WithColumn(CommentColumns.Id).AsGuid().PrimaryKey()
                .WithColumn(CommentColumns.UserName).AsString().NotNullable()
                .WithColumn(CommentColumns.MovieId).AsGuid().ForeignKey(Tables.MovieTable, MovieColumns.Id).OnDelete(Rule.Cascade)
                .WithColumn(CommentColumns.DateAdded).AsDateTime().NotNullable().WithDefaultValue("NOW()")
                .WithColumn(CommentColumns.Text).AsString(500).NotNullable();

            Create.Table(Tables.MovieToWatchTable)
                .WithColumn(MovieToWatchColumns.MovieId).AsGuid().ForeignKey(Tables.MovieTable, MovieColumns.Id).OnDelete(Rule.Cascade)
                .WithColumn(MovieToWatchColumns.UserName).AsString().NotNullable();

            Execute.Sql($"""
                ALTER TABLE "{Tables.MovieToWatchTable}" ADD CONSTRAINT pk_movie_to_watch_username_movieid PRIMARY KEY ("{MovieToWatchColumns.UserName}", "{MovieToWatchColumns.MovieId}")
                """);

            Create.Table(Tables.RatedMovieTable)
                .WithColumn(RatedMovieColumns.MovieId).AsGuid().ForeignKey(Tables.MovieTable, MovieColumns.Id).OnDelete(Rule.Cascade)
                .WithColumn(RatedMovieColumns.UserName).AsString().NotNullable()
                .WithColumn(RatedMovieColumns.Rate).AsString().NotNullable();

            Execute.Sql($"""
                ALTER TABLE "{Tables.RatedMovieTable}" ADD CONSTRAINT pk_rated_movie_username_movieid PRIMARY KEY ("{RatedMovieColumns.UserName}", "{RatedMovieColumns.MovieId}")
                """);

            Create.Table(Tables.MoviesActorsTable)
                .WithColumn(MoviesActorsColumns.ActorId).AsGuid().ForeignKey(Tables.ActorTable, ActorColumns.Id).OnDelete(Rule.Cascade)
                .WithColumn(MoviesActorsColumns.MovieId).AsGuid().ForeignKey(Tables.MovieTable, MovieColumns.Id).OnDelete(Rule.Cascade);

            Execute.Sql($"""
                ALTER TABLE "{Tables.MoviesActorsTable}" ADD CONSTRAINT pk_actorid_movieid PRIMARY KEY ("{MoviesActorsColumns.MovieId}", "{MoviesActorsColumns.ActorId}")
                """);

        }
    }
}