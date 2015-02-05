using System;

namespace Movies.Models
{
    public class Movie
    {
        public Movie(string title)
        {
            if(String.IsNullOrEmpty(title)) throw new ArgumentNullException("title");

            Title = title;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return String.Format("[{0}] - {1}",Id, Title);
        }
    }
}
