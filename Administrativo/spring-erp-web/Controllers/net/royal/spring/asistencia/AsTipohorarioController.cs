using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.framework.web.controller;

namespace net.royal.spring.asistencia
{
    [Route("api/spring/asistencia/[controller]")]
    public class AsTipohorarioController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsTipohorarioServicio asTipohorarioServicio;
        public AsTipohorarioController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asTipohorarioServicio = servicioProveedor.GetService<AsTipohorarioServicio>();
        }

        [HttpGet("[action]")]
        public List<AsTipohorario> listarTodos() { return asTipohorarioServicio.listarTodos(); }

        [HttpGet("[action]/{Tipohorario}")]
        public AsTipohorario obtenerPorId(Int32 tipohorario) {
            AsTipohorarioPk pk = new AsTipohorarioPk();
            pk.Tipohorario = tipohorario;
            return asTipohorarioServicio.obtenerPorId(pk); }

        [HttpGet("[action]/{tipohorario}")]
        public string[] listarHorasSemana(int? tipohorario) { return asTipohorarioServicio.listarHorasSemana(tipohorario); }
    }
}
