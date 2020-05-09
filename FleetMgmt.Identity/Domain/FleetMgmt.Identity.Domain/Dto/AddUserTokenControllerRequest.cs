using System;
using System.ComponentModel.DataAnnotations;

namespace FleetMgmt.Identity.Domain.Dto
{
    public class AddUserTokenControllerRequest
    {
        /// <summary>
        /// property added to get unique ID for user record.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// property added for username
        /// </summary>
        [Required]
        public string UserName { get; set; }
        
        /// <summary>
        /// property added for token value
        /// </summary>
        [Required]
        public string TokenValue { get; set; }
        
        /// <summary>
        /// property added to validate token
        /// </summary>
        [Required]
        public bool IsTokenValid { get; set; }
        
        /// <summary>
        /// property added for comment
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// property added for user/system creating a record
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}