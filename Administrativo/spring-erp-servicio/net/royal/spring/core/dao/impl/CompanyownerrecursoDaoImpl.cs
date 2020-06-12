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
public class CompanyownerrecursoDaoImpl : GenericoDaoImpl<Companyownerrecurso>, CompanyownerrecursoDao 
{
        private IServiceProvider servicioProveedor;


	public CompanyownerrecursoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "companyownerrecurso") {
            servicioProveedor = _servicioProveedor;
	}

        public void eliminarPorTipoRecurso(string tipoRecurso)
        {
            foreach (Companyownerrecurso recurso in listarPorRecurso(tipoRecurso))
            {
                eliminar(recurso);
            }
        }

        public IList<Companyownerrecurso> listarPorRecurso(string tipoRecurso)
        {
            IQueryable<Companyownerrecurso> query = this.getCriteria();

            query = query.Where(p => p.Tiporecurso.Trim() == tipoRecurso.Trim());

            return query.ToList(); 
        }

        public Companyownerrecurso obtenerPorId(CompanyownerrecursoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
