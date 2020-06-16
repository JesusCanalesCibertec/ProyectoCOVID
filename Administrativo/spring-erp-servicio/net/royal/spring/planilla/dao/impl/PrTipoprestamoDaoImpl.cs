using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.dominio;
using System.Collections.Generic;

namespace net.royal.spring.planilla.dao.impl
{
    public class PrTipoprestamoDaoImpl : GenericoDaoImpl<PrTipoprestamo>, PrTipoprestamoDao
    {
        private IServiceProvider servicioProveedor;


        public PrTipoprestamoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "prtipoprestamo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<PrTipoprestamo> listarActivos()
        {
            IQueryable<PrTipoprestamo> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            List<PrTipoprestamo> lstlista = query.ToList();

            return lstlista;
        }

        public List<PrTipoprestamo> listarSoloWeb()
        {
            IQueryable<PrTipoprestamo> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            query = query.Where(p => p.FlagVerWeb == "S");

            List<PrTipoprestamo> lstlista = query.ToList();

            return lstlista;
        }

        public PrTipoprestamo obtenerPorId(PrTipoprestamoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
