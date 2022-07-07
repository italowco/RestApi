using System;

namespace RestApi.Infraestructure.Exceptions
{
    public class AttributeNotFoundException : Exception
    {
        public AttributeNotFoundException(string message) 
            : base(message)
        {
            
        }
    }
}