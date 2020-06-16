using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.dominio;
using System.Collections;
using System.Collections.Generic;

namespace net.royal.spring.planilla.dao.impl
{
    public class PrCalendarioferiadosDaoImpl : GenericoDaoImpl<PrCalendarioferiados>, PrCalendarioferiadosDao
    {
        private IServiceProvider servicioProveedor;


        public PrCalendarioferiadosDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "prcalendarioferiados")
        {
            servicioProveedor = _servicioProveedor;
        }

        public IList<PrCalendarioferiados> listarActivos()
        {
            IQueryable<PrCalendarioferiados> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            List<PrCalendarioferiados> lstlista = query.ToList();
            return lstlista;
        }

    }
}
