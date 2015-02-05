using System;

namespace Movies.Commands
{
    public class ChangeTitle : Command
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
