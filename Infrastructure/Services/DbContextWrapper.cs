using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Services
{
    public class DbContextWrapper<T> : IDbContextWrapper<T> where T : DbContext
    {
        private readonly T _dbContext;

        public DbContextWrapper(T dbContext)
        {
            _dbContext = dbContext;
        }

        public T DbContext => _dbContext;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
