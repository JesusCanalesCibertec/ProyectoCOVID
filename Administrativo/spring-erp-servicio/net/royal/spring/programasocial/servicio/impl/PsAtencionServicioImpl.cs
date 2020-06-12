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
using net.royal.spring.programasocial.dominio.dtos;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsAtencionServicioImpl : GenericoServicioImpl, PsAtencionServicio {

        private IServiceProvider servicioProveedor;
        private PsAtencionDao psAtencionDao;
        private IHostingEnvironment _hostingEnvironment;

        public PsAtencionServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            this._hostingEnvironment = hostingEnvironment;
            psAtencionDao = servicioProveedor.GetService<PsAtencionDao>();
        }

        public PsAtencion coreInsertar(UsuarioActual usuarioActual, DtoAtencion bean) {
            return psAtencionDao.coreInsertar(usuarioActual, bean);
        }

        public PsAtencion coreActualizar(UsuarioActual usuarioActual, DtoAtencion bean) {
            return psAtencionDao.coreActualizar(usuarioActual, bean);
        }

        public PsAtencion coreAnular(UsuarioActual usuarioActual, PsAtencionPk id) {
            return psAtencionDao.coreAnular(usuarioActual, id);
        }

        public PsAtencion coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            return psAtencionDao.coreAnular(usuarioActual, pIdInstitucion, pIdBeneficiario, pIdAtencion);
        }

        public void coreEliminar(PsAtencionPk id) {
            psAtencionDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            psAtencionDao.coreEliminar(pIdInstitucion, pIdBeneficiario, pIdAtencion);
        }


        public PsAtencion obtenerPorId(PsAtencionPk id) {
            return psAtencionDao.obtenerPorId(id);
        }

        public PsAtencion obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdAtencion) {
            return psAtencionDao.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdAtencion);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroRas filtro) {
            return psAtencionDao.listarPaginacion(paginacion, filtro);
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroRas filtro) {
            return psAtencionDao.listarPaginacionConsulta(paginacion, filtro);
        }

        public string Exportar(FiltroRas filtro) {

            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarPaginacionConsulta(filtro.paginacion, filtro);
            List<DtoAtencion> listarAtencion = (List<DtoAtencion>)parametroPaginacionGenerico.ListaResultado;


            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte RAS" + UFechaHora.obtenerNombreScat() + ".xlsx";

            string URL = "../Archivos/Excel/" + sFileName;
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            if (file.Exists) {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, "Archivos/Excel/" + sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file)) {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Hoja 1");
                //First add the headers



                worksheet.Cells[1, 1].Value = "Código";
                worksheet.Cells[1, 2].Value = "Nombre Completo";
                worksheet.Cells[1, 3].Value = "Residencia";
                worksheet.Cells[1, 4].Value = "Diagnóstico 1";
                worksheet.Cells[1, 5].Value = "Tratamientos 1 ";
                worksheet.Cells[1, 6].Value = "Tratamientos 2";
                worksheet.Cells[1, 7].Value = "Tratamientos 3";

                worksheet.Cells[1, 8].Value = "Diagnóstico 2";
                worksheet.Cells[1, 9].Value = "Tratamientos 1 ";
                worksheet.Cells[1, 10].Value = "Tratamientos 2";
                worksheet.Cells[1, 11].Value = "Tratamientos 3";

                worksheet.Cells[1, 12].Value = "Diagnóstico 3";
                worksheet.Cells[1, 13].Value = "Tratamientos 1 ";
                worksheet.Cells[1, 14].Value = "Tratamientos 2";
                worksheet.Cells[1, 15].Value = "Tratamientos 3";
                worksheet.Cells[1, 16].Value = "Comentarios";
                int fila = 2;

                if (listarAtencion.Count > 0) {
                    foreach (DtoAtencion obj in listarAtencion) {
                        worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                        worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                        worksheet.Cells[fila, 3].Value = obj.Residencia;
                        worksheet.Cells[fila, 4].Value = obj.NombreDiagnostico01;
                        worksheet.Cells[fila, 5].Value = obj.IdTratamiento01_1_nombre;
                        worksheet.Cells[fila, 6].Value = obj.IdTratamiento01_2_nombre;
                        worksheet.Cells[fila, 7].Value = obj.IdTratamiento01_3_nombre;
                        worksheet.Cells[fila, 8].Value = obj.NombreDiagnostico02;
                        worksheet.Cells[fila, 9].Value = obj.IdTratamiento02_1_nombre;
                        worksheet.Cells[fila, 10].Value = obj.IdTratamiento02_2_nombre;
                        worksheet.Cells[fila, 11].Value = obj.IdTratamiento02_3_nombre;
                        worksheet.Cells[fila, 12].Value = obj.NombreDiagnostico03;
                        worksheet.Cells[fila, 13].Value = obj.IdTratamiento03_1_nombre;
                        worksheet.Cells[fila, 14].Value = obj.IdTratamiento03_2_nombre;
                        worksheet.Cells[fila, 15].Value = obj.IdTratamiento03_3_nombre;
                        worksheet.Cells[fila, 16].Value = obj.comentario;


                        fila++;
                    }
                }

                worksheet.Cells["A1:M10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 16]) {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }


    }
}
