using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsItemDaoImpl : GenericoDaoImpl<PsItem>, PsItemDao
    {
        private IServiceProvider servicioProveedor;


        public PsItemDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psitem")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsItem obtenerPorId(PsItemPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsItem obtenerPorId(String pIdItem)
        {
            return obtenerPorId(new PsItemPk(pIdItem));
        }

        public PsItem coreInsertar(UsuarioActual usuarioActual, PsItem bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsItem coreActualizar(UsuarioActual usuarioActual, PsItem bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }
            bean.Estado = estado;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdItem)
        {
            coreEliminar(new PsItemPk(pIdItem));
        }

        public void coreEliminar(PsItemPk id)
        {
            PsItem bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsItem coreAnular(UsuarioActual usuarioActual, PsItemPk id)
        {
            PsItem bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsItem coreAnular(UsuarioActual usuarioActual, String pIdItem)
        {
            return coreAnular(usuarioActual, new PsItemPk(pIdItem));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroItem filtro)
        {
            Int32 contador = 0;

            List<DtoItem> lstRetorno = new List<DtoItem>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }



            parametros.Add(new ParametroPersistenciaGenerico("p_id_item", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            contador = this.contar("psitem.filtroContar", parametros);

            paginacion.CantidadRegistrosDevolver = filtro.cantidad.HasValue ? filtro.cantidad.Value : 10;

            _reader = this.listarConPaginacion(paginacion, parametros, "psitem.filtroPaginacion");

            while (_reader.Read())
            {
                DtoItem bean = new DtoItem();

                if (!_reader.IsDBNull(0))
                    bean.idItem = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomItem = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nomTipo = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.nomClase = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nomGrupo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.estado = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.nomUnidad = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.coeficiente = _reader.GetDecimal(7);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }


        public string obtenercadena(string pNombre)
        {
            IQueryable<PsItem> query = this.getCriteria();
            query = query.Where(p => p.Nombre == pNombre);

            List<PsItem> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }
        public string obtenerCadenaxCodigo(string pIdItem)
        {
            IQueryable<PsItem> query = this.getCriteria();
            query = query.Where(p => p.IdItem == pIdItem);

            List<PsItem> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdItem)
        {
            IQueryable<PsItem> query = this.getCriteria();
            query = query.Where(p => p.IdItem == pIdItem);


            List<PsItem> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdItem;
            return null;
        }

        public string obtenerDescripcion(string pIdItem)
        {
            IQueryable<PsItem> query = this.getCriteria();
            query = query.Where(p => p.IdItem == pIdItem);

            List<PsItem> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }
    }
}
