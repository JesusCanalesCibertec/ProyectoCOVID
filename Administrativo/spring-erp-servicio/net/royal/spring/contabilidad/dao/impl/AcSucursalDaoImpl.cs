using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.contabilidad.dao;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;

namespace net.royal.spring.contabilidad.dao.impl
{
    public class AcSucursalDaoImpl : GenericoDaoImpl<AcSucursal>, AcSucursalDao
    {
        private IServiceProvider servicioProveedor;


        public AcSucursalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "acsucursal")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<AcSucursal> listar(FiltroAcSucursal filtro)
        {
            IQueryable<AcSucursal> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Sucursal == filtro.Codigo);

            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcionlocal.Contains(filtro.Nombre));

            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcionlocal);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public List<AcSucursal> listarActivoOrdenadoPorCodigo()
        {
            FiltroAcSucursal filtro = new FiltroAcSucursal();
            filtro.Estado = "A";
            filtro.AtributoOrdenar = "Sucursal";
            return listar(filtro);
        }

        public List<AcSucursal> listarActivoOrdenadoPorNombre()
        {
            FiltroAcSucursal filtro = new FiltroAcSucursal();
            filtro.Estado = "A";
            filtro.AtributoOrdenar = "Descripcionlocal";
            return listar(filtro);
        }

        public AcSucursal obtenerPorId(AcSucursalPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
