using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SeguridadconceptoController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SeguridadconceptoServicio seguridadconceptoServicio;
        public SeguridadconceptoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            seguridadconceptoServicio = servicioProveedor.GetService<SeguridadconceptoServicio>();
        }

        [HttpGet("[action]/{Aplicacioncodigo}/{Grupo}/{Concepto}")]
        public Seguridadconcepto obtenerPorId(String Aplicacioncodigo, String Grupo,String Concepto) {
            SeguridadconceptoPk pk = new SeguridadconceptoPk();
            pk.Aplicacioncodigo = Aplicacioncodigo;
            pk.Grupo = Grupo;
            pk.Concepto = Concepto;
            return seguridadconceptoServicio.obtenerPorId(pk);
        }
    }
}
