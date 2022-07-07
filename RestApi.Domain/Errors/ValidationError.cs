using Newtonsoft.Json;

namespace RestApi.Domain.Model.Errors
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = !string.IsNullOrEmpty(field) ? field : null;
            Message = message;
        }
    }
}