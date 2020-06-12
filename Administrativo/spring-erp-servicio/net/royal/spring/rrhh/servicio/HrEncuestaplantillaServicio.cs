using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestaplantillaServicio : GenericoServicio {
        HrEncuestaplantilla obtenerPorId(HrEncuestaplantillaPk id);
        HrEncuestaplantilla obtenerPorId(int pPlantilla);
        HrEncuestaplantilla coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilla bean);
        HrEncuestaplantilla coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilla bean);
        void coreEliminar(HrEncuestaplantillaPk id);
        void coreEliminar(Nullable<Int32> pPlantilla);
        HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual,HrEncuestaplantillaPk id);
        HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPlantilla);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        List<HrEncuestadetalle> listarPorPlantilla(int id);
    }
}
