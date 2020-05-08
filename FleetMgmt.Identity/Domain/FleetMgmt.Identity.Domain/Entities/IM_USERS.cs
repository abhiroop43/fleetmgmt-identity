using System;
using System.Collections.Generic;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_USERS : BaseEntity
    {
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string USERNAME { get; set; }
        public string USEREMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string TELEPHONE { get; set; }
        public string MOBILE { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string REMARKS { get; set; }
        public bool ISINTERNAL { get; set; }
        public bool ACTIVE { get; set; }
        public bool? TERMS_ACCEPTED { get; set; }

        public virtual ICollection<IM_USERS_GROUPS> ImUsersGroups { get; set; }
    }
}
