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
using net.royal.spring.proyecto.dominio.filtro;
using net.royal.spring.proyecto.dominio.dto;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmPortafolioDaoImpl : GenericoDaoImpl<PmPortafolio>, PmPortafolioDao
    {
        private IServiceProvider servicioProveedor;


        public PmPortafolioDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmportafolio")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<PmPortafolio> listarActivos()
        {
            IQueryable<PmPortafolio> cri = getCriteria();
            cri = cri.Where(x => x.Estado == "A");
            return cri.ToList();
        }
    }
}
