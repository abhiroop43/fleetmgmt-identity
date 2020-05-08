using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SD.BuildingBlocks.Repository;

namespace FleetMgmt.Identity.Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryEF<IM_COMPANY>, ICompanyRepository
    {
        public CompanyRepository(FleetMgmtIdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}