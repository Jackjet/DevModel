using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Baibaomen.FileService
{
    /// <summary>
    /// Instead of require token from header, request token from specified query param or cookie.
    /// </summary>
    public class CookieAndUrlTokenProvider: OAuthBearerAuthenticationProvider
    {
        private readonly string _name;

        /// <summary>
        /// Instead of require token from header, request token from specified query param or cookie.
        /// </summary>
        /// <param name="name">The name of the query param or cookie to get the token string.</param>
        public CookieAndUrlTokenProvider(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            base.RequestToken(context).Wait();

            if (string.IsNullOrEmpty(context.Token))
            {
                var tokenFromUrl = context.Request.Query.Get(_name);
                if (!string.IsNullOrEmpty(tokenFromUrl))
                {
                    context.Token = tokenFromUrl;
                    context.Response.Cookies.Append(_name,tokenFromUrl);
                }
                else if (!string.IsNullOrEmpty(context.Request.Cookies[_name]))
                {
                    context.Token = context.Request.Cookies[_name];
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}