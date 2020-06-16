using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.controller;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net.royal.spring.minedu
{
    [Route("api/spring/minedu/[controller]")]
    public class   MpProyectorecursoController:SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private MpProyectorecursoServicio MpProyectorecursoServicio;

        public MpProyectorecursoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor):base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            MpProyectorecursoServicio = servicioProveedor.GetService<MpProyectorecursoServicio>();
        }


      

        [HttpPost("[action]")]
        public MpProyectorecurso Registrar([FromBody]MpProyectorecurso bean)
        {
            return MpProyectorecursoServicio.registrar(_usuarioActual, bean);
        }

        [HttpGet("[action]")]
        public MpProyectorecurso ObtenerPorId(int pIdPersona)
        {
            return MpProyectorecursoServicio.obtenerPorId(pIdPersona);
        }


        [HttpPost("[action]")]
        public MpProyectorecurso Actualizar([FromBody]MpProyectorecurso bean)
        {
            return MpProyectorecursoServicio.actualizar(_usuarioActual, bean);
        }

      
    }
}
