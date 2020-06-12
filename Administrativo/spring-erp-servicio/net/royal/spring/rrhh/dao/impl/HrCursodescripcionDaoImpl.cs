using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{
public class HrCursodescripcionDaoImpl : GenericoDaoImpl<HrCursodescripcion>, HrCursodescripcionDao
{
        private IServiceProvider servicioProveedor;


        public HrCursodescripcionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"hrcursodescripcion")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public HrCursodescripcion obtenerPorId(HrCursodescripcionPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCursodescripcion obtenerPorId(Nullable<Int32> pCurso)
        {
            return obtenerPorId(new HrCursodescripcionPk( pCurso));
        }

        public int generarCodigo()
        {
            IQueryable<HrCursodescripcion> query = this.getCriteria();

            List<HrCursodescripcion> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.Curso));
            }

            return var + 1;
        }

        public string obtenerDescripcion(string nombre)
        {
            IQueryable<HrCursodescripcion> query = this.getCriteria();
            query = query.Where(p => p.Descripcion == nombre);
            
            List<HrCursodescripcion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Descripcion;
            return null;
        }

        public HrCursodescripcion coreInsertar(UsuarioActual usuarioActual, HrCursodescripcion bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCursodescripcion coreActualizar(UsuarioActual usuarioActual, HrCursodescripcion bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }

            bean.Estado = estado;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pCurso)
        {
            coreEliminar(new HrCursodescripcionPk( pCurso));
        }

        public void coreEliminar(HrCursodescripcionPk id)
        {
            HrCursodescripcion bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public HrCursodescripcion coreAnular(UsuarioActual usuarioActual,HrCursodescripcionPk id)
        {
            HrCursodescripcion bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public HrCursodescripcion coreAnular(UsuarioActual usuarioActual,Nullable<Int32> pCurso)
        {
            return coreAnular(usuarioActual,new HrCursodescripcionPk( pCurso));
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro)
        {
            Int32 contador = 0;

            List<DtoCurso> lstRetorno = new List<DtoCurso>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            

            if (UString.estaVacio(filtro.idTipo))
            {
                filtro.idTipo = null;
            }

            if (UString.estaVacio(filtro.idArea))
            {
                filtro.idArea = null;
            }

            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }



            parametros.Add(new ParametroPersistenciaGenerico("p_id_curso", ConstanteUtil.TipoDato.Integer, filtro.idCurso));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_tipo", ConstanteUtil.TipoDato.String, filtro.idTipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_area", ConstanteUtil.TipoDato.String, filtro.idArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            contador = this.contar("hrcursodescripcion.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrcursodescripcion.filtroPaginacion");

            while (_reader.Read())
            {
                DtoCurso bean = new DtoCurso();

                if (!_reader.IsDBNull(0))
                    bean.idCurso = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.tipoId = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.tipoNombre = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.areaId = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.areaNombre = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.estadoNombre = _reader.GetString(6);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public HrCursodescripcion registrarCurso(UsuarioActual usuarioActual, HrCursodescripcion bean)
        {
            bean.Curso = generarCodigo();
            return  coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCurso filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCurso> lst = new List<DtoCurso>();

            if (UInteger.esCeroOrNulo(filtro.idCurso))
                filtro.idCurso = null;
            if (UString.estaVacio(filtro.nombre))
                filtro.nombre = null;
            if (UString.estaVacio(filtro.estado))
                filtro.estado = null;

            if (UString.estaVacio(filtro.idTipo))
                filtro.idTipo = null;
            if (UString.estaVacio(filtro.idArea))
                filtro.idArea = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_curso", ConstanteUtil.TipoDato.Integer, filtro.idCurso));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.estado));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_tipo", ConstanteUtil.TipoDato.String, filtro.idTipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_area", ConstanteUtil.TipoDato.String, filtro.idArea));

            Int32 contador = this.contar("hrcursodescripcion.listarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrcursodescripcion.listarPaginacion");

            while (_reader.Read())
            {
                DtoCurso bean = new DtoCurso();
                if (!_reader.IsDBNull(0))
                    bean.idCurso = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.tipoId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.tipoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.areaId = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.areaNombre = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.estadoId = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.estadoNombre = _reader.GetString(7);

                lst.Add(bean);
            }

            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            return paginacion;
        }
    }
}
