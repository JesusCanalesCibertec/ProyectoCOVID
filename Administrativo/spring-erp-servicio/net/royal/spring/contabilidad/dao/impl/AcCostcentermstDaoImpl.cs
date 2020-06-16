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
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.contabilidad.dao.impl
{
    public class AcCostcentermstDaoImpl : GenericoDaoImpl<AcCostcentermst>, AcCostcentermstDao
    {
        private IServiceProvider servicioProveedor;

        public AcCostcentermstDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "accostcentermst")
        {
            servicioProveedor = _servicioProveedor;
        }
        public AcCostcentermst obtenerPorId(AcCostcentermstPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }
        public string empleadoEsVendedorEnCentroCosto(int? idEmpleado)
        {
            IQueryable<AcCostcentermst> query = this.getCriteria();
            query = query.Where(p => p.Vendor == idEmpleado);
            List<AcCostcentermst> lst = query.ToList();
            if (lst.Count > 0)
                return "S";
            return "N";
        }

        public List<AcCostcentermst> listar(FiltroAcCostcentermst filtro)
        {
            IQueryable<AcCostcentermst> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
                query = query.Where(p => p.Costcenter == filtro.Codigo);
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
        public string obtenerNombrePorPk(AcCostcentermstPk acCostcentermstPk)
        {
            AcCostcentermst personamast = obtenerPorId(acCostcentermstPk);
            if (personamast == null)
                return null;
            return personamast.Localname;
        }
        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionAcCostcentermst filtro)
        {

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<AcCostcentermst> lstDatos = new List<AcCostcentermst>();

            if (UString.estaVacio(filtro.Codigo))
                filtro.Codigo = null;
            if (UString.estaVacio(filtro.Nombre))
                filtro.Nombre = null;
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_costcenter", ConstanteUtil.TipoDato.String, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            Int32 contador = this.contar("accostcentermst.selectorContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "accostcentermst.selectorListar");

            while (_reader.Read())
            {
                AcCostcentermst bean = new AcCostcentermst();
                if (!_reader.IsDBNull(0))
                    bean.Costcenter = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.Localname = _reader.GetString(1);
				if (!_reader.IsDBNull(2))
                    bean.Status = _reader.GetString(2);
                lstDatos.Add(bean);
            }

            this.dispose();

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstDatos;

            return paginacion;
        }

        public List<AcCostcentermst> listarBusqueda(DtoFiltro filtro)
        {
            IQueryable<AcCostcentermst> query = this.getCriteria();

            if (!UString.estaVacio(filtro.Codigo))
            {
                query = query.Where(p => p.Costcenter == filtro.Codigo);
            }

            if (!UString.estaVacio(filtro.Nombre))
            {
                query = query.Where(p => p.Localname.Contains(filtro.Nombre));
            }

            if (!UString.estaVacio(filtro.Estado))
            {
                query = query.Where(p => p.Status == filtro.Estado);
            }

            if (filtro.NotIn != null)
            {
                foreach (var item in filtro.NotIn)
                {
                    query = query.Where(p => p.Costcenter != item.Codigo);
                }

            }

            List<AcCostcentermst> lstlista = query.ToList();

            return lstlista;
        }
    }
}
