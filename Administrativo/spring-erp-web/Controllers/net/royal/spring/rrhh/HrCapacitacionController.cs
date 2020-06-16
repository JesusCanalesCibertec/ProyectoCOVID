using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh.controladora
{

    [Route("api/spring/rrhh/[controller]")]
    public class HrCapacitacionController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionServicio hrCapacitacionServicio;

        public HrCapacitacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionServicio = servicioProveedor.GetService<HrCapacitacionServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacion registrar([FromBody]HrCapacitacion bean)
        {
            return hrCapacitacionServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacion actualizar([FromBody]HrCapacitacion bean)
        {
            return hrCapacitacionServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacion anular(UsuarioActual usuarioActual, String pCompanyowner, String pCapacitacion)
        {
            return hrCapacitacionServicio.coreAnular(_usuarioActual, pCompanyowner, pCapacitacion);
        }

        [HttpPost("[action]")]
        public HrCapacitacion solicitudAnular([FromBody] DtoSolicitudCapacitacion bean)
        {
            return hrCapacitacionServicio.coreAnular(_usuarioActual, bean.comapnyowner, bean.capacitacionId);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner, String pCapacitacion)
        {
            hrCapacitacionServicio.coreEliminar(pCompanyowner, pCapacitacion);
        }

        [HttpGet("[action]")]
        public HrCapacitacion obtenerporid(String pCompanyowner, String pCapacitacion)
        {
            return hrCapacitacionServicio.obtenerPorId(pCompanyowner, pCapacitacion);
        }

        /*AGREGADOS POR ALEJANDRO INICIO*/

        [HttpPost("[action]")]
        public ParametroPaginacionGenerico solicitudListar([FromBody] FiltroPaginacionCapacitacion filtro)
        {
            return hrCapacitacionServicio.solicitudListar(filtro.paginacion, filtro);
        }

        [HttpPost("[action]")]
        public HrCapacitacion solicitudRegistrar([FromBody]HrCapacitacion bean)
        {
            return hrCapacitacionServicio.solicitudRegistrar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacion solicitudActualizar([FromBody]HrCapacitacion bean)
        {
            return hrCapacitacionServicio.solicitudActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacion solicitudObtenerPorId([FromBody]DtoSolicitudCapacitacion bean)
        {
            HrCapacitacionPk pk = new HrCapacitacionPk();
            pk.Companyowner = bean.comapnyowner;
            pk.Capacitacion = bean.capacitacionId;

            return hrCapacitacionServicio.solicitudObtenerPorId(pk);
        }

        [HttpPost("[action]")]
        public DtoTabla descargarAdjunto([FromBody]HrCapacitacionFoliosPk pk)
        {
            return hrCapacitacionServicio.descargarAdjunto(pk);
        }

        [HttpPost("[action]")]
        public HrCapacitacionFolios eliminarFolio([FromBody]HrCapacitacionFolios pk)
        {
            hrCapacitacionServicio.eliminarFolio(pk);
            return pk;
        }



        /*AGREGADOS POR ALEJANDRO FIN*/

        /*ERNESTO*/
        [HttpPost("[action]")]
        public List<DtoPrevencionSalud> listarPrevencionSaludCabecera([FromBody]FiltroGraficos filtro)
        {
            return hrCapacitacionServicio.listarPrevencionSaludCabecera(filtro);
        }
        [HttpPost("[action]")]
        public List<DtoPrevencionSalud> listarPrevencionSaludDetalle([FromBody]FiltroGraficos filtro)
        {
            return hrCapacitacionServicio.listarPrevencionSaludDetalle(filtro);
        }
        /*ERNESTO*/

        [HttpPost("[action]")]
        public JsonResult exportar([FromBody]HrCapacitacionPk pk)
        {
            return Json(new { url = hrCapacitacionServicio.Exportar(pk) });
        }
    }
}
