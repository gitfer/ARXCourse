using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using MyDocumentalApi.Filters;
using MyDocumentalApi.ParametersBinding;
using MyDocumentalApi.Services;
using MyDocumentalTranslations.Services;
using SDammann.WebApi.Versioning;
using SDammann.WebApi.Versioning.Configuration;
using SDammann.WebApi.Versioning.Discovery;
using SDammann.WebApi.Versioning.Documentation;
using SDammann.WebApi.Versioning.Request;
using TinyIoC;
using ValuesController = MyDocumentalApi.Controllers.Version2.ValuesController;

namespace MyDocumentalApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors(new EnableCorsAttribute(ConfigurationManager.AppSettings["webapp"], "*", "PUT, HEAD, OPTIONS, GET, POST, DELETE"));

            var dependencyContainer = new TinyIoCContainer();

            // API VERSIONING
            // git@github.com:Sebazzz/SDammann.WebApi.Versioning.git Commit: 08ec8ed339b564996743ad0fff13953fe1e95b33
            config.Services.Replace(typeof(IHttpControllerSelector), new MyVersionedApiControllerSelector(config));
            config.Services.Replace(typeof(IApiExplorer), new VersionedApiExplorer(config));
            config.DependencyResolver = new DependencyResolver(dependencyContainer);

            dependencyContainer.Register((c, np) => new DefaultControllerIdentificationDetector(config));
            dependencyContainer.Register((c, np) => new DefaultRequestControllerIdentificationDetector(config));

            RegisterServices(dependencyContainer);
            RegisterControllers(dependencyContainer);

            ApiVersioning.Configure()
                         .ConfigureRequestVersionDetector<DefaultRouteKeyVersionDetector>();

            // Web API routes
            config.MapHttpAttributeRoutes();
            ConfigParameterBindings(config);
            ConfigApiRoutes(config);
            ConfigFilters(config);

        }

        private static void ConfigParameterBindings(HttpConfiguration config)
        {
            config.ParameterBindingRules.Add(typeof(string[]),
                descriptor => new CatchAllRouteParameterBinding(descriptor, '/'));
        }

        private static void ConfigFilters(HttpConfiguration config)
        {
            config.Filters.Add(new GenericErrorAttribute());
        }

        private static void ConfigApiRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "CatchAllApi",
                routeTemplate: "api/v{version}/{controller}/{*parametri}",
                defaults: new { },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v{version}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
        }


        private static void RegisterControllers(TinyIoCContainer dependencyContainer)
        {
            dependencyContainer.Register<ValuesController>()
                .UsingConstructor(() => new ValuesController(new MyValueService(), new LanguageTranslatorServiceService()));
        }

        private static void RegisterServices(TinyIoCContainer dependencyContainer)
        {
            dependencyContainer.Register<IMyValueService, MyValueService>();
            dependencyContainer.Register<ILanguageTranslatorService, LanguageTranslatorServiceService>();
        }


        private sealed class DependencyResolver : IDependencyResolver
        {
            private readonly TinyIoCContainer _dependencyContainer;

            public DependencyResolver(TinyIoCContainer dependencyContainer)
            {
                this._dependencyContainer = dependencyContainer;
            }

            /// <summary>
            ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                // no-op
            }

            /// <summary>
            ///     Retrieves a service from the scope.
            /// </summary>
            /// <returns>
            ///     The retrieved service.
            /// </returns>
            /// <param name="serviceType">The service to be retrieved.</param>
            public object GetService(Type serviceType)
            {
                try
                {
                    return this._dependencyContainer.Resolve(serviceType);
                }
                catch (TinyIoCResolutionException ex)
                {
                    Debug.WriteLine("Exception in resolving {0}: {1}", serviceType, ex.Message);
                    return null;
                }
            }

            /// <summary>
            ///     Retrieves a collection of services from the scope.
            /// </summary>
            /// <returns>
            ///     The retrieved collection of services.
            /// </returns>
            /// <param name="serviceType">The collection of services to be retrieved.</param>
            public IEnumerable<object> GetServices(Type serviceType)
            {
                try
                {
                    return this._dependencyContainer.ResolveAll(serviceType);
                }
                catch (TinyIoCResolutionException ex)
                {
                    Debug.WriteLine("Exception in resolving {0}: {1}", serviceType, ex.Message);
                    return null;
                }
            }

            /// <summary>
            ///     Starts a resolution scope.
            /// </summary>
            /// <returns>
            ///     The dependency scope.
            /// </returns>
            public IDependencyScope BeginScope()
            {
                // no -op
                return new DependencyResolver(this._dependencyContainer);
            }
        }
    }

    public class MyVersionedApiControllerSelector : VersionedApiControllerSelector
    {
        private readonly HttpConfiguration _configuration;

        public MyVersionedApiControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
            _configuration = configuration;
        }

        protected override HttpControllerDescriptor OnSelectController(HttpRequestMessage request)
        {
            ControllerIdentification cName = this.GetControllerIdentificationFromRequest(request);
            var majorVersion = cName.Version.ToString().Split('.')[0];
            var minorVersion = cName.Version.ToString().Split('.')[1];
            var prefixNamespace = "mydocumentalapi.controllers.";
            string requestVersionMajorPlusMinor = string.Format("{0}version{1}_{2}", prefixNamespace, majorVersion, minorVersion);
            string requestVersion = string.Format("{0}version{1}", prefixNamespace, majorVersion);
            string defaultVersion = string.Format("{0}version0", prefixNamespace);

            String controllerName = cName.Name;
            Assembly assembly = Assembly.GetExecutingAssembly();


            var controllerWithExactMajorMinorVersion = assembly
                .GetTypes()
                .FirstOrDefault(i => typeof(IHttpController).IsAssignableFrom(i)
                            && i.Name.ToLower().Equals(string.Format("{0}controller", cName.Name.ToLower()))
                            && i.Namespace.ToLower().Equals(requestVersionMajorPlusMinor)
                );
            // Esiste controller con versione Major+Minor esatta
            if (controllerWithExactMajorMinorVersion != null)
            {
                return new HttpControllerDescriptor(_configuration, controllerName, controllerWithExactMajorMinorVersion);
            }
            // Se la minor è 0 cerco la major
            if (minorVersion == "0")
            {

                var controllerWithExactVersion = assembly
                    .GetTypes()
                    .FirstOrDefault(i => typeof(IHttpController).IsAssignableFrom(i)
                                && i.Name.ToLower().Equals(string.Format("{0}controller", cName.Name.ToLower()))
                                && i.Namespace.ToLower().Equals(requestVersion)
                    );
                if (controllerWithExactVersion != null)
                    return new HttpControllerDescriptor(_configuration, controllerName, controllerWithExactVersion);
            }

            Type defaultControllerType = assembly.GetTypes()
                .Where(i => typeof(IHttpController).IsAssignableFrom(i))
                .FirstOrDefault(i => i.Name.ToLower() == string.Format("{0}controller", cName.Name.ToLower())
                                     && i.Namespace.ToLower().Equals(defaultVersion));
            return new HttpControllerDescriptor(_configuration, controllerName, defaultControllerType);
        }
    }

}
