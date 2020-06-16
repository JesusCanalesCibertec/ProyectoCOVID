using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrCapacitacionDao : GenericoDao<HrCapacitacion>
    {
        HrCapacitacion obtenerPorId(String pCompanyowner,String pCapacitacion);
        HrCapacitacion coreActualizar(UsuarioActual usuarioActual, HrCapacitacion bean, String estado);
        HrCapacitacion coreInsertar(UsuarioActual usuarioActual, HrCapacitacion bean, String estado);
        void coreEliminar(HrCapacitacionPk id);
        void coreEliminar(String pCompanyowner,String pCapacitacion);
        HrCapacitacion coreAnular(UsuarioActual usuarioActual,HrCapacitacionPk id);
        HrCapacitacion coreAnular(UsuarioActual usuarioActual,String pCompanyowner,String pCapacitacion);
        ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, FiltroPaginacionCapacitacion filtro);
        string generarCodigo(string v);
        List<string> listarCorreos(HrCapacitacion hrCapacitacion);
        List<DtoTabla> listarParametros(HrCapacitacion hrCapacitacion);
        List<DtoPrevencionSalud> listarPrevencionSaludCabecera(FiltroGraficos filtro);
        List<DtoPrevencionSalud> listarPrevencionSaludDetalle(FiltroGraficos filtro);
        List<DtoCapacitacionParticipante> listarParticipantes(HrCapacitacionPk pk);
    }
}
