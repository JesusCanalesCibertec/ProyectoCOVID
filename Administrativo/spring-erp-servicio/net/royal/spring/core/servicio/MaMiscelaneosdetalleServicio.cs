using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

public interface MaMiscelaneosdetalleServicio : GenericoServicio {
        MaMiscelaneosdetalle obtenerPorId(MaMiscelaneosdetallePk maMiscelaneosdetallePk);
        MaMiscelaneosdetalle registrar(MaMiscelaneosdetalle maMiscelaneosdetalle);
        MaMiscelaneosdetalle actualizar(MaMiscelaneosdetalle maMiscelaneosdetalle);
        void eliminar(MaMiscelaneosdetallePk maMiscelaneosdetallePk);
        List<MaMiscelaneosdetalle> listarActivos(MaMiscelaneosheaderPk maMiscelaneosheaderPk);
        List<MaMiscelaneosdetalle> listarActivos(String codigoTabla);
        List<MaMiscelaneosdetalle> listarActivos(String aplicacionCodigo, String codigoTabla);
        List<MaMiscelaneosdetalle> listarActivosBean(String aplicacionCodigo, String codigoTabla);
        
        List<MaMiscelaneosdetalle> listarActivosPorCodigo1(String aplicacionCodigo, String codigoTabla,String valorCodigo1);
        List<MaMiscelaneosdetalle> listar(FiltroMiscelaneosDetalle filtro);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico parametroPaginacionGenerico, MaMiscelaneosdetalle filtro);
        List<MaMiscelaneosdetalle> listarEnSentencia(FiltroMiscelaneosDetalle filtro);
        String obtenerDescripcionPorId(MaMiscelaneosdetallePk maMiscelaneosdetallePk);
        String obtenerDescripcionPorId(String aplicacion,String tabla,String elemento);
        List<MaMiscelaneosdetalle> listarDetalle(MaMiscelaneosheaderPk llave);
        List<DtoTabla> listarHeaderPorAplicacion(string aplicacionCodigo, string compania);
    }
}
