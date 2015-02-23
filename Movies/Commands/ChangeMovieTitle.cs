using System;
using Movies.Contracts;

namespace Movies.Commands
{
    public class ChangeMovieTitle : Command
    {
        public ChangeMovieTitle(Guid id, string title)
        {
            if(id == new Guid()) throw new ArgumentException("id cannot be emptry", "id");
            if(String.IsNullOrWhiteSpace(title)) throw new ArgumentException("title cannot be empty", "title");

            Title = title;
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
    }
}
