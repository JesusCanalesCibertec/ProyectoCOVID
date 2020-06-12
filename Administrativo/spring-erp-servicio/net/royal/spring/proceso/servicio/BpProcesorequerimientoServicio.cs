using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.servicio
{
    public interface BpProcesorequerimientoServicio : GenericoServicio
    {
        List<DtoBpProcesorequerimiento> listado(string codigo);
        BpProcesorequerimiento obtenerPorId(BpProcesorequerimientoPk llave);
        BpProcesorequerimiento coreActualizar(UsuarioActual usuarioActual, BpProcesorequerimiento bean);
        BpProcesorequerimiento coreInsertar(UsuarioActual usuarioActual, BpProcesorequerimiento bean);
        void eliminar(string pIdProceso, string pIdfuncionalidad, int pIdVersion);
    }


}
