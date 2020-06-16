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
public class SeguridadconceptoDaoImpl : GenericoDaoImpl<Seguridadconcepto>, SeguridadconceptoDao 
{
        private IServiceProvider servicioProveedor;


	    public SeguridadconceptoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "seguridadconcepto") {
                servicioProveedor = _servicioProveedor;
	    }

        public Seguridadconcepto obtenerPorId(SeguridadconceptoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
