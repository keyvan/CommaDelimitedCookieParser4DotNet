using System;
using System.Web;

namespace CommaDelimitedCookieParser
{
    public class CookieParser : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ParseCookie);
        }

        private void ParseCookie(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpRequest request = application.Request;
            string cookieHeader = string.Empty;
            if (request.Headers["Cookie"] != null)
                cookieHeader = request.Headers["Cookie"];

            try
            {
                CookieParserInternal parser = new CookieParserInternal();
                if (parser.HasCommaDelimitedCookieHeader(cookieHeader))
                    parser.ParseBadCookieHeader(cookieHeader, request.Cookies);
            }
            catch
            {
            }
        }
    }
}
