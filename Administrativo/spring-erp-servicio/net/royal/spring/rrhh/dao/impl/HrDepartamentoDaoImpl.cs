using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.sistema.dominio;
using net.royal.spring.sistema.dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrDepartamentoDaoImpl : GenericoDaoImpl<HrDepartamento>, HrDepartamentoDao
    {
        public HrDepartamentoDaoImpl(GenericoDbContext context) : base(context, "hrdepartamento")
        {
        }

        public List<HrDepartamento> listar(DtoFiltro filtro)
        {

            IQueryable<HrDepartamento> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
            {
                query = query.Where(p => p.Departamento == filtro.Codigo);
            }

            if (!UString.estaVacio(filtro.Nombre))
            {
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));
            }

            if (!UString.estaVacio(filtro.Estado))
            {
                query = query.Where(p => p.Estado == filtro.Estado);
            }

            if (filtro.NotIn != null)
            {
                foreach (var item in filtro.NotIn)
                {
                    query = query.Where(p => p.Departamento != item.Codigo);
                }

            }


            List<HrDepartamento> lstlista = query.ToList();

            return lstlista;

        }

        public List<HrDepartamento> listarActivos()
        {
            IQueryable<HrDepartamento> query = this.getCriteria();
            query = query.Where(p => p.Estado == "A");

            List<HrDepartamento> lstlista = query.ToList();

            return lstlista;
        }
    }
}
