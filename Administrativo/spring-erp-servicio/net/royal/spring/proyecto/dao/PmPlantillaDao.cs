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

    public interface PmPlantillaTareasDao : GenericoDao<PmPlantillaTareas>
    {
        ParametroPaginacionGenerico listarPlantillasTarea(DtoTabla filtro, ParametroPaginacionGenerico paginacion);
        List<PmPlantillaTareas> listarPlantillasTarea(DtoTabla filtro);

     

        PmPlantillaTareas obtenerPorId(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta);
        PmPlantillaTareas coreActualizar(UsuarioActual usuarioActual, PmPlantillaTareas bean);
        PmPlantillaTareas coreInsertar(UsuarioActual usuarioActual, PmPlantillaTareas bean);
        void coreEliminar(PmPlantillaTareasPk id);
        void coreEliminar(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta);

        List<PmPlantillaTareas> listarTareas(int plantilla);

    }
}
