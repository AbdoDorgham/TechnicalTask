using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechnicalTask.BusinessLogic.Entities.General;

namespace TechnicalTask.BusinessLogic.Utils
{
    public class MyTokenHandler
    {
        private static readonly string _secretKey = "C3k0EINrjzlfR6BD0a/5O/kkG5+02rPRz0cOn2EM7IiwJ2iKchP+zKHuNKn3cbhgmhR5S9AdHGwnGfNnFh6aHw==";

        private static string PrivateGenerateToken(ApplicationUser user, int expireDays = 1)
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: new List<Claim> {
                    new Claim(MyCalims.UserId, user.Id.ToString()),
                    new Claim(MyCalims.UserName, user.UserName!),
                    new Claim(MyCalims.Email, user.Email!),
                },
                signingCredentials: new SigningCredentials(
                    key: GetSecurityKey(),
                    algorithm: SecurityAlgorithms.HmacSha256
                    ),
                expires: DateTime.Now.AddDays(expireDays)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }


        public static string GenerateToken(ApplicationUser user, int expireDays = 1) => PrivateGenerateToken(user, expireDays);






        public static SecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

    }
}
