using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_OU : BaseEntity
    {
        public IM_OU()
        {
            this.ImGroupsOus = new List<IM_GROUPS_OU>();
        }

        public string NAME { get; set; }
        public string COMPANY_ID { get; set; }
        public string CODE { get; set; }
        public bool? ACTIVE { get; set; }

        [ForeignKey("COMPANY_ID")]
        public IM_COMPANY Company { get; set; }
        public List<IM_GROUPS_OU> ImGroupsOus { get; set; }
    }
}
