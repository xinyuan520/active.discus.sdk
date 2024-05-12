using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Dtos
{
    public class LoginRequestDto
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }
}
