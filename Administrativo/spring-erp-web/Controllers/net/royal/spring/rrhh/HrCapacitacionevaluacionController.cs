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

namespace net.royal.spring.rrhh.controladora
{

[Route("api/spring/rrhh/[controller]")]
public class HrCapacitacionevaluacionController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionevaluacionServicio hrCapacitacionevaluacionServicio;

        public HrCapacitacionevaluacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionevaluacionServicio = servicioProveedor.GetService<HrCapacitacionevaluacionServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacionevaluacion registrar([FromBody]HrCapacitacionevaluacion bean)
        {
            return hrCapacitacionevaluacionServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionevaluacion actualizar([FromBody]HrCapacitacionevaluacion bean)
        {
            return hrCapacitacionevaluacionServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionevaluacion anular(UsuarioActual usuarioActual, String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return hrCapacitacionevaluacionServicio.coreAnular(_usuarioActual,  pCapacitacion, pSecuenciaempleado, pFactor);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            hrCapacitacionevaluacionServicio.coreEliminar( pCapacitacion, pSecuenciaempleado, pFactor);
        }

        [HttpGet("[action]")]
        public HrCapacitacionevaluacion obtenerporid(String pCapacitacion,Nullable<Decimal> pSecuenciaempleado,Nullable<Decimal> pFactor)
        {
            return hrCapacitacionevaluacionServicio.obtenerPorId( pCapacitacion, pSecuenciaempleado, pFactor);
        }

    }
}
