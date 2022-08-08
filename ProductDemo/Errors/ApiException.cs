using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDemo.Errors
{
    public class ApiException : ApiResponse
    {
       public ApiException(int StatusCode,string Message=null,string details =null):base (StatusCode,Message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
