using System;
using HackerNews.Infrastructure.Framework.Globalization;

namespace HackerNews.Infrastructure.Framework.Exceptions
{
    public class NoInternetConnectionException : Exception
    {
        public NoInternetConnectionException() :base(Texts.NoInternetConnection)
        {

        }
    }
}
