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
public class OcupacionmastDaoImpl : GenericoDaoImpl<Ocupacionmast>, OcupacionmastDao 
{
        private IServiceProvider servicioProveedor;


	public OcupacionmastDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "ocupacionmast") {
            servicioProveedor = _servicioProveedor;
	}

        public Ocupacionmast obtenerPorId(OcupacionmastPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
