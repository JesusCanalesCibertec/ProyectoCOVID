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
    public class SsCie10grupoDaoImpl : GenericoDaoImpl<SsCie10grupo>, SsCie10grupoDao
    {
        private IServiceProvider servicioProveedor;


        public SsCie10grupoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "sscie10grupo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<SsCie10grupo> listarPorCapitulo(string codigo)
        {
            if (UString.estaVacio(codigo))
            {
                return new List<SsCie10grupo>();
            }
            IQueryable<SsCie10grupo> query = this.getCriteria();
            query = query.Where(p => p.IdCapitulo == codigo);

            List<SsCie10grupo> lst = query.ToList();
            
                
            return lst;
        }
    }
}
