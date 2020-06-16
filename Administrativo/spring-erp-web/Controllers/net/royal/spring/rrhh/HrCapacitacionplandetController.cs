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
public class HrCapacitacionplandetController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private HrCapacitacionplandetServicio hrCapacitacionplandetServicio;

        public HrCapacitacionplandetController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrCapacitacionplandetServicio = servicioProveedor.GetService<HrCapacitacionplandetServicio>();
        }

        [HttpPost("[action]")]
        public HrCapacitacionplandet registrar([FromBody]HrCapacitacionplandet bean)
        {
            return hrCapacitacionplandetServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionplandet actualizar([FromBody]HrCapacitacionplandet bean)
        {
            return hrCapacitacionplandetServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public HrCapacitacionplandet anular(UsuarioActual usuarioActual, String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return hrCapacitacionplandetServicio.coreAnular(_usuarioActual,  pCompanyowner, pAnioplan, pCapacitacion);
        }

        [HttpPost("[action]")]
        public void eliminar(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            hrCapacitacionplandetServicio.coreEliminar( pCompanyowner, pAnioplan, pCapacitacion);
        }

        [HttpGet("[action]")]
        public HrCapacitacionplandet obtenerporid(String pCompanyowner,Nullable<Decimal> pAnioplan,String pCapacitacion)
        {
            return hrCapacitacionplandetServicio.obtenerPorId( pCompanyowner, pAnioplan, pCapacitacion);
        }

    }
}
