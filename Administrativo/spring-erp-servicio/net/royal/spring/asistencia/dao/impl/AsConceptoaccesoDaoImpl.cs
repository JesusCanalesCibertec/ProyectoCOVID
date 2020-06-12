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
    public class AsConceptoaccesoDaoImpl : GenericoDaoImpl<AsConceptoacceso>, AsConceptoaccesoDao
    {
        private IServiceProvider servicioProveedor;


        public AsConceptoaccesoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asconceptoacceso")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<AsConceptoacceso> ListarActivos()
        {
            //List<AsConceptoacceso> lst = new List<AsConceptoacceso>();
            IQueryable<AsConceptoacceso> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            //var lstTemp = query.ToList();

            // List<AsConceptoacceso> lst = new List<AsConceptoacceso>();
            //lst.Add(new AsConceptoacceso(CA0Q, Licencia Por Paternidad));
            return query.ToList();
        }

        public string obtenerNombre(AsConceptoaccesoPk pk)
        {
            AsConceptoacceso bean = this.obtenerPorId(pk);
            if (bean == null)
                return null;
            return bean.Descripcionlocal;
        }

        public AsConceptoacceso obtenerPorId(AsConceptoaccesoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public List<AsConceptoacceso> ListarActivosOtros(bool web)
        {
            IQueryable<AsConceptoacceso> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            //query = query.Where(p => p.Flagotrospermisos == "S");
            if (!web)
            {
                query = query.Where(p => p.FlagWeb == "S");
            }
            query.OrderBy(x => x.Descripcionlocal);
            return query.ToList();
        }
    }
}
