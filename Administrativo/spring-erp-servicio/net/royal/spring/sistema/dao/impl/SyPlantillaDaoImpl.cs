using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyPlantillaDaoImpl : GenericoDaoImpl<SyPlantilla>, SyPlantillaDao 
    {
        private IServiceProvider servicioProveedor;


	    public SyPlantillaDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "syplantilla") {
                servicioProveedor = _servicioProveedor;
	    }

        public SyPlantilla obtenerPorId(SyPlantillaPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public ParametroPaginacionGenerico solicitudListar(ParametroPaginacionGenerico paginacion, SyPlantilla filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoSyPlantilla> lstRegistros = new List<DtoSyPlantilla>();

            if (UString.estaVacio(filtro.Aplicacioncodigo))
                filtro.Aplicacioncodigo = null;
            if (UString.estaVacio(filtro.Plantillacodigo))
                filtro.Plantillacodigo = null;
            if (UString.estaVacio(filtro.Estado))
                filtro.Estado = null;

            parametros.Add(new ParametroPersistenciaGenerico("p_aplicacion", ConstanteUtil.TipoDato.String, filtro.Aplicacioncodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_plantilla", ConstanteUtil.TipoDato.String, filtro.Plantillacodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_estado", ConstanteUtil.TipoDato.String, filtro.Estado));
                  
            Int32 contador = this.contar("syplantilla.solicitudListarContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "syplantilla.solicitudListarPaginacion");
            while (_reader.Read())
            {
                DtoSyPlantilla bean = new DtoSyPlantilla();
                if (!_reader.IsDBNull(0))
                    bean.AplicacionCodigo = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.PlantillaCodigo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.Nombre = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.TipoPlantilla = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.Estado = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.AplicacionDescripcion = _reader.GetString(5);
                lstRegistros.Add(bean);
            }
            this.dispose();
            
            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstRegistros;

            return paginacion;
        }
    }
}
