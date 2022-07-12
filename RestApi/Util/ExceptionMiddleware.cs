using System.ComponentModel;
using System.Net;
using RestApi.Domain.Model.Errors;
using RestApi.Infraestructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FluentValidation;

namespace RestApi.Application.Util
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SqlExceptionHandler _sqlHandler;

        public ExceptionMiddleware(RequestDelegate next, SqlExceptionHandler sqlHandler)
        {
            _next = next;
            _sqlHandler = sqlHandler;
        }
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            var status = HttpStatusCode.InternalServerError;
            var type = exception.GetType().Name;
            var detail = exception.InnerException is null 
                ? exception.Message 
                : exception.InnerException.Message;
            
            ValidationError validationError = null;
            
            switch (exception)
            {
                case InvalidEnumArgumentException _:
                case InvalidCastException _:
                case PasswordNullException _:
                case AttributeNotFoundException _:
                    status = HttpStatusCode.BadRequest;
                    detail = exception.Message;
                    break;
                case NullReferenceException _:
                    status = HttpStatusCode.NotFound;
                    detail = "No corresponding item has been found.";
                    break;
                case ForbiddenException _:
                    status = HttpStatusCode.Forbidden;
                    detail = !string.IsNullOrWhiteSpace(exception.Message)
                        ? exception.Message
                        : "You don't have authorization to access this content.";
                    break;
                case DbUpdateException _:
                    var valError = _sqlHandler.ValidateUniqueConstraint(exception);
                    status = HttpStatusCode.UnprocessableEntity;
                    if (valError != null)
                    {
                        type = "NotUniqueValue";
                        validationError = valError;
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            string result;
            if (validationError is null)
                result = JsonConvert.SerializeObject(new {status, type, detail});
            else
                result = JsonConvert.SerializeObject(new {status, type, Errors = validationError});
            
            return context.Response.WriteAsync(result);
        }
    }
}