using System;
using HackerNews.Infrastructure.Framework.Globalization;

namespace HackerNews.Infrastructure.Framework.Exceptions
{
    public class GenericException : Exception
    {
        public GenericException() : base(Texts.GenericError)
        {
        }
    }
}
