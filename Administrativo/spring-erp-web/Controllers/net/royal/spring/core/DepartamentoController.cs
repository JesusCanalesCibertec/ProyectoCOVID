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
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class DepartamentoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private DepartamentoServicio departamentoServicio;
        public DepartamentoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            departamentoServicio = servicioProveedor.GetService<DepartamentoServicio>();
        }

        [HttpGet("[action]")]
        public List<Departamento> listarTodos()
        {
            return departamentoServicio.listarTodos();
        }

        [HttpGet("[action]")]
        public List<Departamento> listarActivosPorPais(String idPais)
        {
            return departamentoServicio.listarActivosPorPais(idPais);
        }

        [HttpPost("[action]")]
        public List<Departamento> listar([FromBody]FiltroDepartamento filtro)
        {
            return departamentoServicio.listar(filtro);
        }


        [HttpGet("[action]")]
        public Departamento obtenerPorId(String departamento)
        {
            DepartamentoPk pk = new DepartamentoPk();
            pk.Departamento = departamento;
            return departamentoServicio.obtenerPorId(pk);
        }
    }
}
