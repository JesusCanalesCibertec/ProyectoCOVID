using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net.royal.spring.minedu.dao.impl
{

    public class MpContratacionadendaentregableDaoImpl : GenericoDaoImpl<MpContratacionadendaentregable>, MpContratacionadendaentregableDao
    {

        private IServiceProvider servicioProveedor;

        public MpContratacionadendaentregableDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "mpContratacionadendaentregable")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {

            filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<MpContratacionadendaentregable> lstResultado = new List<MpContratacionadendaentregable>();

            Int32 contador = 0;


            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            contador = this.contar("mpContratacionadendaentregable.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "mpContratacionadendaentregable.filtroPaginacion");

            while (_reader.Read())
            {
                MpContratacionadendaentregable bean = new MpContratacionadendaentregable();

                if (!_reader.IsDBNull(0))
                    bean.IdCodigo = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Fechainicio = _reader.GetDateTime(2);

                if (!_reader.IsDBNull(3))
                    bean.Fechafin = _reader.GetDateTime(3);

                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);

                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public MpContratacionadendaentregable registrar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean)
        {
            //bean.IdContratacionadendaentregable = this.generarCodigo();
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        //public int generarCodigo()
        //{
        //    IQueryable<MpContratacionadendaentregable> query = this.getCriteria();
        //    Int32? contador = query.Select(p => p.IdContratacionadendaentregable).DefaultIfEmpty(0).Max();
        //    contador++;
        //    return contador.Value;
        //}

        public MpContratacionadendaentregable actualizar(UsuarioActual usuarioActual, MpContratacionadendaentregable bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<MpContratacionadendaentregable> listado(DtoTabla filtro)
        {

            return null;
        }

        public List<MpContratacionadendaentregable> listado(int? pIdContratacion)
        {
            IQueryable<MpContratacionadendaentregable> query = this.getCriteria();
            query = query.Where(p => p.IdContratacion == pIdContratacion);
            List<MpContratacionadendaentregable> lst = query.ToList();

            if (lst.Count > 0)
                return lst;
            return null;
        }


        public void eliminarDetalle(int? pIdContratacion)
        {
            var list = this.listado(pIdContratacion);

            foreach (var item in list)
            {
                this.eliminar(item);
            }


        }

    }
}
