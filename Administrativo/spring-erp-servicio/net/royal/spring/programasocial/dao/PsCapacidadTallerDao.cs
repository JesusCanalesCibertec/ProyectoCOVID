using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsCapacidadTallerDao : GenericoDao<PsCapacidadTaller>
    {
        PsCapacidadTaller obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller);
        PsCapacidadTaller coreActualizar(UsuarioActual usuarioActual, PsCapacidadTaller bean);
        PsCapacidadTaller coreInsertar(UsuarioActual usuarioActual, PsCapacidadTaller bean);
        void coreEliminar(PsCapacidadTallerPk id);
        void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller);
        void eliminarPorCapacidad(int? idCapacidad);
    }
}
