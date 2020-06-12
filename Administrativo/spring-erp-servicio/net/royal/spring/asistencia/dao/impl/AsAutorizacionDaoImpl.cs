using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.web.ui;
using net.royal.spring.core.dao;
using net.royal.spring.asistencia.constante;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.core.dominio;

namespace net.royal.spring.asistencia.dao.impl
{
    public class AsAutorizacionDaoImpl : GenericoDaoImpl<AsAutorizacion>, AsAutorizacionDao
    {
        private IServiceProvider servicioProveedor;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private AsConceptoaccesoDao asConceptoaccesoDao;
        private EmpleadomastDao empleadomastDao;
        private AsTipohorarioDao asTipohorarioDao;

        public AsAutorizacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asautorizacion")
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            asConceptoaccesoDao = servicioProveedor.GetService<AsConceptoaccesoDao>();
            empleadomastDao = servicioProveedor.GetService<EmpleadomastDao>();
            asTipohorarioDao = servicioProveedor.GetService<AsTipohorarioDao>();
        }

        public List<DtoTabla> listarCruces(AsAutorizacion autorizacion)
        {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            //_parametros.Add(new ParametroPersistenciaGenerico("empleado", ConstanteUtil.TipoDato.Integer, autorizacion.Empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(autorizacion.Empleado)));

            _parametros.Add(new ParametroPersistenciaGenerico("fechaDesde", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(autorizacion.Fecha, "yyyy-MM-dd HH:mm:ss.fff")));
            _parametros.Add(new ParametroPersistenciaGenerico("horaDesde", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(autorizacion.Desde, "yyyy-MM-dd HH:mm:ss.fff")));
            _parametros.Add(new ParametroPersistenciaGenerico("fechaHasta", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(autorizacion.Fechafin, "yyyy-MM-dd HH:mm:ss.fff")));
            _parametros.Add(new ParametroPersistenciaGenerico("horaHasta", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(autorizacion.Hasta, "yyyy-MM-dd HH:mm:ss.fff")));
            _parametros.Add(new ParametroPersistenciaGenerico("numeroProceso", ConstanteUtil.TipoDato.Integer, autorizacion.Numeroproceso));

            _reader = this.listarPorQuery("asautorizacion.listarCruces", _parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.Codigo = _reader.GetString(0);

                lst.Add(bean);
            }
            this.dispose();
            return lst;


        }

        public List<DtoPermisos> listarPermisos(DateTime? fechadesde, DateTime? fechahasta, int empleado, string compania, string conceptoacceso, string estado, DateTime? fecharegistro, string unidad, bool conJefe)
        {
            if (UString.estaVacio(conceptoacceso))
            {
                conceptoacceso = null;
            }

            if (UString.estaVacio(estado))
            {
                estado = null;
            }

            if (UString.estaVacio(unidad))
            {
                unidad = null;
            }

            if (UString.estaVacio(compania))
            {
                compania = null;
            }

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPermisos> lst = new List<DtoPermisos>();

            _parametros.Add(new ParametroPersistenciaGenerico("fechadesde", ConstanteUtil.TipoDato.String, fechadesde != null ? UFechaHora.convertirFechaCadena(fechadesde, "yyyy-MM-dd HH:mm:ss.fff") : null));
            _parametros.Add(new ParametroPersistenciaGenerico("fechahasta", ConstanteUtil.TipoDato.String, fechahasta != null ? UFechaHora.convertirFechaCadena(fechahasta, "yyyy-MM-dd HH:mm:ss.fff") : null));
            _parametros.Add(new ParametroPersistenciaGenerico("fecharegistro", ConstanteUtil.TipoDato.String, fecharegistro != null ? UFechaHora.convertirFechaCadena(fecharegistro, "yyyy-MM-dd HH:mm:ss.fff") : null));
            _parametros.Add(new ParametroPersistenciaGenerico("empleado", ConstanteUtil.TipoDato.Integer, empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("compania", ConstanteUtil.TipoDato.String, compania));
            _parametros.Add(new ParametroPersistenciaGenerico("conceptoacceso", ConstanteUtil.TipoDato.String, conceptoacceso));
            _parametros.Add(new ParametroPersistenciaGenerico("estado", ConstanteUtil.TipoDato.String, estado));
            _parametros.Add(new ParametroPersistenciaGenerico("unidad", ConstanteUtil.TipoDato.String, unidad));

            if (conJefe)
            {
                _reader = this.listarPorQuery("asautorizacion.listarPermisos", _parametros);

            }
            else
            {
                _reader = this.listarPorQuery("asautorizacion.listarPermisosSinSerJefe", _parametros);
            }
            while (_reader.Read())
            {
                DtoPermisos bean = new DtoPermisos();

                if (!_reader.IsDBNull(0))
                    bean.fechaSolicitud = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.tipoautorizacion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                {
                    if (_reader.GetValue(2).GetType().Name == "Decimal")
                        bean.codigo = Convert.ToInt32(_reader.GetDecimal(2));
                    if (_reader.GetValue(2).GetType().Name == "Int32")
                        bean.codigo = _reader.GetInt32(2);
                }

                if (!_reader.IsDBNull(3))
                    bean.inicio = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.fin = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.concepto = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.desde = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.hasta = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.autorizadopor = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.nombre = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.personaant = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.docidentidad = _reader.GetString(11);

                if (!_reader.IsDBNull(12))
                    bean.dias = _reader.GetInt32(12).ToString();

                if (!_reader.IsDBNull(13))
                    bean.autogenerado = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.conceptoacceso = _reader.GetString(14);

                if (!_reader.IsDBNull(15))
                    bean.estado = _reader.GetString(15);

                if (!_reader.IsDBNull(16))
                    bean.motivorechazo = _reader.GetString(16);

                if (!_reader.IsDBNull(17))
                    bean.numeroproceso = _reader.GetInt32(17);

                if (!_reader.IsDBNull(18))
                    bean.fecha = _reader.GetDateTime(18);

                if (!_reader.IsDBNull(19))
                    bean.desdeDate = _reader.GetDateTime(19);


                if (!_reader.IsDBNull(20))
                    bean.expresadoen = _reader.GetString(20);

                lst.Add(bean);
            }
            this.dispose();
            return lst;


        }

        public List<DtoPermisosDetalleAutorizacion> listarPermisosDetalleAutorizacion(int empleado, string conceptoacceso)
        {
            if (UString.estaVacio(conceptoacceso))
                conceptoacceso = null;

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPermisosDetalleAutorizacion> lst = new List<DtoPermisosDetalleAutorizacion>();

            _parametros.Add(new ParametroPersistenciaGenerico("empleado", ConstanteUtil.TipoDato.Integer, empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("conceptoacceso", ConstanteUtil.TipoDato.String, conceptoacceso));

            _reader = this.listarPorQuery("asautorizacion.listarPermisosDetalleAutorizacion", _parametros);

            while (_reader.Read())
            {
                DtoPermisosDetalleAutorizacion bean = new DtoPermisosDetalleAutorizacion();

                if (!_reader.IsDBNull(0))
                    bean.fecha = _reader.GetDateTime(0);

                if (!_reader.IsDBNull(1))
                    bean.fechafin = _reader.GetDateTime(1);

                if (!_reader.IsDBNull(2))
                    bean.autorizadopor = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.fechaautorizacion = _reader.GetDateTime(3);
                bean.fechaautorizacion = _reader.GetDateTime(3);

                if (!_reader.IsDBNull(4))
                    bean.observacion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.numdias = _reader.GetInt32(5);

                lst.Add(bean);
            }
            this.dispose();
            return lst;


        }

        public List<DtoPermisosDetalleMarcas> listarPermisosDetalleMarcas(int empleado, DateTime? fechadesde, DateTime? fechahasta)
        {

            String fecdesde = fechadesde.Value.ToString("yyyy-MM-dd");
            String fechasta = fechahasta.Value.ToString("yyyy-MM-dd");

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPermisosDetalleMarcas> lst = new List<DtoPermisosDetalleMarcas>();

            _parametros.Add(new ParametroPersistenciaGenerico("empleado", ConstanteUtil.TipoDato.Integer, empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("desde", ConstanteUtil.TipoDato.String, fecdesde));
            _parametros.Add(new ParametroPersistenciaGenerico("hasta", ConstanteUtil.TipoDato.String, fechasta));

            _reader = this.listarPorQuery("asautorizacion.listarPermisosDetalleMarcas", _parametros);

            while (_reader.Read())
            {
                DtoPermisosDetalleMarcas bean = new DtoPermisosDetalleMarcas();

                if (!_reader.IsDBNull(0))
                    bean.fecha = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.hora = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.carnet = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.codigoempleado = _reader.GetInt32(3);

                if (!_reader.IsDBNull(4))
                    bean.nombre = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.estacion = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.tipo = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.observacion = _reader.GetString(7);

                lst.Add(bean);
            }
            this.dispose();
            return lst;


        }

        public AsAutorizacion obtenerPorNumeroProceso(int numeroproceso)
        {
            IQueryable<AsAutorizacion> query = this.getCriteria();
            query = query.Where(p => p.Numeroproceso == numeroproceso);
            List<AsAutorizacion> lst = query.ToList();

            if (lst.Count == 1)
                return lst[0];

            return null;

        }

        public String obtenerNoGeneraAsistencia(decimal empleado, String compania)
        {
            String resultado = null;
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();

            _parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            _parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, compania));

            _reader = this.listarPorQuery("asautorizacion.obtenerNoGeneraAsistencia", _parametros);


            while (_reader.Read())
            {

                if (!_reader.IsDBNull(0))
                    resultado = _reader.GetString(0);

            }
            this.dispose();
            return resultado;
        }


        public AsAutorizacion obtenerPorAutogenerado(int autogenerado)
        {
            IQueryable<AsAutorizacion> query = this.getCriteria();
            query = query.Where(p => p.Autogenerado == autogenerado);
            List<AsAutorizacion> lst = query.ToList();

            if (lst.Count == 1)
                return lst[0];

            return null;

        }

        public bool validacionFechaCumpleanosNuevo(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, concepto));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, periodo));

            contador = this.contar("asautorizacion.contarValidarFechaCumpleanosNuevo", parametros);

            if (contador == 0)
            {
                return true;
            }

            return false;
        }

        public bool validacionFechaCumpleanosEdicion(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, concepto));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechaini", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, fechahasta));

            contador = this.contar("asautorizacion.contarValidarFechaCumpleanosEdicion", parametros);

            if (contador == 0)
            {
                return true;
            }

            return false;
        }

        public AsAutorizacion registrarPermiso(AsAutorizacion autorizacion)
        {
            this.registrar(autorizacion);
            return autorizacion;
        }

        public AsAutorizacion solicitudActualizar(AsAutorizacion autorizacion)
        {
            this.actualizar(autorizacion);
            return autorizacion;
        }

        public AsAutorizacion obtenerPorId(AsAutorizacionPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public AsAutorizacion solicitudAnular(UsuarioActual usuarioActual, Int32 numeroproceso)
        {
            List<MensajeUsuario> lst = solicitudValidarAccion(usuarioActual, ConstanteUI.ACCION_SOLICITADA_ANULAR, numeroproceso);

            if (lst.Count > 0)
                throw new UException(lst);

            AsAutorizacion capa = this.obtenerPorNumeroProceso(numeroproceso);

            // capa.Estado = ConstanteAutorizacion.ESTADO_ANULADO;
            capa.Ultimafechamodif = DateTime.Today;
            capa.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(capa);
            return capa;
        }

        public void solicitudEliminar(UsuarioActual usuarioActual, Int32 numeroproceso)
        {
            List<MensajeUsuario> lst = solicitudValidarAccion(usuarioActual, ConstanteUI.ACCION_SOLICITADA_ELIMINAR, numeroproceso);

            if (lst.Count > 0)
                throw new UException(lst);

            AsAutorizacion capa = this.obtenerPorNumeroProceso(numeroproceso);

            this.eliminar(capa);
            return;
        }

        public int obtenerNumeroProceso()
        {
            IQueryable<AsAutorizacion> query = this.getCriteria();
            int? var = query.Select(p => p.Numeroproceso).DefaultIfEmpty(0).Max();
            return var == null ? 1 : var.Value + 1;
        }
        public List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, string accionSolicitada, Int32 numeroproceso)
        {
            String estadoNombre = null;
            String msg = null;
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            if (UString.estaVacio(accionSolicitada))
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, ConstanteUI.MSG_ACCION_REQUERIDA));
                return lstRetorno;
            }

            AsAutorizacion capa = this.obtenerPorNumeroProceso(numeroproceso);

            if (accionSolicitada.Equals(ConstanteUI.ACCION_SOLICITADA_EDITAR))
            {
                if (capa == null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, AsAutorizacion.MSG_ASAUTORIZACION_ESREQUERIDO));
                    return lstRetorno;
                }

                if (!capa.Estado.Equals(ConstanteAutorizacion.ESTADO_SOLICITUD))
                {
                    estadoNombre = maMiscelaneosdetalleDao.obtenerDescripcion(ConstanteAutorizacion.APLICACION_CODIGO,
                            ConstanteAutorizacion.MISCELANEO_ESTADO, capa.Estado);
                    estadoNombre = UString.estaVacio(estadoNombre) ? "" : estadoNombre;
                    msg = "No se puede editar la solicitud";
                    msg = msg + ",la solicitud se encuentra en estado :  " + estadoNombre;
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                    return lstRetorno;
                }

                if (capa.Nivelaprobacion != null)
                {
                    if (capa.Nivelaprobacion > 0)
                    {
                        msg = "No se puede editar la solicitud";
                        msg = msg + ", la solicitud ya ha sido aprobada por uno de los niveles de aprobación";
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                        return lstRetorno;
                    }
                }

            }
            if (accionSolicitada.Equals(ConstanteUI.ACCION_SOLICITADA_ANULAR))
            {
                if (capa == null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, AsAutorizacion.MSG_ASAUTORIZACION_ESREQUERIDO));
                    return lstRetorno;
                }
                if (!ConstanteAutorizacion.ESTADO_SOLICITUD.Equals(capa.Estado))
                {
                    estadoNombre = maMiscelaneosdetalleDao.obtenerDescripcion(ConstanteAutorizacion.APLICACION_CODIGO,
                            ConstanteAutorizacion.MISCELANEO_ESTADO, capa.Estado);
                    estadoNombre = UString.estaVacio(estadoNombre) ? "" : estadoNombre;
                    msg = "No se puede anular la solicitud";
                    msg = msg + ",la solicitud se encuentra en estado : " + estadoNombre;
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                    return lstRetorno;
                }

                if (capa.Nivelaprobacion != null)
                {
                    if (capa.Nivelaprobacion > 0)
                    {
                        msg = "No se puede anular la solicitud";
                        msg = msg + ", la solicitud ya ha sido aprobada por uno de los niveles de aprobación";
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                        return lstRetorno;
                    }
                }
            }
            if (accionSolicitada.Equals(ConstanteUI.ACCION_SOLICITADA_ELIMINAR))
            {
                if (capa == null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, AsAutorizacion.MSG_ASAUTORIZACION_ESREQUERIDO));
                    return lstRetorno;
                }
                if (!ConstanteAutorizacion.ESTADO_SOLICITUD.Equals(capa.Estado))
                {
                    estadoNombre = maMiscelaneosdetalleDao.obtenerDescripcion(ConstanteAutorizacion.APLICACION_CODIGO,
                            ConstanteAutorizacion.MISCELANEO_ESTADO, capa.Estado);
                    msg = "No se puede eliminar la solicitud";
                    msg = msg + ",la solicitud se encuentra en estado :" + estadoNombre;
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                    return lstRetorno;
                }

                if (capa.Nivelaprobacion != null)
                {
                    if (capa.Nivelaprobacion > 0)
                    {
                        msg = "No se puede eliminar la solicitud";
                        msg = msg + ", la solicitud ya ha sido aprobada por uno de los niveles de aprobación";
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, msg));
                        return lstRetorno;
                    }
                }
            }
            return lstRetorno;
        }

        public int contarCuponeras(Decimal empleado, DateTime fecha, String accion)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, fecha));

            if (accion == "NUEVO")
            {
                contador = this.contar("asautorizacion.contarCupoerasNuevo", parametros);
            }

            if (accion == "EDITAR")
            {
                contador = this.contar("asautorizacion.contarCupoerasEditar", parametros);
            }

            return contador;
        }

        public bool validacionFechaIngresoNuevo(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, concepto));
            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, periodo));

            contador = this.contar("asautorizacion.contarValidarFechaIngresoNuevo", parametros);

            if (contador == 0)
            {
                return true;
            }

            return false;
        }

        public bool validacionFechaIngresoEdicion(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Decimal, empleado));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, concepto));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechaini", ConstanteUtil.TipoDato.Date, fechadesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechafin", ConstanteUtil.TipoDato.Date, fechahasta));

            contador = this.contar("asautorizacion.contarValidarFechaIngresoEdicion", parametros);

            if (contador == 0)
            {
                return true;
            }

            return false;
        }

        public ParametroPaginacionGenerico listarPermisosReporte(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            if (UString.estaVacio(filtro.EmpleadoConceptoAcceso))
            {
                filtro.EmpleadoConceptoAcceso = null;
            }

            if (UString.estaVacio(filtro.EmpleadoEstado))
            {
                filtro.EmpleadoEstado = null;
            }

            if (UString.estaVacio(filtro.IdUnidadOperativa))
            {
                filtro.IdUnidadOperativa = null;
            }

            if (UInteger.esCeroOrNulo(filtro.EmpleadoId))
            {
                filtro.EmpleadoId = null;
            }

            Int32 contador = 0;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPermisos> lst = new List<DtoPermisos>();


            parametros.Add(new ParametroPersistenciaGenerico("p_fechadesde", ConstanteUtil.TipoDato.Date, filtro.fechaDesde));
            parametros.Add(new ParametroPersistenciaGenerico("p_fechahasta", ConstanteUtil.TipoDato.Date, filtro.fechaHasta));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, filtro.IdCompaniaSocio));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoId));
            parametros.Add(new ParametroPersistenciaGenerico("p_jefe", ConstanteUtil.TipoDato.Integer, filtro.EmpleadoJefe));
            parametros.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, filtro.EmpleadoUsuario));
            parametros.Add(new ParametroPersistenciaGenerico("p_unidadoperativa", ConstanteUtil.TipoDato.String, filtro.IdUnidadOperativa));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, filtro.EmpleadoConceptoAcceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, "A"));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));



            _reader = this.listarPorQuery("asautorizacion.listarPermisosReporte", parametros);

            while (_reader.Read())
            {
                DtoPermisos bean = new DtoPermisos();

                if (!_reader.IsDBNull(0))
                    bean.fechaSolicitud = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.tipoautorizacion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                {
                    if (_reader.GetValue(2).GetType().Name == "Decimal")
                        bean.codigo = Convert.ToInt32(_reader.GetDecimal(2));
                    if (_reader.GetValue(2).GetType().Name == "Int32")
                        bean.codigo = _reader.GetInt32(2);
                }

                if (!_reader.IsDBNull(3))
                    bean.inicio = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.fin = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.concepto = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.desde = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.hasta = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.autorizadopor = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.nombre = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.personaant = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.docidentidad = _reader.GetString(11);

                if (!_reader.IsDBNull(12))
                    bean.dias = _reader.GetInt32(12).ToString();

                if (!_reader.IsDBNull(13))
                    bean.autogenerado = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.conceptoacceso = _reader.GetString(14);

                if (!_reader.IsDBNull(15))
                    bean.estado = _reader.GetString(15);

                if (!_reader.IsDBNull(16))
                    bean.motivorechazo = _reader.GetString(16);

                if (!_reader.IsDBNull(17))
                    bean.numeroproceso = _reader.GetInt32(17);

                if (!_reader.IsDBNull(18))
                    bean.fecha = _reader.GetDateTime(18);

                if (!_reader.IsDBNull(19))
                    bean.desdeDate = _reader.GetDateTime(19);

                if (!_reader.IsDBNull(20))
                    bean.observacion = _reader.GetString(20);

                if (!_reader.IsDBNull(21))
                    contador = _reader.GetInt32(21);


                lst.Add(bean);
            }
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            this.dispose();

            return paginacion;

        }

        public bool contarCumpleanios(decimal? empleado, DateTime fechaComparar)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, fechaComparar));

            contador = this.contar("asautorizacion.contarCumpleanios", parametros);

            if (contador > 0)
            {
                return true;
            }

            return false;
        }

        public bool contarCuponerasHorasLibres(decimal? empleado, DateTime fechaComparar)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, fechaComparar));

            contador = this.contar("asautorizacion.contarCuponerasHorasLibres", parametros);

            if (contador > 0)
            {
                return true;
            }

            return false;
        }


        public bool contarVacaciones(decimal? empleado, DateTime fechaComparar, DateTime fechaComparar2)
        {
            Int32 contador = 0;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_inicio", ConstanteUtil.TipoDato.Date, fechaComparar));
            parametros.Add(new ParametroPersistenciaGenerico("p_fin", ConstanteUtil.TipoDato.Date, fechaComparar2));

            contador = this.contar("asautorizacion.contarVacaciones", parametros);

            if (contador == 0)
            {
                return false;
            }

            return true;
        }

        public int obtenerAutorizacionPorId(AsAutorizacionPk pk)
        {

            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();

            _parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(pk.Empleado)));
            _parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(pk.Fecha, "yyyy-MM-dd HH:mm:ss.fff")));
            _parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, pk.Conceptoacceso));
            _parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.String, UFechaHora.convertirFechaCadena(pk.Desde, "yyyy-MM-dd HH:mm:ss.fff")));

            Int32 cantidad = this.contar("asautorizacion.obtenerPorId", _parametros);

            return cantidad;
        }

        public string obtenerComportamientoSobretiempo(int empleado)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPermisos> lst = new List<DtoPermisos>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, empleado));


            _reader = this.listarPorQuery("asautorizacion.obtenerComportamientoSobretiempo", parametros);

            DtoPermisos bean = new DtoPermisos();
            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    bean.comportamientoSobretiempo = _reader.GetString(0);
            }
            this.dispose();
            return bean.comportamientoSobretiempo;
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
            if (UString.estaVacio(filtro.Concepto))
                filtro.Concepto = null;

            if (UString.estaVacio(filtro.Aprobar1))
                filtro.Aprobar1 = null;
            if (UString.estaVacio(filtro.Aprobar2))
                filtro.Aprobar2 = null;

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
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, filtro.Concepto));
            parametros.Add(new ParametroPersistenciaGenerico("solicitud", ConstanteUtil.TipoDato.DateTime, filtro.FechaSolicitud));


            this.ejecutarPorQuery("asautorizacion.prepararSolicitudes", parametros);

            parametros.Clear();
            parametros.Add(new ParametroPersistenciaGenerico("p_uuid", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioUniqueIdString));
            parametros.Add(new ParametroPersistenciaGenerico("p_aprobar1", ConstanteUtil.TipoDato.String, filtro.Aprobar1));
            parametros.Add(new ParametroPersistenciaGenerico("p_aprobar2", ConstanteUtil.TipoDato.String, filtro.Aprobar2));

            Int32 contador = this.contar("asautorizacion.listarSolicitudesContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "asautorizacion.listarSolicitudesPaginacion");
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

                if (!_reader.IsDBNull(26))
                    bean.Inicio = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.Fin = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.Concepto = _reader.GetString(28);
                if (!_reader.IsDBNull(29))
                    bean.Desde = _reader.GetString(29);
                if (!_reader.IsDBNull(30))
                    bean.Hasta = _reader.GetString(30);
                if (!_reader.IsDBNull(31))
                    bean.Autorizado = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.Aprobar = _reader.GetString(32);


                lstRegistros.Add(bean);
            }
            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

        public AsAutorizacion obtenerPorDbPk(AsAutorizacionPk asAutorizacionPk)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            AsAutorizacion bean = null;


            parametros.Add(new ParametroPersistenciaGenerico("p_fecha", ConstanteUtil.TipoDato.Date, asAutorizacionPk.Fecha));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, asAutorizacionPk.Desde));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(asAutorizacionPk.Empleado)));
            parametros.Add(new ParametroPersistenciaGenerico("p_concepto", ConstanteUtil.TipoDato.String, asAutorizacionPk.Conceptoacceso));

            _reader = this.listarPorQuery("asautorizacion.obtenerPorDbPk", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    bean = new AsAutorizacion();
            }

            this.dispose();

            return bean;
        }

        public string obtenerCantidadSolicitudesParaAprobar()
        {
            String result = "S";
            Int32 contador = this.contar("asautorizacion.obtenerCantidadSolicitudesParaAprobar", null);

            if (contador == 0)
            {
                result = "N";
            }

            return result;
        }

        public AsAutorizacion obtenerHorario(FiltroSolicitudes filtro)
        {
            int? wtipodia = tieneProgramacion(filtro.Empleado.Value, filtro.FechaDesde.Value);
            AsTipohorario horario = new AsTipohorario();
            List<AsAutorizacion> tabla = new List<AsAutorizacion>();
            AsAutorizacion RangoHorario = new AsAutorizacion();

            if (wtipodia == 0)
            {
                EmpleadomastPk pk = new EmpleadomastPk();
                pk.Empleado = filtro.Empleado;
                pk.Companiasocio = filtro.CompaniaSocio;
                Empleadomast empleado = empleadomastDao.obtenerPorId(pk);

                int? wtipohorario = empleado.Tipohorario.Value;

                if (wtipohorario != null)
                {
                    horario = asTipohorarioDao.obtenerPorId(wtipohorario);
                }

                int? numero_dia = Convert.ToInt16(filtro.FechaDesde.Value.DayOfWeek.ToString("d"));

                if (numero_dia != null)
                {
                    switch (numero_dia)
                    {
                        case 1:
                            wtipodia = horario.Lunes.Value;
                            break;
                        case 2:
                            wtipodia = horario.Martes.Value;
                            break;
                        case 3:
                            wtipodia = horario.Miercoles.Value;
                            break;
                        case 4:
                            wtipodia = horario.Jueves.Value;
                            break;
                        case 5:
                            wtipodia = horario.Viernes.Value;
                            break;
                        case 6:
                            wtipodia = horario.Sabado.Value;
                            break;
                        case 7:
                            wtipodia = horario.Domingo.Value;
                            break;
                    }
                }
            }

            if (wtipodia != null)
            {

                List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
               
                parametros.Add(new ParametroPersistenciaGenerico("p_tipoDias", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(wtipodia)));

                _reader = this.listarPorQuery("asautorizacion.obtenerNombreHorario", parametros);

                while (_reader.Read())
                {
                    AsAutorizacion bean = new AsAutorizacion();
                    if (!_reader.IsDBNull(0))
                        bean.Desde = _reader.GetDateTime(0);
                    if (!_reader.IsDBNull(1))
                        bean.Hasta = _reader.GetDateTime(1);
                    tabla.Add(bean);
                }

                this.dispose();

            }

            if (tabla.Count==1)
            {
                RangoHorario = tabla[0];
            }
            return RangoHorario;
        }

        private int tieneProgramacion(int par_empleado, DateTime par_fecha)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_date", ConstanteUtil.TipoDato.Date, par_fecha));
            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, Convert.ToInt32(par_empleado)));

            _reader = this.listarPorQuery("asautorizacion.obtenerProgramacion", parametros);

            DtoSolicitud bean = new DtoSolicitud();

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    bean.tipoDia = _reader.GetInt32(0);
            }

            this.dispose();
            if (bean.tipoDia != null)
            {
                return bean.tipoDia.Value;
            }
            else
            {
                return 0;
            }

        }
    }
}
