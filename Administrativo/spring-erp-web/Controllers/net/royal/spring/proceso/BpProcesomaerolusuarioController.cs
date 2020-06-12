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
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso
{
    [Route("api/spring/proceso/[controller]")]
    public class BpMaeprocesorolusuarioController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesorolusuarioServicio BpMaeprocesorolusuarioServicio;

        public BpMaeprocesorolusuarioController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpMaeprocesorolusuarioServicio = servicioProveedor.GetService<BpMaeprocesorolusuarioServicio>();
        }

        [HttpGet("[action]")]
        public List<DtoBpMaeprocesorolusuario> listado(string pIdProceso, string pIdRol, string pIdTipoSeguridad)
        {
            return BpMaeprocesorolusuarioServicio.listado(pIdProceso, pIdRol,pIdTipoSeguridad);

        }

        [HttpPost("[action]")]
        public BpMaeprocesorolusuario registrar([FromBody]BpMaeprocesorolusuario bean)
        {
            return BpMaeprocesorolusuarioServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesorolusuario actualizar([FromBody]BpMaeprocesorolusuario bean)
        {
            return BpMaeprocesorolusuarioServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesorolusuario obtenerPorId([FromBody]BpMaeprocesorolusuarioPk llave)
        {
            return BpMaeprocesorolusuarioServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public DtoBpMaeprocesorolusuario eliminar([FromBody]DtoBpMaeprocesorolusuario bean)
        {
            BpMaeprocesorolusuarioServicio.eliminar(bean.codProceso, bean.codRol, bean.codTipoSeguridad,bean.codConcepto);
            return bean;

        }



    }
}