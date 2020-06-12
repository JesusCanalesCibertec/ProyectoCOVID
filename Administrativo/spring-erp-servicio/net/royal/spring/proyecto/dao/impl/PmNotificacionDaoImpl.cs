using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmNotificacionDaoImpl : GenericoDaoImpl<PmNotificacion>, PmNotificacionDao
    {
        private IServiceProvider servicioProveedor;

        public PmNotificacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmnotificacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public int? generarIdNotificacion()
        {
            IQueryable<PmNotificacion> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdNotificacion).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<PmNotificacion> listarPorIdTransaccion(int? idTransaccion)
        {
            IQueryable<PmNotificacion> criteria = this.getCriteria();
            criteria = criteria.Where(x => x.ProcesoIdTransaccion == idTransaccion);
            return criteria.ToList();
        }
    }
}
