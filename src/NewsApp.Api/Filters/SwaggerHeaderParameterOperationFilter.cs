using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NewsApp.Api.Filters
{
    public class SwaggerHeaderParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="operation">Operation</param>
        /// <param name="context">OperationFilterContext</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "LanguageCode",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "AppId",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                },
                Description = "Bearer Token For Authorization",
                Name = "Bearer"
            });

        }
    }
}
