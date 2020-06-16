using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.asistencia.dao;
using net.royal.spring.asistencia.servicio;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.asistencia.dominio.filtro;
using Microsoft.AspNetCore.Hosting;
using net.royal.spring.framework.core;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace net.royal.spring.asistencia.servicio.impl
{

    public class AsAccesosdiariosServicioImpl : GenericoServicioImpl, AsAccesosdiariosServicio
    {

        private IServiceProvider servicioProveedor;
        private AsAccesosdiariosDao asAccesosdiariosDao;
        private IHostingEnvironment _hostingEnvironment;

        public AsAccesosdiariosServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            asAccesosdiariosDao = servicioProveedor.GetService<AsAccesosdiariosDao>();
            this._hostingEnvironment = hostingEnvironment;
        }

        public List<DtoMarcaciones> listarMarcaciones(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            return asAccesosdiariosDao.listarMarcaciones(fechadesde, fechahasta, empleado);
        }

        public List<DtoMarcaciones> listarMarcacionesConsolidado(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            return asAccesosdiariosDao.listarMarcacionesConsolidado(fechadesde, fechahasta, empleado);
        }

        public DtoMarcaciones traerMarcas(DateTime fechadesde, DateTime fechahasta, int empleado)
        {
            return asAccesosdiariosDao.traerMarcas(fechadesde, fechahasta, empleado);
        }

        public ParametroPaginacionGenerico listarMarcaciones(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            return asAccesosdiariosDao.listarMarcaciones(paginacion, filtro);
        }

        public DtoMarcaciones traerMarcas(FiltroPaginacionEmpleado filtroPaginacionEmpleado)
        {
            return asAccesosdiariosDao.traerMarcas(filtroPaginacionEmpleado);
        }
        public ParametroPaginacionGenerico listarMarcacionesConsolidado(ParametroPaginacionGenerico paginacion, FiltroPaginacionEmpleado filtro)
        {
            return asAccesosdiariosDao.listarMarcacionesConsolidado(paginacion, filtro);
        }

        public ParametroPaginacionGenerico listar(ParametroPaginacionGenerico paginacion, FiltroAsAccesosDiarios filtroAsAccesosDiarios)
        {
            return asAccesosdiariosDao.listar(paginacion, filtroAsAccesosDiarios);
        }

        public void registrar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios)
        {
            asAccesosdiariosDao.registrar(usuarioActual, asAccesosDiarios);
        }

        public void actualizar(UsuarioActual usuarioActual, AsAccesosdiarios asAccesosDiarios)
        {
            asAccesosdiariosDao.actualizar(usuarioActual, asAccesosDiarios);
        }

        public List<AsAccesosdiarios> obtenerMarcasPorDia(int persona, string compania, DateTime? inicio, DateTime? fin)
        {
            return asAccesosdiariosDao.obtenerMarcasPorDia(persona, compania, inicio, fin);
        }

        public string ExportarMarcas(FiltroPaginacionEmpleado filtro)
        {
            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarMarcaciones(filtro.paginacion, filtro);
            List<DtoMarcaciones> listarMarcaciones = (List<DtoMarcaciones>)parametroPaginacionGenerico.ListaResultado;

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte de Marcas" + UFechaHora.obtenerNombreScat() + ".xlsx";

            string URL = "../Archivos/Excel/" + sFileName;
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoja 1");
                //First add the headers

                worksheet.Cells[1, 1].Value = "Código";
                worksheet.Cells[1, 2].Value = "Apellidos y Nombres";
                worksheet.Cells[1, 3].Value = "Fecha";
                worksheet.Cells[1, 4].Value = "Dia";
                worksheet.Cells[1, 5].Value = "Hora Ingreso";
                worksheet.Cells[1, 6].Value = "Hora Refrigerio";
                worksheet.Cells[1, 7].Value = "Hora Salida";
                worksheet.Cells[1, 8].Value = "Hora Laborada";
                worksheet.Cells[1, 9].Value = "Hora Autorizada";
                worksheet.Cells[1, 10].Value = "Horas No Autorizada";


                int fila = 2;

                if (listarMarcaciones.Count > 0)
                {
                    foreach (DtoMarcaciones obj in listarMarcaciones)
                    {
                        worksheet.Cells[fila, 1].Value = obj.Empleado;
                        worksheet.Cells[fila, 2].Value = obj.Nombre;
                        worksheet.Cells[fila, 3].Value = obj.fecha;
                        worksheet.Cells[fila, 4].Value = obj.Dia;
                        worksheet.Cells[fila, 5].Value = obj.HoraIngreso;
                        worksheet.Cells[fila, 6].Value = obj.HoraSalidaRef;
                        worksheet.Cells[fila, 7].Value = obj.HoraSalida;
                        worksheet.Cells[fila, 8].Value = obj.HoraLaborada;
                        worksheet.Cells[fila, 9].Value = obj.HorasAutorizadas;
                        worksheet.Cells[fila, 10].Value = obj.HorasNoAutorizadas;

                        fila++;
                    }
                }

                worksheet.Cells["A1:L10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 15])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }

        public string ExportarAsistencia(FiltroPaginacionEmpleado filtro)
        {
            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarMarcacionesConsolidado(filtro.paginacion, filtro);
            List<DtoMarcaciones> listarMarcaciones = (List<DtoMarcaciones>)parametroPaginacionGenerico.ListaResultado;

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte de Asistencia" + UFechaHora.obtenerNombreScat() + ".xlsx";

            string URL = "../Archivos/Excel/" + sFileName;
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoja 1");
                //First add the headers

                worksheet.Cells[1, 1].Value = "Código";
                worksheet.Cells[1, 2].Value = "Apellidos y Nombres";
                worksheet.Cells[1, 3].Value = "Semana";
                worksheet.Cells[1, 4].Value = "Lunes";
                worksheet.Cells[1, 5].Value = "Martes";
                worksheet.Cells[1, 6].Value = "Miércoles";
                worksheet.Cells[1, 7].Value = "Jueves";
                worksheet.Cells[1, 8].Value = "Viernes";
                worksheet.Cells[1, 9].Value = "Sábado";
                worksheet.Cells[1, 10].Value = "Domingo";
                worksheet.Cells[1, 11].Value = "Horas Semana";
                worksheet.Cells[1, 12].Value = "Horas Por Compensar";
                worksheet.Cells[1, 13].Value = "No Autorizadas";
                worksheet.Cells[1, 14].Value = "Horas Tardanza";
                worksheet.Cells[1, 15].Value = "Horas Salida Anticipada";


                int fila = 2;

                if (listarMarcaciones.Count > 0)
                {
                    foreach (DtoMarcaciones obj in listarMarcaciones)
                    {

                        worksheet.Cells[fila, 1].Value = obj.Empleado;
                        worksheet.Cells[fila, 2].Value = obj.Nombre;
                        worksheet.Cells[fila, 3].Value = obj.Semana;
                        worksheet.Cells[fila, 4].Value = obj.Lunes;
                        worksheet.Cells[fila, 5].Value = obj.Martes;
                        worksheet.Cells[fila, 6].Value = obj.Miercoles;
                        worksheet.Cells[fila, 7].Value = obj.Jueves;
                        worksheet.Cells[fila, 8].Value = obj.Viernes;
                        worksheet.Cells[fila, 9].Value = obj.Sabado;
                        worksheet.Cells[fila, 10].Value = obj.Domingo;
                        worksheet.Cells[fila, 11].Value = obj.HorasSemana;
                        worksheet.Cells[fila, 12].Value = obj.HorasComp;
                        worksheet.Cells[fila, 13].Value = obj.HorasNoAut;
                        worksheet.Cells[fila, 14].Value = obj.HorasTard;
                        worksheet.Cells[fila, 15].Value = obj.HorasSalAnt;
                        fila++;
                    }
                }

                worksheet.Cells["A1:L10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 15])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.
            }

            return URL;
        }
    }
}
