using Category.Infrastructure.Persistence;
using Core.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Category.Infrastructure.Repositories
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IUnitOfWork"/> interface.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        protected CategoryDbContext Context { get; }

        public UnitOfWork(CategoryDbContext context)
        {
            Context = context;
        }

        /// <inheritdoc />
        public Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => Context.Database.BeginTransactionAsync(isolationLevel);

        /// <inheritdoc />
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
                        => await Context.SaveChangesAsync(cancellationToken);

        /// <inheritdoc />
        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
                        => await Context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }


}
