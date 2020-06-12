using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.dao.impl
{
public class DepartamentoDaoImpl : GenericoDaoImpl<Departamento>, DepartamentoDao 
{
        private IServiceProvider servicioProveedor;


	public DepartamentoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "departamento") {
            servicioProveedor = _servicioProveedor;
	}

        public List<Departamento> listar(FiltroDepartamento filtro)
        {
            IQueryable<Departamento> query = this.getCriteria();

            if (!UString.estaVacio(filtro.IdPais)) ;
            query = query.Where(p => p.Pais == filtro.IdPais);
            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Departamento == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcioncorta.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcioncorta);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public Departamento obtenerPorId(DepartamentoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
