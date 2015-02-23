using System;
using Movies.Contracts;

namespace Movies.Commands
{
    public class CreateMovie : Command
    {
        public CreateMovie(Guid id, string title, DateTime releaseDate, string genre, decimal price)
        {
            if (String.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("title");
            if (releaseDate == new DateTime()) throw new ArgumentException("date cannot be empty", "releaseDate");
            if (String.IsNullOrWhiteSpace(genre)) throw new ArgumentNullException("genre");
            if(id == new Guid()) throw new ArgumentException("id cannot be empty", "id");

            Id = id;
            Title = title;
            ReleaseDate = releaseDate;
            Genre = genre;
            Price = price;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public string Genre { get; private set; }
        public decimal Price { get; private set; }
    }
}
