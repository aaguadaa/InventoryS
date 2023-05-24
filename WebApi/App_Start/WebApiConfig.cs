using Business.Contracts;
using Business.Implementation;
using Business.Services;
using Data;
using Data.Contracts;
using Data.Implementation;
using Data.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles; // Agregar esta referencia
using System.Web.Http;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new Container();

            // Set the default scoped lifestyle
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register services and repositories
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IInventoryService, InventoryService>(Lifestyle.Scoped);
            container.Register<IInventoryRepository, InventoryRepository>(Lifestyle.Scoped);
            container.Register<IProductService, ProductService>(Lifestyle.Scoped);
            container.Register<IProductRepository, ProductRepository>(Lifestyle.Scoped);
            container.Register<ICheckService, CheckService>(Lifestyle.Scoped);
            container.Register<ICheckRepository, CheckRepository>(Lifestyle.Scoped);
            container.Register<IAccountService, AccountService>(Lifestyle.Scoped);
            container.Register<IAccountRepository, AccountRepository>(Lifestyle.Scoped);
            container.Register<InventoryStevDBContext>(Lifestyle.Scoped);

            container.Verify();

            // Set the dependency resolver for Web API
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}