using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadEquipamientoDao : GenericoDao<PsEntidadEquipamiento>
    {
        PsEntidadEquipamiento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdEquipamiento);
        PsEntidadEquipamiento coreActualizar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean);
        PsEntidadEquipamiento coreInsertar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean);
        void coreEliminar(PsEntidadEquipamientoPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdEquipamiento);
        List<PsEntidadEquipamiento> listarBeneficiario(string institucion, int idBeneficiario);
    }
}
