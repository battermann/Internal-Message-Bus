namespace Movies.Infrastructure
{
    public interface IMessageBus : ICommandSender, IEventPublisher, ISubscribable
    {
    }
}