using Contracts.RepositoryContracts;
using Repositories.Queries;
using Repository;
using Dapper;
using Entities.Models;
using Shared.RequestFeatures.EntitiesParameters;
using Shared.Dtos.ActorDtos;
using Shared.Exceptions;
using Shared.Dtos.MovieDtos;

namespace Repositories.EntitiesRepositories
{
    public class MovieRepository : IMovieRepository
    {
        readonly DapperContext _context;
        public MovieRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<(int count, IEnumerable<Movie> movies)> GetMovies(MovieParameters parameters)
        {
            var param = new DynamicParameters(parameters);
            for (int i = 0; i < parameters.SearchedGenresList.Count; i++)
            {
                param.Add($"genre{i}", parameters.SearchedGenresList[i]);
            }
            
            var query = MovieQuery.GetMoviesByParametersQuery(parameters);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);
            
            var count = await multi.ReadSingleAsync<int>();
            var movies = (await multi.ReadAsync<Movie>()).ToList();

            return (count, movies);
        }

        public async Task<(int count, IEnumerable<Movie> movies)> GetMoviesByActorId(Guid id, MovieParameters parameters)
        {
            var param = new DynamicParameters(parameters);
            for (int i = 0; i < parameters.SearchedGenresList.Count; i++)
            {
                param.Add($"genre{i}", parameters.SearchedGenresList[i]);
            }
            param.Add("Id", id);

            var query = MovieQuery.GetMoviesByActorIdQuery(parameters);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var movies = (await multi.ReadAsync<Movie>()).ToList();

            return (count, movies);
        }

        public async Task<(int count, IEnumerable<Movie> movies)> GetMoviesByDirector(Guid id, MovieParameters parameters)
        {
            var param = new DynamicParameters(parameters);
            for (int i = 0; i < parameters.SearchedGenresList.Count; i++)
            {
                param.Add($"genre{i}", parameters.SearchedGenresList[i]);
            }
            param.Add("id", id);
            var query = MovieQuery.GetMoviesByDirectorId(parameters);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var movies = (await multi.ReadAsync<Movie>()).ToList();

            return (count, movies);
        }

        public async Task<(int count, IEnumerable<Movie> movies)> GetMoviesToWatch(MovieParameters parameters, string username)
        {
            var param = new DynamicParameters(parameters);
            for (int i = 0; i < parameters.SearchedGenresList.Count; i++)
            {
                param.Add($"genre{i}", parameters.SearchedGenresList[i]);
            }
            param.Add("username", username);

            var query = MovieQuery.GetMoviesToWatch(parameters);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var movies = (await multi.ReadAsync<Movie>()).ToList();

            return (count, movies);

        }

        public async Task<(int count, IEnumerable<MovieWithActorsDto> movies)> GetMoviesWithActors(MovieParameters parameters, int actorCounts)
        {
            var param = new DynamicParameters(parameters);
            for (int i = 0; i < parameters.SearchedGenresList.Count; i++)
            {
                param.Add($"genre{i}", parameters.SearchedGenresList[i]);
            }
            var query = MovieQuery.GetMoviesWithActors(parameters, actorCounts);

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, param);

            var count = await multi.ReadSingleAsync<int>();
            var movieDict = new Dictionary<Guid, MovieWithActorsDto>();

            var movies = multi.Read<Movie, ActorDto, MovieWithActorsDto>(
                    (movie, actor) =>
                    {
                        if (!movieDict.TryGetValue(movie.Id, out var currentMovie))
                        {
                            currentMovie = new MovieWithActorsDto()
                            {
                                Id = movie.Id,
                                Description = movie.Description,
                                DirectorId = movie.DirectorId,
                                ReleaseDate = movie.ReleaseDate,
                                BadRating = movie.BadRating,
                                Genres = movie.Genres.Split(" ", StringSplitOptions.RemoveEmptyEntries),
                                GoodRating = movie.GoodRating,
                                PhotoUrl = movie.PhotoUrl,
                                Title = movie.Title,
                                VideoPath = movie.VideoPath
                            };
                            movieDict.Add(currentMovie.Id, currentMovie);
                        }

                        currentMovie.Actors.Add(actor);

                        return currentMovie;
                    }, splitOn: $"Id, Id"
                );

            return (count, movies.Distinct());
        }

        public async Task<Movie> GetMovieById(Guid id)
        {
            var query = MovieQuery.GetMovieByIdQuery;

            using var connection = _context.CreateConnection();
            var movie = await connection.QuerySingleOrDefaultAsync<Movie>(query, new { Id = id });
            return movie;
        }

        public async Task<bool> CheckMovieInWatching(Guid id, string username)
        {
            var query = MovieQuery.CheckMovieInWatchinQuery;

            using var connection = _context.CreateConnection();
            var count = await connection.QuerySingleAsync<int>(query, new { id, username });
            return count > 0;
        }

        public async Task UpdateMovie(Guid id, Movie movie)
        {
            var query = MovieQuery.UpdateMovieQuery;
            var param = new DynamicParameters(movie);
            param.Add("Id", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, param);
        }

        public async Task AddMovieToWatching(Guid movieId, string username)
        {
            var query = MovieQuery.AddMovieToWatchingQuery;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new {movieId, username});
        }

        public async Task<Guid> CreateMovie(Movie movie)
        {
            var query = MovieQuery.CreateMovieQuery;
            var param = new DynamicParameters(movie);

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<Guid>(query, param);
            return id;
        }

        public async Task DeleteMovie(Guid id)
        {
            var query = MovieQuery.DeleteMovieQuery;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task DeleteMovieFromWatching(Guid id, string username)
        {
            var query = MovieQuery.DeleteMovieFromWatching;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { id, username });
        }

        public async Task AddActorRelation(Guid movieId, Guid actorId)
        {
            var query = MovieActorQuery.CreateMovieActorRelationQuery;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { movieId, actorId });
        }

        public async Task<RatedMovie> GetMovieRate(Guid movieId, string userName)
        {
            var query = RatedMovieQuery.GetMovieRateQuery;

            using var connection = _context.CreateConnection();
            var ratedMovie = await connection.QuerySingleOrDefaultAsync<RatedMovie>(query, new { movieId, userName });
            return ratedMovie;
        }

        public async Task RateMovie(Guid movieId, string userName, string rate)
        {
            string query = rate == "like" ? RatedMovieQuery.LikeMovieQuery(1) : rate == "dislike" ? RatedMovieQuery.DisLikeMovieQuery(1) :
                throw new BadRequestException($"unknown rate \"{rate}\"");

            using var connection = _context.CreateConnection();
            connection.Open();
            using var trans = connection.BeginTransaction();

            await connection.ExecuteAsync(query, new { Id = movieId }, trans);
            var createRateRelationMovieQuery = RatedMovieQuery.CreateRatedMovieRelation;
            await connection.ExecuteAsync(createRateRelationMovieQuery, new { userName, movieId, rate }, trans);
            trans.Commit();
        }

        public async Task UnRateMovie(Guid movieId, string userName, string rate)
        {
            string unRateQuery = RatedMovieQuery.UnRateMovieQuery;

            using var connection = _context.CreateConnection();
            connection.Open();
            using var trans = connection.BeginTransaction();

            await connection.ExecuteAsync(unRateQuery, new { userName, movieId }, trans);
            var rateMovieQuery = rate.ToLower() == "like" ? RatedMovieQuery.LikeMovieQuery(-1) : RatedMovieQuery.DisLikeMovieQuery(-1);
            await connection.ExecuteAsync(rateMovieQuery, new { Id = movieId }, trans);
            trans.Commit();
        }
    }
}
