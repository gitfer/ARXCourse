using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace MyOwinHost
{ 
    // Note: By default all requests go through this OWIN pipeline. Alternatively you can turn this off by adding an appSetting owin:AutomaticAppStartup with value “false”. 
    // With this turned off you can still have OWIN apps listening on specific routes by adding routes in global.asax file using MapOwinPath or MapOwinRoute extensions on RouteTable.Routes
    public class Startup
    {
        // Invoked once at startup to configure your application.
        public void Configuration(IAppBuilder app)
        {
            // 1o componente della pipeline
            app.Use<MyTracingComponent>();
            // 2o componente della pipeline
            app.Use<MyAutenticationComponent>(new object[] {ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]});

            // NOTA: Il secondo metodo è invocabile anche in questo modo più "Fluent", avendo io implementato il corrispondente Extension Method
            //app.UseMyAutentication(new object[] { ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"] });

            app.Run(Invoke);
        }

        // Invoked once per request.
        public Task Invoke(IOwinContext context)
        {
            context.Response.ContentType = "text/html";

            object messageFromTracingComponent = string.Empty;

            context.Environment.TryGetValue("myDemo.MyTracingComponent",out messageFromTracingComponent);
            return context.Response.WriteAsync(string.Format("<html><body>Hello World<br /><h3>Request path {0} method {1}</h3><div>{2}</div></body></html>", context.Request.Path, context.Request.Method, messageFromTracingComponent));
        }
    }
}