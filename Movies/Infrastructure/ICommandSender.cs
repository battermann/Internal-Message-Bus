using Movies.Commands;
using Movies.Contracts;

namespace Movies.Infrastructure
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;

    }
}