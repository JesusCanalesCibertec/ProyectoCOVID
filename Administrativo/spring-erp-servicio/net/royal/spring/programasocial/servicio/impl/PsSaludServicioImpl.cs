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
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsSaludServicioImpl : GenericoServicioImpl, PsSaludServicio {

        private IServiceProvider servicioProveedor;
        private PsSaludDao psSaludDao;
        private IHostingEnvironment _hostingEnvironment;

        public PsSaludServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            this._hostingEnvironment = hostingEnvironment;
            psSaludDao = servicioProveedor.GetService<PsSaludDao>();
        }

        public PsSalud coreInsertar(UsuarioActual usuarioActual, DtoPsSalud bean) {

            return psSaludDao.coreInsertar(usuarioActual, bean);
        }

        public PsSalud coreActualizar(UsuarioActual usuarioActual, DtoPsSalud bean) {
            return psSaludDao.coreActualizar(usuarioActual, bean);
        }

        public PsSalud coreAnular(UsuarioActual usuarioActual, PsSaludPk id) {
            return psSaludDao.coreAnular(usuarioActual, id);
        }

        public PsSalud coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud) {
            return psSaludDao.coreAnular(usuarioActual, pIdInstitucion, pIdBeneficiario, pIdSalud);
        }

        public void coreEliminar(PsSaludPk id) {
            psSaludDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud) {
            psSaludDao.coreEliminar(pIdInstitucion, pIdBeneficiario, pIdSalud);
        }

        public DtoPsSalud obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud) {
            return psSaludDao.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdSalud);
        }

        public ParametroPaginacionGenerico listarSaludPaginacion(ParametroPaginacionGenerico paginacion, FiltroSalud filtro) {
            return psSaludDao.listarSaludPaginacion(paginacion, filtro);
        }

        public DtoPsSalud obtenerPorListados(string IdInstitucion, int? IdBeneficiario, int? IdSalud) {
            return psSaludDao.obtenerPorListados(IdInstitucion, IdBeneficiario, IdSalud);
        }

        public DtoTabla calcularHemoglobina(DtoPsSalud bean) {
            return psSaludDao.calcularHemoglobina(bean);
        }

        public List<DtoPsSalud> listarDiagnosticos(string IdInstitucion, int? IdBeneficiario, string IdTipoRas) {
            return psSaludDao.listarDiagnosticos(IdInstitucion, IdBeneficiario, IdTipoRas);
        }

        public List<DtoPsSalud> listarTratamientos(string IdInstitucion, int? IdBeneficiario, int? IdDetalle, int? IdDiagnostico) {
            return psSaludDao.listarTratamientos(IdInstitucion, IdBeneficiario, IdDetalle, IdDiagnostico);
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSalud filtro) {
            return psSaludDao.listarPaginacionConsulta(paginacion, filtro);
        }

        public string Exportar(FiltroSalud filtro) {

            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarPaginacionConsulta(filtro.paginacion, filtro);
            List<DtoPsSalud> listarAtencion = (List<DtoPsSalud>)parametroPaginacionGenerico.ListaResultado;


            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Salud" + UFechaHora.obtenerNombreScat() + ".xlsx";

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
                    worksheet.Cells[1, 3].Value = "Fecha de Informe";
                    worksheet.Cells[1, 4].Value = "Descarte Serológico";
                    worksheet.Cells[1, 5].Value = "Descarte TBC";
                    worksheet.Cells[1, 6].Value = "TBC";
                    worksheet.Cells[1, 7].Value = "HTA";
                    worksheet.Cells[1, 8].Value = "Diabetes";
                    worksheet.Cells[1, 9].Value = "Osteoartrosis";
                    worksheet.Cells[1, 10].Value = "Otras Enfermedades Crónicas";
                    worksheet.Cells[1, 11].Value = "Cognitiva(Pfeiffer)";
                    worksheet.Cells[1, 12].Value = "Afectivo(Yesavege)";
                    worksheet.Cells[1, 13].Value = "Tipo Ayuda";
                    worksheet.Cells[1, 14].Value = "No Evaluado";
                    worksheet.Cells[1, 15].Value = "Comentarios";

                    worksheet.Cells[1, 16].Value = "Ayuda 1";
                    worksheet.Cells[1, 17].Value = "Ayuda 2";
                    worksheet.Cells[1, 18].Value = "ayuda 3";

                    worksheet.Cells[1, 19].Value = "Ayuda Biomecánica 1";
                    worksheet.Cells[1, 20].Value = "Ayuda Biomecánica 2";
                    worksheet.Cells[1, 21].Value = "ayuda Biomecánica 3";

                    worksheet.Cells[1, 22].Value = "Estado Ayuda Biomecánica 1";
                    worksheet.Cells[1, 23].Value = "Estado Ayuda Biomecánica 2";
                    worksheet.Cells[1, 24].Value = "Estado Ayuda Biomecánica 3";

                    worksheet.Cells[1, 25].Value = "Diagnóstico 1";
                    worksheet.Cells[1, 26].Value = "Diagnóstico 2";
                    worksheet.Cells[1, 27].Value = "Diagnóstico 3";

                    worksheet.Cells[1, 28].Value = "Discapacidad 1";
                    worksheet.Cells[1, 29].Value = "Discapacidad 2";
                    worksheet.Cells[1, 30].Value = "Discapacidad 3";
                    worksheet.Cells[1, 31].Value = "Estado 1";
                    worksheet.Cells[1, 32].Value = "Estado 2";
                    worksheet.Cells[1, 33].Value = "Estado 3";
                    worksheet.Cells[1, 34].Value = "Exámen 1";
                    worksheet.Cells[1, 35].Value = "Resultado Exámen 1";
                    worksheet.Cells[1, 36].Value = "Exámen 2";
                    worksheet.Cells[1, 37].Value = "Resultado Exámen 2";
                    worksheet.Cells[1, 38].Value = "Exámen 3";
                    worksheet.Cells[1, 39].Value = "Resultado Exámen 3";
                    worksheet.Cells[1, 40].Value = "Condición 1";
                    worksheet.Cells[1, 41].Value = "Sub Condición 1";
                    worksheet.Cells[1, 42].Value = "Condición 2";
                    worksheet.Cells[1, 43].Value = "Sub Condición 2";
                    worksheet.Cells[1, 44].Value = "Condición 3";
                    worksheet.Cells[1, 45].Value = "Sub Condición 3";

                    worksheet.Cells[1, 46].Value = "Terapia 1";
                    worksheet.Cells[1, 47].Value = "Terapia 2";
                    worksheet.Cells[1, 48].Value = "Terapia 3";
                    worksheet.Cells[1, 49].Value = "Tratamiento 1";
                    worksheet.Cells[1, 50].Value = "Tratamiento 2";
                    worksheet.Cells[1, 51].Value = "Tratamiento 3";

                }
                else {
                    worksheet.Cells[1, 1].Value = "Código";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Fecha de Informe";
                    worksheet.Cells[1, 4].Value = "Hemoglobina";
                    worksheet.Cells[1, 5].Value = "Resultado Hemoglobina";
                    worksheet.Cells[1, 6].Value = "Descarte Serológico";
                    worksheet.Cells[1, 7].Value = "Descarte TBC";
                    worksheet.Cells[1, 8].Value = "Tipo Ayuda";
                    worksheet.Cells[1, 9].Value = "No Evaluado";
                    worksheet.Cells[1, 10].Value = "Comentarios";

                    worksheet.Cells[1, 11].Value = "Ayuda 1";
                    worksheet.Cells[1, 12].Value = "Ayuda 2";
                    worksheet.Cells[1, 13].Value = "ayuda 3";
                    worksheet.Cells[1, 14].Value = "Ayuda Biomecánica 1";
                    worksheet.Cells[1, 15].Value = "Ayuda Biomecánica 2";
                    worksheet.Cells[1, 16].Value = "ayuda Biomecánica 3";
                    worksheet.Cells[1, 17].Value = "Estado Ayuda Biomecánica 1";
                    worksheet.Cells[1, 18].Value = "Estado Ayuda Biomecánica 2";
                    worksheet.Cells[1, 19].Value = "Estado Ayuda Biomecánica 3";
                    worksheet.Cells[1, 20].Value = "Diagnóstico 1";
                    worksheet.Cells[1, 21].Value = "Diagnóstico 2";
                    worksheet.Cells[1, 22].Value = "Diagnóstico 3";
                    worksheet.Cells[1, 23].Value = "Discapacidad 1";
                    worksheet.Cells[1, 24].Value = "Discapacidad 2";
                    worksheet.Cells[1, 25].Value = "Discapacidad 3";
                    worksheet.Cells[1, 26].Value = "Estado 1";
                    worksheet.Cells[1, 27].Value = "Estado 2";
                    worksheet.Cells[1, 28].Value = "Estado 3";
                    worksheet.Cells[1, 29].Value = "Exámen 1";
                    worksheet.Cells[1, 30].Value = "Resultado Exámen 1";
                    worksheet.Cells[1, 31].Value = "Exámen 2";
                    worksheet.Cells[1, 32].Value = "Resultado Exámen 2";
                    worksheet.Cells[1, 33].Value = "Exámen 3";
                    worksheet.Cells[1, 34].Value = "Resultado Exámen 3";
                    worksheet.Cells[1, 35].Value = "Condición 1";
                    worksheet.Cells[1, 36].Value = "Sub Condición 1";
                    worksheet.Cells[1, 37].Value = "Condición 2";
                    worksheet.Cells[1, 38].Value = "Sub Condición 2";
                    worksheet.Cells[1, 39].Value = "Condición 3";
                    worksheet.Cells[1, 40].Value = "Sub Condición 3";
                    worksheet.Cells[1, 41].Value = "Terapia 1";
                    worksheet.Cells[1, 42].Value = "Terapia 2";
                    worksheet.Cells[1, 43].Value = "Terapia 3";
                    worksheet.Cells[1, 44].Value = "Tratamiento 1";
                    worksheet.Cells[1, 45].Value = "Tratamiento 2";
                    worksheet.Cells[1, 46].Value = "Tratamiento 3";

                }


                int fila = 2;

                if (listarAtencion.Count > 0) {
                    foreach (DtoPsSalud obj in listarAtencion) {
                        if (!filtro.esNino) {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.FechaInforme.Value.ToString("dd/MM/yyyy"); ;
                            worksheet.Cells[fila, 4].Value = obj.IdDescarteSerologico;
                            worksheet.Cells[fila, 5].Value = obj.IdDescarteTbc;
                            worksheet.Cells[fila, 6].Value = obj.IdTbc;
                            worksheet.Cells[fila, 7].Value = obj.IdHta;
                            worksheet.Cells[fila, 8].Value = obj.IdDiabetes;
                            worksheet.Cells[fila, 9].Value = obj.IdOsteoatrosis;
                            worksheet.Cells[fila, 10].Value = obj.OtrasEnfermedades;
                            worksheet.Cells[fila, 11].Value = obj.NombreCognitivo;
                            worksheet.Cells[fila, 12].Value = obj.NombreAfectivo;
                            worksheet.Cells[fila, 13].Value = obj.NombreAyudaMedica;
                            worksheet.Cells[fila, 14].Value = obj.Evaluado;
                            worksheet.Cells[fila, 15].Value = obj.Comentarios;

                            worksheet.Cells[fila, 16].Value = obj.Id_Nombre;
                            worksheet.Cells[fila, 17].Value = obj.Id_Nombre2;
                            worksheet.Cells[fila, 18].Value = obj.Id_Nombre3;

                            worksheet.Cells[fila, 19].Value = obj.Id_Nombre_ayuda_biomecanica;
                            worksheet.Cells[fila, 20].Value = obj.Id_Nombre_ayuda_biomecanica2;
                            worksheet.Cells[fila, 21].Value = obj.Id_Nombre_ayuda_biomecanica3;

                            worksheet.Cells[fila, 22].Value = obj.Id_Nombre_estado_ayuda_biomecanica;
                            worksheet.Cells[fila, 23].Value = obj.Id_Nombre_estado_ayuda_biomecanica2;
                            worksheet.Cells[fila, 24].Value = obj.Id_Nombre_estado_ayuda_biomecanica3;

                            worksheet.Cells[fila, 25].Value = obj.Id_Nombre_diagnostico;
                            worksheet.Cells[fila, 26].Value = obj.Id_Nombre_diagnostico2;
                            worksheet.Cells[fila, 27].Value = obj.Id_Nombre_diagnostico3;

                            worksheet.Cells[fila, 28].Value = obj.Id_Nombre_discapacidad;
                            worksheet.Cells[fila, 29].Value = obj.Id_Nombre_discapacidad2;
                            worksheet.Cells[fila, 30].Value = obj.Id_Nombre_discapacidad3;
                            worksheet.Cells[fila, 31].Value = obj.Id_Nombre_estado;
                            worksheet.Cells[fila, 32].Value = obj.Id_Nombre_estado2;
                            worksheet.Cells[fila, 33].Value = obj.Id_Nombre_estado3;
                            worksheet.Cells[fila, 34].Value = obj.Id_Nombre_examen;
                            worksheet.Cells[fila, 35].Value = obj.Id_Nombre_resultado;
                            worksheet.Cells[fila, 36].Value = obj.Id_Nombre_examen2;
                            worksheet.Cells[fila, 37].Value = obj.Id_Nombre_resultado2;
                            worksheet.Cells[fila, 38].Value = obj.Id_Nombre_examen3;
                            worksheet.Cells[fila, 39].Value = obj.Id_Nombre_resultado3;
                            worksheet.Cells[fila, 40].Value = obj.Id_Nombre_condicion;
                            worksheet.Cells[fila, 41].Value = obj.Id_Nombre_sub_condicion;
                            worksheet.Cells[fila, 42].Value = obj.Id_Nombre_condicion2;
                            worksheet.Cells[fila, 43].Value = obj.Id_Nombre_sub_condicion2;
                            worksheet.Cells[fila, 44].Value = obj.Id_Nombre_condicion3;
                            worksheet.Cells[fila, 45].Value = obj.Id_Nombre_sub_condicion3;

                            worksheet.Cells[fila, 46].Value = obj.Id_Nombre_terapia;
                            worksheet.Cells[fila, 47].Value = obj.Id_Nombre_terapia2;
                            worksheet.Cells[fila, 48].Value = obj.Id_Nombre_terapia3;
                            worksheet.Cells[fila, 49].Value = obj.Id_Nombre_tratamiento;
                            worksheet.Cells[fila, 50].Value = obj.Id_Nombre_tratamiento;
                            worksheet.Cells[fila, 51].Value = obj.Id_Nombre_tratamiento;



                        }
                        else {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.FechaInforme.Value.ToString("dd/MM/yyyy"); ;
                            worksheet.Cells[fila, 4].Value = obj.Hemoglobina;
                            worksheet.Cells[fila, 5].Value = obj.HemoglobinaResultado;
                            worksheet.Cells[fila, 6].Value = obj.IdDescarteSerologico;
                            worksheet.Cells[fila, 7].Value = obj.IdDescarteTbc;
                            worksheet.Cells[fila, 8].Value = obj.NombreAyudaMedica;
                            worksheet.Cells[fila, 9].Value = obj.Evaluado;
                            worksheet.Cells[fila, 10].Value = obj.Comentarios;

                            worksheet.Cells[fila, 11].Value = obj.Id_Nombre;
                            worksheet.Cells[fila, 12].Value = obj.Id_Nombre2;
                            worksheet.Cells[fila, 13].Value = obj.Id_Nombre3;

                            worksheet.Cells[fila, 14].Value = obj.Id_Nombre_ayuda_biomecanica;
                            worksheet.Cells[fila, 15].Value = obj.Id_Nombre_ayuda_biomecanica2;
                            worksheet.Cells[fila, 16].Value = obj.Id_Nombre_ayuda_biomecanica3;

                            worksheet.Cells[fila, 17].Value = obj.Id_Nombre_estado_ayuda_biomecanica;
                            worksheet.Cells[fila, 18].Value = obj.Id_Nombre_estado_ayuda_biomecanica2;
                            worksheet.Cells[fila, 19].Value = obj.Id_Nombre_estado_ayuda_biomecanica3;

                            worksheet.Cells[fila, 20].Value = obj.Id_Nombre_diagnostico;
                            worksheet.Cells[fila, 21].Value = obj.Id_Nombre_diagnostico2;
                            worksheet.Cells[fila, 22].Value = obj.Id_Nombre_diagnostico3;

                            worksheet.Cells[fila, 23].Value = obj.Id_Nombre_discapacidad;
                            worksheet.Cells[fila, 24].Value = obj.Id_Nombre_discapacidad2;
                            worksheet.Cells[fila, 25].Value = obj.Id_Nombre_discapacidad3;
                            worksheet.Cells[fila, 26].Value = obj.Id_Nombre_estado;
                            worksheet.Cells[fila, 27].Value = obj.Id_Nombre_estado2;
                            worksheet.Cells[fila, 28].Value = obj.Id_Nombre_estado3;
                            worksheet.Cells[fila, 29].Value = obj.Id_Nombre_examen;
                            worksheet.Cells[fila, 30].Value = obj.Id_Nombre_resultado;
                            worksheet.Cells[fila, 31].Value = obj.Id_Nombre_examen2;
                            worksheet.Cells[fila, 32].Value = obj.Id_Nombre_resultado2;
                            worksheet.Cells[fila, 33].Value = obj.Id_Nombre_examen3;
                            worksheet.Cells[fila, 34].Value = obj.Id_Nombre_resultado3;
                            worksheet.Cells[fila, 35].Value = obj.Id_Nombre_condicion;
                            worksheet.Cells[fila, 36].Value = obj.Id_Nombre_sub_condicion;
                            worksheet.Cells[fila, 37].Value = obj.Id_Nombre_condicion2;
                            worksheet.Cells[fila, 38].Value = obj.Id_Nombre_sub_condicion2;
                            worksheet.Cells[fila, 39].Value = obj.Id_Nombre_condicion3;
                            worksheet.Cells[fila, 40].Value = obj.Id_Nombre_sub_condicion3;

                            worksheet.Cells[fila, 41].Value = obj.Id_Nombre_terapia;
                            worksheet.Cells[fila, 42].Value = obj.Id_Nombre_terapia2;
                            worksheet.Cells[fila, 43].Value = obj.Id_Nombre_terapia3;
                            worksheet.Cells[fila, 44].Value = obj.Id_Nombre_tratamiento;
                            worksheet.Cells[fila, 45].Value = obj.Id_Nombre_tratamiento;
                            worksheet.Cells[fila, 46].Value = obj.Id_Nombre_tratamiento;
                        }



                        fila++;
                    }
                }

                worksheet.Cells["A1:L10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 51]) {
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
