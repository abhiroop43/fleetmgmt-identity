using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_GROUPS_OU : BaseEntity
    {
        public string GROUP_ID { get; set; }
        public string OU_ID { get; set; }
        public bool ACTIVE { get; set; }

        [ForeignKey("GROUP_ID")]
        public IM_GROUPS Group { get; set; }

        [ForeignKey("OU_ID")]
        public IM_OU Ou { get; set; }
    }

}
