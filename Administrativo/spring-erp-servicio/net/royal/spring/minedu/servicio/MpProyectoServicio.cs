using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpProyectoServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpProyecto registrar(UsuarioActual usuarioActual, MpProyecto bean);
        MpProyecto obtenerPorId(int pIdProyecto);
        MpProyecto actualizar(UsuarioActual usuarioActual, MpProyecto bean);
        MpProyectoPk cambiarestado(MpProyectoPk pk);
        List<MpProyecto> ListarNombres();
        List<DtoProyecto> Listardetalle(string pTipo, int? pAnio, string pEstado);
        List<DtoTabla> ListarBarraEstados();
        List<DtoTabla> ListarBarraTipos();
        List<MpProyectorecurso> ListarEquipotecnico(int pIdProyecto);
    }
}
