using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreEP.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest resquest) => resquest.QueryString.HasValue ? $"{resquest.Path}" : resquest.Path.ToString();
    }
}
