using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NNice.Web.Helpers
{

    public class ResponseObject<T> where T : class
    {
        public IEnumerable<T> data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Successfully";
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    }
}
