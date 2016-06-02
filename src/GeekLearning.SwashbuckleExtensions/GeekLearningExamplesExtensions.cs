﻿using Swashbuckle.SwaggerGen;
using Swashbuckle.SwaggerGen.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekLearning.SwashbuckleExtensions
{
    public static class GeekLearningExamplesExtensions
    {
        public static void UseExamples(this SwaggerGenOptions documentOptions, Action<ExamplesBuilder> examplesBuilder)
        {
            var examples = new ExamplesBuilder();
            examplesBuilder(examples);
            documentOptions.OperationFilter<AssignExamples>(examples);
        }

        public static void UseSchemes(this SwaggerGenOptions documentOptions, Action<ISet<string>> schemesBuilder)
        {
            var schemes = new HashSet<string>();
            schemesBuilder(schemes);
            documentOptions.DocumentFilter<SchemesDocumentFilter>(schemes);
        }
    }
}
