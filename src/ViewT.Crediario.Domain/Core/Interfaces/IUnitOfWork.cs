using System;
using ViewT.Crediario.Domain.Core.Commands;

namespace ViewT.Crediario.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
        void Rollback();
    }
}
