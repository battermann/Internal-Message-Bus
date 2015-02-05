using System;
using Movies.Contracts;

namespace Movies.Infrastructure
{
    public interface ISubscribable
    {
        IDisposable Register<T>(Action<T> action) where T : IMessage;
    }
}