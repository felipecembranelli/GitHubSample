using GitHubSample.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GitHubSample.Data.Repository;
using GitHubSample.Services;
using System.ComponentModel;
using GitHubSample.Services.IServices;
using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Http;

namespace GiHubSample.Web.App_Start
{
    public static class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            // Configure AutoFac (IOC)

            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());               // mvc
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());            // web api
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            // Inject Repositories
            builder.RegisterAssemblyTypes(typeof(GitHubRepoRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerRequest();

            // Inject Services
            builder.RegisterType<GitHubRepoService>().As<IGitHubRepoService>().InstancePerRequest();

            Autofac.IContainer container = builder.Build();


            // Dependency resolver for WebAPI
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Dependency resolver for MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
