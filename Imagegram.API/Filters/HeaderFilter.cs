using Imagegram.Models.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace Imagegram.API.Filters
{
    public class HeadersParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (operation.OperationId == HeaderRequiredRoutes.Posts.ToString() ||
                operation.OperationId == HeaderRequiredRoutes.Comments.ToString())
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "If-None-Match",
                    Description = "Check if resource was modified",
                    In = ParameterLocation.Header,
                    Required = true
                });
            }

            if (operation.OperationId == HeaderRequiredRoutes.CreatePost.ToString() ||
                operation.OperationId == HeaderRequiredRoutes.Comment.ToString())
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "X-Account-Id",
                    Description = "Account id to authorize with",
                    In = ParameterLocation.Header,
                    Required = true,
                    Example = new OpenApiString(Guid.Empty.ToString())
                });
            }
        }
    }
}
