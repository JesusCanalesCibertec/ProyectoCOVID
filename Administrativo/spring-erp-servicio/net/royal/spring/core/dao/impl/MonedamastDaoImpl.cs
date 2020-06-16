using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using System.Collections.Generic;

namespace net.royal.spring.core.dao.impl
{
public class MonedamastDaoImpl : GenericoDaoImpl<Monedamast>, MonedamastDao 
{
        private IServiceProvider servicioProveedor;


	public MonedamastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "monedamast") {
            servicioProveedor = _servicioProveedor;
	}

        public List<Monedamast> listarActivos()
        {
            IQueryable<Monedamast> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            return query.ToList();
        }

        public Monedamast obtenerPorId(MonedamastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
