using System.Threading;
using Movies.Commands;
using Movies.Contracts;
using Movies.Events;
using Movies.Infrastructure;

namespace Movies
{
    public class CommandHandlers
    {
        private readonly IEventPublisher _bus;

        public CommandHandlers(IEventPublisher bus)
        {
            _bus = bus;
        }

        public void Handle(InsertMovie msg)
        {
            var @event = new MovieInserted
            {
                Title = msg.Title
            };

            _bus.Publish(@event);
        }

        public void Handle(ChangeTitle msg)
        {
            var @event = new TitleChanged
            {
                Id = msg.Id,
                Title = msg.Title
            };

            _bus.Publish(@event);
        }
    }
}
