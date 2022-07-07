using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Infraestructure.Exceptions
{
    public class PasswordNullException : Exception
    {
        public PasswordNullException(string message)
            : base(message)
        {

        }
    }
}
