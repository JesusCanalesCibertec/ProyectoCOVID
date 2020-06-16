using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpConocimientoServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpConocimiento registrar(UsuarioActual usuarioActual, MpConocimiento bean);
        MpConocimiento obtenerPorId(int pIdConocimiento);
        MpConocimiento actualizar(UsuarioActual usuarioActual, MpConocimiento bean);
        MpConocimientoPk cambiarestado(MpConocimientoPk pk);
        List<MpConocimiento> listado(DtoTabla filtro);
    }
}
