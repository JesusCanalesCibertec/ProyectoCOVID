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
public class HrEmpleadocapacitacionController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEmpleadocapacitacionServicio hrEmpleadocapacitacionServicio;

        public HrEmpleadocapacitacionController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEmpleadocapacitacionServicio = servicioProveedor.GetService<HrEmpleadocapacitacionServicio>();
        }

        [HttpPost("[action]")]
        public HrEmpleadocapacitacion registrar([FromBody]HrEmpleadocapacitacion bean)
        {
            return hrEmpleadocapacitacionServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrEmpleadocapacitacion actualizar([FromBody]HrEmpleadocapacitacion bean)
        {
            return hrEmpleadocapacitacionServicio.coreActualizar(_usuarioActual,bean);
        }


        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado)
        {
            hrEmpleadocapacitacionServicio.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado);
        }

        [HttpGet("[action]")]
        public HrEmpleadocapacitacion obtenerporid(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado)
        {
            return hrEmpleadocapacitacionServicio.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado);
        }

    }
}
