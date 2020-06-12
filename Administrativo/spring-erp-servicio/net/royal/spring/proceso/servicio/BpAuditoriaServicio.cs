using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proceso.servicio
{

    public interface BpAuditoriaServicio : GenericoServicio {
        BpAuditoria registrar(UsuarioActual usuarioActual, String idProceso, String idFuncionalidad, String idPeriodo,
            Int32? idEmpleado);

        ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroPaginacionAuditoria filtro);
        List<DtoTabla> listarPeriodoBoleta();
    }
}
