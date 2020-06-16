using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.dao.impl
{
    public class AsAreaDaoImpl : GenericoDaoImpl<AsArea>, AsAreaDao 
    {
        private IServiceProvider servicioProveedor;


	public AsAreaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "asarea") {
            servicioProveedor = _servicioProveedor;
	}

        public AsArea obtenerPorId(AsAreaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
