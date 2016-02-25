using System;
using System.Collections.Generic;

using Antlr4.StringTemplate;

namespace Demo.ServiceStubs.CommandLine.St
{
    public class StringTemplateEngine : ITemplateEngine
    {
        public string Parse(string templateKey, string template, IDictionary<string, object> model)
        {
            var group = new TemplateGroupString("group", "delimiters \"$\", \"$\"\r\nt(x) ::= \" $ x $ \"");

            var renderer = new AdvancedRenderer();
            group.RegisterRenderer(typeof(DateTimeOffset), renderer);
            group.RegisterRenderer(typeof(DateTime), renderer);
            group.RegisterRenderer(typeof(double), renderer);
			group.RegisterRenderer(typeof(decimal), renderer);

            group.DefineTemplate("template", template, new[] { "Model" });

            var stringTemplate = group.GetInstanceOf("template");
            stringTemplate.Add("Model", model);

            return stringTemplate.Render();
        }
    }
}
