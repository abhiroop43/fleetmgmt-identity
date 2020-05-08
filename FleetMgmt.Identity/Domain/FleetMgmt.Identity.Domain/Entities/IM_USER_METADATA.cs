using System;
using System.Collections.Generic;
using System.Text;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_USER_METADATA : BaseEntity
    {
        public string USER_NAME { get; set; }
        public string METADATA { get; set; }
        public string METATYPE { get; set; }
    }

}
