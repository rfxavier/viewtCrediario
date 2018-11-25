using System;
using ViewT.Crediario.Domain.Core.Commands;
using ViewT.Crediario.Domain.Core.Interfaces;
using ViewT.Crediario.Infra.Data.Context;

namespace ViewT.Crediario.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CrediarioContext _context;
        private bool _disposed;

        public UnitOfWork(CrediarioContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        CommandResponse IUnitOfWork.Commit()
        {
            _context.SaveChanges();
            return CommandResponse.Ok;
        }

        public void Rollback()
        {
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}