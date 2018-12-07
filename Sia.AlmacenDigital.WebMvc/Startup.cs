using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sia.AlmacenDigital.WebMvc.Startup))]
namespace Sia.AlmacenDigital.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
