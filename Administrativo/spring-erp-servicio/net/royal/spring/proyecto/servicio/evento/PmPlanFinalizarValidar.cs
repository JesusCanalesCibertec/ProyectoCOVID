using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.proyecto.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.framework.core;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmPlanFinalizarValidar : BpValidarServicio
    {
        private PmProyectoDao pmProyectoDao;
        private IServiceProvider servicioProveedor;
        private PmTareaDao pmTareaDao;

        public PmPlanFinalizarValidar(IServiceProvider _servicioProveedor)
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

            List<DtoTarea> lst = pmTareaDao.listarTareasNoFinalizadas(usuarioActual, pmProyecto);

            MensajeResultado msg = new MensajeResultado();
            msg.FlgValido = true;

            if (lst.Count > 0)
            {
                msg.FlgValido = false;
                String str = "Se tiene tareas pendientes";
                msg.Mensajes.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, str));
                foreach (DtoTarea dtoTarea in lst)
                {
                    str = UString.obtenerValorCadenaSinNulo(dtoTarea.nombreTarea);
                    str = str + " : Estado (" + UString.obtenerValorCadenaSinNulo(dtoTarea.estadodescripcion) + ")";
                    str = str + " Actividad (" + UString.obtenerValorCadenaSinNulo(dtoTarea.elementoNombre) + ")";
                    msg.Mensajes.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, str));
                }
            }

            return msg;
        }
    }
}
