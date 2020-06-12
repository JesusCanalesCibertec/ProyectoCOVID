using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.constante;
using net.royal.spring.framework.core;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.sistema.servicio.impl
{

    public class SyReporteServicioImpl : GenericoServicioImpl, SyReporteServicio
    {

        private IServiceProvider servicioProveedor;
        private SyReporteDao syReporteDao;
        private ParametrosmastDao parametrosmastDao;
        private SyReportearchivoDao syReportearchivoDao;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SyReporteServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            syReporteDao = servicioProveedor.GetService<SyReporteDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            syReportearchivoDao = servicioProveedor.GetService<SyReportearchivoDao>();
            _hostingEnvironment = hostingEnvironment;
        }

        public byte[] ejecutarReporte(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros)
        {
            DtoReporteConfiguracionResultado config = crearReporteEnServidor(reporte);
            return ejecutarReporte(config, lstParametros);
        }

        public string[] obtenerTextoReporte(SyReportearchivoPk pk)
        {
            SyReporte syReporte = syReporteDao.obtenerPorId(new SyReportePk() { Aplicacioncodigo = pk.Aplicacioncodigo, Reportecodigo = pk.Reportecodigo });

            if (syReporte == null)
            {
                return new String[] { "No se encuentra el reporte " + pk.Aplicacioncodigo + " - " + pk.Reportecodigo, "Título" };
            }

            SyReporteArchivo syReportearchivo = syReportearchivoDao.obtenerPorId(pk);
            if (syReportearchivo == null)
            {
                return new String[] { "No se encuentra el reporte " + pk.Aplicacioncodigo + " - " + pk.Reportecodigo, "Título" };
            }
            String cuerpo = UByte.convertirString(syReportearchivo.Reporte);
            return new String[] { cuerpo, syReporte.Descripcionlocal };
        }

        public String ejecutarReporteComoUrlTemporal(UsuarioActual usuarioctual, DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros)
        {
            string urlArchivo = null;
            String rutaArchivo = null;
            string nombreArchivo = null;
            DtoReporteConfiguracionResultado config = crearReporteEnServidor(reporte);
            byte[] respuesta = ejecutarReporte(config, lstParametros);

            if (respuesta == null)
                return null;

            if (usuarioctual == null)
                nombreArchivo = UFechaHora.obtenerNombreUnico();
            else
                nombreArchivo = UString.obtenerValorCadenaSinNulo(usuarioctual.UsuarioLogin) + "-" + UFechaHora.obtenerNombreUnico();
            if (!UString.estaVacio(config.SyReporte.Tiporeporte))
            {
                nombreArchivo = nombreArchivo + "." + config.SyReporte.Tiporeporte;
            }


            urlArchivo = "";

            if (!UString.estaVacio(config.SyReporte.CarpetaTemporal))
            {
                rutaArchivo = _hostingEnvironment.WebRootPath + UFile.getSeparador() + config.SyReporte.CarpetaTemporal + nombreArchivo;
                urlArchivo = urlArchivo + "/" + config.SyReporte.CarpetaTemporal + nombreArchivo;
            }
            else
            {
                rutaArchivo = _hostingEnvironment.WebRootPath + UFile.getSeparador() + "Archivos" + UFile.getSeparador() + "Temporales" + UFile.getSeparador() + nombreArchivo;
                urlArchivo = urlArchivo + "/" + "Archivos" + "/" + "Temporales" + "/" + nombreArchivo;
            }

            File.WriteAllBytes(rutaArchivo, respuesta);

            return urlArchivo;
        }

        private byte[] ejecutarReporte(DtoReporteConfiguracionResultado config, List<ParametroPersistenciaGenerico> lstParametros)
        {
            String _tipoReporte = "HTML";

            if (config == null)
                return null;
            if (config.SyReporte == null)
                _tipoReporte = "HTML";
            else
                _tipoReporte = config.SyReporte.Tiporeporte;

            if (_tipoReporte.Equals(ConstanteReporte.TIPO_FORMATO_HTML))
            {
                String cuerpoCorreo = UByte.convertirString(config.Archivo);

                cuerpoCorreo = cuerpoCorreo.Replace("&lt;", "<").Replace("&gt;", ">");

                foreach (ParametroPersistenciaGenerico para in lstParametros)
                {
                    if (para.Valor != null)
                        cuerpoCorreo = cuerpoCorreo.Replace("[" + para.Campo + "]", (String)para.Valor.ToString());
                    else
                        cuerpoCorreo = cuerpoCorreo.Replace("[" + para.Campo + "]", "");
                }
                return UByte.convertirByte(cuerpoCorreo);
            }

            if (config.SyReporte.Tiporeporte.Equals(ConstanteReporte.TIPO_REPORTE_JASPER))
            {

            }


            /** si no se tiene ningun tipo en particular**/
            return null;
        }
        private String obtenerAsunto(List<ParametroPersistenciaGenerico> lstParametros, String parametro)
        {
            foreach (ParametroPersistenciaGenerico p in lstParametros)
            {
                if (p.Campo.Equals(parametro))
                {
                    String r = "";
                    if (p.Valor != null)
                        r = (String)p.Valor;
                    return r;
                }
            }
            return "";
        }
        public List<Email> generarListaCorreo(DtoReporteParametro reporte, List<ParametroPersistenciaGenerico> lstParametros, List<DtoCorreo> listaCorreos)
        {
            /**
             * si en lstParametros viene el asunto se adiciona al asunto del correo
             * ConstanteCorreo.PARAMETRO_ASUNTO
             */
            String asuntoParametro = obtenerAsunto(lstParametros, ConstanteCorreo.PARAMETRO_ASUNTO);

            String asuntoExterno = "";
            DtoReporteConfiguracionResultado config = crearReporteEnServidor(reporte);
            List<Email> listaEmails = new List<Email>();
            byte[] cuerpoCorreo = ejecutarReporte(config, lstParametros);
            foreach (DtoCorreo email in listaCorreos)
            {
                Email e = new Email();
                asuntoExterno = "Sin asunto";
                if (config.SyReporte != null)
                {
                    asuntoExterno = UString.obtenerValorCadenaSinNulo(config.SyReporte.Descripcionlocal).Trim();
                }
                e.Asunto = asuntoExterno;
                if (!UString.estaVacio(asuntoParametro))
                    e.Asunto = e.Asunto + " | " + asuntoParametro.Trim();

                e.ListaCorreoDestino.Add(new EmailDestino(email.correo.Trim()));
                e.CuerpoCorreo = cuerpoCorreo;
                e.compania = email.compania;
                e.empleado = email.empleado;
                listaEmails.Add(e);
            }
            return listaEmails;
        }

        private DtoReporteConfiguracionResultado crearReporteEnServidor(DtoReporteParametro reporte)
        {
            SyReporteArchivo syReportearchivo = null;
            DtoReporteConfiguracionResultado config = new DtoReporteConfiguracionResultado();
            byte[] archivo = null;
            String rutaReportes = parametrosmastDao.obtenerValorExplicacion(ConstanteReporte.PARAMETRO_RUTA_REPORTES_WEB, ConstanteReporte.APLICACION_CODIGO);
            String nombreReporte = null;

            SyReporte syReporte = syReporteDao.obtenerPorId(new SyReportePk(reporte.AplicacionCodigo, reporte.ReporteCodigo));
            if (syReporte == null)
            {
                config.FlgOk = false;
                config.Archivo = Encoding.ASCII.GetBytes("Reporte no encontrado : " + UString.obtenerValorCadenaSinNulo(reporte.AplicacionCodigo) + " - " + UString.obtenerValorCadenaSinNulo(reporte.ReporteCodigo));
                return config;
                throw new Exception("Reporte no encontrado : " + UString.obtenerValorCadenaSinNulo(reporte.AplicacionCodigo) + " - " + UString.obtenerValorCadenaSinNulo(reporte.ReporteCodigo));
            }
            if (UString.estaVacio(syReporte.Tiporeporte))
            {
                config.FlgOk = false;
                config.Archivo = Encoding.ASCII.GetBytes("Reporte no tiene especificado un tipo");
                return config;
                throw new Exception("Reporte no tiene especificado un tipo");
            }
            if (UString.estaVacio(rutaReportes))
            {
                config.FlgOk = false;
                config.Archivo = Encoding.ASCII.GetBytes("No existe una ruta en donde se encuentren los reportes fisicos:" + ConstanteReporte.PARAMETRO_RUTA_REPORTES_WEB);
                return config;
                throw new Exception("No existe una ruta en donde se encuentren los reportes fisicos");
            }


            nombreReporte = reporte.getNombreCompletoReporte();
            if (syReporte.Tiporeporte.Equals(ConstanteReporte.TIPO_FORMATO_HTML))
                nombreReporte = nombreReporte + ".html";
            if (syReporte.Tiporeporte.Equals(ConstanteReporte.TIPO_REPORTE_JASPER))
                nombreReporte = nombreReporte + ".jasper";

            if (File.Exists(rutaReportes + nombreReporte))
            {
                //archivo = File.ReadAllBytes(rutaReportes + nombreReporte);
                //config.RutaCompletaReporte = rutaReportes + nombreReporte;
                //config.SyReporte = syReporte;
                //config.FlgOk = true;
                //         config.Archivo = archivo;
                //         return config;
                File.Delete(rutaReportes + nombreReporte);
            }

            syReportearchivo = syReportearchivoDao.obtenerPorId(new SyReportearchivoPk(reporte.AplicacionCodigo, reporte.ReporteCodigo,
                reporte.CompaniaSocio, reporte.Periodo, reporte.Version).obtenerArreglo());
            if (syReportearchivo == null)
                syReportearchivo = syReportearchivoDao.obtenerPorId(new SyReportearchivoPk(reporte.AplicacionCodigo, reporte.ReporteCodigo,
                    "999999", "999999", "999999").obtenerArreglo());

            if (syReportearchivo == null)
            {
                config.FlgOk = false;
                config.Archivo = Encoding.ASCII.GetBytes("Reporte no tiene ningun Archivo : " + UString.obtenerValorCadenaSinNulo(reporte.AplicacionCodigo) + "-" + UString.obtenerValorCadenaSinNulo(reporte.ReporteCodigo) + "  " + UString.obtenerValorCadenaSinNulo(reporte.CompaniaSocio) + "-" + UString.obtenerValorCadenaSinNulo(reporte.Periodo) + "-" + UString.obtenerValorCadenaSinNulo(reporte.Version));
                return config;
                //throw new UException("Reporte no tiene ningun Archivo : " + UString.obtenerValorCadenaSinNulo(reporte.AplicacionCodigo) + "-" + UString.obtenerValorCadenaSinNulo(reporte.ReporteCodigo) + "  " + UString.obtenerValorCadenaSinNulo(reporte.CompaniaSocio) + "-" + UString.obtenerValorCadenaSinNulo(reporte.Periodo) + "-" + UString.obtenerValorCadenaSinNulo(reporte.Version));
            }

            archivo = syReportearchivo.Reporte;
            if (archivo != null)
            {
                //File.WriteAllBytes(rutaReportes + nombreReporte, archivo);
            }
            else
            {
                archivo = Encoding.ASCII.GetBytes("Reporte archivo no tiene contenido");
            }

            config.RutaCompletaReporte = rutaReportes + nombreReporte;
            config.SyReporte = syReporte;
            config.FlgOk = true;
            config.Archivo = archivo;

            return config;
            //return null;
        }

        public string obtenerImagenComoCadena(string compania, ConstanteReporte.TipoImagen tipoImagen, string periodo, string tipoRecurso)
        {
            return syReporteDao.obtenerImagenComoCadena(compania, tipoImagen, periodo, tipoRecurso);
        }

        public ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, DtoSyReporte filtro)
        {
            return syReporteDao.listarPorPaginacion(paginacion, filtro);
        }

        public SyReporte registrarReporte(UsuarioActual usuarioActual, SyReporte bean)
        {
            return syReporteDao.registrarReporte(usuarioActual, bean);
        }

        public SyReporte obtenerPorId(SyReportePk pk)
        {
            return syReporteDao.obtenerPorId(pk);
        }

        public SyReporte actualizarReporte(UsuarioActual usuarioActual, SyReporte bean)
        {
            return syReporteDao.actualizarReporte(usuarioActual, bean);
        }

        public SyReporte actualizar(SyReporte recurso)
        {
            syReporteDao.actualizar(recurso);
            return recurso;
        }

        public string crearReporteUrl(string nombreArchivo, byte[] archivo)
        {
            string urlArchivo = "";


            string rutaArchivo = _hostingEnvironment.WebRootPath + UFile.getSeparador() + "Archivos" + UFile.getSeparador() + "Temporales" + UFile.getSeparador() + nombreArchivo;
            urlArchivo = urlArchivo + "/" + "Archivos" + "/" + "Temporales" + "/" + nombreArchivo;



            File.WriteAllBytes(rutaArchivo, archivo);

            return urlArchivo;

        }

        public List<SyReporte> listar()
        {
            return syReporteDao.listar();
        }
    }

}