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
    public class PsCapacidadTallerDaoImpl : GenericoDaoImpl<PsCapacidadTaller>, PsCapacidadTallerDao
    {
        private IServiceProvider servicioProveedor;


        public PsCapacidadTallerDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pscapacidadtaller")
        {
            servicioProveedor = _servicioProveedor;
        }

        public PsCapacidadTaller obtenerPorId(PsCapacidadTallerPk id)
        {
            return base.obtenerPorId(id.obtenerArreglo());
        }

        public PsCapacidadTaller obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller)
        {
            return obtenerPorId(new PsCapacidadTallerPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdTaller));
        }

        public PsCapacidadTaller coreInsertar(UsuarioActual usuarioActual, PsCapacidadTaller bean)
        {
            bean.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.CreacionFecha = DateTime.Now;
            bean.CreacionUsuario = usuarioActual.UsuarioLogin;
            this.registrar(bean);
            return bean;
        }

        public PsCapacidadTaller coreActualizar(UsuarioActual usuarioActual, PsCapacidadTaller bean)
        {
            bean.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            this.actualizar(bean);
            return bean;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller)
        {
            coreEliminar(new PsCapacidadTallerPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdTaller));
        }

        public void coreEliminar(PsCapacidadTallerPk id)
        {
            PsCapacidadTaller bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsCapacidadTaller coreAnular(UsuarioActual usuarioActual, PsCapacidadTallerPk id)
        {
            PsCapacidadTaller bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return coreActualizar(usuarioActual, bean);
        }

        public PsCapacidadTaller coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad, Nullable<Int32> pIdTaller)
        {
            return coreAnular(usuarioActual, new PsCapacidadTallerPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad, pIdTaller));
        }

        public void eliminarPorCapacidad(int? idCapacidad)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("idCapacidad", ConstanteUtil.TipoDato.Integer, idCapacidad));
            ejecutarPorSentenciaSQL("delete from sgseguridadsys.PS_CAPACIDAD_TALLER where ID_CAPACIDAD = :idCapacidad", parametros);
        }
    }
}
