using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using net.royal.spring.planilla.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.planilla.dominio;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.constante;
using net.royal.spring.sistema.constante;
using System.IO;
using net.royal.spring.framework.core;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.core.dao;

namespace net.royal.spring.planilla.servicio.impl
{
    public class PrTipoProcesoServicioImpl : PrTipoProcesoServicio
    {
        private IServiceProvider servicioProveedor;
        private PrTipoProcesoDao prTipoProcesoDao;

        public PrTipoProcesoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            prTipoProcesoDao = servicioProveedor.GetService<PrTipoProcesoDao>();
        }

        public List<PrTipoProceso> listarActivos()
        {
            return prTipoProcesoDao.listarActivos();
        }

        public List<DtoTabla> listarPorPeriodoEmpleado(string periodo, int? personaId)
        {
            return prTipoProcesoDao.listarPorPeriodoEmpleado(periodo, personaId);
        }

        public List<PrTipoProceso> listarProcesosPrestamo()
        {
            return prTipoProcesoDao.listarProcesosPrestamo();
        }
    }
}
