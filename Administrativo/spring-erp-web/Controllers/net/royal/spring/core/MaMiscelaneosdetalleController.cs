
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
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core
{
    [Route("api/spring/core/[controller]")]
    public class MaMiscelaneosdetalleController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private MaMiscelaneosdetalleServicio maMiscelaneosdetalleServicio;

        public MaMiscelaneosdetalleController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosdetalleServicio = servicioProveedor.GetService<MaMiscelaneosdetalleServicio>();
        }

        [HttpPost("[action]")]
        public MaMiscelaneosdetalle registrar([FromBody]MaMiscelaneosdetalle maMiscelaneosdetalle)
        {

            return maMiscelaneosdetalleServicio.registrar(maMiscelaneosdetalle);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosdetalle actualizar([FromBody]MaMiscelaneosdetalle maMiscelaneosdetalle)
        { return maMiscelaneosdetalleServicio.actualizar(maMiscelaneosdetalle); }

        [HttpPost("[action]")]
        public void eliminar([FromBody]MaMiscelaneosdetallePk pk)
        {
            maMiscelaneosdetalleServicio.eliminar(pk);
        }

        [HttpPost("[action]")]
        public MaMiscelaneosdetalle obtenerPorId([FromBody]MaMiscelaneosdetallePk pk)
        {
            return maMiscelaneosdetalleServicio.obtenerPorId(pk);
        }

        [HttpGet("[action]/{codigoTabla}")]
        public List<MaMiscelaneosdetalle> listarActivos(String codigoTabla)
        {
            return maMiscelaneosdetalleServicio.listarActivos(codigoTabla);
        }

        [HttpGet("[action]/{aplicacionCodigo}/{codigoTabla}")]
        public List<MaMiscelaneosdetalle> listarActivos(String aplicacionCodigo, String codigoTabla)
        {
            return maMiscelaneosdetalleServicio.listarActivos(aplicacionCodigo, codigoTabla);
        }

        [HttpGet("[action]/{aplicacionCodigo}/{codigoTabla}")]
        public List<MaMiscelaneosdetalle> listarActivosBean(String aplicacionCodigo, String codigoTabla)
        {
            return maMiscelaneosdetalleServicio.listarActivosBean(aplicacionCodigo, codigoTabla);
        }

        [HttpGet("[action]/{aplicacionCodigo}/{compania}")]
        public List<DtoTabla> listarHeaderPorAplicacion(String aplicacionCodigo, String compania)
        {
            return maMiscelaneosdetalleServicio.listarHeaderPorAplicacion(aplicacionCodigo, compania);
        }

        [HttpGet("[action]/{aplicacionCodigo}/{codigoTabla}/{compania}")]
        public List<MaMiscelaneosdetalle> listarActivos(String aplicacioncodigo, String codigotabla, String compania)
        {
            MaMiscelaneosheaderPk pk = new MaMiscelaneosheaderPk();
            pk.Aplicacioncodigo = aplicacioncodigo;
            pk.Codigotabla = codigotabla;
            pk.Compania = compania;
            return maMiscelaneosdetalleServicio.listarActivos(pk);
        }

        //[HttpGet("[action]/{aplicacionCodigo}/{codigoTabla}/{valorCodigo1}")]
        //public List<MaMiscelaneosdetalle> listarActivosPorCodigo1(String aplicacionCodigo, String codigoTabla, String valorCodigo1) {
        //    return maMiscelaneosdetalleServicio.listarActivosPorCodigo1(aplicacionCodigo, codigoTabla, valorCodigo1);
        //}

        [HttpPost("[action]")]
        public List<MaMiscelaneosdetalle> listar([FromBody]FiltroMiscelaneosDetalle filtro)
        {
            return maMiscelaneosdetalleServicio.listar(filtro);
        }

        [HttpPost("[action]")]
        public List<MaMiscelaneosdetalle> listarEnSentencia([FromBody]FiltroMiscelaneosDetalle filtro)
        {
            return maMiscelaneosdetalleServicio.listarEnSentencia(filtro);
        }

        [HttpGet("[action]/{aplicacioncodigo}/{codigotabla}/{compania}/{codigoelemento}")]
        public String obtenerDescripcionPorId(String aplicacioncodigo, String codigotabla, String compania, String codigoelemento)
        {
            MaMiscelaneosdetallePk pk = new MaMiscelaneosdetallePk();
            pk.Aplicacioncodigo = aplicacioncodigo;
            pk.Codigotabla = codigotabla;
            pk.Compania = compania;
            pk.Codigoelemento = codigoelemento;
            return maMiscelaneosdetalleServicio.obtenerDescripcionPorId(pk);
        }

        [HttpGet("[action]/{aplicacion}/{tabla}/{elemento}")]
        public String obtenerDescripcionPorId(String aplicacion, String tabla, String elemento)
        {
            return maMiscelaneosdetalleServicio.obtenerDescripcionPorId(aplicacion, tabla, elemento);
        }

        /* PENDIENTE-DARIO
        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico parametroPaginacionGenerico, MaMiscelaneosdetalle filtro);
        */

        [HttpPost("[action]")]
        public List<MaMiscelaneosdetalle> listarDetalle([FromBody] MaMiscelaneosheaderPk llave)
        {
            return maMiscelaneosdetalleServicio.listarDetalle(llave);

        }
    }
}
