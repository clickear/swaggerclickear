﻿using SwaggerClickear.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SwaggerClickear
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            // name: "swaggerdoc",
            // routeTemplate: "swagger/docs/{apiVersion}",
            // defaults: null,
            // constraints: new { apiVersion = @".+" },
            // handler: new SwaggerDocHandle()
            // );

            // 取消注释下面的代码行可对具有 IQueryable 或 IQueryable<T> 返回类型的操作启用查询支持。
            // 若要避免处理意外查询或恶意查询，请使用 QueryableAttribute 上的验证设置来验证传入查询。
            // 有关详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=279712。
            //config.EnableQuerySupport();

            // 若要在应用程序中禁用跟踪，请注释掉或删除以下代码行
            // 有关详细信息，请参阅: http://www.asp.net/web-api
           // config.EnableSystemDiagnosticsTracing();
        }
    }
}