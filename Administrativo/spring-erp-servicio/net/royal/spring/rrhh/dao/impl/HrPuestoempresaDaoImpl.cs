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
using net.royal.spring.sistema.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class HrPuestoempresaDaoImpl : GenericoDaoImpl<HrPuestoempresa>, HrPuestoempresaDao
    {
        private IServiceProvider servicioProveedor;

        public HrPuestoempresaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "hrpuestoempresa")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroPuestoempresa filtro)
        {
            Int32 contador = 0;

           

            List<HrPuestoempresa> lstRetorno = new List<HrPuestoempresa>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UInteger.esCeroOrNulo(filtro.codigo))
            {
               filtro.codigo = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));


            contador = this.contar("hrpuestoempresa.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "hrpuestoempresa.filtroPaginacion");

            while (_reader.Read())
            {
                HrPuestoempresa bean = new HrPuestoempresa();

                if (!_reader.IsDBNull(0))
                    bean.CodigoPuesto = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Descripcion = _reader.GetString(1);

             

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }
    }
}
