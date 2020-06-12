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
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.core.dao.impl
{
    public class PmPublicacionDaoImpl : GenericoDaoImpl<PmPublicacion>, PmPublicacionDao
    {
        private IServiceProvider servicioProveedor;

        public PmPublicacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmpublicacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public int? generarId(string idAplicacion)
        {
            IQueryable<PmPublicacion> query = this.getCriteria();
            query = query.Where(p => p.IdAplicacion == idAplicacion);
            int? var = query.Select(p => p.IdPubicacion).DefaultIfEmpty(0).Max();
            return var.HasValue ? var.Value + 1 : 1;
        }

        public ParametroPaginacionGenerico listarBusqueda(FiltroPaginacionPublicaciones filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPublicacion> lst = new List<DtoPublicacion>();


            if (UString.estaVacio(filtro.aplicacion))
                filtro.aplicacion = null;
            if (UString.estaVacio(filtro.estado))
                filtro.estado = null;
            if (UString.estaVacio(filtro.publicacion))
                filtro.publicacion = null;

            if (!filtro.desde.HasValue)
                filtro.desde = new DateTime(2000, 01, 01);

            if (!filtro.hasta.HasValue)
                filtro.hasta = new DateTime(2300, 01, 01);

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.aplicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_publicacion", ConstanteUtil.TipoDato.String, filtro.publicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_desde", ConstanteUtil.TipoDato.Date, filtro.desde));
            parametros.Add(new ParametroPersistenciaGenerico("p_hasta", ConstanteUtil.TipoDato.Date, filtro.hasta));

            Int32 contador = this.contar("pmpublicacion.listarContar", parametros);

            _reader = this.listarConPaginacion(filtro.paginacion, parametros, "pmpublicacion.listarPaginacion");

            while (_reader.Read())
            {
                DtoPublicacion bean = new DtoPublicacion();
                if (!_reader.IsDBNull(0))
                    bean.idAplicacion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.idPublicacion = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.aplicacionDescripcion = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.tipoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.seguridad = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.estado = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.fechaInicio = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.fechaFin = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.descripcion = _reader.GetString(8);

                lst.Add(bean);
            }

            this.dispose();
            filtro.paginacion.RegistroEncontrados = contador;
            filtro.paginacion.ListaResultado = lst;

            return filtro.paginacion;
        }

        public IList<DtoTabla> listarIndicadores(UsuarioActual usuarioActual)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTabla> lst = new List<DtoTabla>();

            parametros.Add(new ParametroPersistenciaGenerico("p_empleado", ConstanteUtil.TipoDato.Integer, usuarioActual.PersonaId));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, usuarioActual.CompaniaSocioCodigo));

            _reader = this.listarPorQuery("pmpublicacion.listarIndicadores", parametros);

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    bean.valorNumerico = _reader.GetInt32(0);
                lst.Add(bean);
            }

            this.dispose();

            return lst;
        }

        public IList<DtoTabla> listarPublicacionesDashboard()
        {
            List<DtoTabla> lista = new List<DtoTabla>();

            IQueryable<PmPublicacion> query = getCriteria();
            query = query.Where(p => p.Estado == "ACTI");

            var lst = query.ToList();

            foreach (var item in lst)
            {
                DtoTabla d = new DtoTabla();
                d.Descripcion = UByte.convertirString(item.Publicacion);
                d.Explicacion = UString.estaVacio(item.Tamanio) ? "ui-g-12 ui-md-12 ui-lg-12" : item.Tamanio;
                d.Porcentaje = Convert.ToDecimal(item.Altura);
                lista.Add(d);
            }
            return lista;
        }
    }
}
