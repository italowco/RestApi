using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.AuthenticationHandler
{
    public class AuthenticationHandler
    {
        private readonly RequestDelegate _next;

        public Task Invoke() {

            return null;
        }

    }
}
