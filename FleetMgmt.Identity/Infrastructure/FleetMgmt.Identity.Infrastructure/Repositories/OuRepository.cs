using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class OuRepository : RepositoryEF<IM_OU>, IOuRepository
    {
        public OuRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}