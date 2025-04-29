using System;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TwitterUalaChallenge.API.Middlewares;

public class SwaggerDefaultValues : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        ApiDescription apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();
        foreach (ApiResponseType supportedResponseType in context.ApiDescription.SupportedResponseTypes)
        {
            string key = (supportedResponseType.IsDefaultResponse
                ? "default"
                : supportedResponseType.StatusCode.ToString());
            OpenApiResponse openApiResponse = operation.Responses[key];
            foreach (string contentType in openApiResponse.Content.Keys)
            {
                if (!supportedResponseType.ApiResponseFormats.Any((ApiResponseFormat x) => x.MediaType == contentType))
                {
                    openApiResponse.Content.Remove(contentType);
                }
            }
        }

        if (operation.Parameters == null)
        {
            return;
        }

        foreach (OpenApiParameter parameter in operation.Parameters)
        {
            ApiParameterDescription apiParameterDescription =
                apiDescription.ParameterDescriptions.First((ApiParameterDescription p) => p.Name == parameter.Name);
            if (parameter.Description == null)
            {
                parameter.Description = apiParameterDescription.ModelMetadata?.Description;
            }

            if (parameter.Schema.Default == null && apiParameterDescription.DefaultValue != null &&
                !(apiParameterDescription.DefaultValue is DBNull))
            {
                ModelMetadata modelMetadata = apiParameterDescription.ModelMetadata;
                if (modelMetadata != null)
                {
                    string json =
                        JsonSerializer.Serialize(apiParameterDescription.DefaultValue, modelMetadata.ModelType);
                    parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                }
            }

            parameter.Required |= apiParameterDescription.IsRequired;
        }
    }
}