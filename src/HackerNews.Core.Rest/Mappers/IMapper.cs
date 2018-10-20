using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Core.Rest.Mappers
{
    public interface IMapper<TDomain, TRest>
    {
        TDomain ToDomainEntity(TRest source);
        TRest ToRestEntity(TDomain source);
    }
}
