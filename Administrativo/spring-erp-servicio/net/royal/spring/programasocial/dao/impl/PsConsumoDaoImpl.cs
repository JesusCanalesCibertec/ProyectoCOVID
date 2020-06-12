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
public class PsConsumoDaoImpl : GenericoDaoImpl<PsConsumo>, PsConsumoDao
{
        private IServiceProvider servicioProveedor;


        public PsConsumoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psconsumo")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsConsumo obtenerPorId(PsConsumoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsConsumo obtenerPorId(String pIdInstitucion,Int32? pIdConsumo)
        {
            return obtenerPorId(new PsConsumoPk( pIdInstitucion, pIdConsumo));
        }

        public PsConsumo coreInsertar(UsuarioActual usuarioActual, PsConsumo bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsConsumo coreActualizar(UsuarioActual usuarioActual, PsConsumo bean, String estado)
        {
            if (UString.estaVacio(estado))
            {
                estado = ConstanteEstadoGenerico.ACTIVO;
            }
            bean.Estado = estado;  
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Int32? pIdConsumo)
        {
            coreEliminar(new PsConsumoPk( pIdInstitucion, pIdConsumo));
        }

        public void coreEliminar(PsConsumoPk id)
        {
            PsConsumo bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroConsumo filtro)
        {
            Int32 contador = 0;

            List<DtoConsumo> lstRetorno = new List<DtoConsumo>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            if (UString.estaVacio( filtro.codigoPeriodo))
            {
                filtro.codigoPeriodo = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_id_institucion", ConstanteUtil.TipoDato.String, filtro.codigoInstitucion));
           parametros.Add(new ParametroPersistenciaGenerico("p_fechainicial", ConstanteUtil.TipoDato.Date, filtro.fechainicial));
           parametros.Add(new ParametroPersistenciaGenerico("p_fechafinal", ConstanteUtil.TipoDato.Date, filtro.fechafinal));
            parametros.Add(new ParametroPersistenciaGenerico("p_estado", ConstanteUtil.TipoDato.String, filtro.estado));


            contador = this.contar("psconsumo.filtroContar", parametros);

            _reader = this.listarConPaginacion(paginacion, parametros, "psconsumo.filtroPaginacion");

            while (_reader.Read())
            {
                DtoConsumo bean = new DtoConsumo();

                if (!_reader.IsDBNull(0))
                    bean.codConsumo = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.codInstitucion = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nomInstitucion = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.codPeriodo = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.fechainicio = _reader.GetDateTime(4);

                if (!_reader.IsDBNull(5))
                    bean.fechafin = _reader.GetDateTime(5);

                if (!_reader.IsDBNull(6))
                    bean.descripcion = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.estado = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.aportante = _reader.GetString(8);




                lstRetorno.Add(bean);
            }
            this.dispose();

            paginacion.ListaResultado = lstRetorno;
            paginacion.RegistroEncontrados = contador;

            return paginacion;
        }

        public int generarCodigo(String pIdInstitucion)
        {
            IQueryable<PsConsumo> query = this.getCriteria();
            query = query.Where(
               p => p.IdInstitucion == pIdInstitucion
               );
            Int32? contador = query.Select(p => p.IdConsumo).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public PsConsumo coreAnular(UsuarioActual usuarioActual, PsConsumoPk id)
        {
            PsConsumo bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;

            return coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsConsumo coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdConsumo)
        {
            return coreAnular(usuarioActual, new PsConsumoPk(pIdInstitucion, pIdConsumo));
        }
    }
}
