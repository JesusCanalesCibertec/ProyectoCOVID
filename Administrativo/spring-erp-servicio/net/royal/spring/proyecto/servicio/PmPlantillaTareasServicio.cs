using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface PmPlantillaTareasServicio : GenericoServicio {
        PmPlantillaTareas obtenerPorId(PmPlantillaTareasPk id);
        PmPlantillaTareas obtenerPorId(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta);
        PmPlantillaTareas coreActualizar(UsuarioActual usuarioActual, PmPlantillaTareas bean);
        PmPlantillaTareas coreInsertar(UsuarioActual usuarioActual, PmPlantillaTareas bean);
        void coreEliminar(PmPlantillaTareasPk id);
        void coreEliminar(Nullable<Int32> pPlantilla,Nullable<Int32> pPregunta);
    }
}
