using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class ParametrosmastController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private ParametrosmastServicio parametrosmastServicio;
        public ParametrosmastController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            parametrosmastServicio = servicioProveedor.GetService<ParametrosmastServicio>();
        }

        [HttpGet("[action]")]
        public Parametrosmast obtenerPorId(ParametrosmastPk pk)
        {
            return parametrosmastServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]")]
        public JsonResult obtenerValorCadena(String parametro, String aplicacion, String compania)
        {
            var cadena = parametrosmastServicio.obtenerValorCadena(parametro, aplicacion, compania);
            return Json(new { valor = cadena });
        }

        [HttpGet("[action]")]
        public JsonResult obtenerValorInteger(String parametro, String aplicacion, String compania)
        {
            var integer = parametrosmastServicio.obtenerValorInteger(parametro, aplicacion, compania);
            return Json(new { valor = integer });
        }

        [HttpGet("[action]")]
        public JsonResult obtenerValorExplicacion(String parametro, String aplicacion, String compania)
        {
            var explicacion = parametrosmastServicio.obtenerValorExplicacion(parametro, aplicacion, compania);
            return Json(new { valor = explicacion });
        }

        [HttpGet("[action]")]
        public JsonResult obtenerValorTipoDatoFlag(String parametro, String aplicacion, String compania)
        {
            var flag = parametrosmastServicio.obtenerValorTipoDatoFlag(parametro, aplicacion, compania);
            return Json(new { valor = flag });
        }
    }
}
