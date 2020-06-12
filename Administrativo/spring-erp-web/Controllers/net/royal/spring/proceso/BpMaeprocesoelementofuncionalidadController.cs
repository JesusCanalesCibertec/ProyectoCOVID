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
    public class BpMaeprocesoelementofuncionalidadController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesoelementofuncionalidadServicio BpMaeprocesoelementofuncionalidadServicio;

        public BpMaeprocesoelementofuncionalidadController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            BpMaeprocesoelementofuncionalidadServicio = servicioProveedor.GetService<BpMaeprocesoelementofuncionalidadServicio>();
        }

        [HttpGet("[action]")]
        public List<DtoBpMaeprocesoelementofuncionalidad> listado(string pIdProceso, string pIdElemento)
        {
            return BpMaeprocesoelementofuncionalidadServicio.listado(pIdProceso, pIdElemento);

        }

        [HttpPost("[action]")]
        public BpMaeprocesoelementofuncionalidad registrar([FromBody]BpMaeprocesoelementofuncionalidad bean)
        {
            return BpMaeprocesoelementofuncionalidadServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoelementofuncionalidad actualizar([FromBody]BpMaeprocesoelementofuncionalidad bean)
        {
            return BpMaeprocesoelementofuncionalidadServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoelementofuncionalidad obtenerPorId([FromBody]BpMaeprocesoelementofuncionalidadPk llave)
        {
            return BpMaeprocesoelementofuncionalidadServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public DtoBpMaeprocesoelementofuncionalidad eliminar([FromBody]DtoBpMaeprocesoelementofuncionalidad bean)
        {
            BpMaeprocesoelementofuncionalidadServicio.eliminar(bean.codProceso, bean.codElemento, bean.codFuncionalidad);
            return bean;

        }



    }
}