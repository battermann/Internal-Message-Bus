﻿using Movies.Commands;

namespace Movies.Infrastructure
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;

    }
}