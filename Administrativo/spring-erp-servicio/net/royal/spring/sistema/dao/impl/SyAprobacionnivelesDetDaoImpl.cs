using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using System.Collections.Generic;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyAprobacionnivelesDetDaoImpl : GenericoDaoImpl<SyAprobacionnivelesDet>, SyAprobacionnivelesDetDao 
    {
        private IServiceProvider servicioProveedor;


	    public SyAprobacionnivelesDetDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "syaprobacionnivelesdet") {
                servicioProveedor = _servicioProveedor;
	    }

        public List<SyAprobacionnivelesDet> listarPorCodigoNivel(int codigo, int nivel, string compania)
        {
            IQueryable<SyAprobacionnivelesDet> query = this.getCriteria();
            query = query.Where(p => p.Codigo == codigo);
            query = query.Where(p => p.Nivel == nivel);
            query = query.Where(p => p.CompanyOwner == compania);
            query = query.OrderBy(p => p.Nivel);
            return query.ToList();
        }


        public List<SyAprobacionnivelesDet> listarPorNivelAprobacion(SyAprobacionnivelesPk syAprobacionnivelesPk)
        {
            IQueryable<SyAprobacionnivelesDet> query = this.getCriteria();

            query = query.Where(p => p.Codigo == syAprobacionnivelesPk.Codigo);
            query = query.Where(p => p.CompanyOwner == syAprobacionnivelesPk.CompanyOwner);
            query = query.OrderBy(p => p.Nivel);
            return query.ToList();
        }

        public List<SyAprobacionnivelesDet> obtenerListaCorreoPorProceso(int? procesoAprobar, int? nivelAprobar)
        {
            IQueryable<SyAprobacionnivelesDet> query = this.getCriteria();

            query = query.Where(p => p.Codigo == procesoAprobar);
            query = query.Where(p => p.Nivel == nivelAprobar);
            query = query.Where(p => p.Correoelectronico != null);


            //query = query.Select(p => place.LocalizedName ??
            //                place.EnglishName).FirstOrDefault();
            //Restrictions.isNotNull("correoelectronico"),
            //        Restrictions.ne("correoelectronico", "")

            return query.ToList();

        }

        public SyAprobacionnivelesDet obtenerPorId(SyAprobacionnivelesDetPk pk)
        {
            return obtenerPorId(pk.obtenerArreglo());
        }

        public List<SyAprobacionnivelesDet> listarPorCodigo(int codigo, string compania)
        {
            IQueryable<SyAprobacionnivelesDet> query = this.getCriteria();
            query = query.Where(p => p.Codigo == codigo);
            query = query.Where(p => p.CompanyOwner == compania);
            query = query.OrderBy(p => p.Nivel);
            return query.ToList();
        }

    }
}
