using System;
using System.Collections.Generic;
using System.Text;

namespace com.ataxlab.alfwm.core.grammar.verbs.http
{
    public enum HttpStrategyRequestMethod { Delete, Get, Head, Patch, Post, Put };

    /// <summary>
    ///  deliberately modelled on HttpRequestMessage
    ///  but with a nod to the existence of 
    ///  multiple implementations
    /// </summary>
    public class HttpStrategyRequestMessage
    {
        public Uri RequestUri { get; set; }
        public HttpStrategyRequestMethod RequestMethod { get; set; }
        public Dictionary<string, string> HttpHeaders { get; set; }
    }
}
