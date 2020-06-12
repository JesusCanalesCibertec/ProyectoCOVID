using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpEnlaceDaoImpl : GenericoDaoImpl<BpEnlace>, BpEnlaceDao
    {
        private IServiceProvider servicioProveedor;


        public BpEnlaceDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpenlace")
        {
            servicioProveedor = _servicioProveedor;
        }

        public DtoEnlace generarEnlace(DtoEnlace enlace)
        {
            BpEnlace bean = new BpEnlace();

            /****/
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

            if (UString.estaVacio(enlace.Correo))
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío el Correo"));
            }
            /*if (UString.estaVacio(enlace.CodigoProceso))
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío el Proceso"));
            }
            if (UString.estaVacio(enlace.Clave))
            {
                enlace.flgValida = false;
                enlace.listaMensaje.Add(new MensajeUsuario(tipo_mensaje.ERROR, "No se envío la Clave del Registro"));
            }*/

            if (!enlace.flgValida)
                return enlace;

            bean.IdEnlace = Guid.NewGuid().ToString();
            bean.FechaEnvio = DateTime.Now;
            bean.FechaFin = bean.FechaEnvio.Value.AddDays(10);
            bean.IdProceso = enlace.CodigoProceso;
            bean.IdEstado = enlace.Estado;
            bean.IdTransaccion = enlace.IdTransaccion;
            bean.IdClaveConcatenado = enlace.Clave;
            bean.Url = enlace.Url;
            bean.IdCorreo = enlace.Correo;
            bean.CreacionFecha = DateTime.Now;
            bean.IdEmpleado = enlace.empleado;
            bean.IdCompaniaSocio = enlace.compania;
            //bean.CreacionTerminal = usuarioActual.UsuarioHostName;
            //bean.CreacionUsuario = usuarioActual.UsuarioLogin;

            registrar(bean);
            enlace.Enlace = bean.IdEnlace;
            return enlace;
        }

        public BpEnlace obtenerPorId(BpEnlacePk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
