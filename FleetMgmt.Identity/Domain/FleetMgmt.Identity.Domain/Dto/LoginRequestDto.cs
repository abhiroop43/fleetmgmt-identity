namespace FleetMgmt.Identity.Domain.Dto
{
    public class LoginRequestDto
    {
        /// <summary>
        /// Properties added for Username
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Properties added for Password
        /// </summary>
        public string Password { get; set; }
    }
}