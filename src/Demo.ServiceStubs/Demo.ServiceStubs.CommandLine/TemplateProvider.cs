using System;
using System.Collections.Generic;
using System.IO;

namespace Demo.ServiceStubs.CommandLine
{
    public class TemplateProvider : ITemplateProvider
    {
        public string GetContentsFor(string key, IDictionary<string, object> parameters)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, key);
            return File.ReadAllText(filePath);
        }
    }
}