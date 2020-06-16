using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dominio.filtro;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.sistema.dao
{

    public interface SyAprobacionprocesoDao : GenericoDao<SyAprobacionproceso>
    {
        List<SyAprobacionproceso> listarCodigoProcesoPorCodigoProcesoBase(String codigoProcesoBase);

        List<SyAprobacionproceso> listar(FiltroAprobacionProceso filtro);
        ParametroPaginacionGenerico listarSolicitudes(UsuarioActual usuarioActual, ParametroPaginacionGenerico paginacion,
                FiltroSolicitudes filtro);
        void SincronizarHorarios(string companiaSocioCodigo);
    }
}
