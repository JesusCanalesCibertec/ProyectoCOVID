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
public class HrCapacitacionevalController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionevalServicio hrCapacitacionevalServicio;

        public HrCapacitacionevalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionevalServicio = servicioProveedor.GetService<HrCapacitacionevalServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacioneval registrar([FromBody]HrCapacitacioneval bean)
        {
            return hrCapacitacionevalServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacioneval actualizar([FromBody]HrCapacitacioneval bean)
        {
            return hrCapacitacionevalServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacioneval anular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionevalServicio.coreAnular(_usuarioActual,  pCompanyowner, pCapacitacion, pEvaluacion);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            hrCapacitacionevalServicio.coreEliminar( pCompanyowner, pCapacitacion, pEvaluacion);
        }

        [HttpGet("[action]")]
        public HrCapacitacioneval obtenerporid(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionevalServicio.obtenerPorId( pCompanyowner, pCapacitacion, pEvaluacion);
        }

    }
}
