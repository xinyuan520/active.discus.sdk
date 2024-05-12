using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.Shared.WebApi.Authorization.JwtBearer
{
    public static class JwtTokenHelper
    {
        /// <summary>
        ///  Create  a TokenValidationParameters instance
        /// </summary>
        /// <param name="tokenConfig"></param>
        /// <returns></returns>
        public static TokenValidationParameters GenarateTokenValidationParameters(JWTConfig tokenConfig) =>
            new()
            {
                ValidateIssuer = tokenConfig.ValidateIssuer,
                ValidIssuer = tokenConfig.ValidIssuer,
                ValidateIssuerSigningKey = tokenConfig.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(tokenConfig.Encoding.GetBytes(tokenConfig.SymmetricSecurityKey)),
                ValidateAudience = tokenConfig.ValidateAudience,
                ValidAudience = tokenConfig.ValidAudience,
                ValidateLifetime = tokenConfig.ValidateLifetime,
                RequireExpirationTime = tokenConfig.RequireExpirationTime,
                ClockSkew = TimeSpan.FromSeconds(tokenConfig.ClockSkew),
            };

        public static JwtToken CreateAccessToken(JWTConfig jwtConfig, string nameId, string jti)
        {
            if (string.IsNullOrWhiteSpace(jti))
                throw new ArgumentNullException(nameof(jti));

            var claims = new Claim[]
                {
                     new Claim(JwtRegisteredClaimNames.NameId, nameId),
                     new Claim(JwtRegisteredClaimNames.Jti,jti),
                };
            return WriteToken(jwtConfig, claims, Tokens.AccessToken);
        }

        //public static JwtToken CreateRefreshToken(JWTConfig jwtConfig, string jti, string nameId)
        //{
        //    var claims = new Claim[]
        //      {
        //             new Claim(JwtRegisteredClaimNames.Jti,jti),
        //      };
        //    return WriteToken(jwtConfig, claims, Tokens.AccessToken);
        //}

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="jwtConfig"></param>
        /// <param name="claims"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        private static JwtToken WriteToken(JWTConfig jwtConfig, Claim[] claims, Tokens tokenType)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SymmetricSecurityKey));
            string issuer = jwtConfig.ValidIssuer;
            string audience = tokenType.Equals(Tokens.AccessToken) ? jwtConfig.ValidAudience : jwtConfig.RefreshTokenAudience;
            int expiresencods = tokenType.Equals(Tokens.AccessToken) ? jwtConfig.Expire : jwtConfig.RefreshTokenExpire;
            var expires = DateTime.Now.AddMinutes(expiresencods);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expires,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtToken(new JwtSecurityTokenHandler().WriteToken(token), expires);
        }
    }
}
