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
    public class AcSucursalgrupoDaoImpl : GenericoDaoImpl<AcSucursalgrupo>, AcSucursalgrupoDao
    {
        private IServiceProvider servicioProveedor;

        public AcSucursalgrupoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "acsucursalgrupo")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<AcSucursalgrupo> listar(FiltroAcSucursalGrupo filtro)
        {
            IQueryable<AcSucursalgrupo> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Sucursalgrupo == filtro.Codigo);

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

        public List<AcSucursalgrupo> listarActivoOrdenadoPorCodigo()
        {
            FiltroAcSucursalGrupo filtro = new FiltroAcSucursalGrupo();
            filtro.Estado = "A";
            filtro.AtributoOrdenar = "Sucursalgrupo";
            return listar(filtro);
        }

        public List<AcSucursalgrupo> listarActivoOrdenadoPorNombre()
        {
            FiltroAcSucursalGrupo filtro = new FiltroAcSucursalGrupo();
            filtro.Estado = "A";
            filtro.AtributoOrdenar = "Descripcionlocal";
            return listar(filtro);
        }

        public AcSucursalgrupo obtenerPorId(AcSucursalgrupoPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}