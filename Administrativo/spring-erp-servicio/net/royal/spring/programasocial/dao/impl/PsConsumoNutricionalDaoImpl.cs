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
using System.Collections.Generic;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao.impl
{
public class PsConsumoNutricionalDaoImpl : GenericoDaoImpl<PsConsumoNutricional>, PsConsumoNutricionalDao
{
        private IServiceProvider servicioProveedor;


        public PsConsumoNutricionalDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psconsumonutricional")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsConsumoNutricional obtenerPorId(PsConsumoNutricionalPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsConsumoNutricional obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional)
        {
            return obtenerPorId(new PsConsumoNutricionalPk( pIdInstitucion, pIdConsumo, pIdConsumoNutricional));
        }

        public PsConsumoNutricional coreInsertar(UsuarioActual usuarioActual, PsConsumoNutricional bean,String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsConsumoNutricional coreActualizar(UsuarioActual usuarioActual, PsConsumoNutricional bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional)
        {
            coreEliminar(new PsConsumoNutricionalPk( pIdInstitucion, pIdConsumo, pIdConsumoNutricional));
        }

        public void coreEliminar(PsConsumoNutricionalPk id)
        {
            PsConsumoNutricional bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

      

       
        public int generarCodigo(String pIdInstitucion, Nullable<Int32> pIdConsumo)
        {
            IQueryable<PsConsumoNutricional> query = this.getCriteria();
            query = query.Where( p => p.IdInstitucion == pIdInstitucion);
            query = query.Where( p => p.IdConsumo == pIdConsumo);
            Int32? contador = query.Select(p => p.IdConsumoNutricional).DefaultIfEmpty(0).Max();
            contador++;
            return contador.Value;
        }

        public List<DtoConsumoNutricional> listardetalle(PsConsumoPk llave)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoConsumoNutricional> lst = new List<DtoConsumoNutricional>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, llave.IdInstitucion));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdConsumo", ConstanteUtil.TipoDato.Integer, llave.IdConsumo));
            

            _reader = this.listarPorQuery("psconsumonutricional.filtro", _parametros);

            while (_reader.Read())
            {
                DtoConsumoNutricional bean = new DtoConsumoNutricional();

                if (!_reader.IsDBNull(0))
                    bean.codItem = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomItem = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.nomUnidad = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.cantidadsolicitada = _reader.GetDecimal(3);

                if (!_reader.IsDBNull(4))
                    bean.cantidadfundacion = _reader.GetDecimal(4);

                if (!_reader.IsDBNull(5))
                    bean.cantidadotros = _reader.GetDecimal(5);

                if (!_reader.IsDBNull(6))
                    bean.comentarios = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.grupo = _reader.GetString(7);


                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }

       
        public void eliminardetalle(PsConsumoPk llave)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();



            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, llave.IdInstitucion));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdConsumo", ConstanteUtil.TipoDato.Integer, llave.IdConsumo));
            


            this.ejecutarPorQuery("psconsumonutricional.eliminardetalle", _parametros);
        }

        public List<PsConsumoNutricional> listadonormal(PsConsumoPk llave)
        {
            IQueryable<PsConsumoNutricional> query = this.getCriteria();
            query = query.Where( p => p.IdInstitucion == llave.IdInstitucion );
            query = query.Where(p => p.IdConsumo == llave.IdConsumo);

            List<PsConsumoNutricional> lst = query.ToList();
            if (lst.Count > 0)
                return lst;
            return new List<PsConsumoNutricional>();
        }
    }
}
