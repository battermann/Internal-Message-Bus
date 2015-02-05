using System;
using System.Linq;
using System.Threading;
using Movies.Commands;
using Movies.Contracts;
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

            bus.Register<InsertMovie>(commandHandler.Handle);
            bus.Register<ChangeTitle>(commandHandler.Handle);
            bus.Register<MovieInserted>(eventHandler.Handle);
            bus.Register<TitleChanged>(eventHandler.Handle);

            bus.Register<MovieInserted>(x => OnMovieInserted(repository));
            bus.Register<TitleChanged>(x => OnTitleChanged(repository));
            
            bus.Send(new InsertMovie { Title = "Pupl Fiction" });
            bus.Send(new InsertMovie { Title = "Kill Bill Vol. I" });

            var id = repository.GetAll().ElementAt(0).Id;
            bus.Send(new ChangeTitle { Id = id, Title = "From Dusk Till Dawn" });
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
