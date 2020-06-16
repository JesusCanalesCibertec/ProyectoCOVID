using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyAprobacionnivelesDao : GenericoDao<SyAprobacionniveles>
    {
        //SyAprobacionniveles obtenerPorCodigoProceso(Int32? codigoProceso);
        SyAprobacionniveles obtenerPorCodigoProcesoCompania(Int32? codigoProceso, String compania);
        List<SyAprobacionniveles> obtenerPorCodigoProcesoCompaniaList(Int32? codigoProceso, String compania);
        SyAprobacionniveles obtenerPorId(SyAprobacionnivelesPk pk);
        DtoEmpleado obtenerEmpleado(Int32? idPersona);
        List<DtoEmpleado> obtenerEmpleados(Int32? idPersona);
        DtoEmpleado obtenerEmpleado(Int32? idPersona, String compania);

        HrDepartamento obtenerDepartamento(String idDepartamento);
        List<SyAprobacionniveles> listar(FiltroNivelAprobacion filtro);
        Int32? generarCodigo();
        Int32? obtenerNroNiveles(SyAprobacionnivelesPk pk);
        String esAdministrador(SyAprobacionnivelesPk pk, Int32? idPersona, String compania);
        String esSuperUsuario(SyAprobacionnivelesPk pk, Int32? idPersona, String compania);
        String esAdministrador(String codigoProceso, Int32? idPersona, String compania);
        String esSuperUsuario(String codigoProceso, Int32? idPersona, String compania);
        List<DtoTabla> listarAplicacionPorUsuario(String idUsuario);

        SyAprobacionniveles obtenerPorIdCompleto(SyAprobacionnivelesPk syAprobacionnivelesPk);
        List<SyAprobacionniveles> listarPorAplicacionActivos(string idAplicacion);
        SyAprobacionniveles registrar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles);
        void actualizar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles);
        void eliminar(UsuarioActual usuarioActual, Int32? codigo, string compania);
        int? obtenerNroNivelesPorProceso(int? procesoAprobar);
        void validarConfiguracionPorCompania(string proceso, string companiaSocioCodigo);
    }
}
