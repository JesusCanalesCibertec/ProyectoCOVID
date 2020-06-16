using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao.impl
{
public class HrEmpleadoDaoImpl : GenericoDaoImpl<HrEmpleado>, HrEmpleadoDao 
{
        private IServiceProvider servicioProveedor;


	public HrEmpleadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "hrempleado") {
            servicioProveedor = _servicioProveedor;
	}

}
}
