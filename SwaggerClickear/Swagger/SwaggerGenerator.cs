using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Description;

namespace SwaggerClickear.Swagger
{
    public class SwaggerGenerator :ISwaggerProvider
    {
        /// <summary>
        /// 获取 api描述接口
        /// </summary>
        private readonly IApiExplorer _apiExplorer;

        public SwaggerGenerator(IApiExplorer apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }


        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            SwaggerDocument doc = new SwaggerDocument();

            var paths = _apiExplorer.ApiDescriptions.GroupBy(a => a.RelativePath)
                .ToDictionary(group => group.Key, group => CreatePathItem(group));

            doc.paths = paths;
                        



            return doc;
        }

        private PathItem CreatePathItem(IEnumerable<ApiDescription> apiDescriptions)
        {
            var pathItem = new PathItem();
            var perMethod = apiDescriptions.GroupBy(apiDesc => apiDesc.HttpMethod.Method.ToLower());
            foreach(var group in perMethod)
            {
                var httpMethod = group.Key;
                var apiDesc = group.First();

                switch (httpMethod)
                {
                    case "get":
                        pathItem.get = CreateOperation(apiDesc);
                        break;

                    case "post":
                        pathItem.post = CreateOperation(apiDesc);
                        break;

                    case "put":
                        pathItem.put = CreateOperation(apiDesc);
                        break;

                    case "delete":
                        pathItem.delete = CreateOperation(apiDesc);
                        break;
                    default:
                        break;
                }
            }
            return pathItem;       
        }

        private Operation CreateOperation(ApiDescription apiDesc)
        {
         //   Parameter

            var param = apiDesc.ParameterDescriptions.Select(paramDesc =>
                    {
                        string location = GetParameterLocation(apiDesc, paramDesc);
                        return CreateParameter(location, paramDesc);
                    })
                 .ToList();

            

            var operation = new Operation
            {
             
                produces = apiDesc.Produces().ToList(),
                consumes = apiDesc.Consumes().ToList(),
                operationId = apiDesc.GetFriendlyId(),
                parameters = param,
                deprecated = apiDesc.IsObsolete()?true:false
            };
            return operation;
        }

        private Parameter CreateParameter(string location, ApiParameterDescription paramDesc)
        {
            var parameter = new Parameter
            {
                @in = location,
                name = paramDesc.Name
            };

            if (paramDesc.ParameterDescriptor == null)
            {
                parameter.type = "string";
                parameter.required = true;
                return parameter;
            }

            parameter.required = location == "path" || !paramDesc.ParameterDescriptor.IsOptional;
            parameter.@default = paramDesc.ParameterDescriptor.DefaultValue;

           

            return parameter;
        }

        private string GetParameterLocation(ApiDescription apiDesc, ApiParameterDescription paramDesc)
        {
            if (apiDesc.RelativePathSansQueryString().Contains("{" + paramDesc.Name + "}"))
                return "path";
            else if (paramDesc.Source == ApiParameterSource.FromBody && apiDesc.HttpMethod != HttpMethod.Get)
                return "body";
            else
                return "query";
        }


    }
}