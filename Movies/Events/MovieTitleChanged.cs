using System;
using Movies.Contracts;

namespace Movies.Events
{
    public class MovieTitleChanged : Event
    {
        public MovieTitleChanged(Guid id, string title)
        {
            if (id == new Guid()) throw new ArgumentException("id cannot be emptry", "id");
            if (String.IsNullOrWhiteSpace(title)) throw new ArgumentException("title cannot be empty", "title");

            Title = title;
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
    }
}
