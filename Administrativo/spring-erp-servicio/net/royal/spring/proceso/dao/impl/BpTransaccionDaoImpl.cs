using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.proyecto.dominio.dto;
using net.royal.spring.framework.constante;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpTransaccionDaoImpl : GenericoDaoImpl<BpTransaccion>, BpTransaccionDao
    {
        private IServiceProvider servicioProveedor;

        public BpTransaccionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bptransaccion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public int? generarIdTransaccion()
        {
            IQueryable<BpTransaccion> query = getCriteria();
            Int32? contador = query.Select(p => p.IdTransaccion).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<DtoTransaccionNotificacion> listarTransaccionesPorUsuario(UsuarioActual usuarioActual, string idProceso)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio(idProceso))
            {
                idProceso = null;
            }
            else if (idProceso == "null")
            {
                idProceso = null;
            }
            parametros.Add(new ParametroPersistenciaGenerico("idProceso", ConstanteUtil.TipoDato.String, idProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_usuario", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioLogin));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_persona", ConstanteUtil.TipoDato.Integer, usuarioActual.PersonaId));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_puesto", ConstanteUtil.TipoDato.Integer, usuarioActual.PuestoEmpresaCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_centro_costo", ConstanteUtil.TipoDato.String, usuarioActual.CentroCostosCodigo));

            List<DtoTransaccionNotificacion> lst = new List<DtoTransaccionNotificacion>();
            _reader = listarPorQuery("bptransaccion.listarTransaccionesPorUsuario", parametros);

            while (_reader.Read())
            {
                var bean = new DtoTransaccionNotificacion();
                if (!_reader.IsDBNull(0))
                    bean.idTransaccion = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.descripcion = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.componente = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.nombreCorto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.idProceso = _reader.GetString(4);
                lst.Add(bean);
            }

            this.dispose();


            return lst;
        }

        public List<DtoTransaccionUsuario> listarTransaccionSeguimiento(UsuarioActual usuarioActual, int idTransaccion, string idProceso)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_transaccion", ConstanteUtil.TipoDato.Integer, idTransaccion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_proceso", ConstanteUtil.TipoDato.String, idProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania_socio", ConstanteUtil.TipoDato.String, usuarioActual.CompaniaSocioCodigo));

            List<DtoTransaccionUsuario> lst = new List<DtoTransaccionUsuario>();
            _reader = listarPorQuery("bptransaccion.listarTransaccionSeguimiento", parametros);

            while (_reader.Read())
            {
                var bean = new DtoTransaccionUsuario();
                if (!_reader.IsDBNull(0))
                    bean.idRol = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.idTipoSeguridad = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.idPersona = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.nombrePersona = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.usuarioCodigo = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.puesto = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.sucursal = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.nombreSucursal = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.centroCosto = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.nombreCentroCosto = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.correoInterno = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.correoExterno = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.correoElectronico = _reader.GetString(12);

                lst.Add(bean);
            }

            this.dispose();


            return lst;
        }

        public string obtenerContenidoCorreo(int tipo, int? idTransaccion)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.Integer, tipo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_transaccion", ConstanteUtil.TipoDato.Integer, idTransaccion));

            String mensaje = "";
            _reader = listarPorQuery("bptransaccion.obtenerContenidoCorreo", parametros);

            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    mensaje = _reader.GetString(0);
            }
            this.dispose();
            return mensaje;
        }


    }
}
