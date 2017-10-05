using FionaFrontoffice_V3.Api.Query;
using System.Collections.Generic;

namespace FionaFrontoffice_V3.Api
{
    public interface ISearchCustomerView
    {
        IEnumerable<CustomerActualResidenceView> CoolSearchEngine { get; }
    }
}