using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using System.IO;
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.sistema.constante;
using net.royal.spring.core.dao;
using net.royal.spring.core.dao.impl;
using net.royal.spring.core.dominio;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyReporteDaoImpl : GenericoDaoImpl<SyReporte>, SyReporteDao
    {
        private IServiceProvider servicioProveedor;
        private CompanyownerrecursoDao companyownerrecursoDao;


        public SyReporteDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "syreporte")
        {
            servicioProveedor = _servicioProveedor;
            companyownerrecursoDao = servicioProveedor.GetService<CompanyownerrecursoDao>();
        }

        public SyReporte registrarReporte(UsuarioActual usuarioActual, SyReporte bean)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            SyReportePk pk = new SyReportePk()
            {
                Aplicacioncodigo = bean.Aplicacioncodigo,
                Reportecodigo = bean.Reportecodigo
            };

            SyReporte reporte = this.obtenerPorId(pk);
            if (reporte != null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El código ya existe para la aplicación seleccionada"));
            }

            if (lstRetorno.Count > 0)
            {
                throw new UException(lstRetorno);
            }

            return base.registrar(bean);
        }

        public SyReporte obtenerPorId(SyReportePk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
        public string obtenerImagenComoCadena(string compania, ConstanteReporte.TipoImagen tipoImagen, string periodo, string tipoReporte)
        {
            string urlImagen = string.Empty;
            if (tipoImagen == ConstanteReporte.TipoImagen.LOGO)
            {
                var base64 = "";
                CompanyownerrecursoPk pk = new CompanyownerrecursoPk();
                pk.Periodo = periodo == null ? "999999" : periodo;
                pk.Tiporecurso = ConstanteReporte.CODIGO_IMAGEN_COMPANIA;
                pk.Companyowner = compania;

                var recurso = companyownerrecursoDao.obtenerPorId(pk.obtenerArreglo());

                if (recurso != null) base64 = "data:image/png;base64," + Convert.ToBase64String(recurso.Recurso);

                urlImagen = base64;
            }

            if (tipoImagen == ConstanteReporte.TipoImagen.FIRMA)
            {
                var base64 = "";
                CompanyownerrecursoPk pk = new CompanyownerrecursoPk();
                pk.Periodo = periodo == null ? "999999" : periodo;
                pk.Tiporecurso = tipoReporte;
                pk.Companyowner = compania;

                var recurso = companyownerrecursoDao.obtenerPorId(pk.obtenerArreglo());

                if (recurso != null) base64 = "data:image/png;base64," + Convert.ToBase64String(recurso.Recurso);

                urlImagen = base64;
            }

            return urlImagen;
        }
        public ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion,
             DtoSyReporte filtro)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoSyReporte> lstRegistros = new List<DtoSyReporte>();

            if (UString.estaVacio(filtro.AplicacionCodigo))
                filtro.AplicacionCodigo = null;
            if (UString.estaVacio(filtro.ReporteCodigo))
                filtro.ReporteCodigo = null;
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.AplicacionCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.ReporteCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            Int32 contador = this.contar("syreporte.solicitudListarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "syreporte.solicitudListarPaginacion");

            while (_reader.Read())
            {
                DtoSyReporte bean = new DtoSyReporte();
                if (!_reader.IsDBNull(0))
                    bean.AplicacionCodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.ReporteCodigo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Nombre = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.TipoReporte = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Estado = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.AplicacionDescripcion = _reader.GetString(5);

                lstRegistros.Add(bean);
            }


            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }


        public SyReporte actualizarReporte(UsuarioActual usuarioActual, SyReporte bean)
        {
            base.actualizar(bean);
            return bean;
        }

        public List<SyReporte> listar()
        {

            IQueryable<SyReporte> query = this.getCriteria();
            query = query.Where(
                p => p.Aplicacioncodigo == "HS" 
                );

            List<SyReporte> lst = query.ToList();
            if (lst.Count > 0)
                return lst;
            return null;

        }
    }
}