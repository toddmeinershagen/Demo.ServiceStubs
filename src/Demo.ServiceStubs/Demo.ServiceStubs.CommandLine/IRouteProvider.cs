using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface IRouteProvider
    {
        List<Route> GetRoutes();
    }
}