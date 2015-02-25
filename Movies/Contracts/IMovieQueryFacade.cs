using System;
using System.Collections.Generic;
using FunctionalExtensions;
using Movies.Models;

namespace Movies.Contracts
{
    public interface IMovieQueryFacade
    {
        Option<Movie> GetById(Guid id);
        IEnumerable<Movie> GetAll(); 
    }
}
