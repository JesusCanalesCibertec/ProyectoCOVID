using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao {

    public interface PsCapacidadCursoDao : GenericoDao<PsCapacidadCurso> {
        PsCapacidadCurso obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, String pIdCurso);
        PsCapacidadCurso coreActualizar(UsuarioActual usuarioActual, PsCapacidadCurso bean);
        PsCapacidadCurso coreInsertar(UsuarioActual usuarioActual, PsCapacidadCurso bean);
        void coreEliminar(PsCapacidadCursoPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, String pIdCurso);
        List<PsCapacidadCurso> obtenerLista(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad);
    }
}
