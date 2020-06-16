using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;
using net.royal.spring.covid.dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace net.royal.spring.covid.dao.impl
{

    public class ConocimientoDaoImpl: GenericoDaoImpl<Conocimiento>, ConocimientoDao
    {

        private IServiceProvider servicioProveedor;

        public ConocimientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "mpConocimiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {

            filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<Conocimiento> lstResultado = new List<Conocimiento>();

            Int32 contador = 0;

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

           contador = this.contar("mpConocimiento.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "mpConocimiento.filtroPaginacion");

            while (_reader.Read())
            {
                Conocimiento bean = new Conocimiento();

                if (!_reader.IsDBNull(0))
                    bean.IdConocimiento = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.Tipo = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.Area = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.Nombre = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.Descripcion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.Cabecera = _reader.GetString(6);

                lstResultado.Add(bean);
            }

            this.dispose();

            paginacion.ListaResultado = lstResultado;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public Conocimiento registrar(UsuarioActual usuarioActual, Conocimiento bean)
        {
            bean.IdConocimiento = this.generarCodigo();
            bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public int generarCodigo()
        {
            IQueryable<Conocimiento> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdConocimiento).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public Conocimiento actualizar(UsuarioActual usuarioActual, Conocimiento bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<Conocimiento> listado(DtoTabla filtro)
        {
            IQueryable<Conocimiento> query = this.getCriteria();

            query = query.Where(p => p.Tipo == filtro.valor1);
            if(filtro.valor1 == "M")
            {
                query = query.Where(p =>  p.Area == null);
            }
            else
            {
                query = query.Where(p => p.Area == filtro.valor2);
            }  
            query = query.Where(p => p.Estado =="A");
            query = query.OrderBy(p => p.Nombre);
            List<Conocimiento> lst = query.ToList();

            if (lst.Count > 0)
                return lst;
            return null;
        }
    }
    
}
