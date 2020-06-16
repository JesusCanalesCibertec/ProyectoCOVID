using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsInstitucionAreaTrabajoDao : GenericoDao<PsInstitucionAreaTrabajo>
    {
        PsInstitucionAreaTrabajo obtenerPorId();
        PsInstitucionAreaTrabajo coreActualizar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean, String estado);
        PsInstitucionAreaTrabajo coreInsertar(UsuarioActual usuarioActual, PsInstitucionAreaTrabajo bean, String estado);
        void coreEliminar(PsInstitucionAreaTrabajoPk id);
        void coreEliminar();
        PsInstitucionAreaTrabajo coreAnular(UsuarioActual usuarioActual,PsInstitucionAreaTrabajoPk id);
        List<PsInstitucionAreaTrabajo> listarActivos();
    }
}
