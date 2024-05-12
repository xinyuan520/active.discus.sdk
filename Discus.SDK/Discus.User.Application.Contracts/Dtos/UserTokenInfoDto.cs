using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Dtos
{
    public class UserTokenInfoDto
    {
        public UserTokenInfoDto(string token, DateTime exprie)
        {
            Token = token;
            Expire = exprie;
        }

        /// <summary>
        /// accesstoken
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// accesstoken exprie time
        /// </summary>
        public DateTime Expire { get; set; }
    }
}
