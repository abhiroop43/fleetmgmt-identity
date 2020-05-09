namespace FleetMgmt.Identity.Domain.Dto
{
    public class UserRegistrationRequestDto
    {
        public string FirstName { get; set; }
         
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        
        public string UserEmail { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        
        public string Telephone { get; set; }
        
        public string Mobile { get; set; }
        
        public string Address { get; set; }
        
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }
        
        public string Remarks { get; set; }
        
        public bool Active { get; set; }

        // public string Company { get; set; }
        //
        // public string Otp { get; set; }

        public bool? TermsAccepted { get; set; }
    }
}