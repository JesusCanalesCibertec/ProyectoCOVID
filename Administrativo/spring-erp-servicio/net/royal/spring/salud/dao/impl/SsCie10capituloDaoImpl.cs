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

namespace net.royal.spring.salud.dao.impl {
    public class SsCie10capituloDaoImpl : GenericoDaoImpl<SsCie10capitulo>, SsCie10capituloDao {
        private IServiceProvider servicioProveedor;


        public SsCie10capituloDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "sscie10capitulo") {
            servicioProveedor = _servicioProveedor;
        }

        public List<SsCie10capitulo> listarTodosActivos() {
            IQueryable<SsCie10capitulo> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
                  
            return query.ToList(); ;
        }
    }
}
