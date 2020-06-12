using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.servicio.impl
{
    public class ParametrosmastServicioImpl : GenericoServicioImpl, ParametrosmastServicio
    {
        private ParametrosmastDao parametrosmastDao;

        public ParametrosmastServicioImpl(ParametrosmastDao _parametrosmastDao) {
            parametrosmastDao = _parametrosmastDao;
        }

        private Parametrosmast obtener(String parametro, String aplicacion, String compania)
        {
            return parametrosmastDao.obtener(parametro, aplicacion, compania);
        }

        public string obtenerValorCadena(string parametro)
        {
            return parametrosmastDao.obtenerValorCadena(parametro);
        }

        public string obtenerValorCadena(string parametro, string aplicacion)
        {
            return parametrosmastDao.obtenerValorCadena(parametro, aplicacion);
        }

        public string obtenerValorCadena(string parametro, string aplicacion, string compania)
        {
            return parametrosmastDao.obtenerValorCadena(parametro, aplicacion, compania);
        }

        public string obtenerValorExplicacion(string parametro)
        {
            return parametrosmastDao.obtenerValorExplicacion(parametro);
        }

        public string obtenerValorExplicacion(string parametro, string aplicacion)
        {
            return parametrosmastDao.obtenerValorExplicacion(parametro, aplicacion);
        }

        public string obtenerValorExplicacion(string parametro, string aplicacion, string compania)
        {
            return parametrosmastDao.obtenerValorExplicacion(parametro, aplicacion, compania);
        }

        public Int32? obtenerValorInteger(string parametro)
        {
            return parametrosmastDao.obtenerValorInteger(parametro);
        }

        public Int32? obtenerValorInteger(string parametro, string aplicacion)
        {
            return parametrosmastDao.obtenerValorInteger(parametro, aplicacion);
        }

        public Int32? obtenerValorInteger(string parametro, string aplicacion, string compania)
        {
            return parametrosmastDao.obtenerValorInteger(parametro, aplicacion, compania);
        }

        public string obtenerValorTipoDatoFlag(string parametro, string aplicacion)
        {
            return parametrosmastDao.obtenerValorTipoDatoFlag(parametro, aplicacion, "999999");
        }
        public string obtenerValorTipoDatoFlag(string parametro, string aplicacion, string compania)
        {
            return parametrosmastDao.obtenerValorTipoDatoFlag(parametro, aplicacion, compania);
        }

        public Parametrosmast obtenerPorId(ParametrosmastPk pk)
        {
            return parametrosmastDao.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
