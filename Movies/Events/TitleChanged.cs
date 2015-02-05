using System;

namespace Movies.Events
{
    public class TitleChanged : Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
