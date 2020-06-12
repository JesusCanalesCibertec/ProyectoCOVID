using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.core.servicio;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.core.dominio.dto;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class PersonamastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private PersonamastServicio personamastServicio;
        public PersonamastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            personamastServicio = servicioProveedor.GetService<PersonamastServicio>();
        }

        [HttpGet("[action]/{personaId}")]
        public string obtenerNombrePorPk(int? personaId)
        {
            PersonamastPk pk = new PersonamastPk();
            pk.Persona = personaId;
            return personamastServicio.obtenerNombrePorPk(pk);
        }

        [HttpGet("[action]")]
        public DtoEmpleadoBasico esJefePorUnidadOperativa(int? personaId)
        {

            DtoEmpleadoBasico empleado = new DtoEmpleadoBasico();
            empleado.esJefe = UBoolean.validarBoolean(personamastServicio.esJefePorUnidadOperativa(personaId));
            return empleado;
        }

        [HttpGet("[action]")]
        public DtoTabla obtenerNombrePorJefeUnidadOperativa(string unidadoperativa)
        {
            return personamastServicio.obtenerNombrePorJefeUnidadOperativa(unidadoperativa);
        }

        [HttpGet("[action]")]
        public Personamast obtenerPorId(int? personaId)
        {
            PersonamastPk pk = new PersonamastPk();
            pk.Persona = personaId;
            return personamastServicio.obtenerPorId(pk);
        }


        [HttpPost("[action]")]
        public Personamast registrar([FromBody]Personamast personamast)
        {
            return personamastServicio.registrar(personamast);
        }

        [HttpPost("[action]")]
        public Personamast actualizar([FromBody]Personamast personamast)
        {
            return personamastServicio.actualizar(personamast);
        }

        [HttpPost("[action]/{personaId}")]
        public void eliminar(int? personaId)
        {
            PersonamastPk pk = new PersonamastPk();
            pk.Persona = personaId;
            personamastServicio.eliminar(pk);
        }


        /* PENDIENTE-DARIO
        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionPersona filtro);
        */
    }
}
