using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Movies.Contracts;

namespace Movies.Infrastructure
{
    public class MessageBus : IMessageBus
    {
        private readonly ISubject<IMessage> _messages;
        private static IMessageBus _bus;

        private MessageBus()
        {
            _messages = new Subject<IMessage>();
        }

        public static IMessageBus GetInstance
        {
            get { return _bus ?? (_bus = new MessageBus()); }
        }

        public void Send<T>(T command) where T : Command
        {
            _messages.OnNext(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            _messages.OnNext(@event);
        }

        private IObservable<T> AsObservable<T>() where T : IMessage
        {
            return _messages.OfType<T>();
        }

        public IDisposable Register<T>(Action<T> action) where T : IMessage
        {
            return AsObservable<T>().Subscribe(action);
        }
    }
}
