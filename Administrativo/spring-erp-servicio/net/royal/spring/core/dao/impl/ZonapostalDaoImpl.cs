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
public class ZonapostalDaoImpl : GenericoDaoImpl<Zonapostal>, ZonapostalDao 
{
        private IServiceProvider servicioProveedor;


	public ZonapostalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "zonapostal") {
            servicioProveedor = _servicioProveedor;
	}

        public List<Zonapostal> listar(FiltroZonaPostal filtro)
        {
            IQueryable<Zonapostal> query = this.getCriteria();

            if (!UString.estaVacio(filtro.IdDepartamento)) ;
                query = query.Where(p => p.Departamento == filtro.IdDepartamento);
            if (!UString.estaVacio(filtro.IdProvincia)) ;
                query = query.Where(p => p.Provincia == filtro.IdProvincia);
            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Codigopostal == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            /*
            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcioncorta);     
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);
            */
            return query.ToList();
        }

        public List<Zonapostal> listar(string idDepartamento, string idProvincia)
        {
            IQueryable<Zonapostal> query = this.getCriteria();
            if (!UString.estaVacio(idDepartamento))
                query = query.Where(p => p.Departamento == idDepartamento);
            if (!UString.estaVacio(idProvincia))
                query = query.Where(p => p.Provincia == idProvincia);
            query = query.OrderBy(p => p.Descripcion);
            return query.ToList();
        }

        public Zonapostal obtenerPorId(ZonapostalPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
