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
public class PsSaludExamenDaoImpl : GenericoDaoImpl<PsSaludExamen>, PsSaludExamenDao 
{
        private IServiceProvider servicioProveedor;


        public PsSaludExamenDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaludexamen")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludExamen obtenerPorId(PsSaludExamenPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludExamen obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado)
        {
            return obtenerPorId(new PsSaludExamenPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdExamen, pIdResultado));
        }

        public PsSaludExamen coreInsertar(UsuarioActual usuarioActual, PsSaludExamen bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludExamen coreActualizar(UsuarioActual usuarioActual, PsSaludExamen bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado)
        {
            coreEliminar(new PsSaludExamenPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdExamen, pIdResultado));
        }

        public void coreEliminar(PsSaludExamenPk id)
        {
            PsSaludExamen bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludExamen coreAnular(UsuarioActual usuarioActual,PsSaludExamenPk id)
        {
            PsSaludExamen bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludExamen coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdExamen,String pIdResultado)
        {
            return coreAnular(usuarioActual,new PsSaludExamenPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdExamen, pIdResultado));
        }

        public List<PsSaludExamen> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludExamen> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);

            return queryable.ToList();
        }
    }
}
