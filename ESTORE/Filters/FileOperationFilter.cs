using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ESTORE.Filters
{
    public class FileOperationFilter : IOperationFilter
    {
 
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var formFileParams = context.MethodInfo.GetParameters()
                    .Where(p => p.ParameterType == typeof(IFormFile))
                    .ToList();

                if (!formFileParams.Any()) return;

                // Remove IFormFile parameters from the operation's parameters
                foreach (var param in formFileParams)
                {
                    var parameter = operation.Parameters.FirstOrDefault(p => p.Name == param.Name);
                    if (parameter != null)
                    {
                        operation.Parameters.Remove(parameter);
                    }
                }

                // Initialize properties dictionary with the file property
                var properties = new Dictionary<string, OpenApiSchema>
                {
                    ["file"] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    }
                };

                // Get additional parameters that are class types and add their properties to the schema
                var additionalParams = context.MethodInfo.GetParameters()
                    .Where(p => p.ParameterType != typeof(IFormFile) && p.ParameterType.IsClass)
                    .SelectMany(p => p.ParameterType.GetProperties())
                    .ToList();

                // Add each property ensuring unique keys
                foreach (var param in additionalParams)
                {
                    if (!properties.ContainsKey(param.Name))
                    {
                        properties.Add(param.Name, new OpenApiSchema { Type = "string" });
                    }
                }

                // Create the request body with the collected properties
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = properties,
                                Required = new HashSet<string> { "file" }
                            }
                        }
                    }
                };
            }
        }
    }
