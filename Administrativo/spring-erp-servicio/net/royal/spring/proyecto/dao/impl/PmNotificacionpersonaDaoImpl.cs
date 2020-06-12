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
    public class PmNotificacionpersonaDaoImpl : GenericoDaoImpl<PmNotificacionpersona>, PmNotificacionpersonaDao
    {
        private IServiceProvider servicioProveedor;

        public PmNotificacionpersonaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmnotificacionpersona")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<PmNotificacionpersona> listarPorNotificacion(PmNotificacionPk pmNotificacionPk)
        {
            IQueryable<PmNotificacionpersona> criteria = this.getCriteria();
            criteria = criteria.Where(x => x.IdNotificacion == pmNotificacionPk.IdNotificacion);
            return criteria.ToList();
        }



        public List<PmNotificacionpersona> listarPorPersona(int? idPersona)
        {
        
                IQueryable<PmNotificacionpersona> criteria = this.getCriteria();
                criteria = criteria.Where(x => x.IdPersona == idPersona.Value);
                criteria = criteria.Where(x => x.Estado == "A");
                return criteria.ToList();
            
  
           
        }
    }
}
