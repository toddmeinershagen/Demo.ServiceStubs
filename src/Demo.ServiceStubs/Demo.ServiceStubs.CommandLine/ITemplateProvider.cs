using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface ITemplateProvider
    {
        string GetContentsFor(string key, IDictionary<string, object> parameters);
    }
}