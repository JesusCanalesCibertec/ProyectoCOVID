using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.planilla.dao;
using net.royal.spring.planilla.dominio;
using net.royal.spring.framework.core.dominio;
using System.Collections.Generic;
using net.royal.spring.framework.core.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core;
using net.royal.spring.framework.web.ui;
using net.royal.spring.planilla.constante;
using net.royal.spring.core.dao;
using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.planilla.dominio.dto;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.constante;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.planilla.dao.impl
{
    public class AsAutorizacionDocAprobacionDaoImpl : GenericoDaoImpl<AsAutorizacionDocAprobacion>, AsAutorizacionDocAprobacionDao
    {
        private IServiceProvider servicioProveedor;
        
        public AsAutorizacionDocAprobacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "asautorizaciondocaprobacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        private int obtenerSecuencia(Int32? NumeroProceso)
        {
            IQueryable<AsAutorizacionDocAprobacion> query = this.getCriteria();
            query = query.Where(p => p.NumeroProceso == NumeroProceso);
            int? var = query.Select(p => p.Secuencial).DefaultIfEmpty(0).Max();
            return var.Value + 1;
        }
        public List<DtoProcesoSeguimiento> listarSeguiemiento(Int32 NumeroProceso) {
            List<DtoProcesoSeguimiento> listarConvenio = new List<DtoProcesoSeguimiento>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_numero_proceso", ConstanteUtil.TipoDato.Integer, NumeroProceso));            
            _reader = this.listarPorQuery("asautorizaciondocaprobacion.listarSeguiemiento", parametros);
            while (_reader.Read())
            {
                DtoProcesoSeguimiento obj = new DtoProcesoSeguimiento();
                if (!_reader.IsDBNull(0))
                    obj.NumeroSolicitud = _reader.GetInt32(0);                
                if (!_reader.IsDBNull(1))
                    obj.Secuencial = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    obj.AprobadorId = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    obj.AprobadorNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    obj.Comentario = _reader.GetString(4);                
                if (!_reader.IsDBNull(5))
                   obj.FechaAprobacion = _reader.GetDateTime(5);                
                if (!_reader.IsDBNull(6))
                    obj.UnidadOperativaId = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    obj.UnidadOperativaNombre = _reader.GetString(7);
                listarConvenio.Add(obj);
            }
            this.dispose();
            return listarConvenio;
        }

        public void registrar(UsuarioActual usuarioActual, AsAutorizacion bean, String comentario)
        {
            AsAutorizacionDocAprobacion entidad = new AsAutorizacionDocAprobacion();
            entidad.NumeroProceso = bean.Numeroproceso;
            entidad.Secuencial = obtenerSecuencia(bean.Numeroproceso);
            entidad.Responsable = usuarioActual.PersonaId;
            entidad.IndicadorAprobacion = 2;
            entidad.FechaAprobacion = DateTime.Now;
            entidad.Comentario = comentario;
            entidad.Estado = "A";
            entidad.IdGrupoAprobacion = 0;
            entidad.UsuarioCreacion = usuarioActual.UsuarioLogin;
            entidad.FechaCreacion = DateTime.Now;
            entidad.UsuarioModificacion = usuarioActual.UsuarioLogin;
            entidad.FechaModificacion = DateTime.Now;
            this.registrar(entidad);
        }
    }

}
