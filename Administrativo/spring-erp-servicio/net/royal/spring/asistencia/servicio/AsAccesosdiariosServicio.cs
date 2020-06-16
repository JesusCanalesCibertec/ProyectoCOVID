using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.asistencia.dominio.filtro;

namespace net.royal.spring.asistencia.servicio
{

    public interface AsAccesosdiariosServicio : GenericoServicio
    {

        DtoMarcaciones traerMarcas(FiltroPaginacionEmpleado filtroPaginacionEmpleado);
        ParametroPaginacionGenerico listarMarcacionesConsolidado(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);


        List<DtoMarcaciones> listarMarcaciones(DateTime fechadesde, DateTime fechahasta, int empleado);
        List<DtoMarcaciones> listarMarcacionesConsolidado(DateTime fechadesde, DateTime fechahasta, int empleado);
        DtoMarcaciones traerMarcas(DateTime fechadesde, DateTime fechahasta, int empleado);
        void registrar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);
        ParametroPaginacionGenerico listarMarcaciones(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro);
        ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroAsAccesosDiarios filtroAsAccesosDiarios);
        void actualizar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios);
        List<AsAccesosdiarios> obtenerMarcasPorDia(int persona, String compania, DateTime? inicio, DateTime? fin);

        string ExportarMarcas(FiltroPaginacionEmpleado filtro);
        string ExportarAsistencia(FiltroPaginacionEmpleado filtro);
    }
}
