using Movies.Contracts;
using Movies.Data;
using Movies.Events;

namespace Movies
{
    public class EventHandlers
    {
        private readonly IMovieRepository _movieRepository;

        public EventHandlers(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Handle(MovieCreated msg)
        {
            _movieRepository.Insert(msg);
        }

        public void Handle(MovieTitleChanged msg)
        {
            _movieRepository.Update(msg);
        }
    }
}
