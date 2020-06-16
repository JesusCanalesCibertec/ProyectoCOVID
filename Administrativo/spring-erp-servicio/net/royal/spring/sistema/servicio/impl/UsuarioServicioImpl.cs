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
using net.royal.spring.framework.core;
using net.royal.spring.sistema.constante;
using net.royal.spring.sistema.util;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.framework.constante;
using Microsoft.Extensions.Logging;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.proceso.dominio;
using Microsoft.AspNetCore.Http;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dao;
using System.Security.Permissions;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using net.royal.spring.minedu.dominio.dto;

namespace net.royal.spring.sistema.servicio.impl
{
    public class UsuarioServicioImpl : GenericoServicioImpl, UsuarioServicio
    {

        private IServiceProvider servicioProveedor;
        private UsuarioDao usuarioDao;
        private ParametrosmastDao parametrosmastDao;
        private SyUsuariopasswordlogDao syUsuariopasswordlogDao;
        private EmpleadomastDao empleadoMastDao;
        private PersonamastDao personamastDao;
        private SyCorreoServicio syCorreoServicio;
        private SyReporteServicio syReporteServicio;
        private BpEnlaceDao bpEnlaceDao;
        private ILogger _logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private SeguridadperfilusuarioDao seguridadperfilusuarioDao;
        private MaMiscelaneosdetalleServicio maMiscelaneosdetalleServicio;
        private SySeguridadautorizacionesDao sySeguridadautorizacionesDao;
        private SyPreferencesDao syPreferencesDao;
        private PsEmpleadoDao psEmpleadoDao;
        private PsEntidadDao psEntidadDao;

        public UsuarioServicioImpl(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            maMiscelaneosdetalleServicio = servicioProveedor.GetService<MaMiscelaneosdetalleServicio>();
            seguridadperfilusuarioDao = servicioProveedor.GetService<SeguridadperfilusuarioDao>();
            usuarioDao = servicioProveedor.GetService<UsuarioDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
            syUsuariopasswordlogDao = servicioProveedor.GetService<SyUsuariopasswordlogDao>();
            empleadoMastDao = servicioProveedor.GetService<EmpleadomastDao>();
            personamastDao = servicioProveedor.GetService<PersonamastDao>();
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            sySeguridadautorizacionesDao = servicioProveedor.GetService<SySeguridadautorizacionesDao>();
            syPreferencesDao = servicioProveedor.GetService<SyPreferencesDao>();
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
            psEmpleadoDao = servicioProveedor.GetService<PsEmpleadoDao>();
            this.bpEnlaceDao = servicioProveedor.GetService<BpEnlaceDao>();
            this.httpContextAccessor = httpContextAccessor;
            _logger = _servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
        }

        /*ernesto inicio*/
        public UsuarioActual Logeo(string idUsuario, string clave)
        {
            if (UString.estaVacio(idUsuario))
            {
                throw new UException("El usuario es requerido");
            }

            if (UString.estaVacio(clave))
            {
                throw new UException("La contraseña es requerida");
            }

            this.GetDirectorySearcher(idUsuario, clave, this.ObtenerDominio());

            UsuarioActual usuario = new UsuarioActual();
            usuario = usuarioDao.Logeo(idUsuario);

            if (usuario == null)
            {
                string cadena = "Su usuario no cuenta con un rol para acceder al sistema.";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.INFORMACION, cadena));
                throw new UException(lista);
            }
            if (usuario.estado.Trim() == "I")
            {
                string cadena = "Su usuario se encuentra inactivo";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.INFORMACION, cadena));
                throw new UException(lista);
            }


            usuario.clave = clave;
            return usuario;

        }

        public DirectorySearcher dirSearch = null;

        public DtoDirectoriousuario GetUserInformation(UsuarioActual bean, string username)
        {
            if (bean.UsuarioLogin == "EACH1")
            {
                bean.UsuarioLogin = "EARBULU";
                bean.clave = "Each@ne1";
            }
            SearchResult rs = null;
            if (username.Trim().IndexOf("@") > 0)
                rs = SearchUserByEmail(GetDirectorySearcher(bean.UsuarioLogin, bean.clave, this.ObtenerDominio()), username);
            else
                rs = SearchUserByUserName(GetDirectorySearcher(bean.UsuarioLogin, bean.clave, this.ObtenerDominio()), username);

            if (rs != null)
            {
                DtoDirectoriousuario dto = new DtoDirectoriousuario();
                if (rs.GetDirectoryEntry().Properties["samaccountname"].Value != null)
                    dto.Usuario = rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();

                if (rs.GetDirectoryEntry().Properties["givenName"].Value != null)
                    dto.Nombre = rs.GetDirectoryEntry().Properties["givenName"].Value.ToString();

                if (rs.GetDirectoryEntry().Properties["sn"].Value != null)
                    dto.Apellidos = rs.GetDirectoryEntry().Properties["sn"].Value.ToString();

                if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
                    dto.Correo = rs.GetDirectoryEntry().Properties["mail"].Value.ToString();

                if (rs.GetDirectoryEntry().Properties["description"].Value != null)
                    dto.Dni = rs.GetDirectoryEntry().Properties["description"].Value.ToString();

                return dto;
            }
            return null;
        }

        private DirectorySearcher GetDirectorySearcher(string username, string passowrd, string domain)
        {
            SearchResultCollection results;
            try
            {
                dirSearch = new DirectorySearcher(
               new DirectoryEntry("LDAP://" + domain, username, passowrd));

                results = dirSearch.FindAll();

                return dirSearch;
            }
            catch (Exception)
            {
                string cadena = "Error de ingreso, verificar datos ingresados.";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, cadena));
                throw new UException(lista);
            }
        }

        private SearchResult SearchUserByUserName(DirectorySearcher ds, string username)
        {
            try
            {
                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + username + "))";

                ds.SearchScope = SearchScope.Subtree;
                ds.ServerTimeLimit = TimeSpan.FromSeconds(90);

                SearchResult userObject = ds.FindOne();

                if (userObject != null)
                    return userObject;
                return null;
            }
            catch (Exception)
            {
                string cadena = "Usuario no identificado";
                List<MensajeUsuario> lista = new List<MensajeUsuario>();
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, cadena));
                throw new UException(lista);
            }

        }

        private SearchResult SearchUserByEmail(DirectorySearcher ds, string email)
        {
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(mail=" + email + "))";

            ds.SearchScope = SearchScope.Subtree;
            ds.ServerTimeLimit = TimeSpan.FromSeconds(90);

            SearchResult userObject = ds.FindOne();

            if (userObject != null)
                return userObject;
            else
                return null;
        }

        private string ObtenerDominio()
        {
            try
            {
                return Domain.GetComputerDomain().ToString().ToLower();
            }
            catch (Exception e)
            {
                e.Message.ToString();
                return string.Empty;
            }
        }
        /*ernesto fin*/

        public void cambiarClave(string usuario, string clave, string clave1, string clave2)
        {
            List<MensajeUsuario> lista = new List<MensajeUsuario>();

            lista = this.validarUsuarioClave(null, usuario, clave, true);

            if (lista.Count > 0)
                throw new UException(lista);

            if (!clave1.Equals(clave2))
            {
                throw new UException("Las nuevas claves no son iguales");
            }

            if (lista.Count == 0)
            {
                Usuario bean = usuarioDao.obtenerPorId(new UsuarioPk(usuario).obtenerArreglo());
                String nuevaClave = claveUsuarioEncriptar(clave1);
                bean.Clave = nuevaClave;
                bean.Expirarpasswordflag = "N";
                usuarioDao.actualizar(bean);
            }
            return;
        }

        private String claveUsuarioEncriptar(String clave)
        {
            if (UString.estaVacio(clave))
                return "";
            try
            {
                return SeguridadHelper.springEncriptar(clave);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.Source);
                _logger.LogError(ex.StackTrace);
            }
            return null;
        }


        public Boolean expiroUsuario(string idUsuario)
        {
            DateTime hoy = DateTime.Today;
            String flgLogClave = parametrosmastDao.obtenerValorCadena(ConstanteSeguridad.PARAMETRO_LOG_CLAVES,
                    ConstanteSeguridad.APLICACION_CODIGO);
            int? diasExpiracion = parametrosmastDao.obtenerValorInteger(
                    ConstanteSeguridad.PARAMETRO_CLAVE_DIAS_EXPIRACION, ConstanteSeguridad.APLICACION_CODIGO);

            if (UString.estaVacio(idUsuario))
            {
                throw new UException(UsuarioPk.MSG_USUARIO_ESREQUERIDO);
            }

            idUsuario = idUsuario.Trim().ToUpper();
            Usuario usuarioBean = usuarioDao.obtenerPorId(new UsuarioPk(idUsuario).obtenerArreglo());

            if (usuarioBean == null)
            {
                String str = maMiscelaneosdetalleServicio.obtenerDescripcionPorId(ConstanteSeguridad.APLICACION_CODIGO, ConstanteSeguridad.MISCELANEO_MENSAJES, ConstanteSeguridad.MISCELANEO_MENSAJES_USUARIO_BLOQUEADO);
                throw new UException(str);
            }

            /***/

            if (UString.obtenerValorCadenaSinNulo(usuarioBean.Expirarpasswordflag).Equals("S"))
            {
                if (usuarioBean.Fechaexpiracion != null)
                {
                    if (hoy > usuarioBean.Fechaexpiracion)
                    {
                        return true;
                    }
                }
            }
            /***/

            if (UString.estaVacio(flgLogClave))
            {
                return false;
            }
            if (flgLogClave.Trim().Equals("N"))
            {
                return false;
            }

            if (UInteger.esCeroOrNulo(diasExpiracion))
            {
                return false;
            }

            DtoUsuarioPasswordLog dtoUsuario = syUsuariopasswordlogDao.obtenerUltimoLogin(idUsuario);

            if (dtoUsuario == null)
            {
                return false;
            }

            int diasEntre = 0;

            //FALTA ESTO
            /* Integer diasEntre = (int)((hoy.getTime() - dtoUsuario.getFechaUltimoLogin().getTime())
                    / (1000 * 60 * 60 * 24));*/

            if (diasEntre > diasExpiracion)
            {
                return true;
            }

            return false;
        }


        private UsuarioActual obtenerDatosEmpleadoPorUsuario(string idCompaniaSocio, string idUsuario, String origen)
        {
            UsuarioActual usuarioActual = null;
            usuarioActual = usuarioDao.obtenerDatosEmpleadoPorUsuario(idCompaniaSocio, idUsuario, origen);

            //if (usuarioActual != null)
            //{
            //    List<DtoTabla> lst = sySeguridadautorizacionesDao.listarPorGrupoYUsuario(idUsuario);
            //    if (lst.Count >= 1)
            //    {
            //        usuarioActual.idInstitucion = lst[0].Codigo;
            //        usuarioActual.idInstitucionNombre = lst[0].Nombre;
            //    }
            //    else
            //    {
            //        usuarioActual.idInstitucion = "";
            //        usuarioActual.idInstitucionNombre = "";
            //    }

            //    String periodo = syPreferencesDao.obtenerPorIdCadena(idUsuario, ConstanteSeguridad.PREFERENCIA_PERIODO_SOCIAL);
            //    usuarioActual.idPeriodoPreferencia = periodo;
            //}


            return usuarioActual;
        }


        public UsuarioActual login(string idAplicacion, string idUsuario, string clave, string idCompaniaSocio, string conformidad, Boolean flgValidarClave, string origen)
        {
            idCompaniaSocio = UString.obtenerValorCadenaSinNulo(idCompaniaSocio);
            UsuarioActual usuarioActual = null;
            //String requiereCompania = parametrosmastDao.obtenerValorTipoDatoFlag(ConstanteSeguridad.PARAMETRO_LOGIN_VALIDAR_COMPANIA, ConstanteSeguridad.APLICACION_CODIGO);
            //Boolean flgValCompania = UBoolean.validarFlag(requiereCompania);

            if (UString.estaVacio(idAplicacion))
                throw new UException(AplicacionesmastPk.MSG_APLICACIONCODIGO_ESREQUERIDO);

            if (UString.estaVacio(idUsuario))
            {
                throw new UException(UsuarioPk.MSG_USUARIO_ESREQUERIDO);
            }

            //if ((!flgValCompania) && UString.estaVacio(idCompaniaSocio))
            //{
            //    obtener compania por defecto si tiene mas de una se toma la primera activa
            //    idCompaniaSocio = usuarioDao.obtenerCompaniaPorDefecto(idUsuario);
            //}

            //if (UString.estaVacio(idCompaniaSocio))
            //{
            //    //obtener compania por defecto si tiene mas de una se toma la primera activa
            //    idCompaniaSocio = usuarioDao.obtenerCompaniaPorDefecto(idUsuario);
            //}

            //if (origen == "fundacion")
            //{
            //    idCompaniaSocio = usuarioDao.obtenerCompaniaPorDefecto(idUsuario);
            //}

            //if (UString.estaVacio(idCompaniaSocio))
            //{
            //    throw new UException(Usuario.MFG_COMPANIA_REQUERIDA_USUARIO);
            //}


            idUsuario = idUsuario.Trim().ToUpper();
            idCompaniaSocio = idCompaniaSocio.Trim().ToUpper();

   
            usuarioActual = obtenerDatosEmpleadoPorUsuario(idCompaniaSocio, idUsuario, origen);

            //String loginNetConexion = parametrosmastDao.obtenerValorCadena(
            //        ConstanteSeguridad.PARAMETRO_CONEXION_ACTIVE_DIRECTORY, ConstanteSeguridad.APLICACION_CODIGO);

            //if (UString.estaVacio(loginNetConexion))
            //{
            //    loginNetConexion = "N";
            //}

            //loginNetConexion = loginNetConexion.Trim();

            //if (loginNetConexion.Equals("N"))
            //{
            //    // AUTENTIFICACION POR BASE DE DATOS
            //    List<MensajeUsuario> lista = this.validarUsuarioClave(idCompaniaSocio, idUsuario, clave, flgValidarClave);
            //    if (lista.Count > 0)
            //        throw new UException(lista);

            //    //usuarioActual = usuarioDao.obtenerDatosEmpleadoPorUsuario(idCompaniaSocio, idUsuario, origen);
            //    usuarioActual = obtenerDatosEmpleadoPorUsuario(idCompaniaSocio, idUsuario, origen);

            //    if (idUsuario.Equals("MISESF"))
            //        usuarioActual.estadoEmpleado = "0";

            //    if (usuarioActual == null)
            //        throw new UException("Empleado con Usuario : " + idUsuario + " no tiene la compañia seleccionada ("
            //                + idCompaniaSocio + ")");
            //    //if (usuarioActual.estadoEmpleado.Equals("2"))
            //    //{
            //    //    throw new UException("El empleado con usuario : " + idUsuario + " ha sido cesado en la compañía seleccionada ("
            //    //           + idCompaniaSocio + ")");
            //    //}
            //    //if (usuarioActual.estado.Equals("I"))
            //    //{
            //    //    throw new UException("El empleado con usuario : " + idUsuario + " ha sido inactivado en la compañía seleccionada ("
            //    //           + idCompaniaSocio + ")");
            //    //}

            //    if (conformidad == "S")
            //    {
            //        //usuarioDao.actualizarConformidad(usuarioActual.UsuarioLogin);
            //        usuarioActual.flagConformidadDatos = "S";
            //    }

            //    long localdata = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //    usuarioActual.UsuarioUniqueIdInteger = Convert.ToInt64(localdata);
            //    usuarioActual.UsuarioUniqueIdString = usuarioActual.UsuarioUniqueIdInteger.ToString();
            //    usuarioActual.UsuarioLogin = usuarioActual.UsuarioLogin.Trim();
            //    usuarioActual.PersonaNombreCompleto =
            //            UString.obtenerValorCadenaSinNulo(usuarioActual.PersonaNombreCompleto).Trim();

            //}
            //else if (loginNetConexion.Equals("S"))
            //{
            //    //AUTENTIFICACION POR ACTIVE DIRECTORY
            //    throw new UException("ACTIVE DIRECTORY NO IMPLEMENTADO !!!");
            //}

            if (usuarioActual == null)
            {
                throw new UException(Usuario.MSG_USUARIO_ACTUAL_NOEXISTE);
            }


            //VALIDAR SI YA EXPIRÓ
            //if (!validaExpiracion(usuarioActual))
            //{
            //    throw new UException(Usuario.MFG_USUARIO_EXPIRADO);
            //}



            return usuarioActual;
        }
        public bool validaExpiracion(UsuarioActual usuarioActual)
        {
            if (UBoolean.validarFlag(parametrosmastDao.obtenerValorCadena("PWDLOG", "SY", "999999")))
            {
                int dias = parametrosmastDao.obtenerValorInteger("PWDLOGDAY", "SY", "999999").Value;

                if (dias <= 0)
                {
                    return true;
                }

                if (UBoolean.validarFlag(usuarioActual.ExpirarPasswordFlag))
                {
                    if (usuarioActual.FechaExpiracion.HasValue)
                    {
                        var hoy = DateTime.Today;
                        var FechaExpiracion = usuarioActual.FechaExpiracion;

                        var diasDesdeFechaExpiracion = (hoy - FechaExpiracion).Value.TotalDays;

                        if (dias - diasDesdeFechaExpiracion <= 0)
                        {
                            return false;
                        }
                    }
                }

            }

            return true;

        }
        public UsuarioActual login(string idAplicacion, string idUsuario, string clave, string idCompaniaSocio, string conformidad, string origen)
        {
            return login(idAplicacion, idUsuario, clave, idCompaniaSocio, conformidad, true, origen);
        }

        public List<MensajeUsuario> validarUsuarioClave(string idCompaniaSocio, string idUsuario, string clave, Boolean flgValidarClave)
        {
            List<MensajeUsuario> lista = new List<MensajeUsuario>();

            var intentos = parametrosmastDao.obtenerValorInteger("LOGINERRNO", "SY", "999999");

            Usuario usuario = usuarioDao.obtenerPorId(new UsuarioPk(idUsuario).obtenerArreglo());

            if (usuario == null)
            {
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ADVERTENCIA, Usuario.MFG_EMPLEADO_NOEXISTE));
                return lista;
            }

            if (!UString.obtenerValorCadenaSinNulo(usuario.Estado).Equals("A"))
            {
                String str = maMiscelaneosdetalleServicio.obtenerDescripcionPorId(ConstanteSeguridad.APLICACION_CODIGO, ConstanteSeguridad.MISCELANEO_MENSAJES, ConstanteSeguridad.MISCELANEO_MENSAJES_USUARIO_BLOQUEADO);
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ADVERTENCIA, str));
                return lista;
            }

            String claveEncriptada = null;
            if (UString.estaVacio(clave))
                claveEncriptada = "";
            else
                claveEncriptada = SeguridadHelper.springEncriptar(clave);

            if (UString.estaVacio(usuario.Clave))
            {
                usuario.Clave = "";
            }

            if (flgValidarClave)
            {
                String claveUsuarioBD = UString.obtenerValorCadenaSinNulo(usuario.Clave).Trim();
                if (!claveEncriptada.Equals(claveUsuarioBD))
                {
                    var mensaje = Usuario.MFG_CLAVE_NOESIGUAL;
                    mensaje = mensaje.Replace("[p_intentos]", intentos.ToString());
                    lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ADVERTENCIA, mensaje));
                    return lista;
                }
            }

            //DtoEmpleadoSeguridad empleado = usuarioDao.obtenerEmpleadoEstados(idCompaniaSocio, idUsuario);

            /*if (empleado == null)
            {
                String str = maMiscelaneosdetalleServicio.obtenerDescripcionPorId(ConstanteSeguridad.APLICACION_CODIGO, ConstanteSeguridad.MISCELANEO_MENSAJES, ConstanteSeguridad.MISCELANEO_MENSAJES_USUARIO_BLOQUEADO);
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ADVERTENCIA, str));
                return lista;
            }

            if (UString.estaVacio(empleado.IdEstadoEmpleado))
                empleado.IdEstadoEmpleado = "";

            if (!empleado.IdEstadoEmpleado.Equals("0"))
            {
                lista.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ADVERTENCIA, Usuario.MFG_EMPLEADO_NOESTAACTIVO));
                return lista;
            }*/

            return lista;
        }

        public List<MensajeUsuario> recuperarClave(string correo, string origen)
        {
            Empleadomast empleado = null;
            Personamast persona = null;
            UsuarioActual usuActual = null;
            List<MensajeUsuario> lstMensaje = new List<MensajeUsuario>();
            Usuario usuario = null;
            EmailConfiguracion configCorreo = null;

            List<Empleadomast> lstEmpleado = new List<Empleadomast>();
            if (origen == "F")
            {
                origen = "fundacion";
                lstEmpleado = empleadoMastDao.listarPorCorreoInterno(correo);
                if (lstEmpleado.Count == 0)
                {
                    lstMensaje.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, "El correo ingresado no le pertenece a ningún empleado de Fundación"));
                    return lstMensaje;
                }
            }
            else
            {
                lstEmpleado = psEmpleadoDao.listarPorCorreoInterno(correo);
                if (lstEmpleado.Count == 0)
                {
                    lstMensaje.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, "El correo ingresado no le pertenece a ningún empleado de Institución"));
                    return lstMensaje;
                }
            }

            empleado = lstEmpleado[0];

            if (origen == "fundacion")
            {
                persona = personamastDao.obtenerPorId(new PersonamastPk(empleado.Empleado.Value));

            }
            else
            {
                var entidad = psEntidadDao.obtenerPorId(new PsEntidad() { IdEntidad = empleado.Empleado }.obtenerArreglo());
                persona = new Personamast() { Nombrecompleto = entidad.Nombrecompleto, Persona = entidad.IdEntidad };
            }

            try
            {
                usuActual = login(ConstanteSpringNetRRHH.APLICACION_CODIGO, empleado.Codigousuario, null, empleado.Companiasocio, null, false, origen);
            }
            catch (UException ex)
            {
                var errores = ex.obtenerErrores();

                foreach (var error in errores)
                {
                    if (error.Mensaje != ConstanteSeguridad.MSG_CONFORMIDAD)
                    {
                        lstMensaje.Add(new MensajeUsuario(tipo_mensaje.INFORMACION, error.Mensaje));
                    }
                }

                return lstMensaje;
            }

            usuario = usuarioDao.obtenerPorId(new UsuarioPk(empleado.Codigousuario));

            if (configCorreo == null)
                configCorreo = syCorreoServicio.obtenerConfiguracion();

            List<ParametroPersistenciaGenerico> lstMetadata = new List<ParametroPersistenciaGenerico>();
            List<DtoCorreo> listaCorreos = new List<DtoCorreo>();
            List<Email> listaEmail = new List<Email>();
            DtoReporteParametro reportePk = new DtoReporteParametro("SN", "RCU");
            listaCorreos.Add(new DtoCorreo(empleado.Empleado.Value, empleado.Companiasocio, empleado.Correointerno));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_solicitante_id", ConstanteUtil.TipoDato.String, Convert.ToString(persona.Persona)));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_solicitante_nombre", ConstanteUtil.TipoDato.String, UString.obtenerValorCadenaSinNulo(persona.Nombrecompleto).Trim()));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_usuario", ConstanteUtil.TipoDato.String, usuario.Usuario));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_correo", ConstanteUtil.TipoDato.String, correo));
            var claveTrim = UString.estaVacio(usuario.Clave) ? "" : usuario.Clave.Trim();
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_clave", ConstanteUtil.TipoDato.String, SeguridadHelper.springDesencriptar(claveTrim)));

            listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstMetadata, listaCorreos);
            syCorreoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);

            return lstMensaje;
        }

        public ParametroPaginacionGenerico listarReporteSeguridad(ParametroPaginacionGenerico paginacion, string compania, int empleado, string rEPORTE_COMPROMISO_DATOS)
        {
            return usuarioDao.listarReporteSeguridad(paginacion, compania, empleado, rEPORTE_COMPROMISO_DATOS);
        }


        public void inactivarUsuario(string v)
        {
            var usuario = usuarioDao.obtenerPorId(new UsuarioPk() { Usuario = v });
            usuario.Estado = "I";
            usuario.UltimoUsuario = v;
            usuario.UltimaFechaModif = DateTime.Now;
            usuarioDao.actualizar(usuario);
        }
        public string esAdministradorWeb(string idUSuario)
        {
            String retorno = "N";

            String perfil = parametrosmastDao.obtenerValorExplicacion("PERADMIWEB", "SY");
            perfil = UString.obtenerValorCadenaSinNulo(perfil);
            idUSuario = UString.obtenerValorCadenaSinNulo(idUSuario).Trim().ToUpper();

            List<Seguridadperfilusuario> lst = seguridadperfilusuarioDao.listarPerfilesActivos(idUSuario);

            foreach (Seguridadperfilusuario sperfil in lst)
            {
                String pf = UString.obtenerValorCadenaSinNulo(sperfil.Perfil).Trim().ToUpper();
                if (perfil.IndexOf(pf) >= 0)
                    return "S";
            }

            return retorno;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroUsuario filtro)
        {
            return usuarioDao.listarPaginacion(paginacion, filtro);
        }


        public Usuario obtenerPorId(UsuarioPk llave)
        {
            return usuarioDao.obtenerPorId(llave);
        }


    }
}
