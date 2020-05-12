using System;
using SD.BuildingBlocks.Infrastructure;

namespace FleetMgmt.Identity.Domain.Entities
{
    public class IM_TEMPLATE_SETTING : BaseEntity
    {
        public string NAME { get; set; }

        public string KEY { get; set; }

        public string VALUE { get; set; }

        public bool ACTIVE { get; set; }

        public string DESCRIPTION { get; set; }
    }
}