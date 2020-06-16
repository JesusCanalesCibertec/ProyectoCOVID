using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsConsumoNutricionalServicioImpl : GenericoServicioImpl, PsConsumoNutricionalServicio {

        private IServiceProvider servicioProveedor;
        private PsConsumoNutricionalDao psConsumoNutricionalDao;

        public PsConsumoNutricionalServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            psConsumoNutricionalDao = servicioProveedor.GetService<PsConsumoNutricionalDao>();
        }

        public PsConsumoNutricional coreInsertar(UsuarioActual usuarioActual, PsConsumoNutricional bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psConsumoNutricionalDao.coreInsertar(usuarioActual,bean, bean.Estado);
        }

        public PsConsumoNutricional coreActualizar(UsuarioActual usuarioActual, PsConsumoNutricional bean)
        {
            return psConsumoNutricionalDao.coreActualizar(usuarioActual,bean,bean.Estado);
        }

  

        public void coreEliminar(PsConsumoNutricionalPk id)
        {
            psConsumoNutricionalDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional)
        {
            psConsumoNutricionalDao.coreEliminar( pIdInstitucion, pIdConsumo, pIdConsumoNutricional);
        }


        public PsConsumoNutricional obtenerPorId(PsConsumoNutricionalPk id)
        {
            return psConsumoNutricionalDao.obtenerPorId(id);
        }

        public PsConsumoNutricional obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional)
        {
            return psConsumoNutricionalDao.obtenerPorId( pIdInstitucion, pIdConsumo, pIdConsumoNutricional);
        }

        public List<DtoConsumoNutricional> listardetalle(PsConsumoPk llave)
        {
            return psConsumoNutricionalDao.listardetalle(llave);
        }
    }
}
