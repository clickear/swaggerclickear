using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SwaggerClickear.Swagger
{
    public class SwaggerDocHandle : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {

            var defaultProvider = new SwaggerGenerator(request.GetConfiguration().Services.GetApiExplorer());

            var doc = defaultProvider.GetSwagger("", "");

            var content = ContentFor(request, doc);
          
            return TaskFor(new HttpResponseMessage { Content = content });            
        }

        private HttpContent ContentFor(HttpRequestMessage request, SwaggerDocument swaggerDoc)
        {
            return new ObjectContent(swaggerDoc.GetType(), swaggerDoc, new JsonMediaTypeFormatter());           
        }

        private Task<HttpResponseMessage> TaskFor(HttpResponseMessage response)
        {
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

    }
}