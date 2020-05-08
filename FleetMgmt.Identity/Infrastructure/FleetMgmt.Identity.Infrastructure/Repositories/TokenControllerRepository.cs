using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class TokenControllerRepository : RepositoryEF<IM_TOKENS_CONTROLLER>, ITokenControllerRepository
    {
        public TokenControllerRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}