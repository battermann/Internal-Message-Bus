using System;
using System.Windows;
using Movies.Commands;
using Movies.Data;
using Movies.Events;
using Movies.Infrastructure;

namespace Movies.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Start();
        }

        private static void Start()
        {
            var bus = MessageBus.GetInstance;
            var repository = new InMemoryMovieRepository();

            var commandHandlers = new CommandHandlers(bus);
            bus.Register<CreateMovie>(commandHandlers.Handle);
            bus.Register<ChangeMovieTitle>(commandHandlers.Handle);

            var eventHandlers = new EventHandlers(repository);
            bus.Register<MovieTitleChanged>(eventHandlers.Handle);
            bus.Register<MovieCreated>(eventHandlers.Handle);

            bus.Send(new CreateMovie(Guid.NewGuid(), "Pupl Fiction", new DateTime(1994, 1, 1), "Crime", 8.5m));

            bus.Send(new CreateMovie(Guid.NewGuid(), "From Dusk Till Dawn", new DateTime(2003, 1, 1), "Action", 8.99m));

            var mainWindow = new MainWindow {DataContext = new MovieListViewModel(bus, repository)};
            mainWindow.ShowDialog();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(1);
        }
    }
}
