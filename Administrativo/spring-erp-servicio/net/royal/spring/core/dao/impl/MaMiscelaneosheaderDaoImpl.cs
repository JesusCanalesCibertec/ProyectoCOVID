using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.core.dao.impl
{
public class MaMiscelaneosheaderDaoImpl : GenericoDaoImpl<MaMiscelaneosheader>, MaMiscelaneosheaderDao
{
        private IServiceProvider servicioProveedor;


	public MaMiscelaneosheaderDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "mamiscelaneosheader") {
            servicioProveedor = _servicioProveedor;
	}

        public MaMiscelaneosheader obtenerPorId(MaMiscelaneosheaderPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public MaMiscelaneosheader obtenerPorId(Nullable<Int32> pPregunta)
        {
            return null; //obtenerPorId(new MaMiscelaneosheaderPk(pPregunta));
        }

        public MaMiscelaneosheader coreInsertar(UsuarioActual usuarioActual, MaMiscelaneosheader bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;

          //  bean.Ultimafechamodif = DateTime.Now;
          //  bean.Ultimousuario = usuarioActual.UsuarioLogin;

            this.registrar(bean);
            return bean;
        }

        public MaMiscelaneosheader coreActualizar(UsuarioActual usuarioActual, MaMiscelaneosheader bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
          //  bean.Ultimafechamodif = DateTime.Now;
          //  bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPregunta)
        {
           // coreEliminar(new MaMiscelaneosheaderPk(pPregunta));
        }

        public void coreEliminar(MaMiscelaneosheaderPk id)
        {
            MaMiscelaneosheader bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

      
        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMiscelaneosheader filtro)
        {
            Int32 contador = 0;

            List<DtoMiscelaneosheader> lstRetorno = new List<DtoMiscelaneosheader>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(filtro.codigo))
            {
                filtro.codigo = null;
            }
            if (UString.estaVacio(filtro.aplicacion))
            {
                filtro.aplicacion = null;
            }
          
            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }

           
            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.aplicacion));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            contador = this.contar("mamiscelaneosheader.filtrocontar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "mamiscelaneosheader.filtropaginacion");

            while (_reader.Read())
            {
                DtoMiscelaneosheader bean = new DtoMiscelaneosheader();

                if (!_reader.IsDBNull(0))
                    bean.aplicacion = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.codigo = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.compania = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.descripcion = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nomaplicacion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.estado = _reader.GetString(5);




                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

    }
}
