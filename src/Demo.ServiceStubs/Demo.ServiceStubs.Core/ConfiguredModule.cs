using System;
using System.Diagnostics;
using System.Threading;

using Nancy;

namespace Demo.ServiceStubs.Core
{
    public class ConfiguredModule : NancyModule
    {
        private readonly ITemplateEngine _engine;

        public ConfiguredModule(IRouteProvider provider, ITemplateEngine engine)
        {
            _engine = engine;

            var routes = provider.GetRoutes();

            foreach (var route in routes)
            {
                switch (route.Type)
                {
                    case RequestType.Get:
                        Get[route.Template] = _ =>
                        {   
                            Func<Response> function = () => GetResponse(route);
                            return ExecuteWithDelay(function, route.CurrentDelayInMilliseconds);
                        };
                        break;
                    case RequestType.Post:
                        Post[route.Template] = _ =>
                        {
                            Func<Response> function = () => GetResponse(route);
                            return ExecuteWithDelay(function, route.CurrentDelayInMilliseconds);
                        };
                        break;
                    case RequestType.Put:
                        Put[route.Template] = _ =>
                        {
                            Func<Response> function = () => GetResponse(route);
                            return ExecuteWithDelay(function, route.CurrentDelayInMilliseconds);
                        };
                        break;
                    case RequestType.Delete:
                        Put[route.Template] = _ =>
                        {
                            Func<Response> function = () => GetResponse(route);
                            return ExecuteWithDelay(function, route.CurrentDelayInMilliseconds);
                        };
                        break;
                    default:
                        break;
                }
            }
        }

        private Response GetResponse(Route route)
        {
            Console.WriteLine($"SUCCESS:  {Context.ResolvedRoute.Description.Path}");

            Response response = _engine.Parse(route.Path, Context.GetParameters());
            response.WithStatusCode(route.Status);

            return response;
        }

        private Response ExecuteWithDelay(Func<Response> function, int delayInMilliseconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = function();

            stopwatch.Stop();
            var left = TimeSpan.FromMilliseconds(delayInMilliseconds) - stopwatch.Elapsed;
            Thread.Sleep(left > TimeSpan.Zero ? left : TimeSpan.Zero);

            return response;
        }
    }
}
