using System;
using System.Linq;
using Movies.Commands;
using Movies.Data;
using Movies.Events;
using Movies.Infrastructure;

namespace Movies.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new InMemoryRepository();
            var bus = MessageBus.GetInstance;

            var commandHandler = new CommandHandlers(bus);
            var eventHandler = new EventHandlers(repository);

            bus.Register<CreateMovie>(commandHandler.Handle);
            bus.Register<UpdateMovie>(commandHandler.Handle);
            bus.Register<MovieCreated>(eventHandler.Handle);
            bus.Register<MovieUpdated>(eventHandler.Handle);

            bus.Register<MovieCreated>(x => OnMovieInserted(repository));
            bus.Register<MovieUpdated>(x => OnTitleChanged(repository));

            bus.Send(new CreateMovie
            {
                Title = "Pupl Fiction",
                Genre = "Crime",
                ReleaseDate = new DateTime(1994, 1, 1),
                Price = 8.5m
            });

            bus.Send(new CreateMovie
            {
                Title = "From Dusk Till Dawn",
                Genre = "Action",
                ReleaseDate = new DateTime(2003, 1, 1),
                Price = 5m
            });

            var id = repository.GetAll().ElementAt(1).Id;
            bus.Send(new UpdateMovie { Id = id, Title = "Kill Bill Vol. I" });
        }

        private static void OnTitleChanged(IRepository repository)
        {
            Console.Clear();
            Console.WriteLine("MOVIE TITLE CHANGED:");
            repository.GetAll().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("press a <ENTER>...");
            Console.ReadLine();
        }

        private static void OnMovieInserted(IRepository repository)
        {
            Console.Clear();
            Console.WriteLine("MOVIE INSERTED:");
            repository.GetAll().ToList().ForEach(Console.WriteLine);
            Console.WriteLine("press a <ENTER>...");
            Console.ReadLine();
        }
    }
}
