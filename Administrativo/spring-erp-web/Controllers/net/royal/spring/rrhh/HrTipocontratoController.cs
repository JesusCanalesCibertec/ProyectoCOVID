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
    public class HrTipocontratoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private HrTipocontratoServicio hrTipocontratoServicio;
        public HrTipocontratoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            hrTipocontratoServicio = servicioProveedor.GetService<HrTipocontratoServicio>();
        }

        [HttpGet("[action]")]
        public HrTipocontrato obtenerPorId(String tipocontrato)
        {
            HrTipocontratoPk pk = new HrTipocontratoPk();
            pk.Tipocontrato = tipocontrato;
            return hrTipocontratoServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public List<HrTipocontrato> listarTodos()
        {
            return hrTipocontratoServicio.listarTodos();
        }

        [HttpPost("[action]")]
        public List<HrTipocontrato> listar([FromBody]DtoFiltro filtro)
        {
            return hrTipocontratoServicio.listar(filtro);
        }

        [HttpGet("[action]")]
        public List<HrTipocontrato> listarActivos()
        {
            return hrTipocontratoServicio.listarActivos();
        }
    }
}
