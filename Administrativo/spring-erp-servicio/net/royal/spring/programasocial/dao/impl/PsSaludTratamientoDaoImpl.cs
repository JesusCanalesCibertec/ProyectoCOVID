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
public class PsSaludTratamientoDaoImpl : GenericoDaoImpl<PsSaludTratamiento>, PsSaludTratamientoDao 
{
        private IServiceProvider servicioProveedor;


        public PsSaludTratamientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor):base(context,"pssaludtratamiento")
        { 
            servicioProveedor = _servicioProveedor;
        }

        public PsSaludTratamiento obtenerPorId(PsSaludDiscapacidadPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsSaludTratamiento obtenerPorId(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento)
        {
            return obtenerPorId(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTratamiento));
        }

        public PsSaludTratamiento coreInsertar(UsuarioActual usuarioActual, PsSaludTratamiento bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsSaludTratamiento coreActualizar(UsuarioActual usuarioActual, PsSaludTratamiento bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento)
        {
            coreEliminar(new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTratamiento));
        }

        public void coreEliminar(PsSaludDiscapacidadPk id)
        {
            PsSaludTratamiento bean = obtenerPorId(id.obtenerArreglo());
            if (bean!=null)
                this.eliminar(bean);
        }

        public PsSaludTratamiento coreAnular(UsuarioActual usuarioActual,PsSaludDiscapacidadPk id)
        {
            PsSaludTratamiento bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean==null)
                return null;
            return coreActualizar(usuarioActual,bean);
        }

        public PsSaludTratamiento coreAnular(UsuarioActual usuarioActual,String pIdInstitucion,Nullable<Int32> pIdBeneficiario,Nullable<Int32> pIdSalud,String pIdTratamiento)
        {
            return coreAnular(usuarioActual,new PsSaludDiscapacidadPk( pIdInstitucion, pIdBeneficiario, pIdSalud, pIdTratamiento));
        }

        public List<PsSaludTratamiento> obtenerListado(string pIdInstitucion, int? pIdBeneficiario, int? pIdSalud) {
            IQueryable<PsSaludTratamiento> queryable = getCriteria();
            queryable = queryable.Where(x => x.IdBeneficiario == pIdBeneficiario);
            queryable = queryable.Where(x => x.IdInstitucion == pIdInstitucion);
            queryable = queryable.Where(x => x.IdSalud == pIdSalud);
         
            return  queryable.ToList();
        }
    }
}
