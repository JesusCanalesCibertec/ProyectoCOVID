using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;

namespace net.royal.spring.programasocial.dao
{

    public interface PsEntidadDocumentoDao : GenericoDao<PsEntidadDocumento>
    {
        PsEntidadDocumento obtenerPorId(Nullable<Int32> pIdEntidad,String pIdTipoDocumento);
        PsEntidadDocumento coreActualizar(UsuarioActual usuarioActual, PsEntidadDocumento bean);
        PsEntidadDocumento coreInsertar(UsuarioActual usuarioActual, PsEntidadDocumento bean);
        void coreEliminar(PsEntidadDocumentoPk id);
        void coreEliminar(Nullable<Int32> pIdEntidad,String pIdTipoDocumento);
        List<PsEntidadDocumento> listarBeneficiario(string institucion, int idBeneficiario);
    }
}
