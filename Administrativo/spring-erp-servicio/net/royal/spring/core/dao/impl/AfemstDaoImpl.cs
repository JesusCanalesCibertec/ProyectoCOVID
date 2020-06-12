using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.dao.impl
{
    public class AfemstDaoImpl : GenericoDaoImpl<Afemst>, AfemstDao
    {
        private IServiceProvider servicioProveedor;


        public AfemstDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "afemst")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<Afemst> listar(FiltroAfe filtro)
        {
            IQueryable<Afemst> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Companyowner))
                query = query.Where(p => p.Companyowner == filtro.Companyowner);
            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Afe == filtro.Codigo);
            if (!UString.estaVacio(filtro.Nombre))
                query = query.Where(p => p.Localname.Contains(filtro.Nombre));
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Status == filtro.Estado);

            if (!UString.estaVacio(filtro.AtributoOrdenar))
                query = query.OrderBy(p => p.Localname);     /** order **/
            else
                query = query.OrderBy(p => filtro.AtributoOrdenar);

            return query.ToList();
        }

        public ParametroPaginacionGenerico listarPaginacion(DtoTabla filtro, ParametroPaginacionGenerico paginacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<Afemst> lst = new List<Afemst>();

            if (UString.estaVacio(filtro.Codigo))
                filtro.Codigo = null;
            if (UString.estaVacio(filtro.Descripcion))
                filtro.Descripcion = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Descripcion));


            Int32 contador = this.contar("afemst.paginacionContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "afemst.paginacionListar");

            while (_reader.Read())
            {
                Afemst bean = new Afemst();
                if (!_reader.IsDBNull(0))
                    bean.Afe = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Companyowner = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Localname = _reader.GetString(2);


                lst.Add(bean);
            }
            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            return paginacion;
        }

        public Afemst obtenerPorId(AfemstPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
    }
}
