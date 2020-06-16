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
using net.royal.spring.sistema.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyAprobacionprocesoDaoImpl : GenericoDaoImpl<SyAprobacionproceso>, SyAprobacionprocesoDao
    {
        private IServiceProvider servicioProveedor;

        public SyAprobacionprocesoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor)
            : base(context, "syaprobacionproceso")
        {
            servicioProveedor = _servicioProveedor;
        }
        /* DARIO - ya no se usan el campo nombre de servicio
        public string obtenerNombreServicioPorProcesoInterno(String Aplicacion, Int32? ProcesoInterno)
        {
            IQueryable<SyAprobacionproceso> query = this.getCriteria();

            query = query.Where(p => p.Aplicacion == Aplicacion);
            query = query.Where(p => p.Procesointerno == ProcesoInterno);

            List<SyAprobacionproceso> lt = query.ToList();
            if (lt.Count > 0)
                return lt[0].Nombreservicio;

            return null;
        }
        public string obtenerNombreServicio(SyAprobacionprocesoPk pk)
        {
            SyAprobacionproceso bean = this.obtenerPorId(pk.obtenerArreglo());
            if (bean == null)
                return null;
            return bean.Nombreservicio;
        }*/

        public List<SyAprobacionproceso> listar(FiltroAprobacionProceso filtro)
        {
            IQueryable<SyAprobacionproceso> query = this.getCriteria();

            if (!UString.estaVacio(filtro.IdAplicacion))
                query = query.Where(p => p.Aplicacion == filtro.IdAplicacion);
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            query = query.OrderBy(p => p.Proceso);     /** order **/

            return query.ToList();
        }

        public ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion, FiltroSolicitudes filtro)
        {
            String pFecha = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoSolicitud> lstRegistros = new List<DtoSolicitud>();

            if (UInteger.esCeroOrNulo(filtro.Tipo))
                filtro.Tipo = 0;
            if (UInteger.esCeroOrNulo(filtro.Nivel))
                filtro.Nivel = 0;
            if (UInteger.esCeroOrNulo(filtro.Plan))
                filtro.Plan = null;
            if (UInteger.esCeroOrNulo(filtro.IdPersonaActual))
                filtro.IdPersonaActual = null;
            if (UInteger.esCeroOrNulo(filtro.IdPersonaSolicitante))
                filtro.IdPersonaSolicitante = 0;

            if (UInteger.esCeroOrNulo(filtro.Proceso))
                filtro.Proceso = 0;
            if (UInteger.esCeroOrNulo(filtro.ProcesoInterno))
                filtro.ProcesoInterno = 0;
            if (UInteger.esCeroOrNulo(filtro.NumeroProceso))
                filtro.NumeroProceso = 0;

            if (UString.estaVacio(filtro.CompaniaSocio))
                filtro.CompaniaSocio = "%";
            if (UString.estaVacio(filtro.Aplicacion))
                filtro.Aplicacion = "%";
            if (UString.estaVacio(filtro.UnidadReplicacion))
                filtro.UnidadReplicacion = "%";
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;
            if (UString.estaVacio(filtro.Documento))
                filtro.Documento = "%";

            if (filtro.FechaDesde == null)
                filtro.FechaHasta = null;
            if (filtro.FechaDesde != null)
            {
                filtro.FechaDesde = UFechaHora.obtenerFechaHoraInicioDia(filtro.FechaDesde);
                if (filtro.FechaHasta == null)
                    filtro.FechaHasta = filtro.FechaDesde;

                filtro.FechaHasta = UFechaHora.obtenerFechaHoraFinDia(filtro.FechaHasta);
            }

            if ((filtro.FechaDesde == null) && (filtro.FechaHasta == null))
                pFecha = "%";
            else
                pFecha = "N";

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.Integer, filtro.Tipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nivel", ConstanteUtil.TipoDato.Integer, filtro.Nivel));
            parametros.Add(new ParametroPersistenciaGenerico("p_plan", ConstanteUtil.TipoDato.Integer, filtro.Plan));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona_actual", ConstanteUtil.TipoDato.Integer, filtro.IdPersonaActual));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona_solicitante", ConstanteUtil.TipoDato.Integer, filtro.IdPersonaSolicitante));
            parametros.Add(new ParametroPersistenciaGenerico("p_proceso_interno", ConstanteUtil.TipoDato.Integer, filtro.ProcesoInterno));
            parametros.Add(new ParametroPersistenciaGenerico("p_proceso", ConstanteUtil.TipoDato.Integer, filtro.Proceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_numero_proceso", ConstanteUtil.TipoDato.Integer, filtro.NumeroProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania_socio", ConstanteUtil.TipoDato.String, filtro.CompaniaSocio)); // usuarioActual.CompaniaSocioCodigo
            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.Aplicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_unidad_replicacion", ConstanteUtil.TipoDato.String, filtro.UnidadReplicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_documento", ConstanteUtil.TipoDato.String, filtro.Documento));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_desde", ConstanteUtil.TipoDato.DateTime, filtro.FechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha_hasta", ConstanteUtil.TipoDato.DateTime, filtro.FechaHasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.String, pFecha));
            parametros.Add(new ParametroPersistenciaGenerico("p_uuid", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioUniqueIdString));

            this.ejecutarPorQuery("syaprobacionproceso.prepararSolicitudes", parametros);

            parametros.Clear();
            parametros.Add(new ParametroPersistenciaGenerico("p_uuid", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioUniqueIdString));

            Int32 contador = this.contar("syaprobacionproceso.listarSolicitudesContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "syaprobacionproceso.listarSolicitudesPaginacion");
            while (_reader.Read())
            {
                DtoSolicitud bean = new DtoSolicitud();
                if (!_reader.IsDBNull(0))
                    bean.Secuencia = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.AplicacionCodigo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.AplicacionNombre = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.ProcesoCodigo = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.ProcesoNombre = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.ProcesoNro = _reader.GetInt32(5);
                if (!_reader.IsDBNull(6))
                    bean.NivelActual = _reader.GetInt32(6);
                if (!_reader.IsDBNull(7))
                    bean.NivelAprobar = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.DocumentoReferencia = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.DocumentoFecha = _reader.GetDateTime(9);
                if (!_reader.IsDBNull(10))
                    bean.CompaniaCodigo = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.CompaniaNombre = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.UnidadReplicacionCodigo = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.UnidadReplicacionNombre = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.SolicitanteId = _reader.GetInt32(14);
                if (!_reader.IsDBNull(15))
                    bean.SolicitanteNombre = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.EstadoCodigo = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.EstadoNombre = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.Observaciones = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.FechaSolicitud = _reader.GetDateTime(19);
                if (!_reader.IsDBNull(20))
                    bean.Uuid = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.ProcesoAprobar = _reader.GetInt32(21);
                if (!_reader.IsDBNull(22))
                    bean.ProcesoInternoAprobar = _reader.GetInt32(22);
                if (!_reader.IsDBNull(23))
                    bean.Llave = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.Capacitaciones = _reader.GetString(24);
                if (!_reader.IsDBNull(25))
                    bean.ProcesoNro = _reader.GetInt32(25);

                lstRegistros.Add(bean);
            }
            this.dispose();

            this.ejecutarPorQuery("syaprobacionproceso.eliminarSolicitudes", parametros);

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        public List<SyAprobacionproceso> listarCodigoProcesoPorCodigoProcesoBase(string codigoProcesoBase)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<SyAprobacionproceso> lstRegistros = new List<SyAprobacionproceso>();

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo_proceso_base", ConstanteUtil.TipoDato.String, codigoProcesoBase));

            _reader = this.listarPorQuery("syaprobacionproceso.listarCodigoProcesoPorCodigoProcesoBase", parametros);

            while (_reader.Read())
            {
                SyAprobacionproceso bean = new SyAprobacionproceso();
                if (!_reader.IsDBNull(0))
                    bean.Codigoproceso = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Procesointerno = _reader.GetInt32(1);

                lstRegistros.Add(bean);
            }

            this.dispose();

            return lstRegistros;
        }

        public void SincronizarHorarios(string companiaSocioCodigo)
        {
            this.ejecutarPorQuery("syaprobacionproceso.SincronizarHorarios", new List<ParametroPersistenciaGenerico>() { new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, companiaSocioCodigo) });
        }
    }
}
