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
using net.royal.spring.proyecto.dominio.filtro;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.core.servicio;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmProyectoDaoImpl : GenericoDaoImpl<PmProyecto>, PmProyectoDao
    {
        private IServiceProvider servicioProveedor;


        public PmProyectoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmproyecto")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PmProyecto obtenerPorId(PmProyectoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PmProyecto obtenerPorId(Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            return obtenerPorId(new PmProyectoPk(pIdPortafolio, pIdPrograma, pIdProyecto));
        }

        public PmProyecto coreInsertar(UsuarioActual usuarioActual, PmProyecto bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            bean.Estado = estado;
            this.registrar(bean);
            return bean;
        }

        public PmProyecto coreActualizar(UsuarioActual usuarioActual, PmProyecto bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            coreEliminar(new PmProyectoPk(pIdPortafolio, pIdPrograma, pIdProyecto));
        }

        public void coreEliminar(PmProyectoPk id)
        {
            PmProyecto bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PmProyecto coreAnular(UsuarioActual usuarioActual, PmProyectoPk id)
        {
            PmProyecto bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PmProyecto coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            return coreAnular(usuarioActual, new PmProyectoPk(pIdPortafolio, pIdPrograma, pIdProyecto));
        }

        public ParametroPaginacionGenerico listarPaginacion(FiltroPaginacionProyecto filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoProyecto> lst = new List<DtoProyecto>();

            if (UInteger.esCeroOrNulo(filtro.Codigo))
                filtro.Codigo = null;
            if (UString.estaVacio(filtro.Nombre))
                filtro.Nombre = null;
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.IdTipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.Codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));


            Int32 contador = this.contar("pmproyecto.paginacionContar", parametros);

            _reader = this.listarConPaginacion(filtro.paginacion, parametros, "pmproyecto.paginacionListar");

            while (_reader.Read())
            {
                DtoProyecto bean = new DtoProyecto();
                if (!_reader.IsDBNull(0))
                    bean.IdPortafolio = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.IdPrograma = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdProyecto = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.NombreProyecto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.ResponsableNombre = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.EstadoDescripcion = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.FechaInicio = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.FechaFin = _reader.GetDateTime(7);
                if (!_reader.IsDBNull(8))
                    bean.IdResponsable = _reader.GetInt32(8);
                if (!_reader.IsDBNull(9))
                    bean.IdTransaccion = _reader.GetInt32(9);
                lst.Add(bean);
            }
            this.dispose();
            filtro.paginacion.RegistroEncontrados = contador;
            filtro.paginacion.ListaResultado = lst;

            return filtro.paginacion;
        }

        public int? generarIdProyecto()
        {
            IQueryable<PmProyecto> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdProyecto).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public PmProyecto obtenerPorIdProyecto(PmProyectoPk pmProyectoPk)
        {
            IQueryable<PmProyecto> cri = this.getCriteria();
            cri = cri.Where(x => x.IdProyecto == pmProyectoPk.IdProyecto);
            cri = cri.Where(x => x.IdPrograma == pmProyectoPk.IdPrograma);
            cri = cri.Where(x => x.IdPortafolio == pmProyectoPk.IdPortafolio);

            List<PmProyecto> lst = cri.ToList();

            if (lst == null)
                return null;

            if (lst.Count == 1)
            {
                PmProyecto pm = (PmProyecto)lst[0];

                if (!UString.estaVacio(pm.Afe))
                {
                    var afe = servicioProveedor.GetService<AfemstServicio>().obtenerporid(new core.dominio.AfemstPk() { Afe = pm.Afe });
                    if (afe != null)
                    {
                        pm.auxAfe = afe.Localname;
                    }
                }

                return pm;
            }
            return null;
        }

        public PmProyecto obtenerPorIdTransaccion(int idTransaccion)
        {
            IQueryable<PmProyecto> query = getCriteria();
            query = query.Where(x => x.IdTransaccion == idTransaccion);

            var lst = query.ToList();
            if (lst.Count() == 1)
                return (PmProyecto)lst[0];

            return null;
        }

        public PmProyecto obtenerPorIdTransaccionParaNotificacion(int idTransaccion)
        {
            IQueryable<PmProyecto> cri = this.getCriteria();
            cri = cri.Where(x => x.IdTransaccion == idTransaccion);

            List<PmProyecto> lst = cri.ToList();

            if (lst == null)
                return null;

            if (lst.Count == 1)
            {
                PmProyecto pm = (PmProyecto)lst[0];
                return pm;
            }
            return null;
        }
    }
}
