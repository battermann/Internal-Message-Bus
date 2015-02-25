using Movies.Infrastructure;

namespace Movies.Contracts
{
    public interface IMessageBus : ICommandSender, IEventPublisher, ISubscribable
    {
    }
}