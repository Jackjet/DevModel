using System;
using System.Transactions;

namespace Baibaomen.DevModel.Infrastructure
{
    public class WorkUnit : IDisposable
    {
        private bool _disposed;
        private readonly TransactionScope _transactionScope;

        public WorkUnit() : this(IsolationLevel.ReadCommitted)
        {
        }

        public WorkUnit(IsolationLevel isolationLevel)
        {
            var options = new TransactionOptions
            {
                IsolationLevel = isolationLevel,
                Timeout = TransactionManager.MaximumTimeout
            };
            _transactionScope = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transactionScope.Dispose();
                }
                _disposed = true;
            }
        }

        public void Commit()
        {
            _transactionScope.Complete();
        }
    }
}