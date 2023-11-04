namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object id) : base($"the entity {entityName} with id [{id}] not found") { }
    }
}
