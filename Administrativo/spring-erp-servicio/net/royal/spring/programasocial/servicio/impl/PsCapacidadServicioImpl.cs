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

    public class PsCapacidadServicioImpl : GenericoServicioImpl, PsCapacidadServicio {

        private IServiceProvider servicioProveedor;
        private PsCapacidadDao psCapacidadDao;
        private IHostingEnvironment _hostingEnvironment;

        public PsCapacidadServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            this._hostingEnvironment = hostingEnvironment;
            psCapacidadDao = servicioProveedor.GetService<PsCapacidadDao>();
        }

        public PsCapacidad coreInsertar(UsuarioActual usuarioActual, DtoCapacidad bean) {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psCapacidadDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PsCapacidad coreActualizar(UsuarioActual usuarioActual, DtoCapacidad bean) {
            return psCapacidadDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsCapacidad coreAnular(UsuarioActual usuarioActual, PsCapacidadPk id) {
            return psCapacidadDao.coreAnular(usuarioActual, id);
        }

        public PsCapacidad coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad) {
            return psCapacidadDao.coreAnular(usuarioActual, pIdInstitucion, pIdBeneficiario, pIdCapacidad);
        }

        public void coreEliminar(PsCapacidadPk id) {
            psCapacidadDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad) {
            psCapacidadDao.coreEliminar(pIdInstitucion, pIdBeneficiario, pIdCapacidad);
        }


        public DtoCapacidad obtenerPorId(PsCapacidadPk id) {
            return psCapacidadDao.obtenerPorId(id);
        }

        public PsCapacidad obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad) {
            return psCapacidadDao.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdCapacidad);
        }

        public ParametroPaginacionGenerico listarCapacidades(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro) {
            return psCapacidadDao.listarCapacidades(paginacion, filtro);
        }

        public DtoCapacidad calcularBarthel(DtoCapacidad bean) {
            return psCapacidadDao.calcularBarthel(bean);
        }

        public DtoCapacidad calcularKatz(DtoCapacidad bean) {
            return psCapacidadDao.calcularKatz(bean);
        }

        public DtoCapacidad calcularAniosRetraso(DtoCapacidad bean) {
            return psCapacidadDao.calcularAniosRetraso(bean);
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro) {
            return psCapacidadDao.listarPaginacionConsulta(paginacion, filtro);
        }

        public string Exportar(FiltroCapacidad filtro) {

            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarPaginacionConsulta(filtro.paginacion, filtro);
            List<DtoCapacidad> listarAtencion = (List<DtoCapacidad>)parametroPaginacionGenerico.ListaResultado;


            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Capacidades" + UFechaHora.obtenerNombreScat() + ".xlsx";

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

                if (!filtro.esNino) {
                    worksheet.Cells[1, 1].Value = "Código";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Dependencia Katz";
                    worksheet.Cells[1, 4].Value = "Riesgo Caida?";

                    worksheet.Cells[1, 5].Value = "Taller Formativo";
                    worksheet.Cells[2, 5].Value = "Rendimiento";
                    worksheet.Cells[2, 6].Value = "C. Asistida";

                    worksheet.Cells[1, 7].Value = "Taller Físico";
                    worksheet.Cells[2, 7].Value = "Rendimiento";
                    worksheet.Cells[2, 8].Value = "C.Asistida";

                    worksheet.Cells[1, 9].Value = "Taller Cognitivo";
                    worksheet.Cells[2, 9].Value = "Rendimiento";
                    worksheet.Cells[2, 10].Value = "C.Asistida";

                    worksheet.Cells[1, 11].Value = "No Evaluado";

                    worksheet.Cells[1, 12].Value = "Comentarios Adicionales de Taller";
                    worksheet.Cells[1, 13].Value = "Habilidades Ocupacionales?";
                    worksheet.Cells[1, 14].Value = "Evaluado en Hab. Ocupacional?";
                    worksheet.Cells[1, 15].Value = "Comentarios Adicionales de Capacitación";

                    worksheet.Cells[1, 5, 1, 6].Merge = true;
                    worksheet.Cells[1, 5, 1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 7, 1, 8].Merge = true;
                    worksheet.Cells[1, 7, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 9, 1, 10].Merge = true;
                    worksheet.Cells[1, 9, 1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    using (var cells = worksheet.Cells[1, 1, 1, 15]) {
                        cells.Style.Font.Bold = true;
                        cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    using (var cells = worksheet.Cells[2, 1, 2, 15]) {
                        cells.Style.Font.Bold = true;
                        cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                }
                else {

                    worksheet.Cells[1, 1].Value = "Código";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Dependencia Barthel";

                    worksheet.Cells[1, 4].Value = "Escolaridad";
                    worksheet.Cells[2, 4].Value = "Tipo Institución";
                    worksheet.Cells[2, 5].Value = "Nivel";
                    worksheet.Cells[2, 6].Value = "Grado Académico Actual";
                    worksheet.Cells[2, 7].Value = "Años de Retraso Educativo";
                    worksheet.Cells[2, 8].Value = "Comentario SI retraso Educativo";

                    worksheet.Cells[1, 9].Value = "Nivel de Rendimiento Educativo Alcanzado";
                    worksheet.Cells[2, 9].Value = "Lógico Matemático";
                    worksheet.Cells[2, 10].Value = "Comunicación";
                    worksheet.Cells[2, 11].Value = "Comprensión Lectora";
                    worksheet.Cells[2, 12].Value = "Personal Social";
                    worksheet.Cells[2, 13].Value = "Ciencia y Ambiente";
                    worksheet.Cells[2, 14].Value = "Tipo de Comunicación";
                    worksheet.Cells[2, 15].Value = "Comentario de Rendimiento Educativo";

                    worksheet.Cells[1, 16].Value = "Taller Formativo";
                    worksheet.Cells[2, 16].Value = "Rendimiento";
                    worksheet.Cells[2, 17].Value = "C. Asistida";

                    worksheet.Cells[1, 18].Value = "Taller Artístico";
                    worksheet.Cells[2, 18].Value = "Rendimiento";
                    worksheet.Cells[2, 19].Value = "C.Asistida";

                    worksheet.Cells[1, 20].Value = "Taller Deportivo";
                    worksheet.Cells[2, 20].Value = "Rendimiento";
                    worksheet.Cells[2, 21].Value = "C.Asistida";

                    worksheet.Cells[1, 22].Value = "No Evaluado";

                    worksheet.Cells[1, 23].Value = "Comentarios Adicionales de Taller";

                    worksheet.Cells[1, 4, 1, 8].Merge = true;
                    worksheet.Cells[1, 4, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 9, 1, 15].Merge = true;
                    worksheet.Cells[1, 9, 1, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 16, 1, 17].Merge = true;
                    worksheet.Cells[1, 16, 1, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 18, 1, 19].Merge = true;
                    worksheet.Cells[1, 18, 1, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    worksheet.Cells[1, 20, 1, 21].Merge = true;
                    worksheet.Cells[1, 20, 1, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    using (var cells = worksheet.Cells[1, 1, 1, 26]) {
                        cells.Style.Font.Bold = true;
                        cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    using (var cells = worksheet.Cells[2, 1, 2, 26]) {
                        cells.Style.Font.Bold = true;
                        cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                }



                int fila = 3;

                if (listarAtencion.Count > 0) {
                    foreach (DtoCapacidad obj in listarAtencion) {
                        if (!filtro.esNino) {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.GradoDependenciaKatz;
                            worksheet.Cells[fila, 4].Value = obj.IdRiesgoCaida;

                            worksheet.Cells[fila, 5].Value = obj.IdNotaTallerFormativo;
                            worksheet.Cells[fila, 6].Value = obj.CantidadTallerFormativo;
                            worksheet.Cells[fila, 7].Value = obj.IdNotaTallerFisico;
                            worksheet.Cells[fila, 8].Value = obj.CantidadTallerFisico;
                            worksheet.Cells[fila, 9].Value = obj.IdNotaTallerCognitivo;
                            worksheet.Cells[fila, 10].Value = obj.CantidadTallerCognitivo;

                            worksheet.Cells[fila, 11].Value = obj.Evaluado;

                            worksheet.Cells[fila, 12].Value = obj.ComentarioTalleres;
                            worksheet.Cells[fila, 13].Value = obj.NombreHabilidadOcupacional;
                            worksheet.Cells[fila, 14].Value = obj.IdEvaluarOcupacional;
                            worksheet.Cells[fila, 15].Value = obj.ComentarioCapacidad;
                        }
                        else {

                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.GradoDependenciaBarthel;
                            worksheet.Cells[fila, 4].Value = obj.NombreInstitucion;

                            worksheet.Cells[fila, 5].Value = obj.NombreNivel;
                            worksheet.Cells[fila, 6].Value = obj.NombreGrado;
                            worksheet.Cells[fila, 7].Value = obj.AnioRetraso;
                            worksheet.Cells[fila, 8].Value = obj.ComentarioRetraso;

                            worksheet.Cells[fila, 9].Value = obj.NotaLogicoMatematico;
                            worksheet.Cells[fila, 10].Value = obj.NotaComunicacion;
                            worksheet.Cells[fila, 11].Value = obj.NotaComprensionLectora;
                            worksheet.Cells[fila, 12].Value = obj.NotaPersonalSocial;
                            worksheet.Cells[fila, 13].Value = obj.NotaCienciaAmbiente;
                            worksheet.Cells[fila, 14].Value = obj.IdTipoComunicacion;
                            worksheet.Cells[fila, 15].Value = obj.ComentarioRendimiento;


                            worksheet.Cells[fila, 16].Value = obj.IdNotaTallerFormativo;
                            worksheet.Cells[fila, 17].Value = obj.CantidadTallerFormativo;

                            worksheet.Cells[fila, 18].Value = obj.IdNotaTallerArtistico;
                            worksheet.Cells[fila, 19].Value = obj.CantidadTallerArtistico;

                            worksheet.Cells[fila, 20].Value = obj.IdNotaTallerDeportivo;
                            worksheet.Cells[fila, 21].Value = obj.CantidadTallerDeportivo;

                            worksheet.Cells[fila, 22].Value = obj.Evaluado;

                            worksheet.Cells[fila, 23].Value = obj.ComentarioTalleres;
                          

                        }


                        fila++;
                    }
                }

                worksheet.Cells["A1:L10000"].AutoFitColumns();
                // worksheet.Cells[Rowstart, ColStart, RowEnd, ColEnd]


              


                package.Save(); //Save the workbook.

                return URL;
            }
        }

    }
}
