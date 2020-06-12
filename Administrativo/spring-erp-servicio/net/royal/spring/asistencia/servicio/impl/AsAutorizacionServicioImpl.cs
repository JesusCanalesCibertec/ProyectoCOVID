using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.servicio;
using net.royal.spring.asistencia.constante;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.servicio;
using net.royal.spring.planilla.constante;
using net.royal.spring.rrhh.constante;
using net.royal.spring.core.dao;
using net.royal.spring.proceso.dao;
using Microsoft.AspNetCore.Http;
using net.royal.spring.framework.web.dao;
using Microsoft.EntityFrameworkCore;
using net.royal.spring.core.dominio;
using Microsoft.Extensions.Logging;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dao;
using System.IO;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.planilla.dao;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio.filtro;

namespace net.royal.spring.asistencia.servicio.impl
{

    public class AsAutorizacionServicioImpl : GenericoServicioImpl, AsAutorizacionServicio
    {

        private IServiceProvider servicioProveedor;
        private AsAutorizacionDao asAutorizacionDao;
        //private BpProcesoRrhhServicio bpProcesoRrhhServicio;
        private AsConceptoaccesoDao asConceptoaccesoDao;
        private SyAprobacionprocesoServicio syAprobacionprocesoServicio;
        private PersonamastDao personamastDao;
        private ParametrosmastServicio parametrosmastServicio;
        private CompanyownerDao companyownerDao;
        private EmpleadomastDao empleadomastDao;
        private AsPeriodoDao asperiodoDao;
        private HrEmpleadoDao hrEmpleadoDao;
        private AsAccesosdiariosDao asAccesosdiariosDao;
        private AsPeriodoDao asPeriodoDao;
        private AsConceptoaccesoReglaDao asConceptoaccesoReglaDao;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ILogger _logger;
        private AsAutorizacionDocAprobacionDao prSolicitudLogDao;
        private ParametrosmastDao parametrosmastDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;

        public AsAutorizacionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            asAutorizacionDao = _servicioProveedor.GetService<AsAutorizacionDao>();
            asConceptoaccesoDao = _servicioProveedor.GetService<AsConceptoaccesoDao>();
            //bpProcesoRrhhServicio = _servicioProveedor.GetService<BpProcesoRrhhServicio>();
            syAprobacionprocesoServicio = _servicioProveedor.GetService<SyAprobacionprocesoServicio>();
            personamastDao = _servicioProveedor.GetService<PersonamastDao>();
            companyownerDao = _servicioProveedor.GetService<CompanyownerDao>();
            httpContextAccessor = _servicioProveedor.GetService<IHttpContextAccessor>();
            parametrosmastServicio = _servicioProveedor.GetService<ParametrosmastServicio>();
            asperiodoDao = _servicioProveedor.GetService<AsPeriodoDao>();
            maMiscelaneosdetalleDao = _servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            empleadomastDao = _servicioProveedor.GetService<EmpleadomastDao>();
            asAccesosdiariosDao = _servicioProveedor.GetService<AsAccesosdiariosDao>();
            hrEmpleadoDao = _servicioProveedor.GetService<HrEmpleadoDao>();
            asPeriodoDao = _servicioProveedor.GetService<AsPeriodoDao>();
            asConceptoaccesoReglaDao = _servicioProveedor.GetService<AsConceptoaccesoReglaDao>();
            prSolicitudLogDao = _servicioProveedor.GetService<AsAutorizacionDocAprobacionDao>();
            parametrosmastDao = _servicioProveedor.GetService<ParametrosmastDao>();
            _logger = servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
        }

        public MensajeUsuario validarPreAccion(UsuarioActual usuarioActual, string accion, DtoSolicitud bean)
        {
            MensajeUsuario mensaje = null;

            if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_APROBAR) || accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_SEGUIMIENTO))
            {
                //9159 Las solicitudes de permisos deben aprobarse dentro del periodo de asistencia hasta que esté se encuentre cerrado. 
                var hoy = DateTime.Today;

                //Se obtenia el ultimo periodo
                //AsPeriodo periodo = asPeriodoDao.obtenerPorEmpleado(bean.SolicitanteId);

                AsAutorizacion pres = asAutorizacionDao.obtenerPorNumeroProceso(bean.ProcesoNro.Value);

                //Ahora se obtiene el periodo de las fechas de rango de permiso
                List<AsPeriodo> periodos = asPeriodoDao.obtenerPorEmpleadoRangoFecha(pres.Fecha, pres.Fechafin, bean.SolicitanteId);

                var dentroRango = true;
                var activo = true;

                if (periodos.Count == 0)
                {
                    mensaje = new MensajeUsuario(tipo_mensaje.ADVERTENCIA, "No se puede aprobar, debido a que el empleado no tiene un periodo de asisencia actual");
                }

                // Es decir, que si mi período es de "01/07/2018 - 29/07/2018" y un empleado solicita el 25/07/2018 y el jefe no lo ve y quiere aprobar el 30/07/2018 no debe permitir aprobarlo.
                // Si esta fuera del periodo, no debe poder aprobarlo
                if (periodos.Count > 0)
                {
                    if (!(hoy >= periodos[periodos.Count - 1].Fechadesde && hoy <= periodos[0].Fechahasta))
                    {
                        dentroRango = false;
                    }
                }

                //si uno esta inactivo, todo esta INACTIVO
                foreach (var item in periodos)
                {
                    if (item.Estado == "I")
                    {
                        activo = false;
                    }
                }

                //SI NO ESTA ACTIVO, NO SE PUEDE APROBAR
                if (!activo)
                {
                    mensaje = new MensajeUsuario(tipo_mensaje.ADVERTENCIA, "Las solicitudes de permisos deben aprobarse dentro del periodo de asistencia hasta que este se encuentre cerrado.");
                }

                if (!dentroRango)
                {
                    mensaje = new MensajeUsuario(tipo_mensaje.ADVERTENCIA, "Las solicitudes de permisos deben aprobarse dentro del periodo de asistencia hasta que este se encuentre cerrado.");
                }

                if (activo && !dentroRango)
                {
                    mensaje = null;
                }

            }

            return mensaje;

        }

        public DtoSolicitud ejecutarAccion(UsuarioActual usuarioActual, string accion, DtoSolicitud solicitud)
        {
            using (var dbTran = asAutorizacionDao.transaccionIniciar())
            {
                try
                {
                    AsAutorizacion asAutorizacion = asAutorizacionDao.obtenerPorNumeroProceso(solicitud.ProcesoNro.Value);

                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_APROBAR))
                    {
                        aprobar(usuarioActual, solicitud);
                    }
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_SEGUIMIENTO))
                    {
                        seguimiento(usuarioActual, solicitud);
                    }
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_DEVOLVER))
                    {
                        // devolver(usuarioActual,solicitud.getProcesoNro(),solicitud.getObservacionAccion());
                    }
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_RECHAZAR))
                    {
                        rechazar(usuarioActual, solicitud);
                    }

                    // DARIO-SEGUIMIENTO
                    String strRegistrarLog = parametrosmastDao.obtenerValorTipoDatoFlag(ConstanteRRHH.PARAMETRO_REGISTRAR_LOG, ConstanteRRHH.APLICACION_CODIGO);
                    if (strRegistrarLog.Equals("S"))
                        prSolicitudLogDao.registrar(usuarioActual, asAutorizacion, solicitud.ObservacionAccion);

                    asAutorizacionDao.transaccionFinalizar();
                    return solicitud;
                }
                catch (Exception ex)
                {
                    asAutorizacionDao.transaccionCancelar();
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    throw ex;
                }
            }
        }

        private void rechazar(UsuarioActual usuarioActual, DtoSolicitud solicitud)
        {
            AsAutorizacion pres;
            pres = asAutorizacionDao.obtenerPorNumeroProceso(solicitud.ProcesoNro.Value);

            pres.Nivelaprobacion = solicitud.NivelAprobar.Value;
            pres.Estado = ConstanteAutorizacion.ESTADO_RECHAZADO;
            pres.Ultimafechamodif = DateTime.Today;
            pres.Ultimousuario = usuarioActual.UsuarioLogin;
            asAutorizacionDao.actualizar(pres);


        }

        private void aprobar(UsuarioActual usuarioActual, DtoSolicitud solicitud)
        {

            AsAutorizacion pres;
            pres = asAutorizacionDao.obtenerPorNumeroProceso(solicitud.ProcesoNro.Value);

            pres.Nivelaprobacion = solicitud.NivelAprobar.Value;
            pres.Estado = ConstanteAutorizacion.ESTADO_APROBADO;
            pres.Ultimafechamodif = DateTime.Today;
            pres.Ultimousuario = usuarioActual.UsuarioLogin;
            pres.Autorizadopor = usuarioActual.UsuarioLogin;
            asAutorizacionDao.actualizar(pres);


        }

        private void seguimiento(UsuarioActual usuarioActual, DtoSolicitud solicitud)
        {

            AsAutorizacion pres;
            pres = asAutorizacionDao.obtenerPorNumeroProceso(solicitud.ProcesoNro.Value);

            pres.Nivelaprobacion = solicitud.NivelAprobar.Value;
            pres.Ultimafechamodif = DateTime.Today;
            pres.Ultimousuario = usuarioActual.UsuarioLogin;
            asAutorizacionDao.actualizar(pres);
        }


        public List<DtoPermisos> listarPermisos(DateTime? fechadesde, DateTime? fechahasta, int empleado, string compania, string conceptoacceso, string estado, DateTime? fecregistro, string unidad, bool conJefe)
        {
            return asAutorizacionDao.listarPermisos(fechadesde, fechahasta, empleado, compania, conceptoacceso, estado, fecregistro, unidad, conJefe);
        }

        public List<DtoPermisosDetalleAutorizacion> listarPermisosDetalleAutorizacion(int empleado, string conceptoacceso)
        {
            return asAutorizacionDao.listarPermisosDetalleAutorizacion(empleado, conceptoacceso);
        }

        public List<DtoPermisosDetalleMarcas> listarPermisosDetalleMarcas(int empleado, DateTime? fechadesde, DateTime? fechahasta)
        {
            return asAutorizacionDao.listarPermisosDetalleMarcas(empleado, fechadesde, fechahasta);
        }

        public DtoSolicitud obtener(string accion, DtoSolicitud solicitud)
        {
            throw new NotImplementedException();
        }

        public List<ParametroPersistenciaGenerico> obtenerMetadata(string accion, DtoSolicitud solicitud)
        {
            String concepto = "";
            AsAutorizacion bean = null;
            List<ParametroPersistenciaGenerico> lstMetadata = new List<ParametroPersistenciaGenerico>();

            if (solicitud == null)
                return lstMetadata;

            bean = asAutorizacionDao.obtenerPorNumeroProceso(solicitud.ProcesoNro.Value);

            // creando enlace y url a verse en el ver
            String url = "/spring/permisos-mantenimiento" + ConstanteCorreo.SEPARADOR_PARAMETROS;
            url = url + UInteger.obtenerValorEnteroSinNulo(bean.Numeroproceso).ToString();
            url = url + "|" + UInteger.obtenerValorEnteroSinNulo(bean.Autogenerado).ToString();

            solicitud.EstadoNombre = maMiscelaneosdetalleDao.obtenerDescripcion(ConstanteAutorizacion.APLICACION_CODIGO, ConstanteAutorizacion.MISCELANEO_ESTADO, bean.Estado);

            if (!UString.estaVacio(bean.Companyowner))
            {
                solicitud.CompaniaNombre = companyownerDao.obtenerNombre(bean.Companyowner);
            }
            solicitud.SolicitanteNombre = personamastDao.obtenerNombrePorPk(new core.dominio.PersonamastPk(Convert.ToInt32(bean.Empleado)));
            concepto = asConceptoaccesoDao.obtenerNombre(new AsConceptoaccesoPk(bean.Conceptoacceso));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_estado_nombre", ConstanteUtil.TipoDato.String, solicitud.EstadoNombre));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_compania_nombre", ConstanteUtil.TipoDato.String, solicitud.CompaniaNombre));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_proceso_nro", ConstanteUtil.TipoDato.String, solicitud.ProcesoNro.ToString()));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_solicitante_id", ConstanteUtil.TipoDato.String, solicitud.SolicitanteId.ToString()));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_solicitante_nombre", ConstanteUtil.TipoDato.String, solicitud.SolicitanteNombre));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_fecha_solicitud", ConstanteUtil.TipoDato.String, UFechaHora
                .convertirFechaCadena(bean.Fechasolicitud, ConstanteUtil.UFECHAHORA_FORMATO_FECHA)));

            lstMetadata.Add(new ParametroPersistenciaGenerico("p_concepto_nombre", ConstanteUtil.TipoDato.String, concepto));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_fecha_inicio", ConstanteUtil.TipoDato.String, UFechaHora
                .convertirFechaCadena(bean.Desde, ConstanteUtil.UFECHAHORA_FORMATO_FECHA)));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_fecha_fin", ConstanteUtil.TipoDato.String, UFechaHora
                .convertirFechaCadena(bean.Hasta, ConstanteUtil.UFECHAHORA_FORMATO_FECHA)));

            lstMetadata.Add(new ParametroPersistenciaGenerico(ConstanteCorreo.PARAMETRO_SYSTEM_URL_REDIRECCION, ConstanteUtil.TipoDato.String, url));

            lstMetadata.Add(new ParametroPersistenciaGenerico(ConstanteCorreo.PARAMETRO_MOTIVO_RECHAZO, ConstanteUtil.TipoDato.String, solicitud.ObservacionAccion));


            return lstMetadata;
        }

        public AsAutorizacion obtenerPorNumeroProceso(int numeroproceso)
        {
            return asAutorizacionDao.obtenerPorNumeroProceso(numeroproceso);
        }

        public AsAutorizacion obtenerSolicitudPorLlave(String Llave)
        {

            if (UString.estaVacio(Llave))
            {
                return null;
            }
            var keys = Llave.Split("|");
            if (keys.Length != 2)
            {
                return null;
            }

            var autogenerado = 0;
            bool result = Int32.TryParse(keys[1], out autogenerado);
            if (!result) autogenerado = 0;

            var numeroproceso = 0;
            result = Int32.TryParse(keys[0], out numeroproceso);
            if (!result) numeroproceso = 0;


            var asAutorizacion = obtenerPorNumeroProceso(numeroproceso);

            if (asAutorizacion == null)
            {
                asAutorizacion = obtenerPorAutogenerado(autogenerado);
            }

            return asAutorizacion;
        }

        public AsAutorizacion obtenerPorAutogenerado(int autogenerado)
        {
            return asAutorizacionDao.obtenerPorAutogenerado(autogenerado);
        }

        public AsAutorizacion solicitudActualizar(AsAutorizacion autorizacion)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            if (autorizacion.Conceptoacceso == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe seleccionar un Concepto"));
                throw new UException(lstRetorno);
            }

            AsConceptoaccesoPk asConceptoaccesopk = new AsConceptoaccesoPk();
            asConceptoaccesopk.Conceptoacceso = autorizacion.Conceptoacceso;
            AsConceptoacceso asConceptoacceso = asConceptoaccesoDao.obtenerPorId(asConceptoaccesopk.obtenerArreglo());

            if (asConceptoacceso.Expresadoen != null)
            {
                if (asConceptoacceso.Expresadoen == "H")
                {
                    if (autorizacion.Desde == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Tipo de Concepto seleccionado requiere ingresar la hora Inicial"));
                    }
                    if (autorizacion.Hasta == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Tipo de Concepto seleccionado requiere ingresar la hora final"));
                    }
                }
            }

            if (asAutorizacionDao.listarCruces(autorizacion).Count > 0)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango de fechas se encuentra en otra solicitud"));
            }

            if (lstRetorno.Count > 0)
            {
                throw new UException(lstRetorno);
            }
            /*****/
            using (var dbTran = asAutorizacionDao.transaccionIniciar())
            {
                try
                {
                    if (autorizacion.conAdjunto && !UString.estaVacio(autorizacion.archivo) && !UString.estaVacio(autorizacion.auxArchivo))
                    {
                        String rutaServer = parametrosmastServicio.obtenerValorExplicacion(ConstanteAutorizacion.PARAMETRO_RUTA_ADJUNTO, ConstanteAutorizacion.APLICACION_CODIGO);

                        String tmp = autorizacion.Companyowner.Trim() + "-" + autorizacion.Empleado.ToString();
                        if (!Directory.Exists(rutaServer + tmp))
                        {
                            Directory.CreateDirectory(rutaServer + tmp);
                        }
                        rutaServer = rutaServer + tmp + Path.DirectorySeparatorChar;


                        String rutaDocumento = Path.GetFileName(autorizacion.archivo);
                        Int32 inicio = rutaDocumento.LastIndexOf(Path.DirectorySeparatorChar);
                        String nombreDocumento = rutaDocumento.Substring(inicio + 1);
                        rutaDocumento = rutaServer + nombreDocumento;
                        if (File.Exists(rutaDocumento))
                            File.Delete(rutaDocumento);

                        File.WriteAllBytes(rutaDocumento, System.Convert.FromBase64String(autorizacion.auxArchivo));
                        autorizacion.archivo = rutaDocumento;
                    }

                    autorizacion = asAutorizacionDao.solicitudActualizar(autorizacion);
                    asAutorizacionDao.transaccionFinalizar();
                    return autorizacion;
                }
                catch (Exception ex)
                {
                    asAutorizacionDao.transaccionCancelar();
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    throw ex;
                }
            }
        }

        public AsAutorizacion solicitudAnular(UsuarioActual usuarioActual, Int32 numeroproceso)
        {
            using (var dbTran = asAutorizacionDao.transaccionIniciar())
            {
                try
                {
                    AsAutorizacion autorizacion = asAutorizacionDao.solicitudAnular(usuarioActual, numeroproceso);

                    asAutorizacionDao.transaccionFinalizar();
                    return autorizacion;
                }
                catch (Exception ex)
                {
                    asAutorizacionDao.transaccionCancelar();
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    throw ex;
                }
            }
        }

        public void solicitudEliminar(UsuarioActual usuarioActual, Int32 numeroproceso)
        {
            using (var dbTran = asAutorizacionDao.transaccionIniciar())
            {
                try
                {
                    asAutorizacionDao.solicitudEliminar(usuarioActual, numeroproceso);
                    asAutorizacionDao.transaccionFinalizar();
                }
                catch (Exception ex)
                {
                    //asAutorizacionDao.transaccionCancelar();
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    throw ex;
                }
            }
        }

        public AsAutorizacion solicitudRegistrar(AsAutorizacion autorizacion, UsuarioActual usuarioActual)
        {

            bool VALIDA_GENERACION_MASIVA = false;
            bool VALIDA_DENTRO_HORARIO = false;
            bool VALIDA_SOLO_MASCULINO = false;
            bool VALIDA_SOLO_FEMENINO = false;
            bool VALIDA_CANTIDAD_DIAS_MAXIMO = false;
            bool VALIDA_FIZCALIZADO = false;
            bool VALIDA_FUERA_HORARIO = false;
            bool CONFIGURACION_CONTAR_SOBRETIEMPO = true;
            bool CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA = false;
            bool CONFIGURACION_COPIA_HORAINICIO_A_HORAFIN = false;

            int VALIDA_DIAS_DEFAULT = 1;


            //PARA TODOS
            VALIDA_FIZCALIZADO = false;

            String strRegistrarLog = parametrosmastDao.obtenerValorTipoDatoFlag(ConstanteRRHH.PARAMETRO_REGISTRAR_LOG, ConstanteRRHH.APLICACION_CODIGO);
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();
            Int32? ie = UInteger.obtenerValorEntero(autorizacion.Empleado);

            lstRetorno = empleadomastDao.validarEmpleadoEnCompania(lstRetorno, autorizacion.Companyowner, ie);
            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            if (VALIDA_FIZCALIZADO && !UBoolean.validarFlag(empleadomastDao.esFiscalizado(Convert.ToInt32(autorizacion.Empleado))))
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El usuario no es fiscalizado, no puede ingresar un permiso"));
                throw new UException(lstRetorno);
            }

            if (autorizacion.Conceptoacceso == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe seleccionar un Concepto"));
                throw new UException(lstRetorno);
            }

            AsConceptoaccesoPk asConceptoaccesopk = new AsConceptoaccesoPk();
            asConceptoaccesopk.Conceptoacceso = autorizacion.Conceptoacceso;
            AsConceptoacceso asConceptoacceso = asConceptoaccesoDao.obtenerPorId(asConceptoaccesopk.obtenerArreglo());



            switch (asConceptoacceso.Conceptoacceso)
            {
                //COMISION DE SERVICIO
                case "03":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_DENTRO_HORARIO = true;
                        break;
                    }
                //PARO
                case "06":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_DENTRO_HORARIO = true;
                        break;
                    }
                //PERMISO PERSONAL X HRS
                case "19":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_DENTRO_HORARIO = true;
                        break;
                    }

                //LACTANCIA
                case "02":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        //VALIDA_DENTRO_HORARIO = true;
                        VALIDA_SOLO_FEMENINO = true;
                        break;
                    }
                //LICENCIA POR PATERNIDAD
                case "28":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_SOLO_MASCULINO = true;
                        VALIDA_CANTIDAD_DIAS_MAXIMO = true;
                        VALIDA_DIAS_DEFAULT = 10;
                        CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA = true;
                        break;
                    }
                //LICENCIA SIN GOCE DE HABER
                case "14":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA = true;
                        break;
                    }
                //DIA NO LABORABLE A CUENTA DE VACACIONES
                case "32":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA = true;
                        break;
                    }
                //LICENCIA POR LUTO
                case "21":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_CANTIDAD_DIAS_MAXIMO = true;
                        VALIDA_DIAS_DEFAULT = 5;
                        CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA = true;
                        break;
                    }
                //CAPACITACION
                case "18":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_DENTRO_HORARIO = true;

                        break;
                    }
                //AUTORIZACION DE SOBRETIEMPOS
                case "PAHE":
                    {
                        VALIDA_GENERACION_MASIVA = true;
                        VALIDA_FUERA_HORARIO = true;
                        break;
                    }

                //OMISION DE MARCADO
                case "OMIS":
                    {
                        CONFIGURACION_COPIA_HORAINICIO_A_HORAFIN = true;
                        break;
                    }

            }

            if (VALIDA_SOLO_MASCULINO)
            {
                Personamast personamast = personamastDao.obtenerPorId(new PersonamastPk() { Persona = Convert.ToInt32(autorizacion.Empleado.Value) });
                if (personamast != null)
                {
                    if (!UString.estaVacio(personamast.Sexo))
                    {
                        if (personamast.Sexo != "M")
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El concepto solo aplica al personal masculino"));
                            throw new UException(lstRetorno);
                        }
                    }
                }
            }

            if (VALIDA_SOLO_FEMENINO)
            {
                Personamast personamast = personamastDao.obtenerPorId(new PersonamastPk() { Persona = Convert.ToInt32(autorizacion.Empleado.Value) });
                if (personamast != null)
                {
                    if (!UString.estaVacio(personamast.Sexo))
                    {
                        if (personamast.Sexo != "F")
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El concepto solo aplica al personal femenino"));
                            throw new UException(lstRetorno);
                        }
                    }
                }
            }

            if (asConceptoacceso.Expresadoen != null)
            {
                if (asConceptoacceso.Expresadoen == "H")
                {
                    if (autorizacion.Desde == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Tipo de Concepto seleccionado requiere ingresar la hora Inicial"));
                    }
                    if (autorizacion.Hasta == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Tipo de Concepto seleccionado requiere ingresar la hora final"));
                    }
                }
            }

            if (!autorizacion.Fecha.HasValue)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Falta ingresar la fecha"));
                throw new UException(lstRetorno);
            }

            if (DateTime.Compare(autorizacion.Fecha.Value, autorizacion.Fechafin.Value) > 0)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha Final No puede ser menor que la Inicial."));
                throw new UException(lstRetorno);
            }
            if (autorizacion.Desde != null && autorizacion.Hasta != null)
            {
                int hora = DateTime.Compare(autorizacion.Desde.Value, autorizacion.Hasta.Value);

                if (hora > 0 && autorizacion.Conceptoacceso!="OMIS")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Hora Desde debe ser menor a la Hora Hasta"));
                    throw new UException(lstRetorno);
                }
            }

            if (VALIDA_CANTIDAD_DIAS_MAXIMO)
            {
                var horasAcceso = asConceptoacceso.CantidadHoras.HasValue ? asConceptoacceso.CantidadHoras.Value.Hour : VALIDA_DIAS_DEFAULT * 24;
                Decimal difDia = UFechaHora.diferenciaDia(autorizacion.Fecha.Value, autorizacion.Fechafin.Value) + 1;
                if ((difDia * 24) > horasAcceso)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No puede asignar un permiso mayor a " + Convert.ToInt32(horasAcceso / 24) + " días para este concepto."));
                    throw new UException(lstRetorno);
                }
            }

            //validacion periodo
            if (!asperiodoDao.esPeriodoAcitvo(autorizacion.Empleado.Value, autorizacion.Fecha.Value, autorizacion.Fechafin.Value))
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización se produce en un periodo cerrado o no existe periodo declarado para el tipo de planilla del trabajador."));
                throw new UException(lstRetorno);
            }

            //validacion horas
            if (asConceptoacceso.FlaghorasMaximo != null)
            {
                if (asConceptoacceso.FlaghorasMaximo == "S")
                {
                    Decimal difhora = UFechaHora.diferenciaHora(autorizacion.Desde.Value, autorizacion.Hasta.Value);
                    Decimal difDia = UFechaHora.diferenciaDia(autorizacion.Fecha.Value, autorizacion.Fechafin.Value) + 1;

                    if ((difhora * difDia) > asConceptoacceso.CantidadHorasMaximo)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No puede asignar un permiso mayor a " + asConceptoacceso.CantidadHorasMaximo + " horas para este concepto."));
                        throw new UException(lstRetorno);
                    }
                }
            }

            // URA 20/11/2017 Validar Aniversario / Cumpleaños
            if (asConceptoacceso.FlagEsAniversario != null)
            {
                if (asConceptoacceso.FlagEsAniversario == "S")
                {
                    if (asConceptoacceso.Conceptoaccesosistema == "PERM")
                    {

                        if (!validacionFechaIngreso(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, autorizacion.Fecha.Value.ToString("yyyyMM"), autorizacion.Fecha.Value, autorizacion.Fechafin.Value, 1, "NUEVO", usuarioActual))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La cuponera solo puede ser usada en el mismo mes de Aniversario."));
                            throw new UException(lstRetorno);
                        }
                        else
                        {
                            if (!validacionFechaIngreso(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, autorizacion.Fecha.Value.ToString("yyyyMM"), autorizacion.Fecha.Value, autorizacion.Fechafin.Value, 2, "NUEVO", usuarioActual))
                            {
                                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Sólo se puede solicitar una solicitud de Aniversario."));
                                throw new UException(lstRetorno);
                            }
                        }

                    }


                }
            }

            //validacion de CUMPLEANIOS   -   (31)
            if (asConceptoacceso.Conceptoacceso == "31")
            {


                if (!validacionFechaCumpleanos(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, autorizacion.Fecha.Value.ToString("yyyyMM"), autorizacion.Fecha.Value, autorizacion.Fechafin.Value, 1, "NUEVO"))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La cuponera solo puede ser usada en el mismo mes de Cumpleaños."));
                    throw new UException(lstRetorno);
                }

                if (!validacionFechaCumpleanos(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, autorizacion.Fecha.Value.ToString("yyyyMM"), autorizacion.Fecha.Value, autorizacion.Fechafin.Value, 2, "NUEVO"))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Sólo se puede solicitar una solicitud de Cumpleaños."));
                    throw new UException(lstRetorno);
                }

                //

                //a esta hora sale
                DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);

                //a esta hora entra
                DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);

                finHorario = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, finHorario.Value);
                inicioHorario = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, inicioHorario.Value);

                DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, autorizacion.Desde.Value);
                DateTime? finSolicitud = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, autorizacion.Hasta.Value);

                if (!((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) && (finSolicitud <= finHorario && finSolicitud > inicioHorario)))
                {

                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido debe estar dentro del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                    throw new UException(lstRetorno);
                }

                //

                var horasPermitidas = asConceptoacceso.CantidadHoras.HasValue ? asConceptoacceso.CantidadHoras.Value.Hour : 0;

                //validar que las horas solicitadas sean como maximo las horasPermitidas
                Decimal difhora = UFechaHora.diferenciaHora(autorizacion.Hasta.Value, autorizacion.Desde.Value);
                Decimal difDia = UFechaHora.diferenciaDia(autorizacion.Fecha.Value, autorizacion.Fechafin.Value) + 1;

                if ((difhora * difDia) > horasPermitidas)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No puede asignar una papeleta mayor a " + horasPermitidas + " horas para este concepto."));
                    throw new UException(lstRetorno);
                }

                //validar que no haya una //Cuponeras de 2 Horas Libres en el mismo dia
                var fechaComparar = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day);

                if (asAutorizacionDao.contarCuponerasHorasLibres(autorizacion.Empleado, fechaComparar))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay una autorización por concepto de 2 horas libres"));
                    throw new UException(lstRetorno);
                }

            }

            //en caso de omisión de marcado validamos que no exista la misma marca
            if (autorizacion.Conceptoacceso == "OMIS")
            {

                if (asAccesosdiariosDao.validarSolicitudOmision(DateTime.Today, Convert.ToInt32(autorizacion.Empleado)))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización se produce en un periodo cerrado o no existe periodo declarado para el tipo de planilla del trabajador."));
                    throw new UException(lstRetorno);
                }

                var inicioAcceso = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day, 0, 0, 0);
                var finAcceso = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day, 23, 59, 59);

                //validar que no haya 2 permisos ya ingresados
                if (asAccesosdiariosDao.contarMarcasPorRango(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, inicioAcceso, finAcceso) > 1)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El límite de marcas es 2 en el día " + inicioAcceso.ToString("dd/MM/yyyy")));
                    throw new UException(lstRetorno);
                }

            }

            //Cuponeras de 2 Horas Libres
            if (autorizacion.Conceptoacceso == "29" || autorizacion.Conceptoacceso == "30")
            {

                //Validar que no sea practicante, ni fuerza de ventas
                Empleadomast e = empleadomastDao.obtenerPorIdEmpleadoCompania(Convert.ToInt32(autorizacion.Empleado.Value), autorizacion.Companyowner);

                if (e.Tipoplanilla == "PR")
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Este concepto no es permitido para practicantes"));
                    throw new UException(lstRetorno);
                }


                Nullable<DateTime> inicioAcceso = null;
                Nullable<DateTime> finAcceso = null;

                if (autorizacion.Conceptoacceso == "29")
                {
                    inicioAcceso = asConceptoacceso.fechavidautilini.HasValue ? new DateTime(autorizacion.Fecha.Value.Year, asConceptoacceso.fechavidautilini.Value.Month, asConceptoacceso.fechavidautilini.Value.Day) : new DateTime(autorizacion.Fecha.Value.Year, 1, 1);
                    finAcceso = asConceptoacceso.fechavidautilfin.HasValue ? new DateTime(autorizacion.Fecha.Value.Year, asConceptoacceso.fechavidautilfin.Value.Month, asConceptoacceso.fechavidautilfin.Value.Day) : new DateTime(autorizacion.Fecha.Value.Year, 6, 30);

                }
                else
                {
                    inicioAcceso = asConceptoacceso.fechavidautilini.HasValue ? new DateTime(autorizacion.Fecha.Value.Year, asConceptoacceso.fechavidautilini.Value.Month, asConceptoacceso.fechavidautilini.Value.Day) : new DateTime(autorizacion.Fecha.Value.Year, 7, 1);
                    finAcceso = asConceptoacceso.fechavidautilfin.HasValue ? new DateTime(autorizacion.Fecha.Value.Year, asConceptoacceso.fechavidautilfin.Value.Month, asConceptoacceso.fechavidautilfin.Value.Day) : new DateTime(autorizacion.Fecha.Value.Year, 12, 31);

                }

                var horasAcceso = asConceptoacceso.CantidadHoras.HasValue ? asConceptoacceso.CantidadHoras.Value.Hour : 0;

                //validar las horas permitidas

                Decimal difhora = UFechaHora.diferenciaHora(autorizacion.Hasta.Value, autorizacion.Desde.Value);
                Decimal difDia = UFechaHora.diferenciaDia(autorizacion.Fecha.Value, autorizacion.Fechafin.Value) + 1;

                if ((difhora * difDia) > horasAcceso)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No puede asignar una papeleta mayor a " + horasAcceso + " horas para este concepto."));
                    throw new UException(lstRetorno);
                }

                //validar que este en el periodo de vigencia
                if (asAccesosdiariosDao.validarSolicitudOmision(DateTime.Today, Convert.ToInt32(autorizacion.Empleado)))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización se produce en un periodo cerrado o no existe periodo declarado para el tipo de planilla del trabajador."));
                    throw new UException(lstRetorno);
                }

                //validar que la fecha este dentro del rango de la cuponera

                if ((autorizacion.Fecha.Value >= finAcceso || autorizacion.Fecha.Value < inicioAcceso))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Solo puede ingresar papeletas en el rango de " + inicioAcceso.Value.ToString("dd/MM/yyyy") + " al " + finAcceso.Value.ToString("dd/MM/yyyy") + " para este concepto."));
                    throw new UException(lstRetorno);
                }

                //validar que solo haya una marca en el mismo día como máximo
                if (asAccesosdiariosDao.contarMarcasPorDiaHorasLibres(autorizacion.Empleado.Value, autorizacion.Fecha.Value, autorizacion.Conceptoacceso) == 2)
                {
                    var texto = UString.estaVacio(asConceptoacceso.Descripcionlocal) ? "" : "de " + asConceptoacceso.Descripcionlocal.Trim();
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Ya existen dos marcas " + texto + " para la fecha indicada."));
                    throw new UException(lstRetorno);
                }

                //validar que no haya 4 permisos ya ingresados
                if (asAccesosdiariosDao.contarMarcasPorRango(autorizacion.Empleado.Value, autorizacion.Conceptoacceso, inicioAcceso.Value, finAcceso.Value) > 3)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El límite de marcas es 4 dentro del rango de " + inicioAcceso.Value.ToString("dd/MM/yyyy") + " al " + finAcceso.Value.ToString("dd/MM/yyyy")));
                    throw new UException(lstRetorno);
                }

                //validar que ese mismo día no haya una vacacion o cuponera de cumpleaios

                var fechaComparar = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day);
                var fechaComparar2 = new DateTime(autorizacion.Fechafin.Value.Year, autorizacion.Fechafin.Value.Month, autorizacion.Fechafin.Value.Day);

                if (asAutorizacionDao.contarCumpleanios(autorizacion.Empleado, fechaComparar))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay una autorización por concepto de Cumpleaños para la fecha seleccionada"));
                    throw new UException(lstRetorno);
                }

                if (asAutorizacionDao.contarVacaciones(autorizacion.Empleado, fechaComparar, fechaComparar2))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay vacaciones que cruzan con la fecha seleccionada"));
                    throw new UException(lstRetorno);
                }


                //VALIDACION DENTRO DEL HORARIO
                //a esta hora sale
                DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);

                //a esta hora entra
                DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);

                finHorario = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, finHorario.Value);
                inicioHorario = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, inicioHorario.Value);

                DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, autorizacion.Desde.Value);
                DateTime? finSolicitud = UFechaHora.fechaCompuesta(autorizacion.Fecha.Value, autorizacion.Hasta.Value);

                if (!((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) && (finSolicitud <= finHorario && finSolicitud > inicioHorario)))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido debe estar dentro del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                    throw new UException(lstRetorno);
                }
                //

            }

            //URA 09/01/2018 Validando si tiene tope de días
            if (asConceptoacceso.FlagTopeDiaSolicitud == "S")
            {
                decimal res = UFechaHora.diferenciaDia(autorizacion.Fecha.Value, DateTime.Today);

                if (res > asConceptoacceso.CantidadTopeDiaSolicitud)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha seleccionada no puede ser menor a la fecha actual en más de " + asConceptoacceso.CantidadTopeDiaSolicitud + " días."));
                    throw new UException(lstRetorno);
                }
            }

            //URA 09/01/2018 Validando si tiene tope de meses
            if (asConceptoacceso.FlagTopeMesesSolicitud == "S")
            {
                Decimal meses = UFechaHora.diferenciaMes(hrEmpleadoDao.obtenerPorId(new HrEmpleadoPk(usuarioActual.PersonaId).obtenerArreglo()).Fechaingresoorganizacion.Value, DateTime.Today);

                if (meses < asConceptoacceso.CantidadTopeMesesSolicitud)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Para hacer uso de este beneficio usted debe contar con más de " + asConceptoacceso.CantidadTopeMesesSolicitud + " meses en la institución."));
                    throw new UException(lstRetorno);
                }
            }

            if (asAutorizacionDao.listarCruces(autorizacion).Count > 0)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango de fechas se encuentra en otro permiso ingresado"));
            }

            if (VALIDA_GENERACION_MASIVA)
            {

                //generar fechas entre el rango y validar si esta en el rango
                var fechaADesde = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day);
                var fechaAhasta = new DateTime(autorizacion.Fechafin.Value.Year, autorizacion.Fechafin.Value.Month, autorizacion.Fechafin.Value.Day);

                while (fechaADesde <= fechaAhasta)
                {
                    //SE VALIDA SI EL PERMISO YA ESTA INGRESADO
                    var posibleCruce = obtenerPorDbPk(new AsAutorizacionPk() { Conceptoacceso = autorizacion.Conceptoacceso, Empleado = autorizacion.Empleado, Desde = autorizacion.Desde, Fecha = fechaADesde });

                    if (posibleCruce != null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización de tipo " + asConceptoacceso.Descripcionlocal + " para el día " + autorizacion.Fecha.Value.ToString("dd/MM/yyyy") + " y hora " + autorizacion.Desde.Value.ToString("HH:mm") + " ya se ha registrado anteriormente"));
                        throw new UException(lstRetorno);
                    }

                    //VALIDAR QUE NO ESTE CONTENIDA EN OTRA SOLICITUD
                    var mensaje = validarPermisoDentroDeOtroPermiso(autorizacion.Fecha.Value, autorizacion.Desde.Value, autorizacion.Fechafin.Value, autorizacion.Hasta.Value, Convert.ToInt32(autorizacion.Empleado));
                    if (mensaje != null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, mensaje.Mensaje));
                        throw new UException(lstRetorno);
                    }

                    if (asAutorizacionDao.contarVacaciones(autorizacion.Empleado, fechaADesde, fechaAhasta))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay vacaciones que cruzan con la fecha seleccionada"));
                        throw new UException(lstRetorno);
                    }

                    //a esta hora sale
                    DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, fechaADesde, CONFIGURACION_CONTAR_SOBRETIEMPO);
                    //a esta hora entra
                    DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, fechaADesde, CONFIGURACION_CONTAR_SOBRETIEMPO);

                    finHorario = UFechaHora.fechaCompuesta(fechaADesde, finHorario.Value);
                    inicioHorario = UFechaHora.fechaCompuesta(fechaADesde, inicioHorario.Value);

                    DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(fechaADesde, autorizacion.Desde.Value);
                    DateTime? finSolicitud = UFechaHora.fechaCompuesta(fechaADesde, autorizacion.Hasta.Value);

                    if (VALIDA_FUERA_HORARIO)
                    {
                        if (inicioHorario.Value.Hour == 23 && inicioHorario.Value.Minute == 59 && finHorario.Value.Hour == 0 && finHorario.Value.Minute == 0)
                        {
                            fechaADesde = fechaADesde.AddDays(1);
                            continue;
                        }
                        else if ((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) || (finSolicitud <= finHorario && finSolicitud > inicioHorario))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido para el día " + fechaADesde.ToString("dd/MM/yyyy") + " debe estar fuera del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                            throw new UException(lstRetorno);
                        }
                    }

                    if (VALIDA_DENTRO_HORARIO)
                    {
                        if (!((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) && (finSolicitud <= finHorario && finSolicitud > inicioHorario)))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido para el día " + fechaADesde.ToString("dd/MM/yyyy") + " debe estar dentro del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                            throw new UException(lstRetorno);
                        }
                    }

                    fechaADesde = fechaADesde.AddDays(1);
                }
            }

            if (asConceptoacceso.Expresadoen == "H" && asConceptoacceso.Conceptoacceso != "OMIS" && asConceptoacceso.Conceptoacceso != "PAHE")
            {
                if (autorizacion.AuxHoraInicio != null && autorizacion.AuxHoraInicio.Value.Year != 1)
                {
                    if (!(UFechaHora.fechaCompuesta(autorizacion.AuxHoraInicio, autorizacion.Desde) >= autorizacion.AuxHoraInicio &&
                          UFechaHora.fechaCompuesta(autorizacion.AuxHoraInicio, autorizacion.Hasta) <= autorizacion.AuxHoraFin))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Estos eventos deben estar dentro del horario " + autorizacion.AuxHoraInicio.Value.ToString("HH:mm") + " - " + autorizacion.AuxHoraFin.Value.ToString("HH:mm")));
                        throw new UException(lstRetorno);
                    }

                }
            }

            //validar que este en el periodo de vigencia
            if (asAccesosdiariosDao.validarSolicitudOmision(DateTime.Today, Convert.ToInt32(autorizacion.Empleado)))
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización se produce en un periodo cerrado o no existe periodo declarado para el tipo de planilla del trabajador."));
                throw new UException(lstRetorno);
            }

            if (!VALIDA_GENERACION_MASIVA)
            {
                //VALIDA SI EL PERMISO YA ESTA INGRESADO
                var permisoPosibleCruce = obtenerPorDbPk(new AsAutorizacionPk() { Conceptoacceso = autorizacion.Conceptoacceso, Empleado = autorizacion.Empleado, Desde = autorizacion.Desde, Fecha = autorizacion.Fecha });
                if (permisoPosibleCruce != null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización de tipo " + asConceptoacceso.Descripcionlocal + " para el día " + autorizacion.Fecha.Value.ToString("dd/MM/yyyy") + " y hora " + autorizacion.Desde.Value.ToString("HH:mm") + " ya se ha registrado anteriormente"));
                }

                //VALIDAR QUE NO ESTE CONTENIDA EN OTRA SOLICITUD
                var mensaej = validarPermisoDentroDeOtroPermiso(autorizacion.Fecha.Value, autorizacion.Desde.Value, autorizacion.Fechafin.Value, autorizacion.Hasta.Value, Convert.ToInt32(autorizacion.Empleado));
                if (mensaej != null)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, mensaej.Mensaje));
                    throw new UException(lstRetorno);
                }

                var fechaADesde = new DateTime(autorizacion.Fecha.Value.Year, autorizacion.Fecha.Value.Month, autorizacion.Fecha.Value.Day);
                var fechaAhasta = new DateTime(autorizacion.Fechafin.Value.Year, autorizacion.Fechafin.Value.Month, autorizacion.Fechafin.Value.Day);

                if (asAutorizacionDao.contarVacaciones(autorizacion.Empleado, fechaADesde, fechaAhasta))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay vacaciones que cruzan con la fecha seleccionada"));
                    throw new UException(lstRetorno);
                }
            }

            if (lstRetorno.Count > 0)
            {
                throw new UException(lstRetorno);
            }

            using (var dbTran = asAutorizacionDao.transaccionIniciar())
            {
                try
                {
                    List<Int32> generados = new List<int>();

                    /*BpTransaccion bpTransaccion = bpProcesoRrhhServicio.registrar(usuarioActual,
                           ConstanteProceso.PROCESO_ASISTENCIA_SOLICITUDES, "Generado desde Spring Net", null,
                           ConstanteAutorizacion.ESTADO_SOLICITUD);*/

                    //autorizacion.IdTransaccion = bpTransaccion.IdTransaccion;
                    autorizacion.Solicitadopor = usuarioActual.UsuarioLogin;
                    autorizacion.Ultimousuario = usuarioActual.UsuarioLogin;
                    autorizacion.Ultimafechamodif = DateTime.Now;
                    autorizacion.Sobretiempoposicion = 1;


                    //autorizacion.Refrigerio = asConceptoacceso.PagarRefrigerio;
                    //autorizacion.Mandatorio = asConceptoacceso.Mandatorio;

                    autorizacion.Periodo = autorizacion.Fecha.Value.ToString("yyyyMM");

                    if (CONFIGURACION_COPIA_HORARIO_PRIMERA_FECHA)
                    {
                        DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);
                        DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(autorizacion.Empleado), autorizacion.Companyowner, autorizacion.Fecha.Value, CONFIGURACION_CONTAR_SOBRETIEMPO);
                        autorizacion.Desde = new DateTime(2000, 1, 1, inicioHorario.Value.Hour, inicioHorario.Value.Minute, inicioHorario.Value.Second);
                        autorizacion.Hasta = new DateTime(2000, 1, 1, finHorario.Value.Hour, finHorario.Value.Minute, finHorario.Value.Second);
                    }

                    if (CONFIGURACION_COPIA_HORAINICIO_A_HORAFIN)
                    {
                        autorizacion.Hasta = autorizacion.Desde;
                    }

                    //autorizacion.Numeroproceso = asAutorizacionDao.obtenerNumeroProceso();
                    autorizacion = asAutorizacionDao.registrarPermiso(autorizacion);
                    generados.Add(autorizacion.Numeroproceso.Value);

                    // DARIO-SEGUIMIENTO
                    if (strRegistrarLog.Equals("S"))
                        prSolicitudLogDao.registrar(usuarioActual, autorizacion, null);

                    asAutorizacionDao.transaccionFinalizar();
                    autorizacion.registrosGenerados = generados;
                    return autorizacion;

                }
                catch (Exception ex)
                {
                    asAutorizacionDao.transaccionCancelar();
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                    throw ex;
                }
            }
        }

        private AsAutorizacion obtenerPorDbPk(AsAutorizacionPk asAutorizacionPk)
        {
            return asAutorizacionDao.obtenerPorDbPk(asAutorizacionPk);
        }

        public bool validacionFechaCumpleanos(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta, int tipo, string accion)
        {
            if (tipo == 1)
            {
                Personamast personamast = new Personamast();
                PersonamastPk personamastpk = new PersonamastPk();
                personamastpk.Persona = Convert.ToInt32(empleado);

                personamast = personamastDao.obtenerPorId(personamastpk);

                if (personamast.Fechanacimiento.Value.Month == Convert.ToInt32(periodo.Substring(4, 2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (tipo == 2)
            {
                if (accion == "NUEVO")
                {
                    return asAutorizacionDao.validacionFechaCumpleanosNuevo(empleado, concepto, periodo, fechadesde, fechahasta);
                }
                if (accion == "EDICION")
                {
                    return asAutorizacionDao.validacionFechaCumpleanosEdicion(empleado, concepto, periodo, fechadesde, fechahasta);
                }
            }

            return false;
        }



        private List<MensajeUsuario> solicitudValidar(List<MensajeUsuario> lstRetorno,
                UsuarioActual usuarioActual,
                string accionSolicitada,
                AsAutorizacion bean,
                AsConceptoaccesoRegla regla)

        {
            AsConceptoaccesoPk asConceptoaccesopk = new AsConceptoaccesoPk();
            asConceptoaccesopk.Conceptoacceso = bean.Conceptoacceso;
            AsConceptoacceso asConceptoacceso = asConceptoaccesoDao.obtenerPorId(asConceptoaccesopk.obtenerArreglo());
            int result = 0;


            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_MARCA_ASISTENCIA))//OK
            {
                // Requiere Marca de Asistencia del dia solicitado
                //validar que el empleado tenga marcaciones para ese dia


                if (bean.Fecha != null)
                {
                    if (asAccesosdiariosDao.contarMarcasPorDiaHorasLibres(bean.Empleado.Value, bean.Fecha.Value, bean.Conceptoacceso) == 0)
                    {
                        var texto = UString.estaVacio(asConceptoacceso.Descripcionlocal) ? "" : "de " + asConceptoacceso.Descripcionlocal.Trim();
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Se requiere que se tenga marcacaciones para solicitar este concepto " + texto + " para la fecha indicada."));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio para la regla : " + ConstanteAutorizacionRegla.REQUIERE_MARCA_ASISTENCIA));
                }


            }

            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_PERIODO_ASISTENCIA))//OK
            {
                // Requiere Periodo de Asistencia Abierto por Tipo de Planilla
                //validar que este en el periodo de vigencia
                if (bean.Fecha != null && bean.Fechafin != null)
                {
                    if (!asperiodoDao.esPeriodoAcitvo(bean.Empleado.Value, bean.Fecha.Value, bean.Fechafin.Value))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La autorización se produce en un periodo cerrado o no existe periodo declarado para el tipo de planilla del trabajador."));
                    }

                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin  para la regla : " + ConstanteAutorizacionRegla.REQUIERE_PERIODO_ASISTENCIA));
                }


            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_MES_CUMPLEANIO)) //OK
            {
                // Permitir el registro si esta en el mismo mes de tu cumpleaños
                if (bean.Fecha != null && bean.Fechafin != null)
                {
                    if (!validacionFechaCumpleanos(bean.Empleado.Value, bean.Conceptoacceso, bean.Fecha.Value.ToString("yyyyMM"), bean.Fecha.Value, bean.Fechafin.Value, 1, accionSolicitada))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La cuponera solo puede ser usada en el mismo mes de Cumpleaños."));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin  para la regla : " + ConstanteAutorizacionRegla.REQUIERE_MES_CUMPLEANIO));
                }

            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_NO_VACACIONES_APROBADAS))//OK
            {
                if (bean.Fecha != null && bean.Fechafin != null)
                {
                    var fechaComparar = new DateTime(bean.Fecha.Value.Year, bean.Fecha.Value.Month, bean.Fecha.Value.Day);
                    var fechaComparar2 = new DateTime(bean.Fechafin.Value.Year, bean.Fechafin.Value.Month, bean.Fechafin.Value.Day);

                    // No debes tener vacaciones aprobadas
                    if (asAutorizacionDao.contarVacaciones(bean.Empleado, fechaComparar, fechaComparar2))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se puede ingresar este concepto, debido a que hay vacaciones que cruzan con la fecha seleccionada"));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin  para la regla : " + ConstanteAutorizacionRegla.REQUIERE_NO_VACACIONES_APROBADAS));
                }

            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_SOLO_FISCALIZADOS))//OK
            {
                // Solo los usuarios fiscalizados puede registrar permisos
                if (!UBoolean.validarFlag(empleadomastDao.esFiscalizado(Convert.ToInt32(bean.Empleado))))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El usuario no es fiscalizado, no puede ingresar un permiso"));
                }
            }

            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_HORA_INICIO_FUERA_HORARIO))//OK
            {
                // Hora Inicio / Hora fin debe estar fuera del horario del empleado
                if (bean.Fecha != null && bean.Fechafin != null)
                {
                    if (bean.Desde != null && bean.Hasta != null)
                    {
                        var fechaADesde = new DateTime(bean.Fecha.Value.Year, bean.Fecha.Value.Month, bean.Fecha.Value.Day);
                        var fechaAhasta = new DateTime(bean.Fechafin.Value.Year, bean.Fechafin.Value.Month, bean.Fechafin.Value.Day);

                        //obtener el horario para el dia de la solicitud
                        //a esta hora sale
                        DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, fechaADesde);
                        //a esta hora entra
                        DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, fechaADesde);

                        finHorario = UFechaHora.fechaCompuesta(fechaADesde, finHorario.Value);
                        inicioHorario = UFechaHora.fechaCompuesta(fechaADesde, inicioHorario.Value);

                        DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(fechaADesde, bean.Desde.Value);
                        DateTime? finSolicitud = UFechaHora.fechaCompuesta(fechaADesde, bean.Hasta.Value);

                        if ((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) || (finSolicitud <= finHorario && finSolicitud > inicioHorario))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido para el día " + fechaADesde.ToString("dd/MM/yyyy") + " debe estar fuera del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                        }
                    }
                    else
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Hora de Inicio y fin  para la regla : " + ConstanteAutorizacionRegla.REQUIERE_HORA_INICIO_FUERA_HORARIO));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin  para la regla : " + ConstanteAutorizacionRegla.REQUIERE_HORA_INICIO_FUERA_HORARIO));
                }
            }

            if (regla.Regla.Equals(ConstanteAutorizacionRegla.REQUIERE_HORA_INICIOFIN_DENTRO_HORARIO))//OK
            {
                // Horario Inicio y Hora Fin debe estar dentro del horario del empleado
                if (bean.Fecha != null)
                {
                    if (bean.Desde != null && bean.Hasta != null)
                    {

                        //a esta hora sale
                        DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

                        //a esta hora entra
                        DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

                        finHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, finHorario.Value);
                        inicioHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, inicioHorario.Value);

                        DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Desde.Value);
                        DateTime? finSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Hasta.Value);

                        if (!((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) && (finSolicitud <= finHorario && finSolicitud > inicioHorario)))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido debe estar dentro del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm")));
                        }
                    }
                    else
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Hora de Inicio para la regla : " + ConstanteAutorizacionRegla.REQUIERE_HORA_INICIOFIN_DENTRO_HORARIO));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio para la regla : " + ConstanteAutorizacionRegla.REQUIERE_HORA_INICIOFIN_DENTRO_HORARIO));
                }

            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_MISMO_CONCEPTO))//OK
            {
                // Hora Inicio - Hora Fin no se debe cruzar con el mismo concepto y dia hora inicio/fin

                //buscar por comcepto y dia
                if (bean.Desde != null && bean.Hasta != null)
                {
                    if (bean.Fecha != null && bean.Fechafin != null)
                    {
                        var mensaej = validarPermisoDelMismoConcepto(bean.Fecha.Value, bean.Desde.Value, bean.Fechafin.Value, bean.Hasta.Value, Convert.ToInt32(bean.Empleado), bean.Conceptoacceso);
                        if (mensaej != null)
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, mensaej.Mensaje));
                        }
                    }
                    else
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_MISMO_CONCEPTO));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Hora de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_MISMO_CONCEPTO));
                }
            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_TODOS_CONCEPTO))//OK
            {
                // Hora Inicio - Hora Fin no se debe cruzar con todos concepto y dia hora inicio/fin
                if (bean.Desde != null && bean.Hasta != null)
                {
                    if (bean.Fecha != null && bean.Fechafin != null)
                    {
                        var mensaej = validarPermisoDentroDeOtroPermiso(bean.Fecha.Value, bean.Desde.Value, bean.Fechafin.Value, bean.Hasta.Value, Convert.ToInt32(bean.Empleado));

                        if (mensaej != null)
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, mensaej.Mensaje));
                        }
                    }
                    else
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Fecha de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_TODOS_CONCEPTO));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Hora de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.CRUZAR_HORA_INICIOFIN_TODOS_CONCEPTO));
                }

            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.HORA_INICIO_FIN_DISTINTAS)) //OK
            {
                // Hora inicial debe ser distinta a la Hora Final

                //Validar la hora desde y hasta
                if (bean.Desde != null && bean.Hasta != null)
                {
                    result = DateTime.Compare(bean.Desde.Value, bean.Hasta.Value);

                    if (result > 0)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La hora hasta No puede ser mayor que la hora desde."));
                    }

                    String horaIncio = bean.Desde.Value.ToString("hh");
                    String horaFin = bean.Hasta.Value.ToString("hh");

                    //Validar que no sea la misma hora
                    if (horaIncio == horaFin)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La hora desde debe ser diferente que la hora hasta."));
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado Hora de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.HORA_INICIO_FIN_DISTINTAS));
                }


            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.VECES_DIA_NRO))//OK
            {
                // Cuantas veces por Dia ###
                //validar que no haya VECES_DIA_NRO permisos ya ingresados
                if (bean.Fecha != null && bean.Fechafin != null)
                {
                    if (regla.Cantidadinicio == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado la cantidad de dias limites en la regla : " + ConstanteAutorizacionRegla.VECES_DIA_NRO));
                    }
                    else
                    {
                        int? numero_dias = regla.Cantidadinicio;
                        if (asAccesosdiariosDao.contarMarcasPorRango(bean.Empleado.Value, bean.Conceptoacceso, bean.Fecha.Value, bean.Fechafin.Value) > numero_dias)
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El límite de marcas es " + numero_dias + " dentro del rango de " + bean.Fecha.Value.ToString("dd/MM/yyyy") + " al " + bean.Fechafin.Value.ToString("dd/MM/yyyy")));
                        }
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado fecha de Inicio y fin para la regla : " + ConstanteAutorizacionRegla.VECES_DIA_NRO));
                }
            }
            if (regla.Regla.Equals(ConstanteAutorizacionRegla.VECES_PERIODO_NRO))//OK
            {
                // Cuantas veces por periodo ###
                //validar que no haya VECES_PERIODO_NRO permisos en el periodo configurado
                if (regla.Fechainicio != null && regla.Fechafin != null)
                {
                    if (regla.Cantidadinicio == null)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se ha ingresado la cantidad de dias limites en la regla : " + ConstanteAutorizacionRegla.VECES_PERIODO_NRO));
                    }
                    else
                    {
                        if (asAccesosdiariosDao.contarMarcasPorRango(bean.Empleado.Value, bean.Conceptoacceso, regla.Fechainicio.Value, regla.Fechafin.Value) > (regla.Cantidadinicio))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El límite de marcas es " + regla.Cantidadinicio + " dentro del rango de " + bean.Fecha.Value.ToString("dd/MM/yyyy") + " al " + bean.Fechafin.Value.ToString("dd/MM/yyyy")));
                        }
                    }
                }
                else
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La fecha de Inicio y la fecha fin no se encuentran ingresadas en la regala : " + ConstanteAutorizacionRegla.VECES_PERIODO_NRO));
                }
            }
            return lstRetorno;
        }


        public List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, string accionSolicitada, AsAutorizacion bean)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();
            Int32? idEmpleado = UInteger.obtenerValorEntero(bean.Empleado);
            DtoEmpleadoBasico dto = empleadomastDao.obtenerInformacionPorIdPersona(usuarioActual);
            List<AsConceptoaccesoRegla> lst = asConceptoaccesoReglaDao.ListarActivos(bean.Conceptoacceso);
            foreach (AsConceptoaccesoRegla regla in lst)
            {
                lstRetorno = solicitudValidar(lstRetorno, usuarioActual, accionSolicitada, bean, regla);
            }
            return lstRetorno;
        }

        public List<MensajeUsuario> solicitudValidarAccion(UsuarioActual usuarioActual, string accionSolicitada, Int32 numeroproceso)
        {
            return asAutorizacionDao.solicitudValidarAccion(usuarioActual, accionSolicitada, numeroproceso);
        }

        public AsAutorizacion obtenerPorId(AsAutorizacionPk pk)
        {
            return asAutorizacionDao.obtenerPorId(pk);
        }

        public bool validacionFechaIngreso(decimal empleado, string concepto, string periodo, DateTime fechadesde, DateTime fechahasta, int tipo, string accion, UsuarioActual usuario)
        {
            if (tipo == 1)
            {
                Empleadomast empleadomast = new Empleadomast();
                EmpleadomastPk empleadomastpk = new EmpleadomastPk();
                empleadomastpk.Empleado = Convert.ToInt32(empleado);
                empleadomastpk.Companiasocio = usuario.CompaniaSocioCodigo;

                empleadomast = empleadomastDao.obtenerPorId(empleadomastpk);

                if (empleadomast.Fechaingreso.Value.Month == Convert.ToInt32(periodo.Substring(4, 2)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (tipo == 2)
            {
                if (accion == "NUEVO")
                {
                    return asAutorizacionDao.validacionFechaIngresoNuevo(empleado, concepto, periodo, fechadesde, fechahasta);
                }
                if (accion == "EDICION")
                {
                    return asAutorizacionDao.validacionFechaIngresoEdicion(empleado, concepto, periodo, fechadesde, fechahasta);
                }
            }

            return false;
        }

        public ParametroPaginacionGenerico listarPermisosReporte(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            return asAutorizacionDao.listarPermisosReporte(paginacion, filtro);
        }


        public ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion, FiltroSolicitudes filtro)
        {
            return asAutorizacionDao.listarSolicitudes(usuarioActual, paginacion, filtro);
        }




        /*
         *
         *   metodos de validaciones de negocio
         *   public MensajeUsuario validarFueraHorario(AsAutorizacion bean)
         *   public MensajeUsuario validarDentroHorario(AsAutorizacion bean)
         *   
         */




        public MensajeUsuario validarFueraHorario(AsAutorizacion bean)
        {
            //a esta hora sale
            DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

            //a esta hora entra
            DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

            finHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, finHorario.Value);
            inicioHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, inicioHorario.Value);

            DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Desde.Value);
            DateTime? finSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Hasta.Value);

            if ((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) || (finSolicitud <= finHorario && finSolicitud > inicioHorario))
            {
                return new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido debe estar fuera del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm"));
            }

            return null;
        }

        public MensajeUsuario validarPermisoDelMismoConcepto(DateTime Fecha, DateTime Desde, DateTime Fechafin, DateTime Hasta, int empleado, String concepto)
        {
            var fechaInicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Desde.Hour, Desde.Minute, 0);
            var fechaFin = new DateTime(Fechafin.Year, Fechafin.Month, Fechafin.Day, Hasta.Hour, Hasta.Minute, 0);

            var cruces = asAccesosdiariosDao.listarCrucesConceptos(fechaInicio, fechaFin, empleado, concepto);

            if (cruces != 0)
            {
                return new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido para el día " + fechaInicio.ToString("dd/MM/yyyy") + " a la(s) " + fechaInicio.ToString("HH:mm") + " se cruza con un permiso ya ingresado");
            }

            return null;
        }

        public MensajeUsuario validarPermisoDentroDeOtroPermiso(DateTime Fecha, DateTime Desde, DateTime Fechafin, DateTime Hasta, int empleado)
        {
            var fechaInicio = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Desde.Hour, Desde.Minute, 0);
            var fechaFin = new DateTime(Fechafin.Year, Fechafin.Month, Fechafin.Day, Hasta.Hour, Hasta.Minute, 0);

            var cruces = asAccesosdiariosDao.listarCruces(fechaInicio, fechaFin, empleado);

            if (cruces != 0)
            {
                return new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido para el día " + fechaInicio.ToString("dd/MM/yyyy") + " a la(s) " + fechaInicio.ToString("HH:mm") + " se cruza con un permiso ya ingresado");
            }

            return null;
        }

        public MensajeUsuario validarDentroHorario(AsAutorizacion bean)
        {
            //a esta hora sale
            DateTime? finHorario = empleadomastDao.obtenerHoraFinHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

            //a esta hora entra
            DateTime? inicioHorario = empleadomastDao.obtenerHoraInicioHorarioPorDia(Convert.ToInt32(bean.Empleado), bean.Companyowner, bean.Fecha);

            finHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, finHorario.Value);
            inicioHorario = UFechaHora.fechaCompuesta(bean.Fecha.Value, inicioHorario.Value);

            DateTime? inicioSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Desde.Value);
            DateTime? finSolicitud = UFechaHora.fechaCompuesta(bean.Fecha.Value, bean.Hasta.Value);

            if (!((inicioSolicitud < finHorario && inicioSolicitud >= inicioHorario) && (finSolicitud <= finHorario && finSolicitud > inicioHorario)))
            {
                return new MensajeUsuario(tipo_mensaje.ERROR, "El rango establecido debe estar dentro del rango " + inicioHorario.Value.ToString("HH:mm") + " - " + finHorario.Value.ToString("HH:mm"));
            }

            return null;
        }

        public AsAutorizacion obtenerValidacionPorConcepto(AsAutorizacion autorizacion)
        {
            AsConceptoaccesoPk pk = new AsConceptoaccesoPk();
            pk.Conceptoacceso = autorizacion.Conceptoacceso;
            AsConceptoacceso acceso = asConceptoaccesoDao.obtenerPorId(pk);

            AsAutorizacion asAutorizacion = new AsAutorizacion();

            asAutorizacion.habilitarhoraInicio = false;
            asAutorizacion.habilitarhoraFin = false;
            asAutorizacion.habilitarFechaInicio = false;
            asAutorizacion.habilitarFechaFin = false;
            asAutorizacion.verhoraFin = true;
            asAutorizacion.verFechaFin = true;
            asAutorizacion.verCompromiso = true;
            asAutorizacion.verTodasHoras = true;
            asAutorizacion.verAdjunto = false;

            if (acceso.Conceptoacceso == "31" || acceso.Conceptoacceso == "29" || acceso.Conceptoacceso == "30")
            {
                if (acceso.CantidadHoras == null)
                {
                    return null;
                }


                var cantidadHoras = acceso.CantidadHoras.Value.Hour;

                // DESABILITAR LA HORA Y FECHA FIN
                asAutorizacion.habilitarhoraFin = true;
                asAutorizacion.habilitarFechaFin = true;

                //AGREGA LAS HORAS SEGUN LA CANTIDAD DE HORAS CONFIGURADAS
                if (asAutorizacion.Desde == null)
                {
                    asAutorizacion.Desde = DateTime.Now.AddHours(0).AddMinutes(0);
                    asAutorizacion.Hasta = asAutorizacion.Desde.Value.AddHours(cantidadHoras);
                }
                else
                {
                    var minutos = asAutorizacion.Desde.Value.Minute;
                    asAutorizacion.Hasta = asAutorizacion.Desde.Value.AddHours(cantidadHoras);
                    asAutorizacion.Desde = asAutorizacion.Desde.Value.AddMinutes(-minutos);
                }
            }

            // oculta la fecha fin y la hora fin
            if (acceso.Conceptoacceso == "OMIS")
            {
                asAutorizacion.verhoraFin = false;
                asAutorizacion.verFechaFin = false;
            }

            if (acceso.Conceptoacceso == "PAHE")
            {
                asAutorizacion.verCompromiso = true;
            }


            if (acceso.FlagAdjunto != null || acceso.FlagAdjunto != "N")
            {
                asAutorizacion.verAdjunto = true;
            }

            if (acceso.Expresadoen == "D")
            {
                asAutorizacion.verTodasHoras = false;
            }

            return asAutorizacion;
        }

        public int obtenerAutorizacionPorId(AsAutorizacionPk pk)
        {
            return obtenerAutorizacionPorId(pk);
        }

        public string obtenerComportamientoSobretiempo(int empleado)
        {
            return asAutorizacionDao.obtenerComportamientoSobretiempo(empleado);
        }

        public string obtenerCantidadSolicitudesParaAprobar()
        {
            return asAutorizacionDao.obtenerCantidadSolicitudesParaAprobar();
        }

        public AsAutorizacion obtenerHorario(FiltroSolicitudes filtro)
        {
            return asAutorizacionDao.obtenerHorario(filtro);
        }
    }
}
