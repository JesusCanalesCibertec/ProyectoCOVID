using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.proceso.dominio;
using net.royal.spring.proyecto.dominio.dto;
using System;
using System.Collections.Generic;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpProcesoconexioncomunicacionDaoImpl : GenericoDaoImpl<BpProcesoconexioncomunicacion>, BpProcesoconexioncomunicacionDao
    {
        private IServiceProvider servicioProveedor;

        public BpProcesoconexioncomunicacionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpprocesoconexioncomunicacion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<DtoTransaccionUsuario> listarUsuariosPorProcesoConexion(BpProcesoconexionPk procesoConexion, string idTipoComunicacion)
        {
            List<DtoTransaccionUsuario> lst = new List<DtoTransaccionUsuario>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_proceso", ConstanteUtil.TipoDato.String, procesoConexion.IdProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_version", ConstanteUtil.TipoDato.Integer, procesoConexion.IdVersion));
            parametros.Add(new ParametroPersistenciaGenerico("p_conexion", ConstanteUtil.TipoDato.Integer, procesoConexion.IdConexion));
            parametros.Add(new ParametroPersistenciaGenerico("p_tipo_comunicacion", ConstanteUtil.TipoDato.String, idTipoComunicacion));

            _reader = listarPorQuery("bpprocesoconexioncomunicacion.listarUsuariosPorProcesoConexion", parametros);

            while (_reader.Read())
            {
                var bean = new DtoTransaccionUsuario();
                if (!_reader.IsDBNull(0))
                    bean.correoInterno = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.correoExterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.correoElectronico = _reader.GetString(2);
                lst.Add(bean);
            }

            this.dispose();
            return lst;

        }
    }
}
