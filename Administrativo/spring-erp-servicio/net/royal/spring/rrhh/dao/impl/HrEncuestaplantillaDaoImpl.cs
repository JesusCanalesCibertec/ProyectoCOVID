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
using net.royal.spring.framework.core.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.rrhh.dao.impl
{
    public class HrEncuestaplantillaDaoImpl : GenericoDaoImpl<HrEncuestaplantilla>, HrEncuestaplantillaDao
    {
        private IServiceProvider servicioProveedor;


        public HrEncuestaplantillaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrencuestaplantilla")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrEncuestaplantilla obtenerPorId(HrEncuestaplantillaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuestaplantilla obtenerPorId(Nullable<Int32> pPlantilla)
        {
            return obtenerPorId(new HrEncuestaplantillaPk(pPlantilla));
        }

        public HrEncuestaplantilla coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilla bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrEncuestaplantilla coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilla bean, String estado)
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

        public void coreEliminar(Nullable<Int32> pPlantilla)
        {
            coreEliminar(new HrEncuestaplantillaPk(pPlantilla));
        }

        public void coreEliminar(HrEncuestaplantillaPk id)
        {
            HrEncuestaplantilla bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual, HrEncuestaplantillaPk id)
        {
            HrEncuestaplantilla bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public HrEncuestaplantilla coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla)
        {
            return coreAnular(usuarioActual, new HrEncuestaplantillaPk(pPlantilla));
        }

        public int generarCodigo()
        {
            IQueryable<HrEncuestaplantilla> query = this.getCriteria();

            List<HrEncuestaplantilla> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.Plantilla));
            }

            return var + 1;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
            Int32 contador = 0;

            List<DtoTabla> lstRetorno = new List<DtoTabla>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            if (UString.estaVacio(filtro.Codigo))
            {
                filtro.Codigo = null;
            }
            if (UString.estaVacio(filtro.Nombre))
            {
                filtro.Nombre = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

            contador = this.contar("hrencuestaplantilla.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrencuestaplantilla.filtroPaginacion");

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.CodigoNumerico = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Estado = _reader.GetString(2);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }


        public void eliminardetalle(int? plantilla)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();



            _parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.Integer, plantilla));



            this.ejecutarPorQuery("hrencuestaplantilla.eliminardetalle", _parametros);
        }
    }
}
