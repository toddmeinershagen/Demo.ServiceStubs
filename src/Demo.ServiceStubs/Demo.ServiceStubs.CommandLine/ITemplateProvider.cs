using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface ITemplateProvider
    {
        string GetContentsFor(string templateKey, IDictionary<string, object> parameters);
    }
}