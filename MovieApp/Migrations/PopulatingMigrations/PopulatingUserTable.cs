using Contracts.ServiceContracts;
using FluentMigrator;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.Dtos.UserDtos;

namespace MovieApp.Migrations.PopulatingMigrations
{
    [Migration(8)]
    public class PopulatingUserTable : Migration
    {
        IServiceManager _service;
        private readonly UserManager<ApplicationUser> _userManager;
        static public UserForRegistrationDto Bob = new UserForRegistrationDto()
        {
            Email = "Bob@gmail.com",
            Password = "Password123",
            PhoneNumber = "12345324120",
            UserName = "Bob"
        };
        static public UserForRegistrationDto Deil = new UserForRegistrationDto()
        {
            Email = "Deil@gmail.com",
            Password = "Password123",
            PhoneNumber = "34626352",
            UserName = "Deil"
        };
        static public UserForRegistrationDto Tucker = new UserForRegistrationDto()
        {
            Email = "Tucker@gmail.com",
            Password = "Password123",
            PhoneNumber = "1234567890",
            UserName = "Tucker"
        };

        public PopulatingUserTable(IServiceManager service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            this._userManager = userManager;
        }

        public override void Down()
        {
            Execute.Sql("""
                DELETE FROM identity_users
                WHERE username = 'Tucker' OR username = 'Bob' OR username = 'Deil'
                """);
        }

        public override void Up()
        {
            var bobCreation = _service.AuthenticationService.RegisterUser(Bob);
            var deilCreation = _service.AuthenticationService.RegisterUser(Deil);
            var tuckerCreation = _service.AuthenticationService.RegisterUser(Tucker);

            Task.WaitAll(bobCreation, deilCreation, tuckerCreation);

            Insert.IntoTable(Tables.MovieToWatchTable)
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[0],
                    movie_to_watch_username = Tucker.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[1],
                    movie_to_watch_username = Tucker.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[2],
                    movie_to_watch_username = Tucker.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[3],
                    movie_to_watch_username = Bob.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[4],
                    movie_to_watch_username = Bob.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[5],
                    movie_to_watch_username = Bob.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[6],
                    movie_to_watch_username = Deil.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[7],
                    movie_to_watch_username = Deil.UserName
                })
                .Row(new
                {
                    movie_to_watch_movie_id = PopulatingMovieTable.MovieIds[8],
                    movie_to_watch_username = Deil.UserName
                });

            var bobId = _userManager.FindByNameAsync(Bob.UserName).Result.Id;

            Update.Table("identity_user_roles")
                .Set(new
                {
                    user_id = bobId,
                    role_id = PopulatingRolesTables.adminRole
                })
                .Where(new
                {
                    user_id = bobId
                });
                
        }
    }
}
