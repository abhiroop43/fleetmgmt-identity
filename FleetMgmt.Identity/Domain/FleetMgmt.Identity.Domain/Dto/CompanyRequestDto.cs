using System;

namespace FleetMgmt.Identity.Domain.Dto
{
    public class CompanyRequestDto 
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Domain { get; set; }
        public string TradeLicense { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public bool Active {get; set; }


    }

    public class CompanyResponseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}