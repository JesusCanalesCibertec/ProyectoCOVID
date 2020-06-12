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
    public class AsAsistenciadiariamarcaDaoImpl : GenericoDaoImpl<AsAsistenciadiariamarca>, AsAsistenciadiariamarcaDao
    {
        private IServiceProvider servicioProveedor;


        public AsAsistenciadiariamarcaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asasistenciadiariamarca")
        {
            servicioProveedor = _servicioProveedor;
        }

        public AsAsistenciadiariamarca obtenerPorId(AsAsistenciadiariamarcaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
