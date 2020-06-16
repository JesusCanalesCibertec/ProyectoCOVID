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
public class AccountmstDaoImpl : GenericoDaoImpl<Accountmst>, AccountmstDao 
{
        private IServiceProvider servicioProveedor;


	public AccountmstDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "accountmst") {
            servicioProveedor = _servicioProveedor;
	}

        public Accountmst obtenerPorId(AccountmstPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
