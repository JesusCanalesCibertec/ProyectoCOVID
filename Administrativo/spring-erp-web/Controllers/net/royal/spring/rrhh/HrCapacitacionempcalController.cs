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
public class HrCapacitacionempcalController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionempcalServicio hrCapacitacionempcalServicio;

        public HrCapacitacionempcalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionempcalServicio = servicioProveedor.GetService<HrCapacitacionempcalServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacionempcal registrar([FromBody]HrCapacitacionempcal bean)
        {
            return hrCapacitacionempcalServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionempcal actualizar([FromBody]HrCapacitacionempcal bean)
        {
            return hrCapacitacionempcalServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionempcal anular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionempcalServicio.coreAnular(_usuarioActual,  pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            hrCapacitacionempcalServicio.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }

        [HttpGet("[action]")]
        public HrCapacitacionempcal obtenerporid(String pCompanyowner,String pCapacitacion,Nullable<Decimal> pEmpleado,Nullable<Decimal> pEvaluacion)
        {
            return hrCapacitacionempcalServicio.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pEvaluacion);
        }

    }
}
