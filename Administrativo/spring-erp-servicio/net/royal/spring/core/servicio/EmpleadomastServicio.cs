using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

    public interface EmpleadomastServicio : GenericoServicio
    {
        Empleadomast obtenerPorId(EmpleadomastPk empleadomastPk);

        Empleadomast registrar(Empleadomast empleadomast);

        Empleadomast actualizar(Empleadomast empleadomast);

        void eliminar(EmpleadomastPk empleadomastPk);

        String confirmarCargo(Int32? idEmpleado, Int32? codigoCargo);

        /**
         * @see net.royal.erp.core.dao.EmpleadomastDao#obtenerPorCodigoUsuario(String)
         */
        Empleadomast obtenerPorCodigoUsuario(String codigousuario);

        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionEmpleado filtro);

        DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario);
        DtoEmpleadoBasico obtenerInformacionPorIdPersonaA(UsuarioActual usuario);

        ParametroPaginacionGenerico listarCumpleanios(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro);

        ParametroPaginacionGenerico listarAniversarios(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro);

        DominioArchivo obtenerFotoActual(Int32? idEmpleado);

        void fotoActualizar(Int32? idEmpleado, String numeroDocumento, DominioArchivo foto);

        DtoEmpleado obtenerCompaniaActiva(int empleado);

        /**
         * se obtiene el id empleado y en base al id busca las compañias si es que tiene mas de una
         */
        List<DtoTabla> listarEmpresasActivasPorUsuario(string idUsuario);
        ParametroPaginacionGenerico listarCumpleaniosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionCumpleanio filtro);

        ParametroPaginacionGenerico listarAniversariosConFoto(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro);
       // DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario);
        ParametroPaginacionGenerico listarBusquedaUnidaOperativa(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        ParametroPaginacionGenerico listarBusquedaPuesto(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        string obtenerHorario(int persona, string compania, DateTime? inicio);
        string esFiscalizado(int persona);
        bool esJefe(int? personaId);

        List<DtoTablaEntero> obtenerDiasPorMes(Int32 nroMes);
    }
}
