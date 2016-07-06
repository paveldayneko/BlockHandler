using System;

namespace BlocksHandler
{
    public static class Constants
    {
        public const string HttpResponseJsonContentType = "application/json";
        public const int InternalServerErrorCode = 500;

        public static readonly string InputXmlFileUri = string.Format("{0}/Pg001.xml", AppDomain.CurrentDomain.BaseDirectory);
    }
}