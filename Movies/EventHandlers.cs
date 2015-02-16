using Movies.Contracts;
using Movies.Data;
using Movies.Events;
using Movies.Models;

namespace Movies
{
    public class EventHandlers
    {
        private readonly IRepository _repository;

        public EventHandlers(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(MovieCreated msg)
        {
            _repository.Insert(new Movie(msg.Title, msg.ReleaseDate, msg.Genre, msg.Price));
        }

        public void Handle(MovieUpdated msg)
        {
            var movie = _repository.GetById(msg.Id);
            _repository.Update(movie.NewMovieWith(msg.Title, msg.ReleaseDate, msg.Genre, msg.Price));
        }
    }
}
