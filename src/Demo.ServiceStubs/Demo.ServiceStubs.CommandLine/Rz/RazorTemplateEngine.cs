using System;
using System.Collections.Generic;

using RazorEngine;
using RazorEngine.Templating;

namespace Demo.ServiceStubs.CommandLine.Rz
{
    //NOTE:  https://github.com/Antaris/RazorEngine#temporary-files
    public class RazorTemplateEngine : ITemplateEngine
    {
        public string Parse(string templateKey, string template, IDictionary<string, object> model)
        {
            var innerKey = new TemplateKey(templateKey);
            Type modelType = typeof(MassiveExpando);

            if (Engine.Razor.IsTemplateCached(innerKey, modelType))
            {
                return Engine.Razor.Run(innerKey, modelType, model.ToExpando());
            }
            else
            {
                return Engine.Razor.RunCompile(template, templateKey, modelType, model.ToExpando());
            }
            
        }
    }

    public class TemplateKey : ITemplateKey
    {
        public TemplateKey(string templateKey)
        {
            Name = templateKey;
            TemplateType = ResolveType.Global;
            Context = this;
        }
        public string GetUniqueKeyString()
        {
            return Name;
        }

        public string Name { get; }
        public ResolveType TemplateType { get; }
        public ITemplateKey Context { get; }
    }
}
