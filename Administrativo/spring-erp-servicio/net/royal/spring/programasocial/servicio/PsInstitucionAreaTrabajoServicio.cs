using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsInstitucionAreaTrabajoServicio : GenericoServicio {
        PsInstitucionAreaTrabajo obtenerPorId(PsInstitucionAreaTrabajoPk id);
        PsInstitucionAreaTrabajo obtenerPorId();
        List<PsInstitucionAreaTrabajo> listarActivos();
        PsInstitucionAreaTrabajo coreActualizar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean);
        PsInstitucionAreaTrabajo coreInsertar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean);
        void coreEliminar(PsInstitucionAreaTrabajoPk id);
        void coreEliminar();
        PsInstitucionAreaTrabajo coreAnular(UsuarioActual usuarioActual,PsInstitucionAreaTrabajoPk id);
        
    }
}
