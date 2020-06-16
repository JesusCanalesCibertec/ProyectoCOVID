using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.kpi.dao;
using net.royal.spring.kpi.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.salud.dominio;

namespace net.royal.spring.salud.dao.impl
{
    public class SsCie10subgrupoDaoImpl : GenericoDaoImpl<SsCie10subgrupo>, SsCie10subgrupoDao
    {
        private IServiceProvider servicioProveedor;


        public SsCie10subgrupoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "sscie10subgrupo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<SsCie10subgrupo> listarPorGrupo(string codigo)
        {
            if (UString.estaVacio(codigo))
            {
                return new List<SsCie10subgrupo>();
            }
            IQueryable<SsCie10subgrupo> query = this.getCriteria();
            query = query.Where(p => p.IdGrupo == codigo);

            List<SsCie10subgrupo> lst = query.ToList();
            
                
            return lst;
        }
    }
}
