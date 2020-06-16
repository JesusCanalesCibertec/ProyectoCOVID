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

namespace net.royal.spring.programasocial.dao.impl
{
public class PsSaludDiscapacidadDaoImpl : GenericoDaoImpl<PsSaludDiscapacidad>, PsSaludDiscapacidadDao {
        private IServiceProvider servicioProveedor;


        public PsSaludDiscapacidadDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaluddiscapacidad")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludDiscapacidad obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludDiscapacidad obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiscapacidad)
        {
            return obtenerPorId(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiscapacidad));
        }

        public PsSaludDiscapacidad coreInsertar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludDiscapacidad coreActualizar(UsuarioActual usuarioActual, PsSaludDiscapacidad bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiscapacidad)
        {
            coreEliminar(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiscapacidad));
        }

        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            PsSaludDiscapacidad bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludDiscapacidad coreAnular(UsuarioActual usuarioActual,PsSaludDiscapacidadPk id)
        {
            PsSaludDiscapacidad bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludDiscapacidad coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdDiscapacidad)
        {
            return coreAnular(usuarioActual,new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdDiscapacidad));
        }

        public List<PsSaludDiscapacidad> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludDiscapacidad> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);
         
            return  queryable.ToList();
        }
    }
}
