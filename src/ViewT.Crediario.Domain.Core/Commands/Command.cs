using System;

namespace ViewT.Crediario.Domain.Core.Commands
{
    public abstract class Command 
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool EstaValido();
    }
}