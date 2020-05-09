using System.Threading.Tasks;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Interfaces
{
    public interface ITransactionalUnitOfWork : IUnitOfWork
    {
        bool IsActive { get; }
        void SetIsActive(bool value);
        void Rollback();
        int Save();
        Task<int> SaveAsync();
    }
}