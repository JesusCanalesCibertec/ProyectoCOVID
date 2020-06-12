using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core;
using System.Collections.Generic;

namespace net.royal.spring.core.dao.impl
{
public class TipocambiomastDaoImpl : GenericoDaoImpl<Tipocambiomast>, TipocambiomastDao 
{
        private IServiceProvider servicioProveedor;


	public TipocambiomastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "tipocambiomast") {
            servicioProveedor = _servicioProveedor;
	}

        public Tipocambiomast obtenerPorFecha(string fechaCadena)
        {
            IQueryable<Tipocambiomast> query = this.getCriteria();

            query = query.Where(p => p.Fechacambiostring == fechaCadena);

            List<Tipocambiomast> lst = query.ToList();

            if (lst.Count == 1)
                return lst[0];

            return null;
        }

        public Tipocambiomast obtenerPorId(TipocambiomastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
