using System;
using System.Collections.Generic;
using System.Linq;
using Movies.Models;

namespace Movies.Contracts
{
    public class InMemoryRepository : IRepository
    {
        private readonly List<Movie> _movies;

        public InMemoryRepository()
        {
            _movies = new List<Movie>();
        }

        public Movie GetById(Guid id)
        {
            return _movies.Single(x => x.Id == id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public void Insert(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Update(Movie movie)
        {
            var m = _movies.Single(x => x.Id == movie.Id);
            m.Title = movie.Title;
        }
    }

    public interface IRepository
    {
        Movie GetById(Guid id);
        IEnumerable<Movie> GetAll(); 
        void Insert(Movie movie);
        void Update(Movie movie);
    }
}
