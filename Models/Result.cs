using System;

namespace ERPXTpl.Models
{
    public class Result
    {
        public Object Data { get; set; }
        public string StatusCode { get; set; } = "ERROR";
        public string Message { get; set; }

        public class Error
        {
            public string Message { get; set; }
        }
    }
}
