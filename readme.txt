Comma Delimited Cookie Parser for ASP.NET
Author: Keyvan Nayyeri
Blog: http://keyvan.io
Podcast: http://keyvan.fm
Twitter: http://twitter.com/keyvan
Contact Info: http://keyvan.tel
******************************************************************
ASP.NET does not parse cookies with comma as their delimiter by
 default, but the use of comma's is valid according to RFC2309.
This HttpModule simulates ASP.NET cookie parser to parse cookies
 with comma as their delimiter.
You can use this code at your own risk. Do not forget that this
 module does not work if comma is used in the value of cookies.

The implementaiton is described in details on my blog:


NuGet Package:

http://nuget.org/packages/CommaDelimitedCookieParser
