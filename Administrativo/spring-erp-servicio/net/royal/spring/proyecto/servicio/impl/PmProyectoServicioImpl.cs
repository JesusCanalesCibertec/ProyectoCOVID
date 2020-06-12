using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.proyecto.dao;
using net.royal.spring.proyecto.servicio;
using net.royal.spring.proyecto.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.proyecto.dominio.filtro;
using net.royal.spring.proceso.servicio;

namespace net.royal.spring.proyecto.servicio.impl
{

    public class PmProyectoServicioImpl : GenericoServicioImpl, PmProyectoServicio
    {

        private IServiceProvider servicioProveedor;
        private PmProyectoDao pmProyectoDao;
        private PmTipoproyectoDao pmTipoproyectoDao;
        private BpStatemachineServicio bpStatemachineServicio;

        public PmProyectoServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            pmProyectoDao = servicioProveedor.GetService<PmProyectoDao>();
            pmTipoproyectoDao = servicioProveedor.GetService<PmTipoproyectoDao>();
            bpStatemachineServicio = servicioProveedor.GetService<BpStatemachineServicio>();
        }

        public PmProyecto coreInsertar(UsuarioActual usuarioActual, PmProyecto bean)
        {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return pmProyectoDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PmProyecto coreActualizar(UsuarioActual usuarioActual, PmProyecto bean)
        {
            return pmProyectoDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PmProyecto coreAnular(UsuarioActual usuarioActual, PmProyectoPk id)
        {
            return pmProyectoDao.coreAnular(usuarioActual, id);
        }

        public PmProyecto coreAnular(UsuarioActual usuarioActual, Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            return pmProyectoDao.coreAnular(usuarioActual, pIdPortafolio, pIdPrograma, pIdProyecto);
        }

        public void coreEliminar(PmProyectoPk id)
        {
            pmProyectoDao.coreEliminar(id);
        }

        public void coreEliminar(Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            pmProyectoDao.coreEliminar(pIdPortafolio, pIdPrograma, pIdProyecto);
        }


        public PmProyecto obtenerPorId(PmProyectoPk id)
        {
            return pmProyectoDao.obtenerPorId(id);
        }

        public PmProyecto obtenerPorId(Nullable<Int32> pIdPortafolio, Nullable<Int32> pIdPrograma, Nullable<Int32> pIdProyecto)
        {
            return pmProyectoDao.obtenerPorId(pIdPortafolio, pIdPrograma, pIdProyecto);
        }

        public ParametroPaginacionGenerico listarPaginacion(FiltroPaginacionProyecto filtro)
        {
            return pmProyectoDao.listarPaginacion(filtro);
        }

        public PmProyecto registrarProyecto(PmProyecto pmProyecto, UsuarioActual usuarioActual)
        {
            PmProgramaServicio pmProgramaServicio = servicioProveedor.GetService<PmProgramaServicio>();
            PmPrograma pmPrograma = pmProgramaServicio.crearProgramaAnual(usuarioActual);

            pmProyecto.IdPortafolio = pmPrograma.IdPortafolio;
            pmProyecto.IdPrograma = pmPrograma.IdPrograma;
            pmProyecto.IdProyecto = pmProyectoDao.generarIdProyecto();

            PmTipoproyecto pmTipoProyecto = pmTipoproyectoDao
            .obtenerPorId(new PmTipoproyectoPk() { IdTipoProyecto = pmProyecto.IdTipoProyecto }.obtenerArreglo());

            if ("S" == pmTipoProyecto.FlagGeneraTransaccion)
            {
                Int32 idTransaccion = bpStatemachineServicio.registrarTransaccion(usuarioActual,
                        pmTipoProyecto.IdProceso, pmProyecto.Nombre);
                pmProyecto.IdTransaccion = idTransaccion;
            }


            return pmProyectoDao.coreInsertar(usuarioActual, pmProyecto, pmProyecto.Estado);
        }

        public PmProyecto actualizarProyecto(PmProyecto pmProyecto, UsuarioActual usuarioActual)
        {
            return this.pmProyectoDao.coreActualizar(usuarioActual, pmProyecto, pmProyecto.Estado);
        }

        public PmProyecto obtenerPorIdProyecto(PmProyectoPk pmProyectoPk)
        {
            return pmProyectoDao.obtenerPorIdProyecto(pmProyectoPk);
        }

        public PmProyecto obtenerPorIdTransaccionParaNotificacion(int idTransaccion)
        {
            return pmProyectoDao.obtenerPorIdTransaccionParaNotificacion(idTransaccion);
        }
    }
}
