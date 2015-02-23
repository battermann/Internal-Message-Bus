using System;
using System.Collections.Generic;
using System.Linq;
using FunctionalExtensions;
using Movies.Events;
using Movies.Models;

namespace Movies.Data
{
    public class InMemoryMovieRepository : IMovieRepository
    {
        private List<MovieDto> _movies;

        public InMemoryMovieRepository()
        {
            _movies = new List<MovieDto>();
        }

        public Option<Movie> GetById(Guid id)
        {
            return _movies
                .SingleOrDefault(x => x.Id == id)
                .ToOption()
                .Select(Map);
        }

        private static Movie Map(MovieDto x)
        {
            return new Movie(x.Id, x.Title, x.ReleaseDate, x.Genre, x.Price);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies.Select(Map);
        }

        public void Insert(MovieCreated e)
        {
            _movies.Add(MapFromEvent(e));
        }

        private static MovieDto MapFromEvent(MovieCreated e)
        {
            return new MovieDto
            {
                Id = e.Id,
                Title = e.Title, 
                ReleaseDate = e.ReleaseDate,
                Genre = e.Genre,
                Price = e.Price
            };
        }

        private static MovieDto MapFromEvent(MovieTitleChanged e, MovieDto movieDto)
        {
            movieDto.Title = e.Title;
            return movieDto;
        }

        public void Update(MovieTitleChanged e)
        {
            _movies = _movies
                .Select(x => x.Id == e.Id ? MapFromEvent(e, x) : x)
                .ToList();
        }
    }

    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }

    public interface IMovieRepository
    {
        Option<Movie> GetById(Guid id);
        IEnumerable<Movie> GetAll(); 
        void Insert(MovieCreated e);
        void Update(MovieTitleChanged e);
    }
}
