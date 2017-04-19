using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwaggerClickear.Swagger
{
    public interface ISwaggerProvider
    {
        /// <summary>
        /// 获取swagger的容器
        /// </summary>
        /// <param name="rootUrl">root</param>
        /// <param name="apiVersion">版本</param>
        /// <returns></returns>
        SwaggerDocument GetSwagger(string rootUrl, string apiVersion);
    }



}
