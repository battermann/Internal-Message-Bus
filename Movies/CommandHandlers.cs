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

            var @event = new MovieCreated
            {
                Title = msg.Title,
                ReleaseDate = msg.ReleaseDate,
                Genre = msg.Genre,
                Price = msg.Price
            };

            _bus.Publish(@event);
        }

        public void Handle(UpdateMovie msg)
        {
            // Todo: Validation

            var @event = new MovieUpdated
            {
                Id = msg.Id,
                Title = msg.Title,
                ReleaseDate = msg.ReleaseDate,
                Genre = msg.Genre,
                Price = msg.Price
            };

            _bus.Publish(@event);
        }
    }
}
