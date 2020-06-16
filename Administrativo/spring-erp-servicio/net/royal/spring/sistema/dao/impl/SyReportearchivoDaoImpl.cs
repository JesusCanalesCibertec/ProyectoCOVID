using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyReportearchivoDaoImpl : GenericoDaoImpl<SyReporteArchivo>, SyReportearchivoDao
    {
        private IServiceProvider servicioProveedor;


        public SyReportearchivoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "syreportearchivo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, DtoSyReporte filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoSyReporteArchivo> lstRegistros = new List<DtoSyReporteArchivo>();

            if (UString.estaVacio(filtro.AplicacionCodigo))
                filtro.AplicacionCodigo = null;
            if (UString.estaVacio(filtro.ReporteCodigo))
                filtro.ReporteCodigo = null;
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.AplicacionCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.ReporteCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            Int32 contador = this.contar("syreportearchivo.solicitudListarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "syreportearchivo.solicitudListarPaginacion");

            while (_reader.Read())
            {
                DtoSyReporteArchivo bean = new DtoSyReporteArchivo();
                if (!_reader.IsDBNull(0))
                    bean.AplicacionCodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.ReporteCodigo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CompaniaSocio = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.Periodo = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Version = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.AplicacionDescripcion = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Reporte = (byte[])_reader[7];

                if (bean.Reporte != null)
                {
                    bean.Aux = UTF8Encoding.UTF8.GetString(bean.Reporte);
                    bean.ReporteBase64 = Convert.ToBase64String(bean.Reporte);
                }


                lstRegistros.Add(bean);
            }

            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        //public SyReporteArchivo obtenerPorId(SyReportearchivoPk pk)
        //{
        //    SyReporteArchivo report=  base.obtenerPorId(pk.obtenerArreglo());

        //    if (report.Reporte!=null)
        //    {
        //        report.AuxString = System.Text.Encoding.UTF8.GetString(report.Reporte);
        //    }
        //    else
        //    {
        //        report.AuxString = null;
        //    }

        //    return report;
        //}

        public SyReporteArchivo registrarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            SyReportearchivoPk pk = new SyReportearchivoPk()
            {
                Aplicacioncodigo = syReportearchivo.Aplicacioncodigo,
                Reportecodigo = syReportearchivo.Reportecodigo,
                Periodo = syReportearchivo.Periodo,
                Companiasocio = syReportearchivo.Companiasocio,
                Version = syReportearchivo.Version
            };

            SyReporteArchivo reporte = this.obtenerPorId(pk.obtenerArreglo());
            if (reporte != null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Ya existe un registro con la misma información"));
            }

            if (lstRetorno.Count > 0)
            {
                throw new UException(lstRetorno);
            }

            if (!String.IsNullOrEmpty(syReportearchivo.AuxString))
            {
                syReportearchivo.Reporte = Encoding.ASCII.GetBytes(syReportearchivo.AuxString);
            }

            return base.registrar(syReportearchivo);
        }

        public SyReporteArchivo actualizarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo)
        {
            if (!String.IsNullOrEmpty(syReportearchivo.AuxString))
            {
                syReportearchivo.Reporte = Encoding.ASCII.GetBytes(syReportearchivo.AuxString);
            }
            else
            {
                syReportearchivo.Reporte = null;
            }

            base.actualizar(syReportearchivo);
            return syReportearchivo;
        }

        public void eliminarReporteArchivo(SyReportearchivoPk syReportearchivo)
        {
            SyReportearchivoPk pk = new SyReportearchivoPk()
            {
                Aplicacioncodigo = syReportearchivo.Aplicacioncodigo,
                Reportecodigo = syReportearchivo.Reportecodigo,
                Periodo = syReportearchivo.Periodo,
                Companiasocio = syReportearchivo.Companiasocio,
                Version = syReportearchivo.Version
            };

            SyReporteArchivo reporte = this.obtenerPorId(pk.obtenerArreglo());

            base.eliminar(reporte);
        }

        public string obtenerCuerpoCorreo(string idAplicacion, string idReporte, List<ParametroPersistenciaGenerico> lstMetadata)
        {
            String cuerpoCorreo = "";
            SyReporteArchivo bean = new SyReporteArchivo();
            bean = this.obtenerReportePorAplicacion(idAplicacion, idReporte);

            if (bean == null)
            {
                cuerpoCorreo = "No existe plantilla";
            }
            else
            {
                cuerpoCorreo = UByte.convertirString(bean.Reporte);
                if (lstMetadata != null)
                {
                    foreach (ParametroPersistenciaGenerico para in lstMetadata)
                    {
                        if (para.Valor != null)
                            cuerpoCorreo = cuerpoCorreo.Replace(para.Campo, (String)para.Valor);
                    }
                }
            }

            return cuerpoCorreo;
        }

        private SyReporteArchivo obtenerReportePorAplicacion(String idAplicacion, String idReporte)
        {
            IQueryable<SyReporteArchivo> cri = this.getCriteria();
            cri = cri.Where(x => x.Aplicacioncodigo == idAplicacion);
            cri = cri.Where(x => x.Reportecodigo == idReporte);

            List<SyReporteArchivo> datos = cri.ToList();

            if (!UValidador.esListaVacia(datos))
            {
                return datos[0];
            }
            return null;
        }
    }
}
