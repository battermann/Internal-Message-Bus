using System;
using Movies.Contracts;
using Movies.Events;

namespace Movies
{
    public static class EventHandlers
    {
        public static void Handle(Func<IMovieRepository> repository, MovieCreated msg)
        {
            repository().Insert(msg);
        }

        public static void Handle(Func<IMovieRepository> repository, MovieTitleChanged msg)
        {
            repository().Update(msg);
        }
    }
}
