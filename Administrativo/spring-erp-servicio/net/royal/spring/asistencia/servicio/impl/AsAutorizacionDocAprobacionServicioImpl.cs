using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.servicio;
using net.royal.spring.planilla.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.interfaz;
using net.royal.spring.sistema.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.proceso.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.planilla.constante;
using net.royal.spring.sistema.constante;
using net.royal.spring.framework.constante;
using net.royal.spring.sistema.servicio;
using net.royal.spring.proceso.dao;
using net.royal.spring.core.dao;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using net.royal.spring.framework.web.dao;
using Microsoft.Extensions.Logging;
using net.royal.spring.sistema.dao;
using static net.royal.spring.sistema.constante.ConstanteReporte;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.planilla.servicio.impl
{

    public class AsAutorizacionDocAprobacionServicioImpl : GenericoServicioImpl, AsAutorizacionDocAprobacionServicio
    {

        private IServiceProvider servicioProveedor;
        private ILogger _logger;
        private readonly IHttpContextAccessor httpContextAccessor;   
        private AsAutorizacionDocAprobacionDao dao;

        public AsAutorizacionDocAprobacionServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            _logger = servicioProveedor.GetService<ILoggerFactory>().CreateLogger(this.GetType().Namespace + "." + this.GetType().Name);
            dao = servicioProveedor.GetService<AsAutorizacionDocAprobacionDao>();                   
        }

        public List<DtoProcesoSeguimiento> listarSeguiemiento(Int32 NumeroProceso) {
            return dao.listarSeguiemiento(NumeroProceso);
        }

        public void registrar(UsuarioActual usuarioActual, AsAutorizacion bean, String comentario) {
            dao.registrar(usuarioActual, bean, comentario);
        }
    }
}
