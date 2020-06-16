using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.asistencia.dominio.filtro;
using net.royal.spring.framework.core;

namespace net.royal.spring.asistencia.dao.impl
{
    public class AsAccesosdiariosDaoImpl : GenericoDaoImpl<AsAccesosdiarios>, AsAccesosdiariosDao
    {
        private IServiceProvider servicioProveedor;


        public AsAccesosdiariosDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asaccesosdiarios")
        {
            servicioProveedor = _servicioProveedor;
        }

        public AsAccesosdiarios obtenerPorId(AsAccesosdiariosPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }


        public DtoMarcaciones traerMarcas(FiltroPaginacionEmpleado filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.fechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.fechaHasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_jefe", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoJefe));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));

            _reader = this.listarPorQuery("asaccesosdiarios.listarMarcas", parametros);

            while (_reader.Read())
            {

                if (!_reader.IsDBNull(0))
                    bean.HorasExtrasAcumuladas = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.HorasDebeAcumuladas = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.HorasFavor = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.HorasExtrasRango = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.HorasDebeRango = _reader.GetString(4);

            }

            this.dispose();

            return bean;
        }

        public List<DtoMarcaciones> listarMarcaciones(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoMarcaciones> lstResultado = new List<DtoMarcaciones>();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, fechahasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("asaccesosdiarios.listarMarcaciones", parametros);

            while (_reader.Read())
            {
                DtoMarcaciones bean = new DtoMarcaciones();

                if (!_reader.IsDBNull(0))
                    bean.fecha = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Dia = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.HoraIngreso = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.HoraSalidaRef = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.HoraRegresoRef = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.HoraSalida = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.HoraLaborada = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Empleado = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.Nombre = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.Sede = _reader.GetString(9);

                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;
        }

        public bool existeMarca(decimal empleado, DateTime fecha)
        {
            bool resultado = false;
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();

            _parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.DateTime, fecha));

            _reader = this.listarPorQuery("asaccesosdiarios.contarmarca", _parametros);


            while (_reader.Read())
            {

                if (!_reader.IsDBNull(0))
                    resultado = _reader.GetInt32(0) > 0 ? true : false;

            }
            this.dispose();
            return resultado;
        }

        public ParametroPaginacionGenerico listarMarcacionesConsolidado(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoMarcaciones> lstResultado = new List<DtoMarcaciones>();
            Int32 contador = 0;

            if (filtro.EmpleadoId == 0)
            {
                filtro.EmpleadoId = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, filtro.fechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, filtro.fechaHasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_jefe", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoJefe));
            parametros.Add(new ParametroPersistenciaGenerico("p_unidadoperativa", ConstanteUtil.TipoDato.String, filtro.IdUnidadOperativa));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));


            _reader = this.listarPorQuery("asaccesosdiarios.listarMarcacionesConsolidado", parametros);

            while (_reader.Read())
            {
                DtoMarcaciones bean = new DtoMarcaciones();

                if (!_reader.IsDBNull(0))
                    bean.idrow = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Semana = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.desde = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.hasta = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.Lunes = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Martes = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Miercoles = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Jueves = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Viernes = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.Sabado = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.Domingo = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.HorasSemana = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.HorasComp = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.HorasNoAut = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.HorasTard = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.HorasSalAnt = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.Empleado = _reader.GetInt32(16);
                if (!_reader.IsDBNull(17))
                    bean.Nombre = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    contador = _reader.GetInt32(18);

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }


        public ParametroPaginacionGenerico listarMarcaciones(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoMarcaciones> lstResultado = new List<DtoMarcaciones>();
            Int32 contador = 0;

            if (filtro.EmpleadoId == 0)
            {
                filtro.EmpleadoId = null;
            }

            if (filtro.EmpleadoJefe==null)
            {
                filtro.EmpleadoJefe = filtro.EmpleadoId;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, filtro.fechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, filtro.fechaHasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_jefe", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoJefe));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_unidadoperativa", ConstanteUtil.TipoDato.String, filtro.IdUnidadOperativa));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));

            this.listarPorQuery("asaccesosdiarios.listarMarcaciones", parametros);

            while (_reader.Read())
            {
                DtoMarcaciones bean = new DtoMarcaciones();

                if (!_reader.IsDBNull(0))
                    bean.fecha = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Dia = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.HoraIngreso = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.HoraSalidaRef = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.HoraRegresoRef = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.HoraSalida = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.HoraLaborada = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Empleado = _reader.GetInt32(7);
                if (!_reader.IsDBNull(8))
                    bean.Nombre = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.Sede = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.HorasAutorizadas = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.HorasNoAutorizadas = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    contador = _reader.GetInt32(12);



                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }


        public List<DtoMarcaciones> listarMarcacionesConsolidado(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoMarcaciones> lstResultado = new List<DtoMarcaciones>();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, fechahasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("asaccesosdiarios.listarMarcacionesConsolidado", parametros);

            while (_reader.Read())
            {
                DtoMarcaciones bean = new DtoMarcaciones();

                if (!_reader.IsDBNull(0))
                    bean.idrow = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Semana = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.desde = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.hasta = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.Lunes = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.Martes = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Miercoles = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Jueves = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Viernes = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.Sabado = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.Domingo = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.HorasSemana = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.HorasExtras = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.HorasDebe = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.HorasFavor = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.Empleado = _reader.GetInt32(15);
                if (!_reader.IsDBNull(16))
                    bean.Nombre = _reader.GetString(16);

                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;
        }

        public DtoMarcaciones traerMarcas(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, fechahasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("asaccesosdiarios.listarMarcas", parametros);

            while (_reader.Read())
            {

                if (!_reader.IsDBNull(0))
                    bean.HorasExtrasAcumuladas = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.HorasExtrasRango = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.HorasDebeAcumuladas = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.HorasDebeRango = _reader.GetString(3);

            }

            this.dispose();

            return bean;
        }

        public ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroAsAccesosDiarios filtroAsAccesosDiarios)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoMarcaciones> lst = new List<DtoMarcaciones>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, filtroAsAccesosDiarios.Empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtroAsAccesosDiarios.EmpleadoCompania));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.DateTime, filtroAsAccesosDiarios.fechaDesde.HasValue ? filtroAsAccesosDiarios.fechaDesde.Value : new DateTime(1800, 1, 1)));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.DateTime, filtroAsAccesosDiarios.fechaHasta.HasValue ? filtroAsAccesosDiarios.fechaHasta.Value : new DateTime(3000, 12, 31)));


            Int32 contador = this.contar("asaccesosdiarios.contaraccesos", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "asaccesosdiarios.listaraccesos");

            while (_reader.Read())
            {
                DtoMarcaciones bean = new DtoMarcaciones();
                if (!_reader.IsDBNull(0))
                    bean.EmpleadoD = _reader.GetDecimal(0);
                if (!_reader.IsDBNull(1))
                    bean.secuencia = _reader.GetDecimal(1);
                if (!_reader.IsDBNull(2))
                    bean.fechaD = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.hora = _reader.GetDateTime(3);
                if (!_reader.IsDBNull(4))
                    bean.observacion = _reader.GetString(4);
                lst.Add(bean);
            }
            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;
            return paginacion;
        }

        public void registrar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios)
        {
            if (UString.estaVacio(usuarioActual.carnetId))
            {
                return;
            }
            asAccesosDiarios.Carnetid = "" + usuarioActual.carnetId;
            asAccesosDiarios.Empleado = usuarioActual.PersonaId;
            asAccesosDiarios.Secuencia = generarSecuencia(asAccesosDiarios);
            asAccesosDiarios.UltimaFechaModif = DateTime.Now;
            asAccesosDiarios.UltimoUsuario = usuarioActual.UsuarioLogin;
            registrar(asAccesosDiarios);
        }

        private int? generarSecuencia(AsAccesosdiarios asAccesosDiarios)
        {
            IQueryable<AsAccesosdiarios> query = getCriteria();
            query = query.Where(x =>
            x.Carnetid == asAccesosDiarios.Carnetid
            && x.Fecha.Value.Year == asAccesosDiarios.Fecha.Value.Year
            && x.Fecha.Value.Month == asAccesosDiarios.Fecha.Value.Month
            && x.Fecha.Value.Day == asAccesosDiarios.Fecha.Value.Day
            );

            decimal? max = query.Max(x => x.Secuencia);
            return max == null ? 1 : (int)max + 1;
        }

        public void actualizar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios)
        {
            IQueryable<AsAccesosdiarios> queryable = getCriteria();
            queryable = queryable.Where(x => x.Empleado == usuarioActual.PersonaId.Value && x.Secuencia == asAccesosDiarios.Secuencia);
            queryable = queryable.Where(x => x.Fecha.Value.Year == asAccesosDiarios.Fecha.Value.Year);
            queryable = queryable.Where(x => x.Fecha.Value.Month == asAccesosDiarios.Fecha.Value.Month);
            queryable = queryable.Where(x => x.Fecha.Value.Day == asAccesosDiarios.Fecha.Value.Day);

            AsAccesosdiarios bean = queryable.FirstOrDefault();

            bean.Hora = asAccesosDiarios.Hora;
            bean.Observacion = asAccesosDiarios.Observacion;
            bean.UltimaFechaModif = DateTime.Now;
            bean.UltimoUsuario = usuarioActual.UsuarioLogin;
            actualizar(bean);
        }

        public bool validarSolicitudOmision(DateTime? fecha, int empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, fecha.Value));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));

            _reader = this.listarPorQuery("asaccesosdiarios.validarSolicitudOmision", parametros);

            var hay = true;

            while (_reader.Read())
            {
                hay = false;
            }

            this.dispose();

            return hay;
        }

        public int contarMarcasPorDia(decimal empleado, DateTime value2)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, value2));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));

            _reader = this.listarPorQuery("asaccesosdiarios.contarMarcasPorDia", parametros);

            int cant = 0;

            while (_reader.Read())
            {
                cant = _reader.GetInt32(0);
            }

            this.dispose();

            return cant;
        }

        public int contarMarcasPorDiaHorasLibres(decimal empleado, DateTime value2, string conceptoacceso)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, value2));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, conceptoacceso));

            _reader = this.listarPorQuery("asaccesosdiarios.contarMarcasPorDiaHorasLibres", parametros);

            int cant = 0;

            while (_reader.Read())
            {
                cant = _reader.GetInt32(0);
            }

            this.dispose();

            return cant;
        }

        public int contarMarcasPorRango(decimal empleado, string conceptoacceso, DateTime inicio, DateTime fin)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.Date, inicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, fin));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, conceptoacceso));

            _reader = this.listarPorQuery("asaccesosdiarios.contarMarcasPorRango", parametros);

            int cant = 0;

            while (_reader.Read())
            {
                cant = _reader.GetInt32(0);
            }

            this.dispose();

            return cant;
        }

        public List<AsAccesosdiarios> obtenerMarcasPorDia(int persona, string compania, DateTime? inicio, DateTime? fin)
        {

            if (!inicio.HasValue)
            {
                return new List<AsAccesosdiarios>();
            }
            if (!fin.HasValue)
            {
                fin = inicio;
            }

            var inicioRango = new DateTime(inicio.Value.Year, inicio.Value.Month, inicio.Value.Day, 0, 0, 0);

            var finRango = new DateTime(fin.Value.Year, fin.Value.Month, fin.Value.Day, 23, 59, 59);

            IQueryable<AsAccesosdiarios> queryable = getCriteria();
            queryable = queryable.Where(x => x.Empleado == persona);
            queryable = queryable.Where(x => x.Fecha >= inicioRango && x.Fecha <= finRango);
            queryable = queryable.OrderBy(x => x.Fecha);

            return queryable.ToList();
        }

        public int listarCruces(DateTime fechaInicio, DateTime fechaFin, decimal? empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.DateTime, fechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.DateTime, fechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));

            // int cant = this.contar("asaccesosdiarios.listarCruces", parametros);
            int cant = 0;

            this.dispose();

            return cant;
        }

        public int listarCrucesConceptos(DateTime fechaInicio, DateTime fechaFin, decimal? empleado, string concepto)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoMarcaciones bean = new DtoMarcaciones();

            parametros.Add(new ParametroPersistenciaGenerico("p_fechainicio", ConstanteUtil.TipoDato.DateTime, fechaInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.DateTime, fechaFin));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, concepto));

            // int cant = this.contar("asaccesosdiarios.listarCrucesConceptos", parametros);
            int cant = 0;

            this.dispose();

            return cant;
        }
    }
}
