using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace DeskLanguage
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Autofac building
            var builder = new ContainerBuilder();
            builder.RegisterType<NativeWrappers.WindowsNativeWrapper>().As<NativeWrappers.INativeWrapper>().SingleInstance();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Default Route",
                routeTemplate: "{controller}"
            );

            // Only support JSON
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            config.Formatters.Clear();
            config.Formatters.Insert(0, jsonFormatter);
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Finalize the config setup
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseWebApi(config);
        }
    }
}