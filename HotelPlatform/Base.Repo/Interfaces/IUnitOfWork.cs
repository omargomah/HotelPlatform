using Microsoft.EntityFrameworkCore.Storage;
using Base.DAL.Models.BaseModels;

namespace Base.Repo.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        Task<int> CompleteAsync(); 

        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}
