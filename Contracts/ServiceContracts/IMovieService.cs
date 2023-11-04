using Entities.Models;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Contracts.ServiceContracts
{
    public interface IMovieService
    {
        Task<PagedList<MovieDto>> GetMovies(MovieParameters parameters);
        Task<PagedList<MovieDto>> GetMoviesByDirector(Guid id, MovieParameters parameters);
        Task<PagedList<MovieWithActorsDto>> GetMoviesWithActors(MovieParameters parameters, int actorsCount);
        Task<PagedList<MovieDto>> GetMoviesToWatch(MovieParameters parameters, string username);
        Task<PagedList<MovieDto>> GetMoviesByActorId(Guid id, MovieParameters parameters);
        Task<MovieDto> GetMovieById(Guid id);
        Task<bool> CheckMovieInWatching(Guid id, string username);
        Task<MovieDto> CreateMovie(MovieForCreation movie);
        Task UpdateMovie(Guid id, MovieForUpdate movie);
        Task AddMovieToWatching(Guid movieId, string username);
        Task DeleteMovie(Guid id);
        Task DeleteMovieFromWatching(Guid id, string username);

        Task AddActorRelation(Guid movieId, Guid actorId);
        Task RateMovie(Guid id, string userName, string rate);
        Task UnRateMovie(Guid movieId, string userName);
        Task<RatedMovie> GetMovieRate(Guid id, string userName);
    }
}
