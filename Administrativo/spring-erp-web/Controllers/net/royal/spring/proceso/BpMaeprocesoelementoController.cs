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
    public class BpMaeprocesoelementoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private BpMaeprocesoelementoServicio bpMaeprocesoelementoServicio;

        public BpMaeprocesoelementoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            bpMaeprocesoelementoServicio = servicioProveedor.GetService<BpMaeprocesoelementoServicio>();
        }


        [HttpGet("[action]")]
        public List<DtoBpMaeprocesoelemento> listado(string codigo)
        {
            return bpMaeprocesoelementoServicio.listado(codigo);

        }

        [HttpPost("[action]")]
        public BpMaeprocesoelemento registrar([FromBody]BpMaeprocesoelemento bean)
        {
            return bpMaeprocesoelementoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoelemento actualizar([FromBody]BpMaeprocesoelemento bean)
        {
            return bpMaeprocesoelementoServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoelemento obtenerPorId([FromBody]BpMaeprocesoelementoPk llave)
        {
            return bpMaeprocesoelementoServicio.obtenerPorId(llave);
        }

        [HttpPost("[action]")]
        public BpMaeprocesoelemento eliminar([FromBody]BpMaeprocesoelemento bean)
        {
            bpMaeprocesoelementoServicio.eliminar(bean.IdProceso, bean.IdElemento);
            return bean;

        }


        [HttpGet("[action]")]
        public List<BpMaeprocesoelemento> listaSeg()
        {
            return bpMaeprocesoelementoServicio.listaSeg();

        }
    }
}