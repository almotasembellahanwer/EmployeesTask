using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTask.Core.DTO
{
    public class APIResponse
    {
        public APIResponse()
        {
            Errormessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object? Result { get; set; }
        public List<string> Errormessages { get; set; } = default!;
    }
}
