using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using net.royal.spring.sistema.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.minedu.servicio
{
    public interface MpPersonaServicio : GenericoServicio
    {
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        MpPersona registrar(UsuarioActual usuarioActual, MpPersona bean);
        MpPersona obtenerPorId(int pIdPersona, int pIdContratacion);
        MpPersona actualizar(UsuarioActual usuarioActual, MpPersona bean);
        MpContratacion cambiarestado(MpContratacion bean);
        List<MpPersona> ListarNombres();
        List<DtoListafechas> ListarPersonal(Nullable<DateTime> parametro);
        List<DtoEventos> ListarEventos(int? pIdPersona);
        ParametroPaginacionGenerico ListarPaginacionUsuario(ParametroPaginacionGenerico paginacion, DtoTabla filtro);
        Seguridadperfilusuario RegistrarUsuario(UsuarioActual usuarioActual, Seguridadperfilusuario bean);
        Seguridadperfilusuario EliminarUsuario(Seguridadperfilusuario bean);
        Seguridadperfilusuario ActualizarUsuario(UsuarioActual usuarioActual, Seguridadperfilusuario bean);
    }
}
