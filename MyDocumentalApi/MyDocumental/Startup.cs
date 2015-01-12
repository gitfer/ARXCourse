using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyDocumental.Startup))]
namespace MyDocumental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
