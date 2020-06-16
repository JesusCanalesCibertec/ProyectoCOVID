using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.proyecto.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proyecto.dao;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmPlanValidar : BpValidarServicio
    {
        private PmProyectoDao pmProyectoDao;
        private IServiceProvider servicioProveedor;
        private PmTareaDao pmTareaDao;

        public PmPlanValidar(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmProyectoDao = servicioProveedor.GetService<PmProyectoDao>();
            pmTareaDao = servicioProveedor.GetService<PmTareaDao>();

        }

        public MensajeResultado validar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            if (bpTransaccion == null)
                return null;

            PmProyecto pmProyecto = pmProyectoDao.obtenerPorIdTransaccion(bpTransaccion.IdTransaccion.Value);

            List<PmTarea> lst = pmTareaDao.listarPorProyecto(pmProyecto.IdProyecto);

            MensajeResultado msg = new MensajeResultado();
            msg.FlgValido = true;

            if (lst.Count == 0)
            {
                msg.FlgValido = false;
                msg.Mensajes.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR,
                        "Debe registrarse al menos una tarea o actividad"));
            }

            return msg;
        }
    }
}
