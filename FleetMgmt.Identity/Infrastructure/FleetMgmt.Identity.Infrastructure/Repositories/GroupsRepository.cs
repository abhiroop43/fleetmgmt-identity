using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class GroupsRepository : RepositoryEF<IM_GROUPS>, IGroupsRepository
    {
        public GroupsRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
