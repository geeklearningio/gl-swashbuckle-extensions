﻿namespace GeekLearning.SwashbuckleExtensions
{
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Linq;

    public class DropTextPlainConsumes : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Consumes.Count > 0)
            {
                var textPlain = operation.Consumes
                    .FirstOrDefault(consume => consume.Equals("text/plain", StringComparison.OrdinalIgnoreCase));

                if (textPlain != null)
                {
                    operation.Consumes.Remove(textPlain);
                }
            }
        }
    }
}
