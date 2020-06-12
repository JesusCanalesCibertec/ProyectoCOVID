using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpProcesoDaoImpl : GenericoDaoImpl<BpProceso>, BpProcesoDao
    {
        private IServiceProvider servicioProveedor;

        public BpProcesoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpproceso")
        {
            servicioProveedor = _servicioProveedor;
        }

        public BpProceso obtenerActual(string idProceso)
        {
            IQueryable<BpProceso> query = this.getCriteria();
            query = query.Where(x => x.IdProceso == idProceso);
            query = query.Where(x => x.FlagVersionActual == "S");

            List<BpProceso> lst = query.ToList();
            if (lst == null)
                return null;
            if (lst.Count == 1)
                return lst[0];
            return null;
        }
    }
}
