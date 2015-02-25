using System;
using Movies.Commands;
using Movies.Contracts;
using Movies.Events;

namespace Movies
{
    public static class CommandHandlers
    {
        public static void Handle(Func<IEventPublisher> bus, CreateMovie msg)
        {
            // Todo: Validation

            var @event = new MovieCreated(Guid.NewGuid(), msg.Title, msg.ReleaseDate, msg.Genre, msg.Price);

            bus().Publish(@event);
        }

        public static void Handle(Func<IEventPublisher> bus, ChangeMovieTitle msg)
        {
            // Todo: Validation

            var @event = new MovieTitleChanged(msg.Id, msg.Title);

            bus().Publish(@event);
        }
    }
}
