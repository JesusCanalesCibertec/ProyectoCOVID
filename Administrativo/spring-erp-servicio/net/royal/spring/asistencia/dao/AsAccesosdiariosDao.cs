using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.asistencia.dominio.filtro;

namespace net.royal.spring.asistencia.dao
{

    public interface AsAccesosdiariosDao : GenericoDao<AsAccesosdiarios>
    {
        AsAccesosdiarios obtenerPorId(AsAccesosdiariosPk pk);

        List<DtoMarcaciones> listarMarcaciones(DateTime fechadesde, DateTime fechahasta, Int32 empleado);
        List<DtoMarcaciones> listarMarcacionesConsolidado(DateTime fechadesde, DateTime fechahasta, Int32 empleado);
        DtoMarcaciones traerMarcas(DateTime fechadesde, DateTime fechahasta, int empleado);

        ParametroPaginacionGenerico listarMarcaciones(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);

        DtoMarcaciones traerMarcas(FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        ParametroPaginacionGenerico listarMarcacionesConsolidado(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);
        bool existeMarca(decimal empleado, DateTime fecha);
        ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroAsAccesosDiarios filtroAsAccesosDiarios);
        void registrar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);
        void actualizar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);
        bool validarSolicitudOmision(DateTime? fecha, int empleado);
        int contarMarcasPorDia(decimal value1, DateTime value2);
        int contarMarcasPorDiaHorasLibres(decimal value1, DateTime value2, string conceptoacceso);
        int contarMarcasPorRango(decimal value1, string conceptoacceso, DateTime value2, DateTime value3);
        List<AsAccesosdiarios> obtenerMarcasPorDia(int persona, string compania, DateTime? inicio, DateTime? fin);
        Int32 listarCruces(DateTime fechaInicio, DateTime fechaFin, decimal? empleado);
        Int32 listarCrucesConceptos(DateTime fechaInicio, DateTime fechaFin, decimal? empleado,String concepto);
    }
}
