using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.Shared.WebApi.Authorization.JwtBearer
{
    public record JwtToken
    {
        public JwtToken(string token, DateTime expire)
        {
            Token = token;
            Expire = expire;
        }
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
