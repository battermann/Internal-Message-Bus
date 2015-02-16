using System;

namespace Movies.Commands
{
    public class UpdateMovie : Command
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal? Price { get; set; }
    }
}
