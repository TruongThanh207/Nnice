using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NNice.Business.DTO
{
    public class ResponseObject
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Successfully";
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    }
    public class ResponseObject<T> where T: class
    {
        public IEnumerable<T> data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Successfully";
        public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    }
}
