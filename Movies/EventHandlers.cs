using Movies.Contracts;
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

        public void Handle(MovieInserted msg)
        {
            _repository.Insert(new Movie(msg.Title));
        }

        public void Handle(TitleChanged msg)
        {
            var movie = _repository.GetById(msg.Id);
            movie.Title = msg.Title;
            _repository.Update(movie);
        }
    }
}
