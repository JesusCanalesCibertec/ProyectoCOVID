using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.dominio;

namespace net.royal.spring.planilla.dao.impl
{
    public class PrTipoplanillaDaoImpl : GenericoDaoImpl<PrTipoplanilla>, PrTipoplanillaDao 
    {
        private IServiceProvider servicioProveedor;


	    public PrTipoplanillaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "prtipoplanilla") {
                servicioProveedor = _servicioProveedor;
	    }

            public PrTipoplanilla obtenerPorId(PrTipoplanillaPk pk)
            {
                return base.obtenerPorId(pk.obtenerArreglo());
            }
        }
}
