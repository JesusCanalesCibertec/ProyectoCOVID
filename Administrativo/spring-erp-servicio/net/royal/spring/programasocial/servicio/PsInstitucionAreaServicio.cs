using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsInstitucionAreaServicio : GenericoServicio {
        
        PsInstitucionArea obtenerPorId(PsInstitucionAreaPk llave);
        PsInstitucionArea coreActualizar(UsuarioActual usuarioActual, PsInstitucionArea bean);
        PsInstitucionArea coreInsertar(UsuarioActual usuarioActual, PsInstitucionArea bean);
        void eliminar(string pIdInstitucion, string pIdArea);
        List<PsInstitucionArea> listado(string pIdInstitucion);
        List<PsInstitucionArea> listadoPorPrograma(string pIdInstitucion, String idPrograma);
        List<PsInstitucionArea> listarTodo();

        PsInstitucionArea coreAnular(UsuarioActual usuarioActual, PsInstitucionAreaPk id);
        PsInstitucionArea coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, String pIdArea);
    }
}
