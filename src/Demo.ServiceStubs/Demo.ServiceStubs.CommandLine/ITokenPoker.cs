using System.Collections.Generic;

namespace Demo.ServiceStubs.CommandLine
{
    public interface ITokenPoker
    {
        string PokeData(string value, IDictionary<string, object> data);
    }
}