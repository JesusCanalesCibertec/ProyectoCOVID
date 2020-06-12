using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.sistema.dao
{

    public interface UsuarioDao : GenericoDao<Usuario>
    {
        Usuario obtenerPorId(UsuarioPk pk);

        /**
	     * Obtener empleado estados.
	     *
	     * @param idCompaniaSocio
	     *            the id compania socio
	     * @param codigoUsuario
	     *            the codigo usuario
	     * @return the dto empleado
	     * @throws Exception
	     *             the exception
	     */
        DtoEmpleadoSeguridad obtenerEmpleadoEstados(String idCompaniaSocio, String codigoUsuario);

        /**
         * Obtener datos empleado por usuario.
         *
         * @param idCompaniaSocio
         *            the id compania socio
         * @param codigoUsuario
         *            the codigo usuario
         * @return the usuario actual
         * @throws Exception
         *             the exception
         */
        UsuarioActual obtenerDatosEmpleadoPorUsuario(String idCompaniaSocio, String codigoUsuario, String origen);
        void actualizarCodigoUsuarioDeEmpleado(String idCompaniaSocio, Int32? idEmpleado, String codigoUsuario);
        Boolean existeEmpleado(String idCompaniaSocio, Int32? idEmpleado);
        ParametroPaginacionGenerico listarReporteSeguridad(ParametroPaginacionGenerico paginacion, string compania, int? empleado, string rEPORTE_COMPROMISO_DATOS);
        void guardarHistorial(string usuario, string clave);
        bool validarClaveHistorico(string usuario, string claveEncriptada);

        String obtenerCompaniaPorDefecto(string codigoUsuario);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroUsuario filtro);
        UsuarioActual Logeo(string idUsuario);

    }
}
