using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SwaggerClickear.Swagger;
using System.Net.Http;

namespace SwaggerClickear
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HttpConfiguration httpConfig = GlobalConfiguration.Configuration;

            httpConfig.Routes.MapHttpRoute(
            name: "swaggerdoc",
            routeTemplate: "swagger/docs/{apiVersion}",
            defaults: null,
            constraints: new { apiVersion = @".+" },
            handler: new SwaggerDocHandle()
            );


            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(httpConfig);


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);



            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //var route = httpConfig.Routes.MapHttpRoute(
            //    name:"swaggerdoc",
            //    routeTemplate: "swagger/docs/{apiVersion}",
            //    defaults:null,
            //    constraints:  new { apiVersion = @".+" },
            //    handler: new SwaggerDocHandle()
            //    );

            //RouteTable.Routes.MapHttpRoute().RouteHandler;




            httpConfig.Services.GetApiExplorer();

            
        }
    }
}