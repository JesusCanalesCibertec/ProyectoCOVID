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
    public class AsAsistenciadiariaDaoImpl : GenericoDaoImpl<AsAsistenciadiaria>, AsAsistenciadiariaDao 
    {
        private IServiceProvider servicioProveedor;


	    public AsAsistenciadiariaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "asasistenciadiaria") {
                servicioProveedor = _servicioProveedor;
	    }

        public AsAsistenciadiaria obtenerPorId(AsAsistenciadiariaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
