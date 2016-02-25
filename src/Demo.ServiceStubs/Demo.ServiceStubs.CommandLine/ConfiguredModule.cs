using System;
using System.Diagnostics;
using System.Threading;

using Nancy;

namespace Demo.ServiceStubs.CommandLine
{
    public class ConfiguredModule : NancyModule
    {
        private readonly ITokenPoker _poker;
        private readonly ITemplateProvider _templateProvider;
        private readonly ITemplateEngine _engine;

        public ConfiguredModule(IRouteProvider provider, ITokenPoker poker, ITemplateProvider templateProvider, ITemplateEngine engine)
        {
            _poker = poker;
            _templateProvider = templateProvider;
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

            var parameters = Context.GetParameters();
            var path = route.Path;
            path = _poker.PokeData(path, parameters);

            var template = _templateProvider.GetContentsFor(path, parameters);
            Response response = _engine.Parse(route.Template, template, parameters);
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
