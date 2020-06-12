using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.constante;
using Microsoft.Extensions.Logging;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpEnlaceServicioImpl : GenericoServicioImpl, BpEnlaceServicio
    {

        private IServiceProvider servicioProveedor;
        private BpEnlaceDao bpEnlaceDao;
        private EmpleadomastDao empleadomastDao;
        private UsuarioServicio usuarioServicio;
        private PersonamastDao personamastDao;
        private ILogger _logger;

        public BpEnlaceServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpEnlaceDao = servicioProveedor.GetService<BpEnlaceDao>();
            empleadomastDao = servicioProveedor.GetService<EmpleadomastDao>();
            usuarioServicio = servicioProveedor.GetService<UsuarioServicio>();
            personamastDao = servicioProveedor.GetService<PersonamastDao>();
            _logger = servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
        }

        public BpEnlace actualizar(BpEnlace bean)
        {
            bpEnlaceDao.actualizar(bean);
            return bean;
        }

        public DtoEnlace generarEnlace(DtoEnlace enlace)
        {
            return bpEnlaceDao.generarEnlace(enlace);
        }

        public BpEnlace obtenerPorId(BpEnlacePk pk)
        {
            return bpEnlaceDao.obtenerPorId(pk.obtenerArreglo());
        }

        public DtoEnlace validarEnlace(DtoEnlace enlace)
        {
            UsuarioActual usuarioActual = null;
            BpEnlace bean = null;
            Empleadomast emp = null;

            if (enlace == null)
            {
                enlace.flgValida = false;
                enlace.listaMensaje = new List<MensajeUsuario>();
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío el ENLACE"));
                return enlace;
            }
            else
            {
                enlace.flgValida = true;
                enlace.listaMensaje = new List<MensajeUsuario>();
            }

            if (UString.estaVacio(enlace.Enlace))
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío el Enlace"));
            }
            if (UString.estaVacio(enlace.Correo))
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío el Correo"));
            }
            bean = bpEnlaceDao.obtenerPorId(new BpEnlacePk(enlace.Enlace, enlace.Correo));

            if (bean == null)
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se encontro el enlace"));
            }
            else
            {
                if (DateTime.Now > bean.FechaFin)
                {
                    enlace.flgValida = false;
                    enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El tiempo para poder acceder al enlace ya caducó"));
                }

                if (!UString.estaVacio(bean.IdEstado) && bean.IdEstado == "USAD")
                {
                    enlace.flgValida = false;
                    enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El enlace ya ha sido usado"));
                }
            }

            /** validar autentificacion de usuario **/
            try
            {
                if (UInteger.esCeroOrNulo(bean.IdEmpleado) || bean.IdEmpleado == -900)
                {
                    if (UString.estaVacio(bean.IdCompaniaSocio))
                    {
                        bean.IdCompaniaSocio = "";
                    }
                    //primero obtener por empleado y compania
                    emp = empleadomastDao.obtenerPorIdEmpleadoCompania(bean.IdEmpleado, bean.IdCompaniaSocio);

                    if (emp == null)
                    {
                        emp = empleadomastDao.obtenerPorCorreoInterno(bean.IdCorreo);
                    }

                    if (emp == null)
                    {
                        List<Personamast> pers = personamastDao.obtenerPorCorreoElectronico(bean.IdCorreo);

                        if (pers.Count > 1)
                        {
                            enlace.flgValida = false;
                            enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El correo debe pertenecer a sólo un empleado de la organización"));
                            return enlace;
                        }
                        if (pers.Count == 1)
                        {
                            if (pers[0] != null)
                            {
                                emp = empleadomastDao.obtenerPorIdEmpleado(pers[0].Persona);
                            }
                        }
                        else
                        {
                            enlace.flgValida = false;
                            enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El correo no pertenece a ningun empleado de la organización"));
                            return enlace;
                        }
                    }
                }

                if (emp == null)
                    emp = empleadomastDao.obtenerPorId(new EmpleadomastPk(bean.IdCompaniaSocio, bean.IdEmpleado));

                //usuarioActual = usuarioServicio.login(ConstanteSpringNetRRHH.APLICACION_CODIGO, emp.Codigousuario, null, emp.Companiasocio, null, false);

                if (usuarioActual == null)
                {
                    enlace.flgValida = false;
                    enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El usuario no fue encontrado"));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != ConstanteSeguridad.MSG_CONFORMIDAD)
                {
                    enlace.flgValida = false;
                    enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, ex.Message));
                    _logger.LogError(ex.Message);
                    _logger.LogError(ex.Source);
                    _logger.LogError(ex.StackTrace);
                }
            }
            /** validar autentificacion de usuario - fin **/

            if (!enlace.flgValida)
            {
                if (bean == null)
                    bpEnlaceDao.eliminar(bean);
                return enlace;
            }

            enlace.usuarioActual = usuarioActual;
            enlace.CodigoProceso = bean.IdProceso;
            enlace.Clave = bean.IdClaveConcatenado;
            enlace.Estado = bean.IdEstado;
            enlace.IdTransaccion = bean.IdTransaccion.Value;
            enlace.NumeroProceso = bean.IdTransaccion.Value;
            enlace.Url = bean.Url;


            //si es para el cambio de clave, mandar si o si el usuariologin
            if (enlace.Url.Contains("/Home/CambiarClaveLogin") && enlace.usuarioActual == null)
            {
                enlace.usuarioActual = new UsuarioActual() { UsuarioLogin = emp.Codigousuario };
            }

            return enlace;
        }
    }
}
