using System;
using System.Collections.Generic;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_COMPANY : BaseEntity
    {
        public IM_COMPANY()
        {
            this.ImOus = new List<IM_OU>();
        }

        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string PHONENUMBER { get; set; }
        public string FAXNUMBER { get; set; }
        public string DOMAIN { get; set; }
        public string TRADELICENSE { get; set; }
        public bool? ACTIVE { get; set; }

        public List<IM_OU> ImOus { get; set; }
    }

}
