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
public class CompanyownerImagenesDaoImpl : GenericoDaoImpl<CompanyownerImagenes>, CompanyownerImagenesDao 
{
        private IServiceProvider servicioProveedor;


	public CompanyownerImagenesDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "companyownerimagenes") {
            servicioProveedor = _servicioProveedor;
	}

        public CompanyownerImagenes obtenerPorId(CompanyownerImagenesPk pk)
        {
            throw new NotImplementedException();
        }
    }
}
