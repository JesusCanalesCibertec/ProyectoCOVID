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
using net.royal.spring.core.dominio.dto;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class CompanyownerrecursoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private CompanyownerrecursoServicio companyownerrecursoServicio;
        public CompanyownerrecursoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            companyownerrecursoServicio = servicioProveedor.GetService<CompanyownerrecursoServicio>();
        }

        [HttpPost("[action]")]
        public DtoRecursoConfiguracionResultado obtenerRecursoEnServidor(DtoRecursoParametro reporte)
        {
            return companyownerrecursoServicio.obtenerRecursoEnServidor(reporte);
        }

        [HttpPost("[action]")]
        public Companyownerrecurso registrar([FromBody]Companyownerrecurso bean)
        {
            return companyownerrecursoServicio.registrar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public Companyownerrecurso actualizar([FromBody]Companyownerrecurso bean)
        {
            return companyownerrecursoServicio.actualizar(_usuarioActual, bean);
        }

        [HttpGet("[action]/{tipoRecurso}")]
        public IList<Companyownerrecurso> listarPorRecurso(String tipoRecurso)
        {
            return companyownerrecursoServicio.listarPorRecurso(tipoRecurso);
        }

        [HttpPost("[action]")]
        public void eliminarPorTipoRecurso([FromBody]Companyownerrecurso bean)
        {
            companyownerrecursoServicio.eliminarPorTipoRecurso(bean.Tiporecurso);
        }

        [HttpGet("[action]/{tiporecurso}/{companyowner}/{periodo}")]
        public Companyownerrecurso obtenerPorId(String tiporecurso, String companyowner, String periodo)
        {
            CompanyownerrecursoPk pk = new CompanyownerrecursoPk();
            pk.Tiporecurso = tiporecurso;
            pk.Periodo = periodo;
            pk.Companyowner = companyowner;

            return companyownerrecursoServicio.obtenerPorId(pk);
        }

    }
}
