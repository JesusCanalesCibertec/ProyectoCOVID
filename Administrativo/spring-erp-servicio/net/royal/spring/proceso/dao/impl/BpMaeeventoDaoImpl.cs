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
using net.royal.spring.proceso.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpMaeeventoDaoImpl : GenericoDaoImpl<BpMaeevento>, BpMaeeventoDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeeventoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeevento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public MensajeUsuario ejecutarEvento(BpMaeevento evento, List<ParametroPersistenciaGenerico> parametros)
        {
            StringBuilder sentenciaSQL = new StringBuilder();

            sentenciaSQL.Append(evento.ObjetoBD);

            List<MensajeUsuario> lstResultado = null;
            if (evento.IdTipoevento == BpMaeevento.TIPO_EVENTO_EJECUCION)
            {
                this.ejecutarPorSentenciaSQL(sentenciaSQL.ToString(), parametros);
                this.dispose();
                return null;
            }
            if (evento.IdTipoevento == BpMaeevento.TIPO_EVENTO_VALIDACION)
            {
                _reader = this.listarPorSentenciaSQL(sentenciaSQL.ToString(), parametros);
                while (_reader.Read())
                {
                    var bean = new MensajeUsuario();
                    if (!_reader.IsDBNull(0))
                        bean.Codigo = _reader.GetString(0);
                    if (!_reader.IsDBNull(1))
                        bean.Titulo = _reader.GetString(1);
                    if (!_reader.IsDBNull(2))
                        bean.Mensaje = _reader.GetString(2);
                    if (!_reader.IsDBNull(3))
                        bean.Fuente = _reader.GetString(3);
                    if (!_reader.IsDBNull(4))
                        bean.Solucion = _reader.GetString(4);
                    lstResultado.Add(bean);
                }
                this.dispose();
            }

            if (lstResultado == null)
                return null;

            if (lstResultado.Count == 0)
                return null;

            if (((MensajeUsuario)lstResultado[0]).Mensaje == null)
                return null;

            return (MensajeUsuario)lstResultado[0];
        }

        public List<BpMaeevento> listarEventosPorConexion(BpProcesoconexionPk bpProcesoconexionPk, string idTipoEvento)
        {
            List<BpMaeevento> lst = new List<BpMaeevento>();
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("p_id_proceso", ConstanteUtil.TipoDato.String, bpProcesoconexionPk.IdProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_version", ConstanteUtil.TipoDato.Integer, bpProcesoconexionPk.IdVersion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_conexion", ConstanteUtil.TipoDato.Integer, bpProcesoconexionPk.IdConexion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_tipo_evento", ConstanteUtil.TipoDato.String, idTipoEvento));

            _reader = listarPorQuery("bpmaeevento.listarEventosPorConexion", parametros);

            while (_reader.Read())
            {
                var bean = new BpMaeevento();
                if (!_reader.IsDBNull(0))
                    bean.ObjetoBD = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdTipoevento = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.IdTipoobjeto = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.ObjetoNetCore = _reader.GetString(3);
                lst.Add(bean);
            }

            this.dispose();
            return lst;
        }
    }
}
