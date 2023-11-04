namespace Contracts.RepositoryContracts
{
    public interface IRepositoryManager
    {
        ICommentRepository CommentRepo { get; }
        IActorRepository ActorRepo { get; }
        IMovieRepository MovieRepo { get; }
        IDirectorRepository DirectorRepo { get; }   
    }
}
