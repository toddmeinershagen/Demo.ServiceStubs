using System.Collections.Generic;

namespace Demo.ServiceStubs.Core
{
    public interface ITemplateEngine
    {
        string Parse(string templateKey, IDictionary<string, object> model);
    }
}
