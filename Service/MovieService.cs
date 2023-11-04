using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Entities.Models;
using Identity.Dapper.Postgres.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Repositories.Queries;
using Shared.Dtos.ActorDtos;
using Shared.Dtos.MovieDtos;
using Shared.Exceptions;
using Shared.RequestFeatures;
using Shared.RequestFeatures.EntitiesParameters;

namespace Service
{
    public class MovieService : IMovieService
    {
        readonly IRepositoryManager _repo;
        readonly IMapper _mapper;
        readonly IHostingEnvironment _webHostEnvironment;

        public MovieService(IRepositoryManager repo, IMapper mapper, IHostingEnvironment webHostEnvironment)
        {
            _repo = repo;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<MovieDto> GetMovieById(Guid id)
        {
            var movie = await TryGetMovie(id);

            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }

        public async Task<PagedList<MovieDto>> GetMovies(MovieParameters parameters)
        {
            var pagedResult = await _repo.MovieRepo.GetMovies(parameters);
            var moviesDto = _mapper.Map<List<MovieDto>>(pagedResult.movies);
            return new PagedList<MovieDto>(moviesDto, pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<MovieDto>> GetMoviesByActorId(Guid id, MovieParameters parameters)
        {
            if (await _repo.ActorRepo.GetActorById(id) is null)
                 throw new NotFoundException(nameof(Actor), id);

            var pagedResult = await _repo.MovieRepo.GetMoviesByActorId(id, parameters);
            var moviesDto = _mapper.Map<List<MovieDto>>(pagedResult.movies);

            return new PagedList<MovieDto>(moviesDto, pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<MovieDto>> GetMoviesByDirector(Guid id, MovieParameters parameters)
        {
            if (await _repo.DirectorRepo.GetDirector(id) is null)
                throw new NotFoundException(nameof(Director), id);

            var pagedResult = await _repo.MovieRepo.GetMoviesByDirector(id, parameters);
            var moviesDto = _mapper.Map<List<MovieDto>>(pagedResult.movies);

            return new PagedList<MovieDto>(moviesDto, pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<MovieDto>> GetMoviesToWatch(MovieParameters parameters, string username)
        {
            var pagedResult = await _repo.MovieRepo.GetMoviesToWatch(parameters, username);
            var moviesDto = _mapper.Map<List<MovieDto>>(pagedResult.movies);

            return new PagedList<MovieDto>(moviesDto, pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<PagedList<MovieWithActorsDto>> GetMoviesWithActors(MovieParameters parameters, int actorsCount)
        {
            var pagedResult = await _repo.MovieRepo.GetMoviesWithActors(parameters, actorsCount);

            return new PagedList<MovieWithActorsDto>(pagedResult.movies.ToList(), pagedResult.count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<bool> CheckMovieInWatching(Guid id, string username)
        {
            await TryGetMovie(id);
            return await _repo.MovieRepo.CheckMovieInWatching(id, username);
        }

        public async Task UpdateMovie(Guid id, MovieForUpdate movie)
        {
            var updatingMovie = await TryGetMovie(id);
            string updatingFilePath = updatingMovie.VideoPath;
            string newFilePath = movie.VideoPath;
            if (_webHostEnvironment.IsEnvironment("Docker"))
            {
                updatingFilePath = updatingFilePath.Replace("\\", "/");
                newFilePath = newFilePath.Replace("\\", "/");
            }
            if (!string.IsNullOrEmpty(updatingFilePath) && !newFilePath.Equals(updatingFilePath) && File.Exists(updatingFilePath))
            {
                File.Delete(updatingFilePath);
            }

            var movieForUpdate = _mapper.Map<Movie>(movie);
            await _repo.MovieRepo.UpdateMovie(id, movieForUpdate);
        }

        public async Task AddMovieToWatching(Guid movieId, string username)
        {
            await TryGetMovie(movieId);
            await _repo.MovieRepo.AddMovieToWatching(movieId, username);
        }

        public async Task AddActorRelation(Guid movieId, Guid actorId)
        {
            await TryGetMovie(movieId);
            if (await _repo.ActorRepo.GetActorById(actorId) is null)
                throw new NotFoundException(nameof(Actor), actorId);
            await _repo.MovieRepo.AddActorRelation(movieId, actorId);
        }

        public async Task RateMovie(Guid id, string userName, string rate)
        {
            await TryGetMovie(id);
            if (await _repo.MovieRepo.GetMovieRate(id, userName) is not null)
                throw new BadRequestException($"user \"{userName}\" already rate movie with id {id}");

            await _repo.MovieRepo.RateMovie(id, userName, rate);
        }

        public async Task UnRateMovie(Guid movieId, string username)
        {
            await TryGetMovie(movieId);
            var grade = await TryGetMovieRate(movieId, username);
            await _repo.MovieRepo.UnRateMovie(movieId, username, grade.Rate);
        }

        public async Task<MovieDto> CreateMovie(MovieForCreation movie)
        {
            var movieForCreate = _mapper.Map<Movie>(movie);
            var createdId = await _repo.MovieRepo.CreateMovie(movieForCreate);
            return new MovieDto
            {
                Id = createdId,
                Description = movie.Description,
                DirectorId = movie.DirectorId,
                VideoPath = movie.VideoPath,
                PhotoUrl = movie.PhotoUrl,
                Genres = movie.Genres,
                ReleaseDate = movie.ReleaseDate,
                Title = movie.Title,
            };
        }

        public async Task DeleteMovie(Guid id)
        {
            var movie = await TryGetMovie(id);
            string filePath = movie.VideoPath;
            if (_webHostEnvironment.IsEnvironment("Docker"))
            {
                filePath = movie.VideoPath.Replace("\\", "/");
            }
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                File.Delete(filePath);
            }else
            {
                Console.WriteLine($"file with path {filePath} not found");
            }

            await _repo.MovieRepo.DeleteMovie(id);
        }

        public async Task DeleteMovieFromWatching(Guid id, string username)
        {
            await TryGetMovie(id);
            await _repo.MovieRepo.DeleteMovieFromWatching(id, username);
        }

        public async Task<RatedMovie> GetMovieRate(Guid id, string username)
        {
            await TryGetMovie(id);
            return await _repo.MovieRepo.GetMovieRate(id, username);
        }

        private async Task<RatedMovie> TryGetMovieRate(Guid id, string username)
        {
            return await _repo.MovieRepo.GetMovieRate(id, username) ??
                throw new NotFoundException("rated movie", id);
        }

        private async Task<Movie> TryGetMovie(Guid id)
        {
            return await _repo.MovieRepo.GetMovieById(id)
                ?? throw new NotFoundException(nameof(Movie), id);
        }
    }
}
