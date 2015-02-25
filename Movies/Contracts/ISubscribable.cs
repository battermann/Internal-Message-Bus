using System;

namespace Movies.Contracts
{
    public interface ISubscribable
    {
        IDisposable Register<T>(Action<T> action) where T : IMessage;
    }
}