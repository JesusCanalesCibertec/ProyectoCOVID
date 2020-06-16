using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.controladora
{

[Route("api/spring/programasocial/[controller]")]
public class PsConsumoNutricionalController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsConsumoNutricionalServicio psConsumoNutricionalServicio;

        public PsConsumoNutricionalController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psConsumoNutricionalServicio = servicioProveedor.GetService<PsConsumoNutricionalServicio>();
        }

        [HttpPost("[action]")]
        public PsConsumoNutricional registrar([FromBody]PsConsumoNutricional bean)
        {
            return psConsumoNutricionalServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsConsumoNutricional actualizar([FromBody]PsConsumoNutricional bean)
        {
            return psConsumoNutricionalServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public PsConsumoNutricional eliminar([FromBody] PsConsumoNutricional bean)
        {
            psConsumoNutricionalServicio.coreEliminar(bean.IdInstitucion,bean.IdConsumo, bean.IdConsumoNutricional);
            return bean;
        }

        [HttpGet("[action]")]
        public PsConsumoNutricional obtenerporid(PsConsumoNutricionalPk llave)
        {
            return psConsumoNutricionalServicio.obtenerPorId(llave.IdInstitucion, llave.IdConsumo, llave.IdConsumoNutricional);
        }


        [HttpPost("[action]")]
        public List<DtoConsumoNutricional> listardetalle([FromBody] PsConsumoPk llave)
        {
            return psConsumoNutricionalServicio.listardetalle(llave);
            
        }







    }
}
