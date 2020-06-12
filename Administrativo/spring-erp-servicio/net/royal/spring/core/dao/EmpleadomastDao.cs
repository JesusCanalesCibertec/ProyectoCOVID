using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface EmpleadomastDao : GenericoDao<Empleadomast>
    {
        List<MensajeUsuario> validarEmpleadoEnCompania(List<MensajeUsuario> lstMensajes, String idCompania, Int32? idEmpleado);
        List<Empleadomast> listarPorCorreoInterno(String codigousuario);
        Empleadomast obtenerPorId(String idCompania, Int32? idEmpleado);
        Empleadomast obtenerPorId(EmpleadomastPk pk);
        Empleadomast obtenerPorIdEmpleado(Int32? idEmpleado);
        DtoEmpleadoBasico obtenerInformacionPorIdPersona(Int32? idPersona);
        Empleadomast obtenerPorCodigoUsuario(String codigousuario);
        Empleadomast obtenerPorCorreoInterno(string CorreoInternoEnviado);
        List<Empleadomast> obtenerVariosPorCorreoInterno(string CorreoInternoEnviado);
        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionEmpleado filtro);
        ParametroPaginacionGenerico listarCumpleanios(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionCumpleanio filtro);
        ParametroPaginacionGenerico listarAniversarios(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro);
        ParametroPaginacionGenerico listarAniversariosFiltroDividido(ParametroPaginacionGenerico paginacion, FiltroPaginacionAniversario filtro);
        string obtenerAfp(string codigoAfp);

        DtoEmpleado obtenerCompaniaActiva(int empleado);
        DtoEmpleadoBasico obtenerInformacionPorIdPersona(UsuarioActual usuario);
        DtoEmpleadoBasico obtenerInformacionPorIdPersonaA(UsuarioActual usuario);

        Empleadomast obtenerPorIdEmpleadoCompania(int? idEmpleado, string compania);
        ParametroPaginacionGenerico listarBusquedaUnidaOperativa(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        List<Empleadomast> listarPorUsuario(string usuarioRecuperar);
        ParametroPaginacionGenerico listarBusquedaPuesto(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        List<DtoTabla> listarEmpresasActivasPorUsuario(string idUsuario);
        string obtenerHorario(int persona, string compania, DateTime? inicio);

        DateTime? obtenerHoraFinHorarioPorDia(int v, string companyowner, DateTime? desde);
        DateTime? obtenerHoraInicioHorarioPorDia(int v, string companyowner, DateTime? fechafin);

        string esFiscalizado(int persona);

        bool esJefe(int? personaId);
        List<DtoHorario> obtenerHorarioEmpleado(DateTime par_desde, DateTime par_hasta, int par_empleado);

        DateTime? obtenerHoraFinHorarioPorDia(int v, string companyowner, DateTime fechaADesde, bool cONFIGURACION_CONTAR_SOBRETIEMPO);
        DateTime? obtenerHoraInicioHorarioPorDia(int v, string companyowner, DateTime fechaADesde, bool cONFIGURACION_CONTAR_SOBRETIEMPO);
    }
}
