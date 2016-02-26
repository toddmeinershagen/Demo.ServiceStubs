using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface ITemplateEngine
    {
        string Parse(string templateKey, IDictionary<string, object> model);
    }
}
