using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using System.Collections.Generic;

namespace net.royal.spring.core.dao.impl
{
public class MaUnidadnegocioDaoImpl : GenericoDaoImpl<MaUnidadnegocio>, MaUnidadnegocioDao 
{
        private IServiceProvider servicioProveedor;


	public MaUnidadnegocioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "maunidadnegocio") {
            servicioProveedor = _servicioProveedor;
	}

        public List<MaUnidadnegocio> listarActivos()
        {
            IQueryable<MaUnidadnegocio> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            return query.ToList();
        }

        public MaUnidadnegocio obtenerPorId(MaUnidadnegocioPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
