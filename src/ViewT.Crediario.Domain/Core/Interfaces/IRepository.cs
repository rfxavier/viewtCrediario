using System;

namespace ViewT.Crediario.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        
    }
}
