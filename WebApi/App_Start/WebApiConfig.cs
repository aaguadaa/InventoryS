using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using Business.Contracts;
using Business.Implementation;
using Data.Contracts;
using Data.Implementation;
using Business.Services;
using Data.Repositories;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // SimpleInjector configuration
            var container = new SimpleInjector.Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register services and repositories
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IInventoryService, InventoryService>(Lifestyle.Scoped);
            container.Register<IInventoryRepository, InventoryRepository>(Lifestyle.Scoped);
            container.Register<IProductService, ProductService>(Lifestyle.Scoped);
            container.Register<IProductRepository, ProductRepository>(Lifestyle.Scoped);

            // Verify the container's configuration
            container.Verify();

            // Set the dependency resolver for Web API to use SimpleInjector
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Web API configuration and routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}