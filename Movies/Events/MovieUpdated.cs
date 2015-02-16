﻿using System;

namespace Movies.Events
{
    public class MovieUpdated : Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal? Price { get; set; }
    }
}
