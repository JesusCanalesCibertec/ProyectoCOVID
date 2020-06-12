using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEncuestaplantillaDao : GenericoDao<HrEncuestaplantilla>
    {
        HrEncuestaplantilla obtenerPorId(Nullable<Int32> pPlantilla);
        HrEncuestaplantilla coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilla bean, String estado);
        HrEncuestaplantilla coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilla bean, String estado);
        void coreEliminar(HrEncuestaplantillaPk id);
        void coreEliminar(Nullable<Int32> pPlantilla);
        HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual,HrEncuestaplantillaPk id);
        HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pPlantilla);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);

        int generarCodigo();
        void eliminardetalle(int? plantilla);
    }
}
