using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proceso.servicio.evento;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace net.royal.spring.proyecto.servicio.evento
{
    public class PmTareaRegistroFinalizarValidar : BpValidarServicio
    {
        private IServiceProvider servicioProveedor;
        private PmTareaDao pmTareaDao;

        public PmTareaRegistroFinalizarValidar(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmTareaDao = servicioProveedor.GetService<PmTareaDao>();

        }

        public MensajeResultado validar(UsuarioActual usuarioActual, BpTransaccion bpTransaccion, BpProcesoconexion bpProcesoConexion)
        {
            /*Int32 contador = 0;
            if (bpTransaccion == null)
                return null;

            PmTarea pmTarea = pmTareaDao.obtenerPorIdTransaccion(bpTransaccion.IdTransaccion.Value);

            List<DtoTarea> lst = pmTareaDao.listarRegistrosValidos(usuarioActual, pmTarea);

            MensajeResultado msg = new MensajeResultado();
            msg.FlgValido = true;

            if (lst.Count == 0)
            {
                msg.FlgValido = false;
                String str = "La tarea no tienes REGISTROS relacionados";
                msg.Mensajes.Add(new MensajeUsuario(MensajeUsuario.tipo_mensaje.ERROR, str));
            }*/

            return null;
        }
    }
}
