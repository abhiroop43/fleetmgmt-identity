using System;
using System.Collections.Generic;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_GROUPS : BaseEntity
    {
        public IM_GROUPS()
        {
            this.ImGroupsOus = new List<IM_GROUPS_OU>();
            this.ImUsersGroups = new List<IM_USERS_GROUPS>();
        }

        public string NAME { get; set; }
        public string REMARKS { get; set; }
        public bool ACTIVE { get; set; }

        public List<IM_GROUPS_OU> ImGroupsOus { get; set; }
        public List<IM_USERS_GROUPS> ImUsersGroups { get; set; }
    }

}
