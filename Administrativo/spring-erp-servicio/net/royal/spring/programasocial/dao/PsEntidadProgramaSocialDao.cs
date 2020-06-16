using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadProgramaSocialDao : GenericoDao<PsEntidadProgramaSocial>
    {
        PsEntidadProgramaSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdProgramaSocial);
        PsEntidadProgramaSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean);
        PsEntidadProgramaSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadProgramaSocial bean);
        void coreEliminar(PsEntidadProgramaSocialPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdProgramaSocial);
        List<PsEntidadProgramaSocial> listarBeneficiario(string institucion, int idBeneficiario);
    }
}
