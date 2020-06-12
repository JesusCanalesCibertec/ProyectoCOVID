using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class MpSistemaController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpSistemaServicio mpSistemaServicio;


        public MpSistemaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            mpSistemaServicio = servicioProveedor.GetService<MpSistemaServicio>();
        }

        [HttpGet("[action]")]
        public List<MpSistema> ListarSistemas()
        {
            return mpSistemaServicio.ListarSistemas();
        }

    }
}
