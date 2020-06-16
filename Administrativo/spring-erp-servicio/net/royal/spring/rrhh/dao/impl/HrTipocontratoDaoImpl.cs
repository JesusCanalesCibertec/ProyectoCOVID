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
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core;

namespace net.royal.spring.rrhh.dao.impl
{
public class HrTipocontratoDaoImpl : GenericoDaoImpl<HrTipocontrato>, HrTipocontratoDao 
{
        private IServiceProvider servicioProveedor;


	public HrTipocontratoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "hrtipocontrato") {
            servicioProveedor = _servicioProveedor;
	}

        public List<HrTipocontrato> listar(DtoFiltro filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            IQueryable<HrTipocontrato> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Tipocontrato == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Descripcion.Contains(filtro.Nombre));   /** like **/
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);

            if (UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Descripcion);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public List<HrTipocontrato> listarActivos()
        {
            IQueryable<HrTipocontrato> query = this.getCriteria();

            query = query.Where(p => p.Estado == "A");
            return query.ToList();
        }

        public HrTipocontrato obtenerPorId(HrTipocontratoPk hrTipocontratoPk)
        {
            return this.obtenerPorId(hrTipocontratoPk.obtenerArreglo());
        }
    }
}
