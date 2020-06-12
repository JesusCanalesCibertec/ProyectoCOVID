using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmPlantillaDaoImpl : GenericoDaoImpl<PmPlantilla>, PmPlantillaDao
    {
        private IServiceProvider servicioProveedor;

        public PmPlantillaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmplantilla")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<PmPlantilla> listarPlantillas()
        {
            IQueryable<PmPlantilla> query = getCriteria();
            query = query.Where(x => x.Estado == "A");
            return query.ToList();
        }

        public PmPlantilla obtenerPorId(PmPlantillaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PmPlantilla obtenerPorId(Nullable<Int32> pPlantilla)
        {
            return obtenerPorId(new PmPlantillaPk(pPlantilla));
        }

        public PmPlantilla coreInsertar(UsuarioActual usuarioActual, PmPlantilla bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
           
            this.registrar(bean);
            return bean;
        }

        public PmPlantilla coreActualizar(UsuarioActual usuarioActual, PmPlantilla bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }

            bean.Estado = estado;
            bean.UltimaFechaModif = DateTime.Now;
            bean.UltimoUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPlantilla)
        {
            coreEliminar(new PmPlantillaPk(pPlantilla));
        }

        public void coreEliminar(PmPlantillaPk id)
        {
            PmPlantilla bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PmPlantilla coreAnular(UsuarioActual usuarioActual, PmPlantillaPk id)
        {
            PmPlantilla bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PmPlantilla coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla)
        {
            return coreAnular(usuarioActual, new PmPlantillaPk(pPlantilla));
        }

        public int generarCodigo()
        {
            IQueryable<PmPlantilla> query = this.getCriteria();

            List<PmPlantilla> lst = query.ToList();
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


            if (UInteger.esCeroOrNulo(filtro.CodigoNumerico))
            {
                filtro.CodigoNumerico = null;
            }
            if (UString.estaVacio(filtro.Descripcion))
            {
                filtro.Descripcion = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_descripcion", ConstanteUtil.TipoDato.String, filtro.Descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));


            contador = this.contar("pmplantilla.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "pmplantilla.filtroPaginacion");

            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();

                if (!_reader.IsDBNull(0))
                    bean.CodigoNumerico = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

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



            this.ejecutarPorQuery("pmplantilla.eliminardetalle", _parametros);
        }
    }
}
