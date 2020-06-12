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
    public class AsPeriodoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private AsPeriodoServicio asPeriodoServicio;
        public AsPeriodoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            asPeriodoServicio = servicioProveedor.GetService<AsPeriodoServicio>();
        }

        [HttpGet("[action]")]
        public List<AsPeriodo> listarTodos() { return asPeriodoServicio.listarTodos(); }

        [HttpGet("[action]/{empleado}/{fechadesde}/{fechahasta}")]
        public bool esPeriodoAcitvo(Decimal empleado, DateTime fechadesde, DateTime fechahasta) {
            return asPeriodoServicio.esPeriodoAcitvo(empleado, fechadesde, fechahasta); }

        [HttpGet("[action]/{Secuencia}")]
        public AsPeriodo obtenerPorId(Nullable<Decimal> Secuencia) {
            AsPeriodoPk pk = new AsPeriodoPk();
            pk.Secuencia = Secuencia;
            return asPeriodoServicio.obtenerPorId(pk); }

        [HttpGet("[action]")]
        public String obtenerPeriodo(int empleado)
        {
           return asPeriodoServicio.obtenerPeriodo(empleado);
        }
    }
}
