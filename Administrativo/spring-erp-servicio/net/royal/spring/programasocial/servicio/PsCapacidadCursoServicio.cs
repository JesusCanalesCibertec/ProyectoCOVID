using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsCapacidadCursoServicio : GenericoServicio {
        PsCapacidadCurso obtenerPorId(PsCapacidadCursoPk id);
        PsCapacidadCurso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad,String pIdCurso);
        PsCapacidadCurso coreActualizar(UsuarioActual usuarioActual, PsCapacidadCurso bean);
        PsCapacidadCurso coreInsertar(UsuarioActual usuarioActual, PsCapacidadCurso bean);
        void coreEliminar(PsCapacidadCursoPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad,String pIdCurso);
    }
}
