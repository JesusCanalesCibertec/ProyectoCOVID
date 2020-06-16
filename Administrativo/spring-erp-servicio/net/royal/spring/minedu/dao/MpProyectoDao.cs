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
    public interface MpProyectoDao : GenericoDao<MpProyecto>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpProyecto registrar(UsuarioActual usuarioActual, MpProyecto bean);
        MpProyecto actualizar(UsuarioActual usuarioActual, MpProyecto bean);
        List<MpProyecto> ListarNombres();
        List<DtoProyecto> Listardetalle(string pTipo, int? pAnio, string pEstado);
        void eliminarListarecursos(int? pIdProyecto);
        int generarCodigo();
        List<DtoTabla> ListarBarraEstados();
        List<DtoTabla> ListarBarraTipos();
    }
}
