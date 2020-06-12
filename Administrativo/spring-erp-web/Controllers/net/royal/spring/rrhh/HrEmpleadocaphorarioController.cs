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
public class HrEmpleadocaphorarioController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrEmpleadocaphorarioServicio hrEmpleadocaphorarioServicio;

        public HrEmpleadocaphorarioController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrEmpleadocaphorarioServicio = servicioProveedor.GetService<HrEmpleadocaphorarioServicio>();
        }

        [HttpPost("[action]")]
        public HrEmpleadocaphorario registrar([FromBody]HrEmpleadocaphorario bean)
        {
            return hrEmpleadocaphorarioServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrEmpleadocaphorario actualizar([FromBody]HrEmpleadocaphorario bean)
        {
            return hrEmpleadocaphorarioServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrEmpleadocaphorario anular(UsuarioActual usuarioActual, String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadocaphorarioServicio.coreAnular(_usuarioActual,  pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            hrEmpleadocaphorarioServicio.coreEliminar( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

        [HttpGet("[action]")]
        public HrEmpleadocaphorario obtenerporid(String pCompanyowner,String pCapacitacion,Nullable<Int32> pEmpleado,Nullable<Int32> pSecuencia)
        {
            return hrEmpleadocaphorarioServicio.obtenerPorId( pCompanyowner, pCapacitacion, pEmpleado, pSecuencia);
        }

    }
}
