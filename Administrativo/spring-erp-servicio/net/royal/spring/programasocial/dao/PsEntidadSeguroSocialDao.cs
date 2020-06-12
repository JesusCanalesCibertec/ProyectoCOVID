using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadSeguroSocialDao : GenericoDao<PsEntidadSeguroSocial>
    {
        PsEntidadSeguroSocial obtenerPorId(Nullable<Int32> pIdEntidad,String pIdSeguroSocial);
        PsEntidadSeguroSocial coreActualizar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean);
        PsEntidadSeguroSocial coreInsertar(UsuarioActual usuarioActual, PsEntidadSeguroSocial bean);
        void coreEliminar(PsEntidadSeguroSocialPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdSeguroSocial);
        List<PsEntidadSeguroSocial> listarBeneficiario(string institucion, int idBeneficiario);
    }
}
