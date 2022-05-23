using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebServer
{
    public class Request
    {
        public string? Protocol { get; set; }
        public string? Host { get; set; }
        public int? Port { get; set; }
        public string? ControllerName => _cname;
        public string? ActionName => _aname;
        public string? Id { get; set; }

        HttpListenerRequest? _httpRequest;
        public HttpListenerRequest? HttpRequest => _httpRequest;
        public NameValueCollection? Query => _httpRequest?.QueryString;

        string? _cname;
        string? _aname;
        public Request(HttpListenerRequest? request)
        {
            _httpRequest = request;
            if (request?.RawUrl != null)
            {
                var s = request.RawUrl.Split('/');
                if (s.Length > 1)
                {
                    _cname = s[1];
                    if (s.Length > 2)
                    {
                        _aname = s[2];
                        if (s.Length > 3 && s[3] != string.Empty)
                        {
                            Id = s[3];
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(_cname)) { _cname = "home"; }
            if (string.IsNullOrEmpty(_aname)) { _aname = "index"; }
        }
    }
}
