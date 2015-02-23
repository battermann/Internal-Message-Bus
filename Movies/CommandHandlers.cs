using System;
using Movies.Commands;
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

        public void Handle(CreateMovie msg)
        {
            // Todo: Validation

            var @event = new MovieCreated(Guid.NewGuid(), msg.Title, msg.ReleaseDate, msg.Genre, msg.Price);

            _bus.Publish(@event);
        }

        public void Handle(ChangeMovieTitle msg)
        {
            // Todo: Validation

            var @event = new MovieTitleChanged(msg.Id, msg.Title);

            _bus.Publish(@event);
        }
    }
}
