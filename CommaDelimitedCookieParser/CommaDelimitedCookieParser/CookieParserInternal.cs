using System;
using System.Web;

namespace CommaDelimitedCookieParser
{
    internal class CookieParserInternal
    {
        public bool HasCommaDelimitedCookieHeader(string cookieHeader)
        {
            if (cookieHeader.Contains(","))
                return true;

            return false;
        }

        public void ParseBadCookieHeader(string cookieHeader, HttpCookieCollection cookies)
        {
            cookies.Clear();

            FillInCookiesCollection(cookies, cookieHeader);
        }

        public void FillInCookiesCollection(HttpCookieCollection cookieCollection, string cookieHeader)
        {
            int length;

            string knownRequestHeader = cookieHeader;
            if (knownRequestHeader != null)
            {
                length = knownRequestHeader.Length;
            }
            else
            {
                length = 0;
            }
            int num = length;
            int num1 = 0;
            HttpCookie value = null;
            while (num1 < num)
            {
                string str = ExtractNextCookie(knownRequestHeader, num, ref num1);
                if (str.Length == 0)
                {
                    continue;
                }
                HttpCookie httpCookie = CreateCookieFromString(str);
                if (value != null)
                {
                    string name = httpCookie.Name;
                    if (name != null && name.Length > 0 && name[0] == '$')
                    {
                        if (!EqualsIgnoreCase(name, "$Path"))
                        {
                            if (!EqualsIgnoreCase(name, "$Domain"))
                            {
                                continue;
                            }
                            value.Domain = httpCookie.Value;
                            continue;
                        }
                        else
                        {
                            value.Path = httpCookie.Value;
                            continue;
                        }
                    }
                }
                cookieCollection.Add(httpCookie); //This had to change. Internal
                value = httpCookie;
            }
            return;
        }

        private static string ExtractNextCookie(string knownRequestHeader, int num, ref int num1)
        {
            int i = num1;
            for (i = num1; i < num; i++)
            {
                char chr = knownRequestHeader[i];
                if (chr == ',')
                {
                    break;
                }
            }
            string str = knownRequestHeader.Substring(num1, i - num1).Trim();
            num1 = i + 1;
            return str;
        }

        public HttpCookie CreateCookieFromString(string s)
        {
            int num;
            int length;
            HttpCookie httpCookie = new HttpCookie("");
            if (s != null)
            {
                length = s.Length;
            }
            else
            {
                length = 0;
            }
            int num1 = length;
            int num2 = 0;
            bool flag = true;
            int num3 = 1;
            while (num2 < num1)
            {
                int num4 = s.IndexOf('&', num2);
                if (num4 < 0)
                {
                    num4 = num1;
                }
                if (flag)
                {
                    num = s.IndexOf('=', num2);
                    if (num < 0 || num >= num4)
                    {
                        if (num4 == num1)
                        {
                            httpCookie.Name = s;
                            break;
                        }
                    }
                    else
                    {
                        httpCookie.Name = s.Substring(num2, num - num2);
                        num2 = num + 1;
                    }
                    flag = false;
                }
                num = s.IndexOf('=', num2);
                if (num >= 0 || num4 != num1 || num3 != 0)
                {
                    if (num < 0 || num >= num4)
                    {
                        httpCookie.Values.Add(null, s.Substring(num2, num4 - num2));
                        num3++;
                    }
                    else
                    {
                        httpCookie.Values.Add(s.Substring(num2, num - num2), s.Substring(num + 1, num4 - num - 1));
                        num3++;
                    }
                }
                else
                {
                    httpCookie.Value = s.Substring(num2, num1 - num2);
                }
                num2 = num4 + 1;
            }
            return httpCookie;
        }

        public bool EqualsIgnoreCase(string s1, string s2)
        {
            if (!string.IsNullOrEmpty(s1) || !string.IsNullOrEmpty(s2))
            {
                if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                {
                    return false;
                }
                else
                {
                    if (s2.Length == s1.Length)
                    {
                        return 0 == string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }
    }
}
