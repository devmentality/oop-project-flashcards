﻿using System.IO;

namespace FlashcardsClient
{
    public abstract class ConsoleCommand
    {
        public string Name { get; }
        public string Help { get; }

        protected ConsoleCommand(string name, string help)
        {
            Name = name;
            Help = help;
        }

        public abstract void Execute(FlashcardsClient client, string userName);
    }
}