using net.royal.spring.framework.web.dao;
using net.royal.spring.sistema.dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.dao
{
    public interface ParametrosmastDao : GenericoDao<Parametrosmast>
    {
        Parametrosmast obtenerPorId(ParametrosmastPk pk);

        Parametrosmast obtener(String parametro, String aplicacion, String compania);

        String obtenerValorCadena(String parametro);

        String obtenerValorCadena(String parametro, String aplicacion);

        String obtenerValorCadena(String parametro, String aplicacion, String compania);

        Int32? obtenerValorInteger(String parametro);

        Int32? obtenerValorInteger(String parametro, String aplicacion);

        Int32? obtenerValorInteger(String parametro, String aplicacion, String compania);

        String obtenerValorExplicacion(String parametro);

        String obtenerValorExplicacion(String parametro, String aplicacion);

        String obtenerValorExplicacion(String parametro, String aplicacion, String compania);

        String obtenerValorDescripcion(String parametro, String aplicacion);

        String obtenerValorDescripcion(String parametro, String aplicacion, String compania);
        String obtenerValorTipoDatoFlag(String parametro, String aplicacion);
        String obtenerValorTipoDatoFlag(String parametro, String aplicacion, String compania);        
    }
}
