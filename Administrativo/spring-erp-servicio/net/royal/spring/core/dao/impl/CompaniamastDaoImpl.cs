using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.dao.impl
{
public class CompaniamastDaoImpl : GenericoDaoImpl<Companiamast>, CompaniamastDao 
{
        private IServiceProvider servicioProveedor;


	public CompaniamastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "companiamast") {
            servicioProveedor = _servicioProveedor;
	}

        public Companiamast obtenerPorId(CompaniamastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
