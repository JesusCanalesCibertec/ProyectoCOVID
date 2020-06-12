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
    public class AsConceptoaccesosistemaDaoImpl : GenericoDaoImpl<AsConceptoaccesosistema>, AsConceptoaccesosistemaDao 
    {
        private IServiceProvider servicioProveedor;


	    public AsConceptoaccesosistemaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "asconceptoaccesosistema") {
                servicioProveedor = _servicioProveedor;
	    }

        public AsConceptoaccesosistema obtenerPorId(AsConceptoaccesosistemaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
