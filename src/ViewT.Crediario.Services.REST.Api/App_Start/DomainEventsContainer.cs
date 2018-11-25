using System;
using System.Collections.Generic;
using SimpleInjector;
using ViewT.Crediario.Domain.Core.DomainNotification;

namespace ViewT.Crediario.Services.REST.Api
{
    public class DomainEventsContainer: IContainer
    {
        private readonly Container _container;

        public DomainEventsContainer(Container container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container)
                .GetService(serviceType);
            //return _resolver.GetService(serviceType);
        }

        public T GetService<T>()
        {
            return (T) ((IServiceProvider)_container)
                .GetService(typeof(T));
            //return (T) _resolver.GetService(typeof(T));
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
            //return _resolver.GetServices(serviceType);
        }

        public IEnumerable<T> GetServices<T>()
        {
            return (IEnumerable<T>) _container.GetAllInstances(typeof(T));
            //return (IEnumerable<T>) _resolver.GetServices(typeof(T));
        }
    }
}