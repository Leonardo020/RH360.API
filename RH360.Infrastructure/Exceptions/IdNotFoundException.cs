using System.Runtime.Serialization;

namespace RH360.API.Exceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException()
            : base("ID not found.") { }

        public IdNotFoundException(string entity)
            : base($"{entity} with the specified ID was not found.") { }
    }
}
