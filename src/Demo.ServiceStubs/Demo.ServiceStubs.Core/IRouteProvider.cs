using System.Collections.Generic;

namespace Demo.ServiceStubs.Core
{
    public interface IRouteProvider
    {
        List<Route> GetRoutes();
    }
}