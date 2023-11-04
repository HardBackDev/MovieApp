using Entities.Models;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.RepositoryContracts
{
    public interface IMovieRepository
    {
        Task<(int count, IEnumerable<Movie> movies)> GetMovies(MovieParameters parameters);
        Task<(int count, IEnumerable<Movie> movies)> GetMoviesByDirector(Guid id, MovieParameters parameters);
        Task<(int count, IEnumerable<Movie> movies)> GetMoviesByActorId(Guid id, MovieParameters parameters);
        Task<(int count, IEnumerable<Movie> movies)> GetMoviesToWatch(MovieParameters parameters, string username);
        Task<(int count, IEnumerable<MovieWithActorsDto> movies)> GetMoviesWithActors(MovieParameters parameters, int actorCounts);
        Task<Movie> GetMovieById(Guid id);
        Task<bool> CheckMovieInWatching(Guid id, string username);
        Task<Guid> CreateMovie(Movie movie);
        Task UpdateMovie(Guid id, Movie movie);
        Task AddMovieToWatching(Guid movieId, string username);
        Task DeleteMovie(Guid id);
        Task DeleteMovieFromWatching(Guid id, string username);

        Task AddActorRelation(Guid movieId, Guid actorId);
        Task RateMovie(Guid movieId, string userName, string rate);
        Task UnRateMovie(Guid movieId, string userName, string rate);
        Task<RatedMovie> GetMovieRate(Guid movieId, string userName);
    }
}
