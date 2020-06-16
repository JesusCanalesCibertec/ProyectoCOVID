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

using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsNutricionServicioImpl : GenericoServicioImpl, PsNutricionServicio {

        private IServiceProvider servicioProveedor;
        private PsNutricionDao psNutricionDao;
        private IHostingEnvironment _hostingEnvironment;

        public PsNutricionServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            this._hostingEnvironment = hostingEnvironment;
            psNutricionDao = servicioProveedor.GetService<PsNutricionDao>();
        }

        public PsNutricion actualizar(UsuarioActual usuarioActual, PsNutricion bean) {
            return psNutricionDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsNutricion anular(UsuarioActual usuarioActual, string pIdInstitucion, int? pIdBeneficiario, int? pIdNutricion) {
            throw new NotImplementedException();
        }

        public PsNutricion calcular(UsuarioActual usuarioActual, PsNutricion bean) {
            throw new NotImplementedException();
        }

        public List<PsNutricion> consultar(FiltroNutricion filtro) {
            return psNutricionDao.consultar(filtro);
        }

        public ParametroPaginacionGenerico consultarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro) {
            return psNutricionDao.consultarNutricion(paginacion, filtro);
        }

        public PsNutricion guardar(UsuarioActual usuarioActual, PsNutricion bean) {
            throw new NotImplementedException();
        }

        public PsNutricion insertar(UsuarioActual usuarioActual, PsNutricion bean) {

            return psNutricionDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public ParametroPaginacionGenerico listarNutricion(ParametroPaginacionGenerico paginacion, FiltroNutricion filtro) {
            return psNutricionDao.listarNutricion(paginacion, filtro);
        }

        public DtoNutricion obtenerCalculos(DtoNutricion bean) {
            return psNutricionDao.obtenerCalculos(bean);
        }

        public PsNutricion obtenerPorId(string pIdInstitucion, int? pIdBeneficiario, int? pIdNutricion) {

            PsNutricionPk pk = new PsNutricionPk();
            pk.IdBeneficiario = pIdBeneficiario;
            pk.IdInstitucion = pIdInstitucion;
            pk.IdNutricion = pIdNutricion;

            PsNutricion psNutricion = psNutricionDao.obtenerPorId(pk.obtenerArreglo());

            return psNutricion;
        }

        public string Exportar(FiltroNutricion filtro) {

            ParametroPaginacionGenerico parametroPaginacionGenerico = this.consultarNutricion(filtro.paginacion, filtro);
            List<PsNutricion> listarAtencion = (List<PsNutricion>)parametroPaginacionGenerico.ListaResultado;

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Furh" + UFechaHora.obtenerNombreScat() + ".xlsx";

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
                    worksheet.Cells[1, 3].Value = "Peso";
                    worksheet.Cells[1, 4].Value = "Talla";
                    worksheet.Cells[1, 5].Value = "IMC";
                    worksheet.Cells[1, 6].Value = "Prensión Manual";
                    worksheet.Cells[1, 7].Value = " % Variación Peso";
                    worksheet.Cells[1, 8].Value = "Circunferencia Pierna";
                    worksheet.Cells[1, 9].Value = "Valoración";
                    worksheet.Cells[1, 10].Value = "Tipo Dieta";
                    worksheet.Cells[1, 11].Value = "C.por Día";
                    worksheet.Cells[1, 12].Value = "Suplemento";
                    worksheet.Cells[1, 13].Value = "C.por Día";
                    worksheet.Cells[1, 14].Value = "No Evaluado";
                    worksheet.Cells[1, 15].Value = "Comentario";
                }
                else {
     
                    worksheet.Cells[1, 1].Value = "Código";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Peso";
                    worksheet.Cells[1, 4].Value = "Talla";
                    worksheet.Cells[1, 5].Value = "Peso/Edad";
                    worksheet.Cells[1, 6].Value = "Talla/Edad ";
                    worksheet.Cells[1, 7].Value = "Peso/Talla";
                    worksheet.Cells[1, 8].Value = "IMC";
                    worksheet.Cells[1, 9].Value = "Prensión Manual";
                    worksheet.Cells[1, 10].Value = "Valoración";
                    worksheet.Cells[1, 11].Value = "Tipo Dieta";
                    worksheet.Cells[1, 12].Value = "C.por Día";
                    worksheet.Cells[1, 13].Value = "Suplemento";
                    worksheet.Cells[1, 14].Value = "C.por Día";
                    worksheet.Cells[1, 15].Value = "No Evaluado";
                    worksheet.Cells[1, 16].Value = "Comentario";

                }

                int fila = 2;

                if (listarAtencion.Count > 0) {
                    foreach (PsNutricion obj in listarAtencion) {
                        if (!filtro.esNino) {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.Peso;
                            worksheet.Cells[fila, 4].Value = obj.Talla;
                            worksheet.Cells[fila, 5].Value = obj.Imc;
                            worksheet.Cells[fila, 6].Value = obj.NombrePresion;
                            worksheet.Cells[fila, 7].Value = obj.VariacionPeso;

                            worksheet.Cells[fila, 8].Value = obj.NombrePerimetro;
                            worksheet.Cells[fila, 9].Value = obj.NombreValoracionAdultos;
                            worksheet.Cells[fila, 10].Value = obj.NombreTipoDietaAdultos;
                            worksheet.Cells[fila, 11].Value = obj.TipoDietaPorDia;
                            worksheet.Cells[fila, 12].Value = obj.NombreSuplemento;
                            worksheet.Cells[fila, 13].Value = obj.SuplementoNutricionalPorDia;
                            worksheet.Cells[fila, 14].Value = obj.Evaluado;
                            worksheet.Cells[fila, 15].Value = obj.Comentarios;
                        }
                        else {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.Peso;
                            worksheet.Cells[fila, 4].Value = obj.Talla;
                            worksheet.Cells[fila, 5].Value = obj.PesoEdad;
                            worksheet.Cells[fila, 6].Value = obj.TallaEdad;
                            worksheet.Cells[fila, 7].Value = obj.PesoTalla;
                            worksheet.Cells[fila, 8].Value = obj.Imc;
                            worksheet.Cells[fila, 9].Value = obj.NombrePresion;
                            worksheet.Cells[fila, 10].Value = obj.NombreValoracionNinos;
                            worksheet.Cells[fila, 11].Value = obj.NombreTipoDietaNinos;
                            worksheet.Cells[fila, 12].Value = obj.TipoDietaPorDia;
                            worksheet.Cells[fila, 13].Value = obj.NombreSuplemento;
                            worksheet.Cells[fila, 14].Value = obj.SuplementoNutricionalPorDia;
                            worksheet.Cells[fila, 15].Value = obj.Evaluado;
                            worksheet.Cells[fila, 16].Value = obj.Comentarios;

                        }



                        fila++;
                    }
                }

                worksheet.Cells["A1:L10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 15]) {
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
