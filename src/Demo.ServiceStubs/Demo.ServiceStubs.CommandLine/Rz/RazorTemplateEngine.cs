﻿using System.Collections.Generic;

using RazorEngine;
using RazorEngine.Templating;

namespace Demo.ServiceStubs.CommandLine.Rz
{
    //NOTE:  https://github.com/Antaris/RazorEngine#temporary-files
    public class RazorTemplateEngine : ITemplateEngine
    {
        private readonly ITokenPoker _tokenPoker;
        private readonly ITemplateProvider _templateProvider;

        public RazorTemplateEngine(ITokenPoker tokenPoker, ITemplateProvider templateProvider)
        {
            _tokenPoker = tokenPoker;
            _templateProvider = templateProvider;
        }

        public string Parse(string templateKey, IDictionary<string, object> model)
        {
            templateKey = _tokenPoker.PokeData(templateKey, model);

            var innerKey = new TemplateKey(templateKey);
            var modelType = typeof(MassiveExpando);

            if (Engine.Razor.IsTemplateCached(innerKey, modelType))
            {
                return Engine.Razor.Run(innerKey, modelType, model.ToExpando());
            }

            var templateContents = _templateProvider.GetContentsFor(templateKey, model);
            return Engine.Razor.RunCompile(templateContents, templateKey, modelType, model.ToExpando());
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
