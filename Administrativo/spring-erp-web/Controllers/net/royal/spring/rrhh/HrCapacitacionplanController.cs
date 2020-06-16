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
public class HrCapacitacionplanController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionplanServicio hrCapacitacionplanServicio;

        public HrCapacitacionplanController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionplanServicio = servicioProveedor.GetService<HrCapacitacionplanServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacionplan registrar([FromBody]HrCapacitacionplan bean)
        {
            return hrCapacitacionplanServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionplan actualizar([FromBody]HrCapacitacionplan bean)
        {
            return hrCapacitacionplanServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionplan anular(UsuarioActual usuarioActual, String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return hrCapacitacionplanServicio.coreAnular(_usuarioActual,  pCompanyowner, pAnioplan);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            hrCapacitacionplanServicio.coreEliminar( pCompanyowner, pAnioplan);
        }

        [HttpGet("[action]")]
        public HrCapacitacionplan obtenerporid(String pCompanyowner,Nullable<Decimal> pAnioplan)
        {
            return hrCapacitacionplanServicio.obtenerPorId( pCompanyowner, pAnioplan);
        }

    }
}
