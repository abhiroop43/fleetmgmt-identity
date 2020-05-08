using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_USERS_GROUPS : BaseEntity
    {
        public string GROUP_ID { get; set; }
        public string USER_NAME { get; set; }
        public bool ACTIVE { get; set; }
        public string REMARKS { get; set; }

        [ForeignKey("GROUP_ID")]
        public IM_GROUPS Group { get; set; }

        public virtual IM_USERS User { get; set; }
    }

}
