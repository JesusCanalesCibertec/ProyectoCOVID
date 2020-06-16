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
    public class BpProcesoconexioneventoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpProcesoconexioneventoServicio BpProcesoconexioneventoServicio;

        public BpProcesoconexioneventoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpProcesoconexioneventoServicio = servicioProveedor.GetService<BpProcesoconexioneventoServicio>();
        }

        [HttpGet("[action]")]
        public List<DtoBpProcesoconexionevento> listado(string pIdProceso, int pIdConexion)
        {
            return BpProcesoconexioneventoServicio.listado(pIdProceso,pIdConexion);

        }

        [HttpPost("[action]")]
        public BpProcesoconexionevento registrar([FromBody]BpProcesoconexionevento bean)
        {
            return BpProcesoconexioneventoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesoconexionevento actualizar([FromBody]BpProcesoconexionevento bean)
        {
            return BpProcesoconexioneventoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpProcesoconexionevento obtenerPorId([FromBody]BpProcesoconexioneventoPk llave)
        {
            return BpProcesoconexioneventoServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public DtoBpProcesoconexionevento eliminar([FromBody]DtoBpProcesoconexionevento bean)
        {
            BpProcesoconexioneventoServicio.eliminar(bean.codProceso, bean.codVersion, bean.codConexion,bean.codEvento);
            return bean;

        }



    }
}