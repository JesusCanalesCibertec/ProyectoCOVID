using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.minedu.dominio.dto;

namespace net.royal.spring.sistema.servicio
{

    public interface UsuarioServicio : GenericoServicio
    {
        UsuarioActual login(string idAplicacion, string idUsuario, string clave, string idCompaniaSocio, string conformidad, Boolean flgValidarClave, string origen);
        UsuarioActual login(String idAplicacion, String idUsuario, String clave, String idCompaniaSocio, string conformidad,string origen);
        void cambiarClave(String usuario, String clave, String clave1, String clave2);
        List<MensajeUsuario> validarUsuarioClave(String idCompaniaSocio, String idUduario, String clave, Boolean flgValidarClave);
        Boolean expiroUsuario(String usuario);
        
        List<MensajeUsuario> recuperarClave(String correoElectronico, String origen);
        ParametroPaginacionGenerico listarReporteSeguridad(ParametroPaginacionGenerico paginacion, string compania, int empleado, string rEPORTE_COMPROMISO_DATOS);
        //bool tieneFlagConformidadBoleta(string usuarioLogin);
        //void ActualizarConformidadBoleta(string usuarioLogin);
        void inactivarUsuario(string v);

        string esAdministradorWeb(string idUSuario);
        //bool tieneFlagConformidadCambioHorario(string usuarioLogin);
        //void ActualizarConformidadCambioHorario(string usuarioLogin);

        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroUsuario filtro);
        Usuario obtenerPorId(UsuarioPk llave);
        UsuarioActual Logeo(string idUsuario, string clave);
        DtoDirectoriousuario GetUserInformation(UsuarioActual usuarioActual, string pUsuario);
    }
}
