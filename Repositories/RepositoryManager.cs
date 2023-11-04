using Contracts.RepositoryContracts;
using Repositories.EntitiesRepositories;
using Repository;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IMovieRepository> _movieRepo;
        private readonly Lazy<IActorRepository> _actorRepo;
        private readonly Lazy<ICommentRepository> _commentRepo;
        private readonly Lazy<IDirectorRepository> _directorRepo;

        public RepositoryManager(DapperContext context)
        {
            _movieRepo = new Lazy<IMovieRepository>(() => new MovieRepository(context));
            _actorRepo = new Lazy<IActorRepository>(() => new ActorRepository(context));
            _commentRepo = new Lazy<ICommentRepository>(() => new CommentRepository(context));
            _directorRepo = new Lazy<IDirectorRepository>(() => new DirectorRepository(context));
        }

        public ICommentRepository CommentRepo => _commentRepo.Value;
        public IMovieRepository MovieRepo => _movieRepo.Value;
        public IActorRepository ActorRepo => _actorRepo.Value;
        public IDirectorRepository DirectorRepo => _directorRepo.Value;
    }
}
