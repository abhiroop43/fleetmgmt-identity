using System.Collections.Generic;

namespace FleetMgmt.Identity.Domain.Dto
{
    public class LoginResponseDto
    {
        /// <summary>
        /// Properties added for Username
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// properties added to get user group details.
        /// </summary>
        public string[] UserGroup { get; set; }
        
        /// <summary>
        /// Properties added to get first name
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Properties added to get first name
        /// </summary>
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public List<OUResponseDto> OrganizationUnits { get; set; }
        
        public List<CompanyResponseDto> Organizations { get; set; }
        public bool UserActive { get; set; }
        public bool IsUserAuthenticated { get; set; }
    }
}