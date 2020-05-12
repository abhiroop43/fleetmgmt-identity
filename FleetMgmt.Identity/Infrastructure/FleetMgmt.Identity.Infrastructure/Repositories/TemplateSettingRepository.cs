using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class TemplateSettingRepository : RepositoryEF<IM_TEMPLATE_SETTING>, ITemplateSettingRepository
    {
        public TemplateSettingRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
