using System;
using System.Collections.Generic;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_TOKENS_CONTROLLER : BaseEntity
    {
        public string USER_NAME { get; set; }
        public string VALUE { get; set; }
        public bool ISTOKENVALID { get; set; }
        public string REMARKS { get; set; }
    }

}
