using Microservices.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Persistence.Repository.EfCore.Transaction
{
    public class EfTransactionBuilder<TDbContext> : ITransactionBuilder,IDisposable
        where TDbContext : DbContext
    {
        private bool disposedValue;

        private TDbContext _dbContext { get; }
        public EfTransactionBuilder(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task BeginTransactionAsync()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public void DisposeTransaction()
        {
            try
            {
                if (_dbContext.Database.CurrentTransaction != null)
                    _dbContext.Database.CurrentTransaction.Dispose();
            }
            catch
            {

            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeTransaction();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EfTransactionBuilder()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
