using System;

using Nancy.Bootstrapper;
using Nancy.Hosting.Self;

namespace Demo.ServiceStubs.Core
{
    public class ServiceStubsHost : NancyHost, IDisposable
    {
        public ServiceStubsHost(Uri baseUri)
            : this(baseUri, new ServiceStubsBootstrapper())
        {}

        public ServiceStubsHost(Uri baseUri, INancyBootstrapper bootstrapper)
            : base(baseUri, bootstrapper)
        {}

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}