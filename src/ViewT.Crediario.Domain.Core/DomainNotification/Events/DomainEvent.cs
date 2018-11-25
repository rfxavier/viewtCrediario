using System;
using System.Collections.Generic;
using ViewT.Crediario.Domain.Core.DomainNotification.Events.Contracts;
using ViewT.Crediario.Domain.Core.DomainNotification.Exceptions;

namespace ViewT.Crediario.Domain.Core.DomainNotification.Events
{
    public static class DomainEvent
    {
        #region Para possibilitar unit testing - actions para callback
        [ThreadStatic]
        private static List<Delegate> _actions;

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (_actions == null)
            {
                _actions = new List<Delegate>();
            }
            _actions.Add(callback);
        }

        public static void ClearCallbacks()
        {
            _actions = null;
        }
        #endregion

        public static IContainer Container { get; set; }

        public static void Raise<T>(T args) where T : IDomainEvent
        {
            try
            {
                if (Container != null)
                {
                    var obj = Container.GetService(typeof (IHandler<T>));
                    ((IHandler<T>) obj).Handle(args);
                }
            }
            catch (Exception ex)
            {
                throw new RaiseEventException("Failed to raise event", ex);
            }

            #region Para possibilitar unit testing - actions para callback

            if (_actions != null)
            {
                foreach (var action in _actions)
                {
                    if (action is Action<T>)
                    {
                        ((Action<T>)action)(args);
                    }
                }
            }

            #endregion
        }
    }
}