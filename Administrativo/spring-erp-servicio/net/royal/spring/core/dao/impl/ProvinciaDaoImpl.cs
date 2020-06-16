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
public class ProvinciaDaoImpl : GenericoDaoImpl<Provincia>, ProvinciaDao 
{
        private IServiceProvider servicioProveedor;


	    public ProvinciaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "provincia") {
                servicioProveedor = _servicioProveedor;
	    }

        public List<Provincia> listar(FiltroProvincia filtro)
        {
            IQueryable<Provincia> query = this.getCriteria();

            if (!UString.estaVacio(filtro.IdDepartamento)) ;
                query = query.Where(p => p.Departamento == filtro.IdDepartamento);
            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Provincia == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcion);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public List<Provincia> listar(string idDepartamento)
        {
            IQueryable<Provincia> query = this.getCriteria();
            if (!UString.estaVacio(idDepartamento))
                query = query.Where(p => p.Departamento == idDepartamento);
            query = query.OrderBy(p => p.Descripcion);
            return query.ToList();
        }

        public Provincia obtenerPorId(ProvinciaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
