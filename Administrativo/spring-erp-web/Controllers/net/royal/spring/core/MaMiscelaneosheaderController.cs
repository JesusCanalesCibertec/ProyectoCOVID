
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.core.dominio;
using net.royal.spring.core.servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net.royal.spring.framework.web.controller;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core
    
{
    [Route("api/spring/core/[controller]")]
    public class MaMiscelaneosheaderController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private MaMiscelaneosheaderServicio maMiscelaneosheaderServicio;

        public MaMiscelaneosheaderController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosheaderServicio = servicioProveedor.GetService<MaMiscelaneosheaderServicio>();
        }

        [HttpGet("[action]")]
        public MaMiscelaneosheader obtenerPorId(Nullable<Int32> pPregunta)
        {
            return maMiscelaneosheaderServicio.obtenerPorId(pPregunta);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosheader actualizar([FromBody]MaMiscelaneosheader bean)
        {
            return maMiscelaneosheaderServicio.coreActualizar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosheader actualizarMiscelaneo([FromBody]MaMiscelaneosheader bean)
        {
            return maMiscelaneosheaderServicio.actualizarMiscelaneo(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosheader registrar([FromBody]MaMiscelaneosheader bean)
        {
            return maMiscelaneosheaderServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosheader registrarPregunta([FromBody]MaMiscelaneosheader bean)
        {
            return maMiscelaneosheaderServicio.registrarPregunta(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public void eliminar(Nullable<Int32> pPregunta)
        {
            maMiscelaneosheaderServicio.coreEliminar(pPregunta);
        }

        [HttpPost("[action]")]
        public DtoEncuestapregunta eliminarPregunta([FromBody] DtoEncuestapregunta bean)
        {

            maMiscelaneosheaderServicio.eliminarPregunta(bean.pregunta);
            return bean;
        }

       
        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroMiscelaneosheader filtro)
        {
            return maMiscelaneosheaderServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpGet("[action]")]
        public MaMiscelaneosheader solicitudObtenerPorId(string aplicacion, string compania, string codigo)
        {

            return maMiscelaneosheaderServicio.solicitudObtenerPorId(aplicacion,compania,codigo);
        }
    }
}
