using System;

namespace FleetMgmt.Identity.Domain.Dto
{
    public class OURequestDto
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Company_Id { get; set; }
        public string Code { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
        public string CompanyName { get; set; }

    }

    public class OUActionRequestDto : OURequestDto {
    }

    public class OUResponseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}