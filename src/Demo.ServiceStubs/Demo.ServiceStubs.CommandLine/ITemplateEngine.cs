using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface ITemplateEngine
    {
        string Parse(string templateKey, string template, IDictionary<string, object> model);
    }
}
