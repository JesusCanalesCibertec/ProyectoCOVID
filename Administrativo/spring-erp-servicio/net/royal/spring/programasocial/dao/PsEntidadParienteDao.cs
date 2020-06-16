using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadParienteDao : GenericoDao<PsEntidadPariente>
    {
        PsEntidadPariente obtenerPorId(Nullable<Int32> pIdEntidad,Nullable<Int32> pIdPariente);
        PsEntidadPariente coreActualizar(UsuarioActual usuarioActual, PsEntidadPariente bean);
        PsEntidadPariente coreInsertar(UsuarioActual usuarioActual, PsEntidadPariente bean);
        void coreEliminar(PsEntidadParientePk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,Nullable<Int32> pIdPariente);
        List<PsEntidadPariente> listarBeneficiario(string institucion, int idBeneficiario);
    }
}
