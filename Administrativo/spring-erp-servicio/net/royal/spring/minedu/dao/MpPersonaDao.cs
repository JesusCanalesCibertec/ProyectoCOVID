using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.dao
{
    public interface MpPersonaDao : GenericoDao<MpPersona>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersona registrar(UsuarioActual usuarioActual, MpPersona bean);
        MpPersona actualizar(UsuarioActual usuarioActual, MpPersona bean);
        List<MpPersona> ListarNombres();
        MpPersona ObtenerPersonaxUsuario(string usuario);
        MpPersona ObtenerPersonaxDNI(string pDNI);
        List<DtoListafechas> ListarPersonal(DateTime? parametro);
        int generarCodigo();
        List<DtoEventos> ListarEventos(int? pIdPersona);
    }
}
