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
    public class PmPlantillaTareasDaoImpl : GenericoDaoImpl<PmPlantillaTareas>, PmPlantillaTareasDao
    {
        private IServiceProvider servicioProveedor;

        public PmPlantillaTareasDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmplantillatareas")
        {
            servicioProveedor = _servicioProveedor;
        }
        public ParametroPaginacionGenerico listarPlantillasTarea(DtoTabla filtro, ParametroPaginacionGenerico paginacion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<PmPlantillaTareas> lst = new List<PmPlantillaTareas>();

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.Id));

            Int32 contador = this.contar("pmplantillatareas.paginacionContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "pmplantillatareas.paginacionListar");

            while (_reader.Read())
            {
                PmPlantillaTareas bean = new PmPlantillaTareas();
                if (!_reader.IsDBNull(0))
                    bean.Secuencia = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.FlagComision = _reader.GetString(2);

                lst.Add(bean);
            }
            this.dispose();
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lst;

            return paginacion;
        }

        public List<PmPlantillaTareas> listarPlantillasTarea(DtoTabla filtro)
        {
            IQueryable<PmPlantillaTareas> query = getCriteria();
            query = query.Where(x => x.Plantilla == filtro.Id);
            query = query.Where(x => x.Estado == "A");

            return query.ToList();
        }

        public PmPlantillaTareas obtenerPorId(PmPlantillaTareasPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PmPlantillaTareas obtenerPorId(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            return obtenerPorId(new PmPlantillaTareasPk(pPlantilla, pPregunta));
        }

        public PmPlantillaTareas coreInsertar(UsuarioActual usuarioActual, PmPlantillaTareas bean)
        {
            this.registrar(bean);
            return bean;
        }

        public PmPlantillaTareas coreActualizar(UsuarioActual usuarioActual, PmPlantillaTareas bean)
        {
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            coreEliminar(new PmPlantillaTareasPk(pPlantilla, pPregunta));
        }

        public void coreEliminar(PmPlantillaTareasPk id)
        {
            PmPlantillaTareas bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PmPlantillaTareas coreAnular(UsuarioActual usuarioActual, PmPlantillaTareasPk id)
        {
            PmPlantillaTareas bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PmPlantillaTareas coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta)
        {
            return coreAnular(usuarioActual, new PmPlantillaTareasPk(pPlantilla, pPregunta));
        }

        public List<PmPlantillaTareas> listarTareas(int plantilla)
        {
            IQueryable<PmPlantillaTareas> query = getCriteria();
            query = query.Where(x => x.Plantilla == plantilla);
            return query.ToList();
        }
    }
}
