using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.framework.web.controller;
using net.royal.spring.proceso.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpAuditoriaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpAuditoriaServicio bpAuditoriaServicio;

        public BpAuditoriaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpAuditoriaServicio = servicioProveedor.GetService<BpAuditoriaServicio>();
        }

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listar([FromBody]FiltroPaginacionAuditoria filtro)
        {
            filtro.paginacion.CantidadRegistrosDevolver = _usuarioActual.paginacion;
            return bpAuditoriaServicio.listar(filtro.paginacion, filtro);
        }

        [HttpGet("[action]")]
        public List<DtoTabla> listarPeriodoBoleta()
        {
            return bpAuditoriaServicio.listarPeriodoBoleta();
        }
    }
}