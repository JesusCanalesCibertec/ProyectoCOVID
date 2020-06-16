using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Angular.Controllers.net.royal.spring;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.framework.core.dominio;
using Newtonsoft.Json;

namespace net.royal.spring.framework.web.controller
{
    public static class IHttpContextAccessorExtension
    {
        public static LoginUser CurrentUser(this IHttpContextAccessor httpContextAccessor)
        {
            var stringId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var companiaId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sid)?.Value;
            return new LoginUser() { Usuario = stringId, Compania = companiaId };
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SecuredBaseController : BaseController
    {
        public SecuredBaseController(IServiceProvider servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, servicioProveedor)
        {

        }

    }
}