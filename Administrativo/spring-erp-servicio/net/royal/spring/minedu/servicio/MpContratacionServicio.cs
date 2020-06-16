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
    public interface MpContratacionServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpContratacion registrar(UsuarioActual usuarioActual, MpContratacion bean);
        MpContratacion obtenerPorId(int pIdContratacion);
        MpContratacion actualizar(UsuarioActual usuarioActual, MpContratacion bean);
        MpContratacionPk cambiarestado(MpContratacionPk pk);
        MpContratacion registrarlista(UsuarioActual usuarioActual, MpContratacion bean);
        List<DtoContratacion> Contratosactivos();
        List<DtoContratacion> ListarPersonaldisponible(DtoTabla filtro);
        List<DtoTabla> ListarPie();
        List<DtoContratacion> ListarContratoPorPersona(int pIdPersona);
        ParametroPaginacionGenerico ListarPersonaldisponible1(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
    }
}
