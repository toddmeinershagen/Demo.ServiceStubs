using System.Collections.Generic;

namespace Demo.ServiceStubs.Core
{
    public interface ITemplateProvider
    {
        string GetContentsFor(string templateKey, IDictionary<string, object> parameters);
    }
}