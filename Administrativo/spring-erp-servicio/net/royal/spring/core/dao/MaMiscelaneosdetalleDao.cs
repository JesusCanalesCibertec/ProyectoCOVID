using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.core.dao
{
    public interface MaMiscelaneosdetalleDao : GenericoDao<MaMiscelaneosdetalle>
    {
        MaMiscelaneosdetalle obtenerPorId(MaMiscelaneosdetallePk pk);
        List<MaMiscelaneosdetalle> listarActivos(MaMiscelaneosheaderPk maMiscelaneosheaderPk);
        List<MaMiscelaneosdetalle> listar(FiltroMiscelaneosDetalle filtro);
        String obtenerDescripcion(String aplicacioncodigo, String codigotabla, String codigoelemento);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, MaMiscelaneosdetalle filtro);
        String obtenerValorCodigo4(String aplicacioncodigo, String codigotabla, String codigoelemento);
        String obtenerValorCodigo4CuandoPkValorCodigo2(String aplicacioncodigo, String codigotabla, String valorCodigo2);
        List<MaMiscelaneosdetalle> listarEnSentencia(FiltroMiscelaneosDetalle filtro);
        List<MaMiscelaneosdetalle> listarDetalle(MaMiscelaneosheaderPk llave);

        List<MaMiscelaneosdetalle> listarActivosBean(String aplicacionCodigo, String codigoTabla);

        void coreEliminar(MaMiscelaneosheaderPk bean);

        string cadena(string codelemento);

        string generarCodigo();


    }
}
