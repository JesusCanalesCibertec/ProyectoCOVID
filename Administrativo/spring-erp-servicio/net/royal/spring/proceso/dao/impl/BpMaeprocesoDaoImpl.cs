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
using net.royal.spring.proceso.dominio.filtro;
using net.royal.spring.proceso.dominio.dto;
using net.royal.spring.framework.constante;

namespace net.royal.spring.proceso.dao.impl
{

    public class BpMaeprocesoDaoImpl : GenericoDaoImpl<BpMaeproceso>, BpMaeprocesoDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeproceso")
        {
            servicioProveedor = _servicioProveedor;
        }


        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBpMaeproceso filtro)
        {
            Int32 contador = 0;

            List<DtoBpMaeproceso> lstRetorno = new List<DtoBpMaeproceso>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.IdProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            

            contador = this.contar("bpmaeproceso.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "bpmaeproceso.filtroPaginacion");

            while (_reader.Read())
            {
                DtoBpMaeproceso bean = new DtoBpMaeproceso();

                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.IdVersion = _reader.GetInt32(2);

                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public string obtenercadena(string cadena)
        {
            IQueryable<BpMaeproceso> query = this.getCriteria();
            query = query.Where(p => p.Nombre == cadena);

            List<BpMaeproceso> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string codigo)
        {
            IQueryable<BpMaeproceso> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == codigo);

            List<BpMaeproceso> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdProceso;
            return null;
        }
    }
}
