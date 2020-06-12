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
    public interface MpContratacionDao : GenericoDao<MpContratacion>
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpContratacion registrar(UsuarioActual usuarioActual, MpContratacion bean);
        MpContratacion actualizar(UsuarioActual usuarioActual, MpContratacion bean);
        int? generarCodigo();
        List<DtoContratacion> Contratosactivos();
        //List<DtoContratacion> ListarPersonaldisponible(DtoTabla filtro);
        MpContratacion obtenerUltimoXPersona(int pIdPersona);
        List<DtoTabla> ListarPie();
        List<DtoContratacion> ListarContratoPorPersona(int pIdPersona);
        MpContratacion ObtenerContratoxNOrden(string pOrden);

        List<DtoFechasdisponibles> obtenerFechasnodisponibles(DtoTabla filtro);
        ParametroPaginacionGenerico ListarPersonaldisponible1(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
    }
}
