using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.dao.impl
{
    public class ParametrosmastDaoImpl : GenericoDaoImpl<Parametrosmast>, ParametrosmastDao
    {
        private IServiceProvider servicioProveedor;

        public ParametrosmastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) :base(context, "parametrosmast") {
            servicioProveedor = _servicioProveedor;
        }

        public Parametrosmast obtener(string parametro, string aplicacion, string compania)
        {
            IQueryable<Parametrosmast> query = this.getCriteria();

            query = query.Where(p => p.Parametroclave == parametro);

            if (!UString.estaVacio(aplicacion))
                query = query.Where(p => p.Aplicacioncodigo == aplicacion);
            if (!UString.estaVacio(compania))
                query = query.Where(p => p.Companiacodigo == compania);
            
            List<Parametrosmast> lst = query.ToList();

            if (lst.Count == 1)
                return lst[0];

            return null;            
        }

        public Parametrosmast obtenerPorId(ParametrosmastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public string obtenerValorCadena(string parametro)
        {
            return obtenerValorCadena(parametro, null, null);
        }

        public string obtenerValorCadena(string parametro, string aplicacion)
        {
            return obtenerValorCadena(parametro, aplicacion, null);
        }

        public string obtenerValorCadena(string parametro, string aplicacion, string compania)
        {
            Parametrosmast para = obtener(parametro, aplicacion, compania);
            if (para == null)
                return null;
            return para.Texto;
        }

        public string obtenerValorDescripcion(string parametro, string aplicacion)
        {
            return obtenerValorDescripcion(parametro, aplicacion, null);
        }

        public string obtenerValorDescripcion(string parametro, string aplicacion, string compania)
        {
            Parametrosmast para = obtener(parametro, aplicacion, compania);
            if (para == null)
                return null;
            return para.Descripcionparametro;
        }

        public string obtenerValorExplicacion(string parametro)
        {
            return obtenerValorExplicacion(parametro, null, null);
        }

        public string obtenerValorExplicacion(string parametro, string aplicacion)
        {
            return obtenerValorExplicacion(parametro, aplicacion, null);
        }

        public string obtenerValorExplicacion(string parametro, string aplicacion, string compania)
        {
            Parametrosmast para = obtener(parametro, aplicacion, compania);
            if (para == null)
                return null;
            return para.Explicacion;
        }

        public Int32? obtenerValorInteger(string parametro)
        {
            return obtenerValorInteger(parametro, null, null);
        }

        public Int32? obtenerValorInteger(string parametro, string aplicacion)
        {
            return obtenerValorInteger(parametro, aplicacion, null);
        }

        public Int32? obtenerValorInteger(string parametro, string aplicacion, string compania)
        {
            Parametrosmast para = obtener(parametro, aplicacion, compania);
            if (para == null)
                return null;

            if (para.Numero == null) {
                Int32 retorno=0;
                return retorno;
            }               

            return Convert.ToInt32(para.Numero);
        }

        public String obtenerValorTipoDatoFlag(String parametro, String aplicacion) {
            return obtenerValorTipoDatoFlag(parametro, aplicacion, "999999");
        }
        public String obtenerValorTipoDatoFlag(String parametro, String aplicacion, String compania) {
            Parametrosmast para = obtener(parametro, aplicacion, compania);
            if (para == null)
                return "N";
            return UString.obtenerValorCadenaSinNulo(para.Tipodedatoflag).Trim();
        }
    }
}
