using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class GroupsOuRepository : RepositoryEF<IM_GROUPS_OU>, IGroupsOuRepository
    {
        public GroupsOuRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}