using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.web.servicio;

namespace net.royal.spring.sistema.servicio
{
    public interface ParametrosmastServicio : GenericoServicio
    {

        Parametrosmast obtenerPorId(ParametrosmastPk pk);

        String obtenerValorCadena(String parametro);

        String obtenerValorCadena(String parametro, String aplicacion);

        String obtenerValorCadena(String parametro, String aplicacion, String compania);

        Int32? obtenerValorInteger(String parametro);

        Int32? obtenerValorInteger(String parametro, String aplicacion);

        Int32? obtenerValorInteger(String parametro, String aplicacion, String compania);

        String obtenerValorExplicacion(String parametro);

        String obtenerValorExplicacion(String parametro, String aplicacion);

        String obtenerValorExplicacion(String parametro, String aplicacion, String compania);
        String obtenerValorTipoDatoFlag(String parametro, String aplicacion);
        String obtenerValorTipoDatoFlag(String parametro, String aplicacion, String compania);
    }
}
