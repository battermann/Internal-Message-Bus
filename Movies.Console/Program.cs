using System;
using System.Linq;
using Movies.Commands;
using Movies.Contracts;
using Movies.Data;
using Movies.Events;
using Movies.Infrastructure;

namespace Movies.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new InMemoryMovieRepository();
            var bus = MessageBus.GetInstance;

            var commandHandler = new CommandHandlers(bus);
            var eventHandler = new EventHandlers(repository);

            bus.Register<CreateMovie>(commandHandler.Handle);
            bus.Register<ChangeMovieTitle>(commandHandler.Handle);
            bus.Register<MovieCreated>(eventHandler.Handle);
            bus.Register<MovieTitleChanged>(eventHandler.Handle);

            bus.Register<MovieCreated>(x => OnMovieInserted(repository));
            bus.Register<MovieTitleChanged>(x => OnTitleChanged(repository));

            bus.Send(new CreateMovie(Guid.NewGuid(), "Pupl Fiction", new DateTime(1994, 1, 1), "Crime", 8.5m));

            bus.Send(new CreateMovie(Guid.NewGuid(), "From Dusk Till Dawn", new DateTime(2003, 1,1), "Action", 8.99m));

            var id = repository.GetAll().ElementAt(1).Id;
            bus.Send(new ChangeMovieTitle (id, "Kill Bill Vol. I" ));
        }

        private static void OnTitleChanged(IMovieQueryFacade movieRepository)
        {
            Console.Clear();
            Console.WriteLine("MOVIE TITLE CHANGED:");
            movieRepository.GetAll().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("press a <ENTER>...");
            Console.ReadLine();
        }

        private static void OnMovieInserted(IMovieQueryFacade movieRepository)
        {
            Console.Clear();
            Console.WriteLine("MOVIE INSERTED:");
            movieRepository.GetAll().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("press a <ENTER>...");
            Console.ReadLine();
        }
    }
}
