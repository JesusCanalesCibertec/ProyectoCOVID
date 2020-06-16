
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.interfaz;
using net.royal.spring.planilla.servicio.impl;
using net.royal.spring.planilla.servicio;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.core.dao;
using Microsoft.AspNetCore.Http;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio;
using System.Net.Mail;
using System.Net;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dao;

namespace net.royal.spring.sistema.servicio.impl
{

    public class SyAprobacionprocesoServicioImpl : GenericoServicioImpl, SyAprobacionprocesoServicio
    {

        private IServiceProvider servicioProveedor;
        private SyAprobacionprocesoDao syAprobacionprocesoDao;
        private SyAprobacionnivelesDao syAprobacionnivelesDao;
        private SyAprobacionnivelesDetDao syAprobacionnivelesDetDao;
        private AprobacionRrhhDao aprobacionRrhhDao;
        private SyCorreoServicio syCorreoServicio;
        private SyPlantillaServicio syPlantillaServicio;
        private SyReporteServicio syReporteServicio;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private readonly IHttpContextAccessor httpContextAccessor;
        private BpEnlaceServicio bpEnlaceServicio;        
        private EmpleadomastDao empleadomastDao;
        // private HrRequerimientoDao requerimientoServicio;
        // private HrContratosSolrenovacionServicio hrContratosSolrenovacionServicio;

        private ParametrosmastDao parametrosmastDao;

        public SyAprobacionprocesoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            //hrContratosSolrenovacionServicio = _servicioProveedor.GetService<HrContratosSolrenovacionServicio>();
            syAprobacionprocesoDao = servicioProveedor.GetService<SyAprobacionprocesoDao>();
            syAprobacionnivelesDao = servicioProveedor.GetService<SyAprobacionnivelesDao>();
            syAprobacionnivelesDetDao = servicioProveedor.GetService<SyAprobacionnivelesDetDao>();
            aprobacionRrhhDao = servicioProveedor.GetService<AprobacionRrhhDao>();
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            syPlantillaServicio = servicioProveedor.GetService<SyPlantillaServicio>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            httpContextAccessor = _servicioProveedor.GetService<IHttpContextAccessor>();
            bpEnlaceServicio = _servicioProveedor.GetService<BpEnlaceServicio>();
            empleadomastDao = _servicioProveedor.GetService<EmpleadomastDao>();
            parametrosmastDao = _servicioProveedor.GetService<ParametrosmastDao>();
            //this.requerimientoServicio = requerimientoServicio;
        }

        private List<DtoCorreo> generarCorreosNivel0(UsuarioActual usuarioActual, DtoSolicitud bean)
        {
            List<DtoCorreo> lst = new List<DtoCorreo>();

            if (bean == null)
                throw new UException("No se envío información de la Solicitud");

            SyAprobacionniveles ncab = syAprobacionnivelesDao.obtenerPorCodigoProcesoCompania(bean.ProcesoAprobar, UString.estaVacio(bean.CompaniaCodigo) ? usuarioActual.CompaniaSocioCodigo : bean.CompaniaCodigo);

            if (ncab == null)
                throw new UException("No se encontro los niveles de aprobación :" + UInteger.obtenerValorEnteroSinNulo(bean.ProcesoAprobar).ToString());

            DtoEmpleado empleado = null;
            empleado = syAprobacionnivelesDao.obtenerEmpleado(bean.SolicitanteId, bean.CompaniaCodigo);
            if (empleado == null)
            {
                empleado = syAprobacionnivelesDao.obtenerEmpleado(bean.SolicitanteId);
            }

            //obtener el jefe de 
            DtoCorreo jefe = !empleado.JefeResponsable.HasValue ? null : obtenerDatosCorreoPorSolicitud(empleado.JefeResponsable, bean.CompaniaCodigo);

            String[] correos2 = UString.split(UString.obtenerValorCadenaSinNulo(ncab.Nivel0Correoelectronico), ";");
            foreach (var correoId in correos2)
            {
                lst.Add(new DtoCorreo(-900, null, correoId));
            }
            if (UBoolean.validarFlag(ncab.Nivel0Flagsolicitante))
                lst.Add(new DtoCorreo(bean.SolicitanteId.Value, bean.CompaniaCodigo, empleado.CorreoInterno));
            if (UBoolean.validarFlag(ncab.Nivel0Flagsuperior) && jefe != null)
            {
                lst.Add((jefe));
            }

            return lst;
        }

        public List<SyAprobacionproceso> listar(FiltroAprobacionProceso filtro)
        {
            return syAprobacionprocesoDao.listar(filtro);
        }

        public List<SyAprobacionproceso> listarPorAplicacion(string idAplicacion)
        {
            FiltroAprobacionProceso filtro = new FiltroAprobacionProceso();
            filtro.IdAplicacion = idAplicacion;
            return listar(filtro);
        }

        public List<SyAprobacionproceso> listarPorAplicacionActivos(string idAplicacion)
        {
            FiltroAprobacionProceso filtro = new FiltroAprobacionProceso();
            filtro.IdAplicacion = idAplicacion;
            filtro.Estado = "A";
            return listar(filtro);
        }

        public ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion, FiltroSolicitudes filtro)
        {
            return syAprobacionprocesoDao.listarSolicitudes(usuarioActual, paginacion, filtro);
        }

        public List<DtoSolicitud> solicitudEventoPreparar(UsuarioActual usuarioActual, string accion, List<DtoSolicitud> listaSolicitudes)
        {
            String flg;
            DtoSolicitud solicitud;
            //SyAccionInterfaz interfazPrestamo = servicioProveedor.GetService<PrPrestamoServicio>();

            for (int i = 0; i < listaSolicitudes.Count; i++)
            {
                if (listaSolicitudes[i].FlgSeleccionado.Equals("S"))
                {
                    solicitud = listaSolicitudes[i];

                    /**
                     * para todos los casos se le pone en null - ya en su proceso lo
                     * evaluara
                     **/
                    listaSolicitudes[i].FlgSolicitarInformacionPrestamo = "N";

                    /** buscar si el usuario es SuperUsuario - Por Defecto N **/
                    flg = syAprobacionnivelesDao.esSuperUsuario(new SyAprobacionnivelesPk(solicitud.ProcesoAprobar, solicitud.CompaniaCodigo),
                            usuarioActual.PersonaId, solicitud.CompaniaCodigo);
                    listaSolicitudes[i].FlgEsSuperUsuario = flg;

                    listaSolicitudes[i].FlgAdministradorApruebaTodo = null;
                    /** buscar si el usuario es Administrador - Por Defecto N **/
                    flg = syAprobacionnivelesDao.esAdministrador(new SyAprobacionnivelesPk(solicitud.ProcesoAprobar, solicitud.CompaniaCodigo),
                            usuarioActual.PersonaId, solicitud.CompaniaCodigo);
                    listaSolicitudes[i].FlgEsAdministrador = flg;

                    listaSolicitudes[i].FlgTieneCorreos = "N";
                    listaSolicitudes[i].FlgEnviarSinCorreos = null;
                    /** buscar si existe correos a enviar - Por Defecto N **/


                    if (!UValidador.esListaVacia(syAprobacionnivelesDetDao.obtenerListaCorreoPorProceso(solicitud.ProcesoAprobar, solicitud.NivelAprobar)))
                    {
                        listaSolicitudes[i].FlgTieneCorreos = "S";
                    }

                    listaSolicitudes[i].FlgSolicitarObservaciones = "N";
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_RECHAZAR))
                    {
                        listaSolicitudes[i].FlgEsAdministrador = "N";
                        listaSolicitudes[i].FlgEsSuperUsuario = "N";
                        listaSolicitudes[i].FlgSolicitarObservaciones = "S";
                    }

                    /** solo para prestamos **/
                    //evaluar si es el ultimo nivel
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_APROBAR))
                    {
                        var nivel = new SyAprobacionniveles();

                        nivel = syAprobacionnivelesDao.obtenerPorCodigoProcesoCompania(solicitud.ProcesoAprobar, solicitud.CompaniaCodigo);
                        if (nivel == null)
                        {
                            throw new Exception("No se tiene configuracion del proceso " + solicitud.ProcesoInternoAprobar);
                        }
                        Int32? nroNiveles = nivel.Nroniveles;
                        nroNiveles = UInteger.obtenerValorEnteroSinNulo(nroNiveles);

                        if (nroNiveles == solicitud.NivelAprobar)
                        {
                            if (solicitud.ProcesoCodigo.Equals(ConstanteProceso.PROCESO_PLANILLA_PRESTAMO))
                            {
                                // YA NO SE PIDE LOS DATOS DEL OTORGAMIENTO
                                //solicitud.FlgSolicitarInformacionPrestamo = "S";
                            }
                            solicitud.FlgEsAdministrador = "N";
                            solicitud.FlgEsAdministrador = "N";
                        }
                        listaSolicitudes[i] = solicitud;

                    }
                }
            }

            return listaSolicitudes;
        }

        public List<DtoSolicitud> solicitudEventoEjecutar(UsuarioActual usuarioActual, string accion, List<DtoSolicitud> listaSolicitudes)
        {
            SyAccionInterfaz interfaz = null;
            String estadoNivel = null;
            String observacionRechazo = null;
            String observacionReversa = null;
            String observacionAprobacion = null;
            String nombreServicio = null;
            String accionInicial = accion;

            EmailConfiguracion configCorreo = syCorreoServicio.obtenerConfiguracion();

            for (int i = 0; i < listaSolicitudes.Count; i++)
            {
                accion = accionInicial;

                DtoSolicitud bean = listaSolicitudes[i];

                if (bean.FlgSeleccionado.Equals("S"))
                {
                    observacionAprobacion = null;
                    observacionRechazo = null;
                    observacionReversa = null;

                    nombreServicio = maMiscelaneosdetalleDao.obtenerValorCodigo4CuandoPkValorCodigo2(ConstanteAprobacion.APLICACION_CODIGO, ConstanteAprobacion.MISCELANEO_PROCESO_APROBACION, bean.ProcesoCodigo);

                    nombreServicio = UString.obtenerValorCadenaSinNulo(nombreServicio);
                    interfaz = null;
                    if (nombreServicio.Equals("BeanServicioAsAutorizacion"))
                        interfaz = servicioProveedor.GetService<AsAutorizacionServicio>();
                    /*
                    if (nombreServicio.Equals("BeanServicioHrRequerimiento"))
                        interfaz = servicioProveedor.GetService<HrRequerimientoServicio>();
                    if (nombreServicio.Equals("BeanServicioHrCapacitacion"))
                        interfaz = servicioProveedor.GetService<HrCapacitacionServicio>();                    
                    if (nombreServicio.Equals("BeanServicioPrSolicitudvacacion"))
                        interfaz = servicioProveedor.GetService<PrSolicitudvacacionServicio>();
                    if (nombreServicio.Equals("BeanServicioPrPrestamo"))
                        interfaz = servicioProveedor.GetService<PrPrestamoServicio>();
                    if (nombreServicio.Equals("BeanServicioHrActualizacionfichaempleado"))
                        interfaz = servicioProveedor.GetService<HrActualizacionfichaempleadoServicio>();
                    if (nombreServicio.Equals("BeanServicioHrRenovacionContratos"))
                        interfaz = servicioProveedor.GetService<HrContratosSolrenovacionServicio>();
                    if (nombreServicio.Equals("BeanServicioHrFichaRequerimiento"))
                        interfaz = servicioProveedor.GetService<HrFichaRequerimientoServicio>();
                    if (nombreServicio.Equals("BeanServicioAsActualizacionHorarioEmpleado"))
                        interfaz = servicioProveedor.GetService<HrSolicitudGeneralServicio>();
                    */
                    bean.servicio = nombreServicio;

                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_RECHAZAR))
                    {
                        estadoNivel = "R";
                        observacionRechazo = "";
                        interfaz.ejecutarAccion(usuarioActual, accion, bean);
                    }
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_APROBAR))
                    {
                        estadoNivel = "A";
                        observacionAprobacion = "Proceso Normal.";

                        Boolean flgEjecutarAprobacion = false;
                        accion = ConstanteAprobacion.SOLICITUD_ACCION_SEGUIMIENTO;


                        var nivel = syAprobacionnivelesDao.obtenerPorCodigoProcesoCompania(bean.ProcesoAprobar, bean.CompaniaCodigo);

                        if (nivel == null)
                        {
                            throw new Exception("No se tiene configuracion del proceso " + bean.ProcesoInternoAprobar);
                        }
                        Int32? nroNiveles = nivel.Nroniveles;
                        nroNiveles = UInteger.obtenerValorEnteroSinNulo(nroNiveles);

                        //en contratos, el nro niveles solo se contaran hasta aquellos que se relacionen con
                        //el tipo de contrato de la solicitud actual
                       
                        if (nroNiveles == bean.NivelAprobar)
                        {
                            flgEjecutarAprobacion = true;
                            accion = ConstanteAprobacion.SOLICITUD_ACCION_APROBAR;
                            observacionAprobacion = "Proceso ejecutado por Usuario Administrador (Aprobación definitiva del registro).";
                        }
                        if (bean.FlgEsAdministrador.Equals("S"))
                        {
                            observacionAprobacion = "Proceso ejecutado por Usuario Administrador (Flujo Normal).";
                        }
                        if (bean.FlgEsAdministrador.Equals("S") && bean.FlgAdministradorApruebaTodo.Equals("S"))
                        {
                            flgEjecutarAprobacion = true;
                            accion = ConstanteAprobacion.SOLICITUD_ACCION_APROBAR;
                            observacionAprobacion = "Proceso ejecutado por Usuario Administrador (Aprobación definitiva del registro).";
                        }
                        if (bean.FlgEsSuperUsuario.Equals("S"))
                        {
                            flgEjecutarAprobacion = true;
                            accion = ConstanteAprobacion.SOLICITUD_ACCION_APROBAR;
                            observacionAprobacion = "Proceso Aprobado por Super Usuario";
                        }

                        MensajeUsuario mensajeValidacion = interfaz.validarPreAccion(usuarioActual, accion, bean);


                        if (mensajeValidacion != null)
                        {
                            throw new Exception(mensajeValidacion.Mensaje);
                        }

                        interfaz.ejecutarAccion(usuarioActual, accion, bean);
                    }
                    if (accion.Equals(ConstanteAprobacion.SOLICITUD_ACCION_DEVOLVER))
                    {
                        estadoNivel = "D";
                        observacionReversa = "";
                        interfaz.ejecutarAccion(usuarioActual, accion, bean);
                    }

                    /** INSERT INTO APROBACION_RRHH **/
                    AprobacionRrhh aprobacionRrhh = new AprobacionRrhh();
                    aprobacionRrhh.Aplicacion = bean.AplicacionCodigo;
                    aprobacionRrhh.Codigoproceso = bean.ProcesoCodigo;
                    aprobacionRrhh.Companiasocio = bean.CompaniaCodigo;
                    aprobacionRrhh.Estadonivel = estadoNivel;
                    aprobacionRrhh.Nivel = bean.NivelAprobar;
                    aprobacionRrhh.Numeroproceso = bean.ProcesoNro;
                    aprobacionRrhh.Proceso = bean.ProcesoAprobar;
                    aprobacionRrhh.Procesointerno = bean.ProcesoInternoAprobar;
                    aprobacionRrhh.Usuarioaprobador = usuarioActual.PersonaId;

                    aprobacionRrhh.MotivoRechazo = observacionRechazo;
                    aprobacionRrhh.Observacion = observacionAprobacion;
                    aprobacionRrhh.MotivoReversion = observacionReversa;
                    aprobacionRrhh.Ultimafechamodif = DateTime.Today;
                    aprobacionRrhh.Ultimousuario = usuarioActual.UsuarioLogin;
                    if (aprobacionRrhh.Numeroproceso != null)
                    {
                        aprobacionRrhhDao.registrar(aprobacionRrhh);
                    }


                    enviarCorreoPorTransaccion(usuarioActual,
                        bean,
                        accion, interfaz, syCorreoServicio, configCorreo);
                }
            }
            return listaSolicitudes;
        }

        private List<DtoCorreo> generarCorreos(UsuarioActual usuarioActual, DtoSolicitud bean)
        {
            // DARIO: esto debe ser parametrizable si se desea el modo normal de SPRING
            Boolean flgEnvioCorreoEscalonado = true;

            String tipoBusqueda = parametrosmastDao.obtenerValorCadena(ConstanteAprobacion.PARAMETRO_TIPO_BUSQUEDA, ConstanteAprobacion.APLICACION_CODIGO);
            if (UString.estaVacio(tipoBusqueda))
                tipoBusqueda = ConstanteAprobacion.TIPO_BUSQUEDA_DEPARTAMENTO;

            List<DtoCorreo> lst = new List<DtoCorreo>();

            if (bean == null)
                throw new UException("No se envío información de la Solicitud");

            SyAprobacionniveles ncab = syAprobacionnivelesDao.obtenerPorCodigoProcesoCompania(bean.ProcesoAprobar, UString.estaVacio(bean.CompaniaCodigo) ? usuarioActual.CompaniaSocioCodigo : bean.CompaniaCodigo);

            if (ncab == null)
                throw new UException("No se encontro los niveles de aprobación :" + UInteger.obtenerValorEnteroSinNulo(bean.ProcesoAprobar).ToString());


            //
            Int32? nroNiveles = ncab.Nroniveles;
            nroNiveles = UInteger.obtenerValorEnteroSinNulo(nroNiveles);

            //en contratos, el nro niveles solo se contaran hasta aquellos que se relacionen con
            //el tipo de contrato de la solicitud actual
            if (bean.servicio != null)
            {
            }
            //

            DtoEmpleado empleado = null;
            empleado = syAprobacionnivelesDao.obtenerEmpleado(bean.SolicitanteId, bean.CompaniaCodigo);
            if (empleado == null)
            {
                empleado = syAprobacionnivelesDao.obtenerEmpleado(bean.SolicitanteId);
            }

            //obtener el jefe de 
            DtoCorreo jefe = !empleado.JefeResponsable.HasValue ? null : obtenerDatosCorreoPorSolicitud(empleado.JefeResponsable, bean.CompaniaCodigo);

            if (flgEnvioCorreoEscalonado)
            {
                bean.NivelAprobar = UInteger.obtenerValorEnteroSinNulo(bean.NivelAprobar) + 1;
                if (bean.NivelAprobar > nroNiveles)
                {
                    lst.Add(new DtoCorreo(bean.SolicitanteId.Value, bean.CompaniaCodigo, empleado.CorreoInterno));
                    bean.NivelAprobar = -1;

                    return listaLimpiarDuplicadosNulos(generarCorreosNivel0(usuarioActual, bean));
                }

            }

            List<SyAprobacionnivelesDet> lstDetalle = syAprobacionnivelesDetDao.listarPorCodigoNivel(ncab.Codigo.Value, bean.NivelAprobar.Value, ncab.CompanyOwner);

            if (bean == null)
                return lst;

            if (bean.accion == ConstanteAprobacion.SOLICITUD_ACCION_APROBAR || bean.accion == ConstanteAprobacion.SOLICITUD_ACCION_RECHAZAR)
            {

                lst = new List<DtoCorreo>();
                lst.AddRange(generarCorreosNivel0(usuarioActual, bean));

                return lst;
            }

            // ** si es super usuario **//
            if (UBoolean.validarFlag(ncab.Flagusuariosuper))
            {

                lst.AddRange(obtenerCorreoPorCadena(ncab.Correoelectronico, bean.CompaniaCodigo, ncab.CompanyOwnerusuario, ncab.Usuario));

                if (UBoolean.validarFlag(ncab.Flagsuperior))
                {
                    if (jefe != null)
                    {
                        lst.Add((jefe));
                    }
                }

                if (UBoolean.validarFlag(ncab.Flagsolicitante))
                {
                    lst.Add(new DtoCorreo(bean.SolicitanteId.Value, bean.CompaniaCodigo, empleado.CorreoInterno));
                }

                return listaLimpiarDuplicadosNulos(lst);
            }

            // ** CUANDO ES ADMINISTRADOR Y REALIZA LA APROBACION FINAL **//
            if (UBoolean.validarFlag(bean.FlgEsAdministrador)
                    && UBoolean.validarFlag(bean.FlgAdministradorApruebaTodo))
            {
                if (empleado != null)
                {
                    lst.Add(new DtoCorreo(bean.SolicitanteId.Value, bean.CompaniaCodigo, empleado.CorreoInterno));
                }

                lst.AddRange(generarCorreosNivel0(usuarioActual, bean));

                return listaLimpiarDuplicadosNulos(lst);
            }

            /** por cada uno de los niveles **/
            foreach (SyAprobacionnivelesDet ndet in lstDetalle)
            {
                Boolean flgObtenerCorreo = false;
                if (!UBoolean.validarFlag(ndet.Flagarea))
                {
                    if (tipoBusqueda.Equals(ConstanteAprobacion.TIPO_BUSQUEDA_UNIDAD_OPERATIVA) &&
                        UString.obtenerValorCadenaSinNulo(empleado.UnidadOperativa).Length > 0)
                    {
                        String buscar = UString.obtenerValorCadenaSinNulo(empleado.UnidadOperativa).Trim() + ",";
                        if (UString.obtenerValorCadenaSinNulo(ndet.Area).IndexOf(buscar) >= 0)
                        {
                            flgObtenerCorreo = true;
                        }
                    }
                    if (tipoBusqueda.Equals(ConstanteAprobacion.TIPO_BUSQUEDA_DEPARTAMENTO) &&
                        UString.obtenerValorCadenaSinNulo(empleado.DepartamentoOperacional).Length > 0)
                    {
                        String buscar = UString.obtenerValorCadenaSinNulo(empleado.DepartamentoOperacional).Trim() + ",";
                        if (UString.obtenerValorCadenaSinNulo(ndet.Area).IndexOf(buscar) >= 0)
                        {
                            flgObtenerCorreo = true;
                        }
                    }
                    if (tipoBusqueda.Equals(ConstanteAprobacion.TIPO_BUSQUEDA_DEPARTAMENTO_ORGANIZACION) &&
                        UString.obtenerValorCadenaSinNulo(empleado.DeptoOrganizacion).Length > 0)
                    {
                        String buscar = UString.obtenerValorCadenaSinNulo(empleado.DeptoOrganizacion).Trim() + ",";
                        if (UString.obtenerValorCadenaSinNulo(ndet.Area).IndexOf(buscar) >= 0)
                        {
                            flgObtenerCorreo = true;
                        }
                    }
                    if (tipoBusqueda.Equals(ConstanteAprobacion.TIPO_BUSQUEDA_CENTROCOSTO) &&
                        UString.obtenerValorCadenaSinNulo(empleado.CentroCostos).Length > 0)
                    {
                        String buscar = UString.obtenerValorCadenaSinNulo(empleado.CentroCostos).Trim() + ",";
                        if (UString.obtenerValorCadenaSinNulo(ndet.Area).IndexOf(buscar) >= 0)
                        {
                            flgObtenerCorreo = true;
                        }
                    }
                }
                else
                {
                    flgObtenerCorreo = true;
                }

                if (flgObtenerCorreo)
                {
                    lst.AddRange(obtenerCorreoPorCadena(ndet.Correoelectronico, bean.CompaniaCodigo, ndet.companyownerusuario, ndet.Usuario));

                    if (UBoolean.validarFlag(ndet.Flagsolicitante))
                        lst.Add(new DtoCorreo(bean.SolicitanteId.Value, bean.CompaniaCodigo, empleado.CorreoInterno));
                    if (UBoolean.validarFlag(ndet.Flagsuperior) && jefe != null)
                    {
                        lst.Add((jefe));
                    }
                }
            }

            return listaLimpiarDuplicadosNulos(lst);
        }

        public DtoCorreo obtenerCorreoPorPreDatos(List<DtoEmpleado> preDatos, string solicitudCompania)
        {
            DtoEmpleado dato = null;
            if (preDatos.Count == 1)
            {
                dato = preDatos[0];
            }
            else if (preDatos.Count != 0)
            {
                foreach (var item in preDatos)
                {
                    if (item.Compania.Equals(solicitudCompania))
                    {
                        dato = item;
                        break;
                    }
                }
            }
            if (dato != null)
            {
                return new DtoCorreo(dato.empleado, dato.Compania, dato.CorreoInterno);
            }
            return null;
        }

        public DtoCorreo obtenerCorreoPorPreDatos(List<Empleadomast> preDatos, string solicitudCompania)
        {
            Empleadomast dato = null;
            if (preDatos.Count == 1)
            {
                dato = preDatos[0];
            }
            else if (preDatos.Count != 0)
            {
                foreach (var item in preDatos)
                {
                    if (item.Companiasocio.Equals(solicitudCompania))
                    {
                        dato = item;
                        break;
                    }
                }
            }
            if (dato != null)
            {
                return new DtoCorreo(dato.Empleado.Value, dato.Companiasocio, dato.Correointerno);
            }
            return null;
        }

        public DtoCorreo obtenerDatosCorreoPorSolicitud(Int32? empleado, string solicitudCompania)
        {
            List<DtoEmpleado> preDatos = syAprobacionnivelesDao.obtenerEmpleados(empleado);
            return obtenerCorreoPorPreDatos(preDatos, solicitudCompania);
        }

        public List<DtoCorreo> obtenerCorreoPorCadena(string cadena, string solicitudCompania, string responsablenivelCompania, int? responsablenivel)
        {
            List<DtoCorreo> correos = new List<DtoCorreo>();

            //remover en caso se encuetre el correo de responsablenivelCompania
            Empleadomast emp = empleadomastDao.obtenerPorIdEmpleadoCompania(responsablenivel, responsablenivelCompania);

            if (emp != null)
            {
                if (!UString.estaVacio(emp.Correointerno))
                {
                    if (!UString.estaVacio(cadena))
                    {
                        cadena = cadena.Replace(emp.Correointerno + ";", "");
                    }

                    correos.Add(new DtoCorreo(responsablenivel.Value, responsablenivelCompania, emp.Correointerno));
                }
            }


            if (UString.estaVacio(cadena))
            {
                return correos;
            }

            String[] correos2 = UString.split(cadena, ";");

            foreach (var correoId in correos2)
            {
                List<Empleadomast> preDatos = empleadomastDao.obtenerVariosPorCorreoInterno(correoId);

                if (preDatos.Count == 0)
                {
                    correos.Add(new DtoCorreo(-900, null, correoId));
                }
                else
                {
                    DtoCorreo e = obtenerCorreoPorPreDatos(preDatos, solicitudCompania);

                    if (e != null)
                    {
                        correos.Add(e);
                    }
                }
            }

            return correos;
        }

        public List<DtoCorreo> listaLimpiarDuplicadosNulos(List<DtoCorreo> correos)
        {
            List<DtoCorreo> correosLimpios = new List<DtoCorreo>();
            foreach (var item in correos)
            {
                if (!UString.estaVacio(item.correo))
                {
                    var esta = false;
                    foreach (var item2 in correosLimpios)
                    {
                        if (item.correo.Equals(item2.correo))
                        {
                            esta = true;
                        }
                    }
                    if (!esta)
                    {
                        correosLimpios.Add(item);
                    }
                }
            }
            return correosLimpios;
        }

        public void enviarCorreoPorTransaccion(UsuarioActual usuarioActual,
            DtoSolicitud bean,
            String accion)
        {
            enviarCorreoPorTransaccion(usuarioActual, bean, accion, null, null, null);
        }

        public List<SyAprobacionproceso> listarCodigoProcesoPorCodigoProcesoBase(String codigoProcesoBase)
        {
            return syAprobacionprocesoDao.listarCodigoProcesoPorCodigoProcesoBase(codigoProcesoBase);
        }

        public Int32 obtenerCodigoProcesoAprobacion(String codigoProcesoBase)
        {

            List<SyAprobacionproceso> lst = listarCodigoProcesoPorCodigoProcesoBase(codigoProcesoBase);

            if (lst.Count == 0)
                throw new UException("No se encontro Código de Proceso para :" + UString.obtenerValorCadenaSinNulo(codigoProcesoBase));
            if (lst.Count > 1)
                throw new UException("Se encontro mas de un Código de Proceso para :" + UString.obtenerValorCadenaSinNulo(codigoProcesoBase));
            if (lst.Count == 1)
                return lst[0].Codigoproceso.Value;

            if (UString.estaVacio(codigoProcesoBase))
                return 0;

            if (codigoProcesoBase.Equals("CA")) // CONTROL DE ACTIVIDADES
                return 4;
            if (codigoProcesoBase.Equals("CP")) // CAPACITACION
                return 2;
            if (codigoProcesoBase.Equals("EC")) // ENCUESTA
                return 6;
            if (codigoProcesoBase.Equals("FP")) // FICHA DEL EMPLEADO
                return 17;
            if (codigoProcesoBase.Equals("MP")) // MOVIMIENTO DE PERSONAL
                return 3;
            if (codigoProcesoBase.Equals("PA")) // PLAN DE ACTIVIDADES
                return 12;
            if (codigoProcesoBase.Equals("PC")) // PLAN DE CAPACITACIÓN
                return 7;
            if (codigoProcesoBase.Equals("PL")) // DESCANSO MÉDICO Y SUBSIDIO
                return 5;
            if (codigoProcesoBase.Equals("PR")) // PRÉSTAMOS
                return 10;
            if (codigoProcesoBase.Equals("RS")) // RECLUTAMIENTO Y SELECCION
                return 1;
            if (codigoProcesoBase.Equals("SO")) // SOLICITUDES
                return 11;
            if (codigoProcesoBase.Equals("VP")) // VACACIONES PAGO
                return 9;
            if (codigoProcesoBase.Equals("VU")) // VACACIONES UTILIZACION
                return 8;
            if (codigoProcesoBase.Equals("VW")) // VACACION UTILIZACION WEB
                return 18;
            return 0;
        }

        public void enviarCorreoPorTransaccion(UsuarioActual usuarioActual,
           DtoSolicitud solicitud,
            String accion, SyAccionInterfaz interfaz,
            SyCorreoServicio syCorreo, EmailConfiguracion configCorreo)
        {
            String urlSystemPath = this.obtenerServidorWeb(httpContextAccessor);

            String flg;

            if (configCorreo == null)
                configCorreo = syCorreoServicio.obtenerConfiguracion();

            if (configCorreo.FlgEnviar.Contains("N"))
            {
                return;
            }

            if (UInteger.esCeroOrNulo(solicitud.ProcesoAprobar))
            {
                solicitud.ProcesoAprobar = obtenerCodigoProcesoAprobacion(solicitud.ProcesoCodigo);
                solicitud.ProcesoAprobar = solicitud.ProcesoAprobar;
            }

            flg = syAprobacionnivelesDao.esSuperUsuario(new SyAprobacionnivelesPk(solicitud.ProcesoAprobar, solicitud.CompaniaCodigo),
                    usuarioActual.PersonaId, solicitud.CompaniaCodigo);
            solicitud.FlgEsSuperUsuario = flg;
            flg = syAprobacionnivelesDao.esAdministrador(new SyAprobacionnivelesPk(solicitud.ProcesoAprobar, solicitud.CompaniaCodigo),
                    usuarioActual.PersonaId, solicitud.CompaniaCodigo);
            if (UString.estaVacio(solicitud.FlgEsAdministrador))
            {
                solicitud.FlgEsAdministrador = flg;
                solicitud.FlgAdministradorApruebaTodo = null;
            }

            if (interfaz == null)
            {
                String nombreServicio = maMiscelaneosdetalleDao.obtenerValorCodigo4CuandoPkValorCodigo2(ConstanteAprobacion.APLICACION_CODIGO, ConstanteAprobacion.MISCELANEO_PROCESO_APROBACION, solicitud.ProcesoCodigo);

                if (UString.estaVacio(nombreServicio))
                    nombreServicio = "";

                if (nombreServicio.Equals("BeanServicioAsAutorizacion"))
                    interfaz = servicioProveedor.GetService<AsAutorizacionServicio>();
                /*
                if (nombreServicio.Equals("BeanServicioHrRequerimiento"))
                    interfaz = servicioProveedor.GetService<HrRequerimientoServicio>();
                if (nombreServicio.Equals("BeanServicioHrCapacitacion"))
                    interfaz = servicioProveedor.GetService<HrCapacitacionServicio>();
                if (nombreServicio.Equals("BeanServicioAsAutorizacion"))
                    interfaz = servicioProveedor.GetService<AsAutorizacionServicio>();
                if (nombreServicio.Equals("BeanServicioPrSolicitudvacacion"))
                    interfaz = servicioProveedor.GetService<PrSolicitudvacacionServicio>();
                if (nombreServicio.Equals("BeanServicioPrPrestamo"))
                    interfaz = servicioProveedor.GetService<PrPrestamoServicio>();                
                if (nombreServicio.Equals("BeanServicioHrActualizacionfichaempleado"))
                    interfaz = servicioProveedor.GetService<HrActualizacionfichaempleadoServicio>();
                if (nombreServicio.Equals("BeanServicioHrRenovacionContratos"))
                    interfaz = servicioProveedor.GetService<HrContratosSolrenovacionServicio>();
                if (nombreServicio.Equals("BeanServicioHrFichaRequerimiento"))
                    interfaz = servicioProveedor.GetService<HrFichaRequerimientoServicio>();
                if (nombreServicio.Equals("BeanServicioAsActualizacionHorarioEmpleado"))
                    interfaz = servicioProveedor.GetService<HrSolicitudGeneralServicio>();
                */
                solicitud.servicio = nombreServicio;

            }

            /** OBTENER EL LISTADO DE CORREOS **/
            // se agrego el nivel 0 para que obtenga de la cabecera
            solicitud.accion = accion;
            List<DtoCorreo> listaCorreos = generarCorreos(usuarioActual, solicitud);

            /** ARMAR EL FORMATO CORREOS **/
            String codigoReporte = solicitud.ProcesoAprobar.ToString();
            if (codigoReporte.Length == 1)
                codigoReporte = "0" + codigoReporte;
            List<Email> listaEmail = new List<Email>();

            if (interfaz != null)
            {
                DtoReporteParametro reportePk = new DtoReporteParametro("SN", "&" + solicitud.ProcesoCodigo);
                reportePk.Version = accion.Substring(0, 6);
                List<ParametroPersistenciaGenerico> lstParametros = interfaz.obtenerMetadata(accion, solicitud);

                listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstParametros, listaCorreos);

                String url = "";
                foreach (ParametroPersistenciaGenerico beanPara in lstParametros)
                {
                    if (beanPara.Campo.Equals(ConstanteCorreo.PARAMETRO_SYSTEM_URL_REDIRECCION))
                    {
                        url = (String)beanPara.Valor;
                    }
                }

                for (int i = 0; i < listaEmail.Count; i++)
                {
                    String correoPara = "";
                    if (configCorreo.FlgCorreoPrueba.Equals("S"))
                    {
                        correoPara = configCorreo.CorreoPrueba;
                    }
                    else
                    {
                        if (!UValidador.esListaVacia(listaEmail[i].ListaCorreoDestino))
                        {
                            var c = listaEmail[i].ListaCorreoDestino[0];
                            correoPara = c.CorreoDestino;
                        }
                    }
                    if (listaEmail[i].CuerpoCorreo != null)
                    {
                        String cuerpito = UByte.convertirString(listaEmail[i].CuerpoCorreo);

                        DtoEnlace enlace = new DtoEnlace();
                        enlace.Url = url;
                        enlace.Correo = correoPara;

                        enlace.empleado = listaEmail[i].empleado;
                        enlace.compania = listaEmail[i].compania;
                        enlace.CodigoProceso = solicitud.ProcesoCodigo;
                        enlace.IdTransaccion = solicitud.ProcesoNro.Value;

                        enlace = bpEnlaceServicio.generarEnlace(enlace);

                        String nombreAplicacion = parametrosmastDao.obtenerValorExplicacion("NOMWEBSERV", "SY", "999999");
                        if (UString.estaVacio(nombreAplicacion))
                        {
                            nombreAplicacion = "";
                        }

                        cuerpito = cuerpito.Replace("[" + ConstanteCorreo.PARAMETRO_SYSTEM_URL_SERVER_PATH + "]", urlSystemPath + "/" + nombreAplicacion);
                        cuerpito = cuerpito.Replace("[" + ConstanteCorreo.PARAMETRO_SYSTEM_CORREO_PARA + "]", correoPara);
                        cuerpito = cuerpito.Replace("[" + ConstanteCorreo.PARAMETRO_SYSTEM_ENLACE_UNICO + "]", enlace.Enlace);

                        listaEmail[i].CuerpoCorreo = UByte.convertirByte(cuerpito);
                    }
                }
            }

            if (configCorreo.EnvioAsincrono.Equals("S"))
                syCorreoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);
            else
                syCorreoServicio.enviarCorreInmediato(configCorreo, listaEmail);

        }

        public DtoSolicitud obtenerSolicitud(int? codigoProceso, int? numeroProceso, UsuarioActual usuarioActual)
        {
            ParametroPaginacionGenerico paginacion = new ParametroPaginacionGenerico();
            paginacion.CantidadRegistrosDevolver = 1;
            paginacion.RegistroInicio = 0;

            FiltroSolicitudes filtroSolicitudes = new FiltroSolicitudes();
            filtroSolicitudes.IdPersonaActual = usuarioActual.PersonaId;
            filtroSolicitudes.Proceso = codigoProceso;
            filtroSolicitudes.NumeroProceso = numeroProceso;

            var lista = syAprobacionprocesoDao.listarSolicitudes(usuarioActual, paginacion, filtroSolicitudes).ListaResultado as List<DtoSolicitud>;

            return lista.Count == 0 ? null : lista[0];
        }

        public void SincronizarHorarios(string companiaSocioCodigo)
        {
            syAprobacionprocesoDao.SincronizarHorarios(companiaSocioCodigo);
        }
    }
}