using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpPersonaconocimientoServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersonaconocimiento registrar(UsuarioActual usuarioActual, MpPersonaconocimiento bean);
        MpPersonaconocimiento obtenerPorId(int pIdPersona);
        MpPersonaconocimiento actualizar(UsuarioActual usuarioActual, MpPersonaconocimiento bean);
        MpPersonaconocimientoPk cambiarestado(MpPersonaconocimientoPk pk);
        List<DtoTabla> listado(int? pIdPersona);
        void eliminar(MpPersonaconocimiento bean);
    }
}
