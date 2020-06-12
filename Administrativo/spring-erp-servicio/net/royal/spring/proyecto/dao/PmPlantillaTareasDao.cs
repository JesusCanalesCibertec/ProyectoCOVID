using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proyecto.dominio.filtro;

namespace net.royal.spring.proyecto.dao
{

    public interface PmPlantillaDao : GenericoDao<PmPlantilla>
    {
        List<PmPlantilla> listarPlantillas();

        PmPlantilla obtenerPorId(Nullable<Int32> pPlantilla);
        PmPlantilla coreActualizar(UsuarioActual usuarioActual, PmPlantilla bean, String estado);
        PmPlantilla coreInsertar(UsuarioActual usuarioActual, PmPlantilla bean, String estado);
        void coreEliminar(PmPlantillaPk id);
        void coreEliminar(Nullable<Int32> pPlantilla);
        PmPlantilla coreAnular(UsuarioActual usuarioActual, PmPlantillaPk id);
        PmPlantilla coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);

        int generarCodigo();
        void eliminardetalle(int? plantilla);

    }
}
