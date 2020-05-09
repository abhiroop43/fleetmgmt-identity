using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace FleetMgmt.Identity.TokenGenerator
{
     public sealed class TokenBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 120;
        private DateTime expiryInYears = DateTime.Now.AddYears(1);

        public TokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        public TokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        public TokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        public TokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            claims.ToList().ForEach(x => this.claims.Add(x.Key, x.Value));

            return this;
        }

        public TokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        public TokenBuilder AddLongExpiry(int addExpiryYears)
        {
            this.expiryInYears = DateTime.Now.AddYears(addExpiryYears);
            return this;
        }

        public Token Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, this.subject),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(

                              issuer: this.issuer,
                              audience: this.audience,
                              claims: claims,
                             // expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                               expires: DateTime.Now.AddMinutes(expiryInMinutes),
                              signingCredentials: new SigningCredentials(
                                                        this.securityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new Token(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addExpiryInYears"></param>
        /// <returns></returns>
        public Token BuildForLongDuration(int addExpiryInYears)
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, this.subject),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(

                              issuer: this.issuer,
                              audience: this.audience,
                              claims: claims,
                              expires: DateTime.Now.AddYears(addExpiryInYears),
                              signingCredentials: new SigningCredentials(
                                                        this.securityKey,
                                                        SecurityAlgorithms.HmacSha256));

            return new Token(token);
        }

        #region " private "

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("Audience");
        }

        #endregion

    }
}