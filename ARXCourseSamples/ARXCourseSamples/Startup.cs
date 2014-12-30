using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ARXCourseSamples.Startup))]
namespace ARXCourseSamples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
