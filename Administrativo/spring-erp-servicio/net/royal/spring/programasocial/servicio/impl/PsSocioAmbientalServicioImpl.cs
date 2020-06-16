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

    public class PsSocioAmbientalServicioImpl : GenericoServicioImpl, PsSocioAmbientalServicio {

        private IServiceProvider servicioProveedor;
        private PsSocioAmbientalDao psSocioAmbientalDao;
        private IHostingEnvironment _hostingEnvironment;

        public PsSocioAmbientalServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            this._hostingEnvironment = hostingEnvironment;
            psSocioAmbientalDao = servicioProveedor.GetService<PsSocioAmbientalDao>();
        }

        public PsSocioAmbiental coreInsertar(UsuarioActual usuarioActual, PsSocioAmbiental bean) {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psSocioAmbientalDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PsSocioAmbiental coreActualizar(UsuarioActual usuarioActual, PsSocioAmbiental bean) {
            return psSocioAmbientalDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, PsSocioAmbientalPk id) {
            return psSocioAmbientalDao.coreAnular(usuarioActual, id);
        }

        public PsSocioAmbiental coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            return psSocioAmbientalDao.coreAnular(usuarioActual, pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental);
        }

        public void coreEliminar(PsSocioAmbientalPk id) {
            psSocioAmbientalDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            psSocioAmbientalDao.coreEliminar(pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental);
        }


        public PsSocioAmbiental obtenerPorId(PsSocioAmbientalPk id) {
            return psSocioAmbientalDao.obtenerPorId(id.obtenerArreglo());
        }

        public PsSocioAmbiental obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSocioAmbiental) {
            return psSocioAmbientalDao.obtenerPorId(pIdInstitucion, pIdBeneficiario, pIdSocioAmbiental);
        }

        public ParametroPaginacionGenerico listarSocioAmbiental(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro) {
            return psSocioAmbientalDao.listarSocioAmbiental(paginacion, filtro);
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSocioAmbiental filtro) {
            return psSocioAmbientalDao.listarPaginacionConsulta(paginacion, filtro);
        }

        public string Exportar(FiltroSocioAmbiental filtro) {

            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarPaginacionConsulta(filtro.paginacion, filtro);
            List<PsSocioAmbiental> listarAtencion = (List<PsSocioAmbiental>)parametroPaginacionGenerico.ListaResultado;

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Socio Ambiental" + UFechaHora.obtenerNombreScat() + ".xlsx";

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
                    worksheet.Cells[1, 3].Value = "Integración";
                    worksheet.Cells[1, 4].Value = "Comunicación";
                    worksheet.Cells[1, 5].Value = "Participación";
                    worksheet.Cells[1, 6].Value = "Cooperación";
                    worksheet.Cells[1, 7].Value = "Asertividad";
                    worksheet.Cells[1, 8].Value = "Empatía";
                    worksheet.Cells[1, 9].Value = "Comentario";
                }
                else {
                    worksheet.Cells[1, 1].Value = "Código";
                    worksheet.Cells[1, 2].Value = "Nombre Completo";
                    worksheet.Cells[1, 3].Value = "Resol.Conflictos";
                    worksheet.Cells[1, 4].Value = "Autoc. Emocional";
                    worksheet.Cells[1, 5].Value = "Comunicación";
                    worksheet.Cells[1, 6].Value = "Cooperación";
                    worksheet.Cells[1, 7].Value = "Asertividad";
                    worksheet.Cells[1, 8].Value = "Empatía";
                    worksheet.Cells[1, 9].Value = "Comentario";

                }


                int fila = 2;

                if (listarAtencion.Count > 0) {
                    foreach (PsSocioAmbiental obj in listarAtencion) {

                        if (!filtro.esNino) {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.NombreIntegracion;
                            worksheet.Cells[fila, 4].Value = obj.NombreComunicacion;
                            worksheet.Cells[fila, 5].Value = obj.NombreParticipacion;
                            worksheet.Cells[fila, 6].Value = obj.NombreCooperacion;
                            worksheet.Cells[fila, 7].Value = obj.NombreAsertividad;
                            worksheet.Cells[fila, 8].Value = obj.NombreEmpatia;
                            worksheet.Cells[fila, 9].Value = obj.Comentarios;
                        }
                        else {
                            worksheet.Cells[fila, 1].Value = obj.IdBeneficiario;
                            worksheet.Cells[fila, 2].Value = obj.NombreCompleto;
                            worksheet.Cells[fila, 3].Value = obj.NombreConflictos;
                            worksheet.Cells[fila, 4].Value = obj.NombreEmocional;
                            worksheet.Cells[fila, 5].Value = obj.NombreComunicacion;
                            worksheet.Cells[fila, 6].Value = obj.NombreCooperacion;
                            worksheet.Cells[fila, 7].Value = obj.NombreAsertividad;
                            worksheet.Cells[fila, 8].Value = obj.NombreEmpatia;
                            worksheet.Cells[fila, 9].Value = obj.Comentarios;

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
