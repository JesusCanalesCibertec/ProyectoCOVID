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

    public class MpConocimientoDaoImpl: GenericoDaoImpl<MpConocimiento>, MpConocimientoDao
    {

        private IServiceProvider servicioProveedor;

        public MpConocimientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context, "mpConocimiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, DtoTabla filtro)
        {

            filtro.Estado = UString.estaVacio(filtro.Estado) ? "A" : filtro.Estado;

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<MpConocimiento> lstResultado = new List<MpConocimiento>();

            Int32 contador = 0;

            parametros.Add(new ParametroPersistenciaGenerico("p_tipo", ConstanteUtil.TipoDato.String, filtro.valor1));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.valor2));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombre", ConstanteUtil.TipoDato.String, filtro.Nombre));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.Estado));

           contador = this.contar("MpConocimiento.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion,parametros, "MpConocimiento.filtroPaginacion");

            while (_reader.Read())
            {
                MpConocimiento bean = new MpConocimiento();

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

        public MpConocimiento registrar(UsuarioActual usuarioActual, MpConocimiento bean)
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
            IQueryable<MpConocimiento> query = this.getCriteria();
            Int32? contador = query.Select(p => p.IdConocimiento).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public MpConocimiento actualizar(UsuarioActual usuarioActual, MpConocimiento bean)
        {

            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public List<MpConocimiento> listado(DtoTabla filtro)
        {
            IQueryable<MpConocimiento> query = this.getCriteria();

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
            List<MpConocimiento> lst = query.ToList();

            if (lst.Count > 0)
                return lst;
            return null;
        }
    }
    
}
