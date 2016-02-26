using System.Collections.Generic;

namespace Demo.ServiceStubs.Core
{
    public interface ITokenPoker
    {
        string PokeData(string value, IDictionary<string, object> data);
    }
}