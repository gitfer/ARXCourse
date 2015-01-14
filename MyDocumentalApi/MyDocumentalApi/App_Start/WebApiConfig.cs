using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using MyDocumentalApi.Controllers.Version0;
using MyDocumentalApi.Filters;
using MyDocumentalApi.ParametersBinding;
using MyDocumentalApi.Services;
using MyDocumentalTranslations;
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
            config.Services.Replace(typeof(IHttpControllerSelector), new VersionedApiControllerSelector(config));
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
            config.ParameterBindingRules.Add(typeof (string[]),
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
                defaults: new {id = RouteParameter.Optional}
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

    //public class MyDefaultVersionDetector : RouteKeyVersionDetector, IRequestVersionDetector 
    //{
    //    private const string DefaultRouteKey = "version";

    //    public ApiVersion GetVersion(HttpRequestMessage requestMessage)
    //    {
    //        if (requestMessage == null)
    //        {
    //            throw new ArgumentNullException("requestMessage");
    //        }

    //        IHttpRouteData routeData = requestMessage.GetRouteData();
    //        if (routeData == null)
    //        {
    //            return default(ApiVersion);
    //        }

    //        ApiVersion apiVersion = this.GetVersion(requestMessage);
    //        return new SemVerApiVersion(new Version("0.0"));
    //        return this.GetControllerVersionFromRouteData(routeData);
    //    }

    //    protected override string RouteKey
    //    {
    //        get { return DefaultRouteKey; }
    //    }
    //}

    //public class MyVersionedApiControllerSelector : VersionedApiControllerSelector
    //{
    //    private readonly HttpConfiguration _configuration;

    //    public MyVersionedApiControllerSelector(HttpConfiguration configuration) : base(configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    protected override HttpControllerDescriptor OnSelectController(HttpRequestMessage request)
    //    {
    //        HttpControllerDescriptor controller;
    //        try
    //        {
    //            controller = base.SelectController(request);
    //        }
    //        catch (Exception ex)
    //        {

    //            ControllerIdentification cName = this.GetControllerIdentificationFromRequest(request);
    //            String controllerName = cName.Name;
    //            Assembly assembly = Assembly.LoadFile(String.Format("{0}\\{1}.dll", HostingEnvironment.ApplicationPhysicalPath, controllerName));
    //            Type controllerType = assembly.GetTypes()
    //              .Where(i => typeof(IHttpController).IsAssignableFrom(i))
    //              .FirstOrDefault(i => i.Name.ToLower() == controllerName.ToLower() + "controller");
    //            controller = new HttpControllerDescriptor(_configuration, controllerName, controllerType);
    //        }
    //        return controller;
    //    }

    //}


}
