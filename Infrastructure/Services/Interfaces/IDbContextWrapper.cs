using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Services.Interfaces
{
    public interface IDbContextWrapper<T> where T : DbContext
    {
        T DbContext { get; }

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
