using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class UsersGroupsRepository : RepositoryEF<IM_USERS_GROUPS>, IUsersGroupsRepository
    {
        public UsersGroupsRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}