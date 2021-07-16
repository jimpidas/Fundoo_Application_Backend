using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication
{
    public class UserAuthenticationJWT
    {
        private readonly IConfiguration config;
        public UserAuthenticationJWT(IConfiguration config)
        {
            this.config = config;
        }
        public string GenerateSessionJWT(ResponseUserAccount userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddHours(2);
            return GenerateJSONWebToken(userInfo, ExpireTime);
        }
        public string GeneratePasswordResetJWT(ResponseUserAccount userInfo)
        {
            DateTime ExpireTime = DateTime.Now.AddHours(2);
            return GenerateJSONWebToken(userInfo, ExpireTime);
        }
        public string GenerateJSONWebToken(ResponseUserAccount userInfo, DateTime ExpireTime)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            IEnumerable<Claim> Claims = new Claim[] {
                new Claim("UserModelID", userInfo.UserModelID.ToString()),
                new Claim("Email", userInfo.Email) };

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
            claims: Claims,
            expires: ExpireTime,
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
