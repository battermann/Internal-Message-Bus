using System;
using System.Collections.Generic;
using FunctionalExtensions;
using Movies.Events;
using Movies.Models;

namespace Movies.Contracts
{
    public interface IMovieRepository
    {
        Option<Movie> GetById(Guid id);
        IEnumerable<Movie> GetAll(); 
        void Insert(MovieCreated e);
        void Update(MovieTitleChanged e);
    }
}
