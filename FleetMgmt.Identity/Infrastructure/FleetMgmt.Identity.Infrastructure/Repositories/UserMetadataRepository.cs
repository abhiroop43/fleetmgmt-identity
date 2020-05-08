using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class UserMetadataRepository : RepositoryEF<IM_USER_METADATA>, IUserMetadataRepository
    {
        public UserMetadataRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}