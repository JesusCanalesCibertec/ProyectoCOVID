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
public class PsBeneficiarioIngresoController : SecuredBaseController {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioIngresoServicio PsBeneficiarioIngresoServicio;

        public PsBeneficiarioIngresoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            PsBeneficiarioIngresoServicio = servicioProveedor.GetService<PsBeneficiarioIngresoServicio>();
        }

        [HttpPost("[action]")]
        public PsBeneficiarioIngreso registrar([FromBody]PsBeneficiarioIngreso bean)
        {
            return PsBeneficiarioIngresoServicio.coreInsertar(_usuarioActual,bean);
        }

        [HttpPost("[action]")] 
        public PsBeneficiarioIngreso actualizar([FromBody]PsBeneficiarioIngreso bean)
        {
            return PsBeneficiarioIngresoServicio.coreActualizar(_usuarioActual,bean);
        }

        [HttpPost("[action]")]
        public PsBeneficiarioIngreso obtenerUltimoIngreso([FromBody] PsBeneficiarioIngresoPk llave)
        {
            return PsBeneficiarioIngresoServicio.obtenerUltimoIngreso(llave);
        }

        /*
        [HttpPost("[action]")]
        public PsBeneficiarioIngreso eliminar([FromBody] PsBeneficiarioIngreso bean)
        {

            PsBeneficiarioIngresoServicio.eliminar(bean.IdInstitucion,bean.IdArea);
            return bean;
        }

        [HttpPost("[action]")]
        public PsBeneficiarioIngreso obtenerPorId([FromBody] PsBeneficiarioIngresoPk llave)
        {
            return PsBeneficiarioIngresoServicio.obtenerPorId(llave);
        }
        */
        [HttpGet("[action]")]
        public List<DtoBeneficiarioIngreso> listado(Int32 pIdBeneficiario, string pIdInstitucion)
        {
            return PsBeneficiarioIngresoServicio.listado(pIdBeneficiario, pIdInstitucion);

        }
        



    }
}
