using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Application.Exceptions
{
    public class WebException: Exception
    {
        public string Code { get; private set; } = "9999";

        public WebException()
        { }

        public WebException(string message)
            : base(message)
        { }

        public WebException(string code, string message)
           : base(message)
        {
            this.Code = code;
        }

        public WebException(int code, string message)
           : base(message)
        {
            this.Code = code.ToString();
        }

        public WebException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public WebException(string code, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Code = code;
        }

        public WebException(int code, string message, Exception innerException)
           : base(message, innerException)
        {
            this.Code = code.ToString();
        }
    }
}
