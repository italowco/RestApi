using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Domain.TO
{
    public class ResponseTO
    {
        public ResponseTO(string? status, List<string>? messages)
        {
            Status = status;
            Messages = messages;
        }

        public string? Status { get; set; }
        public List<string>? Messages { get; set; }
    }

    public class Response
    {
        

        public string? Status { get; set; }
        public string? Message { get; set; }
    }
}
