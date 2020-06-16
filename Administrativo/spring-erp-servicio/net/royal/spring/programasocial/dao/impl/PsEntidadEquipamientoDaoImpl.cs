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
    public class PsEntidadEquipamientoDaoImpl : GenericoDaoImpl<PsEntidadEquipamiento>, PsEntidadEquipamientoDao
    {
        private IServiceProvider servicioProveedor;


        public PsEntidadEquipamientoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "psentidadequipamiento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsEntidadEquipamiento obtenerPorId(PsEntidadEquipamientoPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsEntidadEquipamiento obtenerPorId(Nullable<Int32> pIdEntidad, String pIdEquipamiento)
        {
            return obtenerPorId(new PsEntidadEquipamientoPk(pIdEntidad, pIdEquipamiento));
        }

        public PsEntidadEquipamiento coreInsertar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsEntidadEquipamiento coreActualizar(UsuarioActual usuarioActual, PsEntidadEquipamiento bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(Nullable<Int32> pIdEntidad, String pIdEquipamiento)
        {
            coreEliminar(new PsEntidadEquipamientoPk(pIdEntidad, pIdEquipamiento));
        }

        public void coreEliminar(PsEntidadEquipamientoPk id)
        {
            PsEntidadEquipamiento bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsEntidadEquipamiento coreAnular(UsuarioActual usuarioActual, PsEntidadEquipamientoPk id)
        {
            PsEntidadEquipamiento bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsEntidadEquipamiento coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdEntidad, String pIdEquipamiento)
        {
            return coreAnular(usuarioActual, new PsEntidadEquipamientoPk(pIdEntidad, pIdEquipamiento));
        }

        public List<PsEntidadEquipamiento> listarBeneficiario(string institucion, int idBeneficiario)
        {
            IQueryable<PsEntidadEquipamiento> query = getCriteria();
            query = query.Where(x => x.IdEntidad == idBeneficiario);
            return query.ToList();

        }
    }
}
