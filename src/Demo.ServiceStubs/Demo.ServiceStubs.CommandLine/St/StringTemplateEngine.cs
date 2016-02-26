using System;
using System.Collections.Generic;

using Antlr4.StringTemplate;

namespace Demo.ServiceStubs.CommandLine.St
{
    public class StringTemplateEngine : ITemplateEngine
    {
        private readonly ITemplateProvider _templateProvider;

        public StringTemplateEngine(ITemplateProvider templateProvider)
        {
            _templateProvider = templateProvider;
        }

        public string Parse(string templateKey, IDictionary<string, object> model)
        {
            var group = new TemplateGroupString("group", "delimiters \"$\", \"$\"\r\nt(x) ::= \" $ x $ \"");

            var renderer = new AdvancedRenderer();
            group.RegisterRenderer(typeof(DateTimeOffset), renderer);
            group.RegisterRenderer(typeof(DateTime), renderer);
            group.RegisterRenderer(typeof(double), renderer);
			group.RegisterRenderer(typeof(decimal), renderer);

            var templateContent = _templateProvider.GetContentsFor(templateKey, model);
            group.DefineTemplate("template", templateContent, new[] { "Model" });

            var stringTemplate = group.GetInstanceOf("template");
            stringTemplate.Add("Model", model);

            return stringTemplate.Render();
        }
    }
}
