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
    public class HrCentroestudiosDaoImpl : GenericoDaoImpl<HrCentroestudios>, HrCentroestudiosDao
    {
        private IServiceProvider servicioProveedor;


        public HrCentroestudiosDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrcentroestudios")
        {
            servicioProveedor = _servicioProveedor;
        }

        public HrCentroestudios obtenerPorId(HrCentroestudiosPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public HrCentroestudios obtenerPorId(Nullable<Int32> pCentro)
        {
            return obtenerPorId(new HrCentroestudiosPk(pCentro));
        }

        public HrCentroestudios coreInsertar(UsuarioActual usuarioActual, HrCentroestudios bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public HrCentroestudios coreActualizar(UsuarioActual usuarioActual, HrCentroestudios bean)
        {
            bean.Ultimafechamodif = DateTime.Now;
            bean.Ultimousuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pCentro)
        {
            coreEliminar(new HrCentroestudiosPk(pCentro));
        }

        public void coreEliminar(HrCentroestudiosPk id)
        {
            HrCentroestudios bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public HrCentroestudios coreAnular(UsuarioActual usuarioActual, HrCentroestudiosPk id)
        {
            HrCentroestudios bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public HrCentroestudios coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pCentro)
        {
            return coreAnular(usuarioActual, new HrCentroestudiosPk(pCentro));
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionCentroEstudio filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCentroEstudios> lstRegistros = new List<DtoCentroEstudios>();

            if (UInteger.esCeroOrNulo(filtro.idCentroEstudios))
                filtro.idCentroEstudios = null;
            if (UString.estaVacio(filtro.nombre))
                filtro.nombre = null;
            if (UString.estaVacio(filtro.estado))
                filtro.estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_id_centro_estudios", ConstanteUtil.TipoDato.Integer, filtro.idCentroEstudios));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.estado));

            Int32 contador = this.contar("hrcentroestudios.listarBusquedaContar", parametros);
            _reader = this.listarConPaginacion(paginacion, parametros, "hrcentroestudios.listarBusquedaPaginacion");


            while (_reader.Read())
            {
                DtoCentroEstudios bean = new DtoCentroEstudios();
                if (!_reader.IsDBNull(0))
                    bean.idCentroEstudios = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.estadoId = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.estadoNombre = _reader.GetString(3);

                lstRegistros.Add(bean);
            }

            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }

    }
}
