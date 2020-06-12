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
public class HrEmpleadoasistenciasController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEmpleadoasistenciasServicio hrEmpleadoasistenciasServicio;

        public HrEmpleadoasistenciasController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEmpleadoasistenciasServicio = servicioProveedor.GetService<HrEmpleadoasistenciasServicio>();
        }

        [HttpPost("[action]")]
        public HrEmpleadoasistencias registrar([FromBody]HrEmpleadoasistencias bean)
        {
            return hrEmpleadoasistenciasServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrEmpleadoasistencias actualizar([FromBody]HrEmpleadoasistencias bean)
        {
            return hrEmpleadoasistenciasServicio.coreActualizar(_usuarioActual,bean);
        }


        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            hrEmpleadoasistenciasServicio.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

        [HttpGet("[action]")]
        public HrEmpleadoasistencias obtenerporid(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadoasistenciasServicio.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

    }
}
