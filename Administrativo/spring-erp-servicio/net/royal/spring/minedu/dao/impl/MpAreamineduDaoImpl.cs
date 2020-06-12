using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.minedu.dominio;
using net.royal.spring.minedu.dominio.dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net.royal.spring.minedu.dao.impl
{

    public class MpAreamineduDaoImpl: GenericoDaoImpl<MpAreaminedu>, MpAreamineduDao
    {

        private IServiceProvider servicioProveedor;

        public MpAreamineduDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "MpAreaminedu")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {
          
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            List<DtoAreaminedu> lstResultado = new List<DtoAreaminedu>();

            Int32 contador = 0;

            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.Integer, filtro.CodigoNumerico));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombrecorto", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Descripcion));


           contador = this.contar("MpAreaminedu.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "MpAreaminedu.filtroPaginacion");

            while (_reader.Read())
            {
                DtoAreaminedu bean = new DtoAreaminedu();

                if (!_reader.IsDBNull(0))
                    bean.codigo = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.primercorto = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.primero = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.segundocorto = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.segundo = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.ultimocorto = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.ultimo = _reader.GetString(6);

        
                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

       
    }
    
}
