using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrCapacitacionFoliosDaoImpl : GenericoDaoImpl<HrCapacitacionFolios>, HrCapacitacionFoliosDao
    {
        private IServiceProvider servicioProveedor;


        public HrCapacitacionFoliosDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrcapacitacionfolios")
        {
            servicioProveedor = _servicioProveedor;
        }

        public Int32 generarCodigo(HrCapacitacionPk pk)
        {
            IQueryable<HrCapacitacionFolios> query = this.getCriteria();
            query = query.Where(x => x.Companyowner == pk.Companyowner);
            query = query.Where(x => x.Capacitacion == pk.Capacitacion);
            Int32? contador = query.Select(p => p.Secuencia).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<HrCapacitacionFolios> listarPorCapacitacion(HrCapacitacionPk pk)
        {
            IQueryable<HrCapacitacionFolios> query = this.getCriteria();
            query = query.Where(x => x.Companyowner == pk.Companyowner);
            query = query.Where(x => x.Capacitacion == pk.Capacitacion);
            return query.ToList();
        }
    }
}
