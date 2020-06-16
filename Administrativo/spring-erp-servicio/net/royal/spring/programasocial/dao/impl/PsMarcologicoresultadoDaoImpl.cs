using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsMarcologicoresultadoDaoImpl : GenericoDaoImpl<PsMarcologicoresultado>, PsMarcologicoresultadoDao
    {
        private IServiceProvider servicioProveedor;


        public PsMarcologicoresultadoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psmarcologicoresultado")
        {
            servicioProveedor = _servicioProveedor;
        }



        public PsMarcologicoresultado coreInsertar(UsuarioActual usuarioActual, PsMarcologicoresultado bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsMarcologicoresultado coreActualizar(UsuarioActual usuarioActual, PsMarcologicoresultado bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

   

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMarcologico filtro)
        {
            Int32 contador = 0;

            List<PsMarcologicoresultado> lstRetorno = new List<PsMarcologicoresultado>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();



            if (UString.estaVacio(filtro.estado))
            {
                filtro.estado = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_codigo", ConstanteUtil.TipoDato.String, filtro.codigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("psmarcologicoresultado.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psmarcologicoresultado.filtroPaginacion");

            while (_reader.Read())
            {
                PsMarcologicoresultado bean = new PsMarcologicoresultado();

                if (!_reader.IsDBNull(0))
                    bean.IdMarcologico = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.Nombre = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Estado = _reader.GetString(2);


                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }





        /*
        public string obtenercadena(string pIdNombre)
        {
            IQueryable<PsMarcologicoresultado> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.Nombre == pIdNombre);

            List<PsMarcologicoresultado> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdModologico)
        {
            IQueryable<PsMarcologicoresultado> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.Where(p => p.IdArea == pIdArea);

            List<PsMarcologicoresultado> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdArea;
            return null;
        }

        public List<PsMarcologicoresultado> listado(string pIdInstitucion)
        {
            IQueryable<PsMarcologicoresultado> query = this.getCriteria();
            query = query.Where(p => p.IdInstitucion == pIdInstitucion);
            query = query.OrderByDescending(p => p.CreacionFecha);

            List<PsMarcologicoresultado> lst = query.ToList();
            return lst;

        }
        */
    }
}
