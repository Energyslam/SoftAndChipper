namespace InformationExchange.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("The requested entity was not found.") { }
    }
}
