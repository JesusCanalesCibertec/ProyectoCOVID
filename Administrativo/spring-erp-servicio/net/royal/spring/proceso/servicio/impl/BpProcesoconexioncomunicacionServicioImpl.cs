using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.sistema.dao;

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpProcesoconexioncomunicacionServicioImpl : GenericoServicioImpl, BpProcesoconexioncomunicacionServicio
    {

        private IServiceProvider servicioProveedor;
        private BpProcesoconexioncomunicacionDao bpProcesoconexioncomunicacionDao;

        public BpProcesoconexioncomunicacionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            bpProcesoconexioncomunicacionDao = servicioProveedor.GetService<BpProcesoconexioncomunicacionDao>();
        }

        public List<DtoTransaccionUsuario> listarUsuariosPorProcesoConexion(BpProcesoconexionPk procesoConexion, string idTipoComunicacion)
        {
            return bpProcesoconexioncomunicacionDao.listarUsuariosPorProcesoConexion(procesoConexion, idTipoComunicacion);
        }
    }
}
