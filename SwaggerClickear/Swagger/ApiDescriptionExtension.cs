using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SwaggerClickear.Swagger
{
    public static class ApiDescriptionExtension
    {
        public static IEnumerable<string> Produces(this ApiDescription apiDesc)
        {
            return apiDesc.SupportedResponseFormatters
                          .SelectMany(formatter => formatter.SupportedMediaTypes.Select(mediaType => mediaType.MediaType))
                          .Distinct();
        }

        public static IEnumerable<string> Consumes(this ApiDescription apiDesc)
        {
            return apiDesc.SupportedRequestBodyFormatters
                           .SelectMany(formatter => formatter.SupportedMediaTypes.Select(mediaType => mediaType.MediaType))
                           .Distinct();
        }

        public static string GetFriendlyId(this ApiDescription apiDesc)
        {
            return String.Format("{0}_{1}", apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName, apiDesc.ActionDescriptor.ActionName);
        }

        public static string RelativePathSansQueryString(this ApiDescription apiDesc)
        {
            return apiDesc.RelativePath.Split('?').First();
        }

        public static bool IsObsolete(this ApiDescription apiDescription)
        {
            return apiDescription.ActionDescriptor.GetCustomAttributes<ObsoleteAttribute>().Any();
        }

    }
}