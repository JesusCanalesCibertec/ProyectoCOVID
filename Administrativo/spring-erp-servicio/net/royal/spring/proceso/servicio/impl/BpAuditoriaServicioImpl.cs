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

namespace net.royal.spring.proceso.servicio.impl
{

    public class BpAuditoriaServicioImpl : GenericoServicioImpl, BpAuditoriaServicio {

        private IServiceProvider servicioProveedor;
        private BpAuditoriaDao bpAuditoriaDao;

        public BpAuditoriaServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            bpAuditoriaDao = servicioProveedor.GetService<BpAuditoriaDao>();
        }

        public ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroPaginacionAuditoria filtro)
        {
            return bpAuditoriaDao.listar(paginacion, filtro);
        }

        public List<DtoTabla> listarPeriodoBoleta()
        {
            return bpAuditoriaDao.listarPeriodoBoleta();
        }

        public BpAuditoria registrar(UsuarioActual usuarioActual, string idProceso, string idFuncionalidad, string idPeriodo, int? idEmpleado)
        {
            BpAuditoria bpAuditoria = new BpAuditoria();

            bpAuditoria.IdProceso = idProceso;
            bpAuditoria.IdFuncionalidad = idFuncionalidad;
            bpAuditoria.IdPersona = idEmpleado;
            if (UInteger.esCeroOrNulo(bpAuditoria.IdPersona))
            {
                bpAuditoria.IdPersona = usuarioActual.PersonaId;
            }
            Int32? idSecuencia = bpAuditoriaDao.generarIdSecuencia(idProceso, idFuncionalidad,
                    bpAuditoria.IdPersona);
            bpAuditoria.IdSecuencia = idSecuencia;

            bpAuditoria.IdPeriodo = idPeriodo;
            bpAuditoria.CreacionFecha = DateTime.Now;
            bpAuditoria.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bpAuditoria.CreacionUsuario = usuarioActual.UsuarioLogin;
            bpAuditoria.ModificacionFecha = DateTime.Now;

            bpAuditoria.IdSucursal = usuarioActual.SucursalCodigo;
            bpAuditoria.IdCentrocosto = usuarioActual.CentroCostosCodigo;
            bpAuditoria.IdTipoPlanilla = usuarioActual.tipoPlanilla;

            bpAuditoria.IdCompaniasocio = usuarioActual.CompaniaSocioCodigo;
            bpAuditoriaDao.registrar(bpAuditoria);
            return bpAuditoria;
        }
    }
}
