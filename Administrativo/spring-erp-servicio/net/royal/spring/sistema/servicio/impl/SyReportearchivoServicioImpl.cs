using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.sistema.servicio.impl
{

    public class SyReportearchivoServicioImpl : GenericoServicioImpl, SyReportearchivoServicio {

        private IServiceProvider servicioProveedor;
        private SyReportearchivoDao syReportearchivoDao;

        public SyReportearchivoServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            syReportearchivoDao = servicioProveedor.GetService<SyReportearchivoDao>();
        }

        public ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, DtoSyReporte filtro)
        {
            return syReportearchivoDao.listarPorPaginacion(paginacion, filtro);
        }

        public SyReporteArchivo registrarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo)
        {
           return syReportearchivoDao.registrarReporteArchivo(usuarioActual, syReportearchivo);
        }

        public SyReporteArchivo actualizarReporteArchivo(UsuarioActual usuarioActual, SyReporteArchivo syReportearchivo)
        {
           return syReportearchivoDao.actualizarReporteArchivo(usuarioActual, syReportearchivo);
        }

        public void eliminarReporteArchivo(SyReportearchivoPk syReportearchivo)
        {
            syReportearchivoDao.eliminarReporteArchivo(syReportearchivo);
        }

        public SyReporteArchivo obtenerPorId(SyReportearchivoPk pk)
        {
            SyReporteArchivo reporte= syReportearchivoDao.obtenerPorId(pk.obtenerArreglo());
            if (reporte.Reporte != null)
            {
                reporte.AuxString = System.Text.Encoding.UTF8.GetString(reporte.Reporte);
            }
            else
            {
                reporte.AuxString = null;
            }

            return reporte;
        }

    }
}
