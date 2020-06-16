using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.planilla.dominio;
using net.royal.spring.sistema.interfaz;
using static net.royal.spring.sistema.constante.ConstanteReporte;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.planilla.servicio
{

    public interface AsAutorizacionDocAprobacionServicio : GenericoServicio
    {
        List<DtoProcesoSeguimiento> listarSeguiemiento(Int32 NumeroProceso);
        void registrar(UsuarioActual usuarioActual, AsAutorizacion bean, String comentario);
    }
}
