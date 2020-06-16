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
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.framework.constante;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpTransaccionServicioImpl : GenericoServicioImpl, BpTransaccionServicio
    {

        private IServiceProvider servicioProveedor;
        private BpTransaccionDao bpTransaccionDao;


        public BpTransaccionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpTransaccionDao = servicioProveedor.GetService<BpTransaccionDao>();
        }

        public List<DtoTransaccionNotificacion> listarTransaccionesPorUsuario(UsuarioActual usuarioActual, string idProceso)
        {
            return bpTransaccionDao.listarTransaccionesPorUsuario(usuarioActual, idProceso);
        }

        public List<DtoTransaccionUsuario> listarTransaccionSeguimiento(UsuarioActual usuarioActual, int idTransaccion, string idProceso)
        {
            return bpTransaccionDao.listarTransaccionSeguimiento(usuarioActual, idTransaccion, idProceso);


        }

        public string obtenerContenidoCorreo(int tipo, int? idTransaccion)
        {
            return bpTransaccionDao.obtenerContenidoCorreo(tipo, idTransaccion);
        }

        public BpTransaccion obtenerPorId(int idTransaccion)
        {
            return bpTransaccionDao.obtenerPorId(new BpTransaccionPk(idTransaccion).obtenerArreglo());
        }
    }
}
