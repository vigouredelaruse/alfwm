using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.library.activity.httpactivity
{
    public class HttpActivityException : Exception
    {
        public HttpActivityException(string message) : base(message)
        { }
    }
}
