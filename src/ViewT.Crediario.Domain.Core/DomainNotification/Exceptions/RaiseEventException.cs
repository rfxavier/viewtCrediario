using System;

namespace ViewT.Crediario.Domain.Core.DomainNotification.Exceptions
{
    internal class RaiseEventException : Exception
    {
        public RaiseEventException()
        {
        }

        public RaiseEventException(string message)
            : base(message)
        {
        }

        public RaiseEventException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}