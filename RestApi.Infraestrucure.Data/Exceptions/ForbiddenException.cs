using System.Security;

namespace RestApi.Infraestructure.Exceptions
{
    public class ForbiddenException : SecurityException
    {
        public ForbiddenException(string message) : base(message)
        {
            
        }
    }
}