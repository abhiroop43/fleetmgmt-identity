using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class TransactionalUnitOfWork : ITransactionalUnitOfWork
    {
        private readonly FleetMgmtIdentityDbContext _dbContext;
        readonly IDbContextTransaction _transaction;
        public TransactionalUnitOfWork(FleetMgmtIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
            IsActive = true;
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public int AffectedRows { get; private set; }
        public bool IsActive { get; private set; }

        public void Rollback()
        {
            _transaction.Rollback();
        }
        public int Save()
        {
            AffectedRows = _dbContext.SaveChanges();
            return AffectedRows;
        }

        public async Task<int> SaveAsync()
        {
            AffectedRows = await _dbContext.SaveChangesAsync();
            return AffectedRows;
        }
        public int Commit()
        {
            var result = Save();
            if (IsActive)
                _transaction.Commit();
            return result;
        }

        public async Task<int> CommitAsync()
        {
            var result = await SaveAsync();
            if (IsActive)
                _transaction.Commit();
            return result;
        }

        public void SetIsActive(bool value)
        {
            IsActive = value;
        }
    }
}