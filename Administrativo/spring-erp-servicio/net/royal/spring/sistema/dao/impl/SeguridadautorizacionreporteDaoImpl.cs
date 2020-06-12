using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.sistema.dao.impl
{
public class SeguridadautorizacionreporteDaoImpl : GenericoDaoImpl<Seguridadautorizacionreporte>, SeguridadautorizacionreporteDao 
{
        private IServiceProvider servicioProveedor;


	public SeguridadautorizacionreporteDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "seguridadautorizacionreporte") {
            servicioProveedor = _servicioProveedor;
	}

}
}
