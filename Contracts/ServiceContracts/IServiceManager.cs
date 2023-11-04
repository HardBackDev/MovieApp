namespace Contracts.ServiceContracts
{
    public interface IServiceManager
    {
        ICommentService CommentService { get; }
        IMovieService MovieService { get; }
        IActorService ActorService { get; }
        IAuthenticationService AuthenticationService { get; }
        IDirectorService DirectorService { get; }
    }
}
