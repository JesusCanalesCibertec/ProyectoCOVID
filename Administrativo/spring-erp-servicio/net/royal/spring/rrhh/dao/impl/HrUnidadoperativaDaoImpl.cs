using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrUnidadoperativaDaoImpl : GenericoDaoImpl<HrUnidadoperativa>, HrUnidadoperativaDao
    {
        private IServiceProvider servicioProveedor;


        public HrUnidadoperativaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrunidadoperativa")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<HrUnidadoperativa> listarActivos()
        {
            IQueryable<HrUnidadoperativa> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");
            List<HrUnidadoperativa> lstlista = query.ToList();

            return lstlista;
        }

        public List<HrUnidadoperativa> listarBusqueda(DtoFiltro filtro)
        {
            IQueryable<HrUnidadoperativa> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
            {
                query = query.Where(p => p.Unidadoperativa == filtro.Codigo);
            }

            if (!UString.estaVacio(filtro.Nombre))
            {
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));
            }

            if (!UString.estaVacio(filtro.Estado))
            {
                query = query.Where(p => p.Estado == filtro.Estado);
            }

            if (!UInteger.esCeroOrNulo(filtro.jefe))
            {
                query = query.Where(p => p.Responsable == filtro.jefe);
            }

            if (filtro.NotIn != null)
            {
                foreach (var item in filtro.NotIn)
                {
                    query = query.Where(p => p.Unidadoperativa != item.Codigo);
                }

            }

            List<HrUnidadoperativa> lstlista = query.ToList();

            return lstlista;
        }

        public HrUnidadoperativa obtenerPorId(HrUnidadoperativaPk hrUnidadoperativaPk)
        {
            return this.obtenerPorId(hrUnidadoperativaPk.obtenerArreglo());
        }
    }
}
