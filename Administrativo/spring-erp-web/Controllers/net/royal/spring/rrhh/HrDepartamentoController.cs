using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.servicio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.web.controller;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.rrhh
{
    [Route("api/spring/rrhh/[controller]")]
    public class HrDepartamentoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private HrDepartamentoServicio hrDepartamentoServicio;
        public HrDepartamentoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrDepartamentoServicio = servicioProveedor.GetService<HrDepartamentoServicio>();
        }

        [HttpPost("[action]")]
        public List<HrDepartamento> listar([FromBody]DtoFiltro filtro)
        {
            return hrDepartamentoServicio.listar(filtro);
        }


        [HttpGet("[action]")]
        public HrDepartamento obtenerPorId(String departamento)
        {
            HrDepartamentoPk pk = new HrDepartamentoPk();
            pk.Departamento = departamento;
            return hrDepartamentoServicio.obtenerPorId(pk);
        }
    }
}
