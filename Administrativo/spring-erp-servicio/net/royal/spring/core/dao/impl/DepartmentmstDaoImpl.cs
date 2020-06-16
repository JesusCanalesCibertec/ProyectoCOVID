using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.core.dao.impl
{
    public class DepartmentmstDaoImpl : GenericoDaoImpl<Departmentmst>, DepartmentmstDao
    {
        private IServiceProvider servicioProveedor;


        public DepartmentmstDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "departmentmst")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<Departmentmst> listar(DtoFiltro filtro)
        {
            IQueryable<Departmentmst> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Status == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Description);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public List<Departmentmst> listarBusqueda(DtoFiltro filtro)
        {
            IQueryable<Departmentmst> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
            {
                query = query.Where(p => p.Department == filtro.Codigo);
            }

            if (!UString.estaVacio(filtro.Nombre))
            {
                query = query.Where(p => p.Description.Contains(filtro.Nombre));
            }

            if (!UString.estaVacio(filtro.Estado))
            {
                query = query.Where(p => p.Status == filtro.Estado);
            }

            if (filtro.NotIn != null)
            {
                foreach (var item in filtro.NotIn)
                {
                    query = query.Where(p => p.Description != item.Codigo);
                }

            }

            List<Departmentmst> lstlista = query.ToList();

            return lstlista;
        }

        public Departmentmst obtenerPorId(DepartmentmstPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
