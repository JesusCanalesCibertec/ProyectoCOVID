using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.dominio;
using System.Collections.Generic;

namespace net.royal.spring.asistencia.dao.impl
{
    public class AsConceptoaccesoReglaDaoImpl : GenericoDaoImpl<AsConceptoaccesoRegla>, AsConceptoaccesoReglaDao
    {
        private IServiceProvider servicioProveedor;


        public AsConceptoaccesoReglaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asconceptoaccesoregla")
        {
            servicioProveedor = _servicioProveedor;
        }
        
        public AsConceptoaccesoRegla obtenerPorId(AsConceptoaccesoReglaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public List<AsConceptoaccesoRegla> ListarActivos(String conceptoacceso)
        {
            IQueryable<AsConceptoaccesoRegla> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            query = query.Where(p => p.Conceptoacceso == conceptoacceso);
            //query.OrderBy(x => x.Descripcionlocal);
            return query.ToList();
        }
    }
}
