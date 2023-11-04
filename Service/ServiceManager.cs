using AutoMapper;
using Contracts.RepositoryContracts;
using Contracts.ServiceContracts;
using Identity.Dapper.Postgres.Models;
using LoggerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMovieService> _movieService;
        private readonly Lazy<IActorService> _actorService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<IDirectorService> _directorService;

        public ServiceManager(IRepositoryManager repo, IMapper mapper, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, ILoggerManager loggerManager, IHostingEnvironment webHostEnvironment)
        {
            _commentService = new Lazy<ICommentService>(() => new CommentService(repo, userManager));
            _movieService = new Lazy<IMovieService>(() => new MovieService(repo, mapper, webHostEnvironment));
            _actorService = new Lazy<IActorService>(() => new ActorService(repo));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(loggerManager, userManager, configuration));
            _directorService = new Lazy<IDirectorService>(() => new DirectorService(repo));
        }

        public ICommentService CommentService => _commentService.Value;
        public IMovieService MovieService => _movieService.Value;
        public IActorService ActorService => _actorService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IDirectorService DirectorService => _directorService.Value;
    }
}
