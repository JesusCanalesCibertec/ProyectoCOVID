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
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.framework.constante;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpMaerequerimientoDaoImpl : GenericoDaoImpl<BpMaerequerimiento>, BpMaerequerimientoDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaerequerimientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaerequerimiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRequerimiento filtro)
        {
            Int32 contador = 0;

            List<BpMaerequerimiento> lstRetorno = new List<BpMaerequerimiento>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.IdRequerimiento));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));


            contador = this.contar("bpmaerequerimiento.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "bpmaerequerimiento.filtroPaginacion");

            while (_reader.Read())
            {
                BpMaerequerimiento bean = new BpMaerequerimiento();

                if (!_reader.IsDBNull(0))
                    bean.IdRequerimiento = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Descripcion = _reader.GetString(2);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }
    }
}
