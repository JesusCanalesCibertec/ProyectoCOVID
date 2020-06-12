using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio.dtoscomponente;

namespace net.royal.spring.programasocial.servicio
{
    public interface PsRasServicio : GenericoServicio
    {
        List<DtoComponenteRas> consultar(UsuarioActual usuarioActual, FiltroRas filtro);
        DtoComponenteRas obtenerPorId(String idInstitucion, Nullable<Int32> idBeneficiario, Nullable<Int32> idAtencion);
        DtoComponenteRas actualizar(UsuarioActual usuarioActual, DtoComponenteRas bean);
        DtoComponenteRas insertar(UsuarioActual usuarioActual, DtoComponenteRas bean);
        DtoComponenteRas anular(UsuarioActual usuarioActual, String idInstitucion, Nullable<Int32> idBeneficiario, Nullable<Int32> idAtencion);
    }
}
