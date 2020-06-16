using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.core.dominio;
using System.Collections.Generic;

namespace net.royal.spring.sistema.dao.impl
{
    public class AplicacionesmastDaoImpl : GenericoDaoImpl<Aplicacionesmast>, AplicacionesmastDao 
    {
        private IServiceProvider servicioProveedor;

	    public AplicacionesmastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "aplicacionesmast") {
                servicioProveedor = _servicioProveedor;
	    }

        public List<Aplicacionesmast> ListarActivos()
        {
            IQueryable<Aplicacionesmast> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            query = query.Where(p => p.Estadisponible == "S");
            query = query.OrderBy(p => p.Descripcioncorta);
            return query.ToList();
        }

    }
}
