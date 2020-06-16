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
public class PsBeneficiarioIngresoDaoImpl : GenericoDaoImpl<PsBeneficiarioIngreso>, PsBeneficiarioIngresoDao 
{
        private IServiceProvider servicioProveedor;


        public PsBeneficiarioIngresoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"psbeneficiarioingreso")
        { 
            servicioProveedor = _servicioProveedor;
        }



        public PsBeneficiarioIngreso obtenerPorId(PsBeneficiarioIngresoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsBeneficiarioIngreso obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            return obtenerPorId(new PsBeneficiarioIngresoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso));
        }

        public PsBeneficiarioIngreso coreInsertar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean,String estado)
        {
        
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsBeneficiarioIngreso coreActualizar(UsuarioActual usuarioActual, PsBeneficiarioIngreso bean, String estado)
        {
            if (UString.estaVacio(estado))
                estado = ConstanteEstadoGenerico.ACTIVO;
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            coreEliminar(new PsBeneficiarioIngresoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso));
        }

        public void coreEliminar(PsBeneficiarioIngresoPk id)
        {
            PsBeneficiarioIngreso bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,PsBeneficiarioIngresoPk id)
        {
            PsBeneficiarioIngreso bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean,ConstanteEstadoGenerico.INACTIVO);
        }

        public PsBeneficiarioIngreso coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdIngreso)
        {
            return coreAnular(usuarioActual,new PsBeneficiarioIngresoPk( pIdInstitucion, pIdBeneficiario, pIdIngreso));
        }

        public int generarCodigo(Int32? pIdBeneficiario)
        {
            IQueryable<PsBeneficiarioIngreso> query = this.getCriteria();
            query = query.Where(p => p.IdBeneficiario == pIdBeneficiario);

            List<PsBeneficiarioIngreso> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdIngreso));
            }

            return var + 1;
        }

        public int ObtenerUltimoIngreso(Int32? pIdBeneficiario)
        {
            IQueryable<PsBeneficiarioIngreso> query = this.getCriteria();
            query = query.Where(p => p.IdBeneficiario == pIdBeneficiario);

            List<PsBeneficiarioIngreso> lst = query.ToList(); 
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdIngreso));
            }

            return var;
        }

        public List<DtoBeneficiarioIngreso> listado(int pIdBeneficiario, string pIdInstitucion)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBeneficiarioIngreso> lst = new List<DtoBeneficiarioIngreso>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdBeneficiario", ConstanteUtil.TipoDato.Integer, pIdBeneficiario));
            _parametros.Add(new ParametroPersistenciaGenerico("p_IdInstitucion", ConstanteUtil.TipoDato.String, pIdInstitucion));


            _reader = this.listarPorQuery("psbeneficiarioingreso.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBeneficiarioIngreso bean = new DtoBeneficiarioIngreso();

                if (!_reader.IsDBNull(0))
                    bean.idIngreso = _reader.GetInt32(0);

                if (!_reader.IsDBNull(1))
                    bean.fechaingreso = _reader.GetDateTime(1);

                if (!_reader.IsDBNull(2))
                    bean.situacionlegal = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.comentariosingreso = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.fechaegreso = _reader.GetDateTime(4);

                if (!_reader.IsDBNull(5))
                    bean.motivoegreso = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.comentariosegreso = _reader.GetString(6);

                

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }
    }
}
