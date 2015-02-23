using System;

namespace Movies.Models
{
    public class Movie
    {
        public Movie(string title, DateTime releaseDate, string genre, decimal price)
            : this(Guid.NewGuid(), title, releaseDate, genre, price)
        {
        }

        public Movie(Guid id, string title, DateTime releaseDate, string genre, decimal price)
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

        public Movie NewMovieWith(string title = null, DateTime? releaseDate = null, string genre = null, decimal? price = null)
        {
            return new Movie(Id, title ?? Title, releaseDate.HasValue ? releaseDate.Value : ReleaseDate, genre ?? Genre, price.HasValue ? price.Value : Price);
        }

        public override string ToString()
        {
            return String.Join(" - ", Title, ReleaseDate.ToString("yyyy"), Genre, Price);
        }
    }
}
