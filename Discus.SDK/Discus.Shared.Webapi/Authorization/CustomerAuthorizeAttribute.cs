using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.Shared.WebApi.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CustomerAuthorizeAttribute : AuthorizeAttribute
    {
        public string AuthCode { get; set; }

        public CustomerAuthorizeAttribute(string schemes = JwtBearerDefaults.AuthenticationScheme)
        {
            Policy = AuthorizePolicy.Policy;
            if (string.IsNullOrWhiteSpace(schemes))
                throw new ArgumentNullException(nameof(schemes));
            else
                AuthenticationSchemes = schemes;
        }

        public CustomerAuthorizeAttribute(string authCode, string schemes = JwtBearerDefaults.AuthenticationScheme)
        {
            AuthCode = authCode;
            Policy = AuthorizePolicy.Policy;
            if (string.IsNullOrWhiteSpace(schemes))
                throw new ArgumentNullException(nameof(schemes));
            else
                AuthenticationSchemes = schemes;
        }
    }
}
