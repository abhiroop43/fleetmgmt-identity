using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class UserRepository : RepositoryEF<IM_USERS>, IUserRepository
    {
        public UserRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}