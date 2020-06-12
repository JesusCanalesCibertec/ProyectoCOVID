using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.web.servicio.impl
{
    public class GenericoServicioImpl : GenericoServicio
    {
        public String obtenerServidorWeb(IHttpContextAccessor httpContextAccessor) {
            String ser = String.Format("{0}://{1}", httpContextAccessor.HttpContext.Request.Scheme, httpContextAccessor.HttpContext.Request.Host);
            return ser;
        }
    }
}
