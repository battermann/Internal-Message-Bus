using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Movies.Commands;
using Movies.Data;
using Movies.Events;
using Movies.Infrastructure;
using Movies.Web.Controllers;

namespace Movies.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureAutofac();
        }

        private static void ConfigureAutofac()
        {
            var repository = new InMemoryMovieRepository();
            var bus = new MessageBus();

            bus.Register<CreateMovie>(x => CommandHandlers.Handle(() => bus, x));
            bus.Register<ChangeMovieTitle>(x => CommandHandlers.Handle(() => bus, x));

            bus.Register<MovieCreated>(x => EventHandlers.Handle(() => repository, x));
            bus.Register<MovieTitleChanged>(x => EventHandlers.Handle(() => repository, x));

            bus.Send(new CreateMovie(Guid.NewGuid(), "Pupl Fiction", new DateTime(1994, 1, 1), "Crime", 8.5m));

            bus.Send(new CreateMovie(Guid.NewGuid(), "From Dusk Till Dawn", new DateTime(2003, 1, 1), "Action", 8.99m));

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof (MvcApplication).Assembly);

            builder.RegisterType<MoviesController>()
                .WithParameter("commandSender", bus)
                .WithParameter("movieQueryFacade", repository);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
