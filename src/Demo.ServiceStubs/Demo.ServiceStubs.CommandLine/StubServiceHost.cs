using System;
using System.Collections.Generic;

using Demo.ServiceStubs.CommandLine.Rz;

using Nancy;
using Nancy.Bootstrapper;
using Nancy.ErrorHandling;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;

namespace Demo.ServiceStubs.CommandLine
{
    public class StubServiceHost : NancyHost
    {
        public StubServiceHost(Uri baseUri)
            : base(baseUri, new StubServiceBootstrapper())
        {}
    }

    public class StubServiceBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<ITemplateEngine, RazorTemplateEngine>().AsMultiInstance();
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(x =>
                {
                    x.StatusCodeHandlers = new List<Type>
                    {
                        typeof (NotFoundErrorHandler),
                        typeof (DefaultStatusCodeHandler)
                    };
                });
            }
        }
    }
}