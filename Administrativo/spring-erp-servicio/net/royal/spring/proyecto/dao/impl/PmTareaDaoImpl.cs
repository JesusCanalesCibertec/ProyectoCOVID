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
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.proyecto.dominio.filtro;
using System.Collections.Generic;

namespace net.royal.spring.proyecto.dao.impl
{
    public class PmTareaDaoImpl : GenericoDaoImpl<PmTarea>, PmTareaDao
    {
        private IServiceProvider servicioProveedor;


        public PmTareaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pmtarea")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PmTarea obtenerPorId(PmTareaPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PmTarea obtenerPorId(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            return obtenerPorId(new PmTareaPk(pIdProyecto, pIdTarea));
        }

        public PmTarea coreInsertar(UsuarioActual usuarioActual, PmTarea bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PmTarea coreActualizar(UsuarioActual usuarioActual, PmTarea bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            coreEliminar(new PmTareaPk(pIdProyecto, pIdTarea));
        }

        public void coreEliminar(PmTareaPk id)
        {
            PmTarea bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PmTarea coreAnular(UsuarioActual usuarioActual, PmTareaPk id)
        {
            PmTarea bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PmTarea coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdProyecto, Nullable<Int32> pIdTarea)
        {
            return coreAnular(usuarioActual, new PmTareaPk(pIdProyecto, pIdTarea));
        }

        public List<DtoTarea> listarTareaSegunGrupoProyecto(FiltroTarea tarea)
        {
            if (UInteger.esCeroOrNulo(tarea.idProyecto))
                tarea.idProyecto = null;
            if (UString.estaVacio(tarea.nombre))
                tarea.nombre = null;
            if (UString.estaVacio(tarea.descripcion))
                tarea.descripcion = null;
            if (UInteger.esCeroOrNulo(tarea.idResponsable))
                tarea.idResponsable = null;
            if (UString.estaVacio(tarea.estado))
                tarea.estado = null;
            if (UString.estaVacio(tarea.idTipo1))
                tarea.idTipo1 = null;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoTarea> listaTarea = new List<DtoTarea>();

            parametros.Add(new ParametroPersistenciaGenerico("id_proyecto", ConstanteUtil.TipoDato.Integer, tarea.idProyecto));
            parametros.Add(new ParametroPersistenciaGenerico("nombretarea", ConstanteUtil.TipoDato.String, tarea.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("descripciontarea", ConstanteUtil.TipoDato.String, tarea.descripcion));
            parametros.Add(new ParametroPersistenciaGenerico("id_responsable", ConstanteUtil.TipoDato.Integer, tarea.idResponsable));
            parametros.Add(new ParametroPersistenciaGenerico("id_proceso", ConstanteUtil.TipoDato.String, tarea.idProceso));
            parametros.Add(new ParametroPersistenciaGenerico("id_estado", ConstanteUtil.TipoDato.String, tarea.estado));
            parametros.Add(new ParametroPersistenciaGenerico("id_tipo_proyecto", ConstanteUtil.TipoDato.String, tarea.idTipoProyecto));
            parametros.Add(new ParametroPersistenciaGenerico("id_tipo_1", ConstanteUtil.TipoDato.String, tarea.idTipo1));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_clave_compuesta", ConstanteUtil.TipoDato.String, tarea.externoIdGrupo));

            _reader = this.listarPorQuery("pmtarea.listarTareaSegunGrupoProyecto", parametros);

            while (_reader.Read())
            {
                DtoTarea bean = new DtoTarea();
                if (!_reader.IsDBNull(0))
                    bean.idProyecto = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombreProyecto = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.fechaRegistro = _reader.GetDateTime(2);
                if (!_reader.IsDBNull(3))
                    bean.idTarea = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.nombreTarea = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.descripcionTarea = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.fechaUltimaActualizacion = _reader.GetDateTime(6);
                if (!_reader.IsDBNull(7))
                    bean.responsableNombre = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.estadodescripcion = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.estado = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.idTipo1 = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.idTipo2 = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.idTipo3 = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.estado2 = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.estadoTransaccion = _reader.GetString(14);


                if (!_reader.IsDBNull(15))
                    bean.flagGeneraCompromiso = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.permiso = _reader.GetInt32(16);
                if (!_reader.IsDBNull(17))
                    bean.color = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.estadopermiso = _reader.GetString(18);

                if (!_reader.IsDBNull(19))
                    bean.programacionFechaInicio = _reader.GetDateTime(19);
                if (!_reader.IsDBNull(20))
                    bean.programacionFechaFin = _reader.GetDateTime(20);

                listaTarea.Add(bean);
            }
            this.dispose();

            return listaTarea;
        }

        public int? generarIdTarea(int? idProyecto)
        {
            IQueryable<PmTarea> query = this.getCriteria();
            query = query.Where(x => x.IdProyecto == idProyecto);
            Int32? contador = query.Select(p => p.IdTarea).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<DtoTarea> listarTareasNoFinalizadas(UsuarioActual usuarioActual, PmProyecto pmProyecto)
        {
            List<DtoTarea> lst = new List<DtoTarea>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_proyecto", ConstanteUtil.TipoDato.Integer, pmProyecto.IdProyecto));

            _reader = this.listarPorQuery("pmtarea.listarTareasNoFinalizadas", parametros);

            while (_reader.Read())
            {
                DtoTarea bean = new DtoTarea();
                if (!_reader.IsDBNull(0))
                    bean.idTarea = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombreTarea = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.estadodescripcion = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.elementoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTransaccion = _reader.GetInt32(4);

                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }

        public List<DtoTarea> listarTareasFinalizadasExitosamente(UsuarioActual usuarioActual, PmProyecto pmProyecto)
        {
            List<DtoTarea> lst = new List<DtoTarea>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_proyecto", ConstanteUtil.TipoDato.Integer, pmProyecto.IdProyecto));

            _reader = this.listarPorQuery("pmtarea.listarTareasFinalizadasExitosamente", parametros);

            while (_reader.Read())
            {
                DtoTarea bean = new DtoTarea();
                if (!_reader.IsDBNull(0))
                    bean.idTarea = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombreTarea = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.estadodescripcion = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.elementoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTransaccion = _reader.GetInt32(4);

                lst.Add(bean);
            }
            this.dispose();

            return lst;

        }

        public List<PmTarea> listarPorProyecto(int? idProyecto)
        {
            IQueryable<PmTarea> query = getCriteria();
            query = query.Where(x => x.IdProyecto == idProyecto);
            return query.ToList();
        }

        public PmTarea obtenerPorIdTransaccion(int value)
        {
            IQueryable<PmTarea> query = getCriteria();
            query = query.Where(x => x.IdTransaccion == value);
            return query.ToList()[0];
        }

        public List<DtoTarea> listarRegistrosValidos(UsuarioActual usuarioActual, PmTarea pmTarea)
        {
            List<DtoTarea> lst = new List<DtoTarea>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            String idExterno = pmTarea.IdProyecto.ToString() + '-' + pmTarea.IdTarea.ToString();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_externo", ConstanteUtil.TipoDato.Integer, idExterno));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_tipo_externo", ConstanteUtil.TipoDato.String, "PMTAREA"));

            _reader = this.listarPorQuery("pmtarea.listarTareasFinalizadasExitosamente", parametros);

            while (_reader.Read())
            {
                DtoTarea bean = new DtoTarea();
                if (!_reader.IsDBNull(0))
                    bean.idTarea = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.nombreTarea = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.estadodescripcion = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.elementoNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idTransaccion = _reader.GetInt32(4);

                lst.Add(bean);
            }
            this.dispose();

            return lst;
        }

        public PmTarea obtenerPorIdTransaccionParaNotificacion(int idTransaccion)
        {
            IQueryable<PmTarea> query = getCriteria();
            query = query.Where(x => x.IdTransaccion == idTransaccion);
            return query.ToList()[0];
        }

        public IList<PmTarea> listarTareasPorPersona(int value)
        {
            IQueryable<PmTarea> query = getCriteria();
            query = query.Where(x => x.IdResponsable == value);
            return query.ToList();
        }
    }
}
