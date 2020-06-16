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

namespace net.royal.spring.programasocial.servicio.impl
{

    public class PsConsumoServicioImpl : GenericoServicioImpl, PsConsumoServicio
    {

        private IServiceProvider servicioProveedor;
        private PsConsumoDao psConsumoDao;
        private PsConsumoNutricionalDao psConsumoNutricionalDao;
        private PsInstitucionDao psInstitucionDao;
        private IHostingEnvironment _hostingEnvironment;


        public PsConsumoServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            psConsumoDao = servicioProveedor.GetService<PsConsumoDao>();
            psConsumoNutricionalDao = servicioProveedor.GetService<PsConsumoNutricionalDao>();
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
            this._hostingEnvironment = hostingEnvironment;

        }

        public PsConsumo coreInsertar(UsuarioActual usuarioActual, PsConsumo bean)
        {

            bean.IdConsumo = psConsumoDao.generarCodigo(bean.IdInstitucion);

            foreach (PsConsumoNutricional deta in bean.listadetalle)
            {
                deta.IdConsumo = bean.IdConsumo;
                deta.IdConsumoNutricional = psConsumoNutricionalDao.generarCodigo(bean.IdInstitucion, bean.IdConsumo);
                //deta.Pregunta = bean.Pregunta;
                if (UString.estaVacio(deta.Estado))
                    deta.Estado = ConstanteEstadoGenerico.ACTIVO;


                psConsumoNutricionalDao.coreInsertar(usuarioActual, deta, deta.Estado);
            }



            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;

            //this.Exportar(bean);

            return psConsumoDao.coreInsertar(usuarioActual, bean, bean.Estado);


        }

        public string Exportar(DtoConsumo bean)
        {

            PsConsumoPk llave = new PsConsumoPk();
            llave.IdInstitucion = bean.codInstitucion;
            llave.IdConsumo = bean.codConsumo;
            List<DtoConsumoNutricional> listado = psConsumoNutricionalDao.listardetalle(llave);

            PsConsumo b = psConsumoDao.obtenerPorId(new PsConsumoPk() { IdConsumo = bean.codConsumo, IdInstitucion = bean.codInstitucion }.obtenerArreglo());





            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Consumo" + UFechaHora.obtenerNombreScat() + ".xlsx";

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

                worksheet.Cells[1, 1].Value = "Código de consumo";
                worksheet.Cells[1, 2].Value = "Institución";
                worksheet.Cells[1, 3].Value = "Descripción";
                worksheet.Cells[1, 4].Value = "Fecha de solicitud";

                worksheet.Cells[1, 5].Value = "Inicio de consumo";
                worksheet.Cells[1, 6].Value = "Fin de consumo";
                worksheet.Cells[1, 7].Value = "Fecha de despacho";
                worksheet.Cells[1, 8].Value = "Aportante";
                worksheet.Cells[1, 9].Value = "Estado";
                worksheet.Cells[1, 10].Value = "Comentario";
                worksheet.Cells[1, 11].Value = "Valoracion";

                worksheet.Cells[2, 1].Value = bean.codConsumo;
                worksheet.Cells[2, 2].Value = bean.nomInstitucion;
                worksheet.Cells[2, 3].Value = bean.descripcion;
                worksheet.Cells[2, 4].Value = String.Format("{0:dd/MM/yyyyy}", bean.fechainicio);
                worksheet.Cells[2, 5].Value = b.inicioConsumo.HasValue ? b.inicioConsumo.Value.ToString("dd/MM/yyyy") : "";
                worksheet.Cells[2, 6].Value = b.finConsumo.HasValue ? b.finConsumo.Value.ToString("dd/MM/yyyy") : "";
                worksheet.Cells[2, 7].Value = b.fechaDespacho.HasValue ? b.fechaDespacho.Value.ToString("dd/MM/yyyy") : "";
                worksheet.Cells[2, 8].Value = b.Aportante == "F" ? "Fundación" : "Otros";
                worksheet.Cells[2, 9].Value = b.Estado == "A" ? "Activo" : b.Estado == "C" ? "Cerrado" : b.Estado == "I" ? "Inactivo" : "";
                worksheet.Cells[2, 10].Value = b.comentario;
                worksheet.Cells[2, 11].Value = b.valoracion;



                worksheet.Cells[4, 1].Value = "Detalle de Consumo";
                worksheet.Cells[4, 1, 4, 7].Merge = true;
                worksheet.Cells[4, 1, 4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                worksheet.Cells[5, 1].Value = "Código de item";
                worksheet.Cells[5, 2].Value = "Grupo";
                worksheet.Cells[5, 3].Value = "Descripción";
                worksheet.Cells[5, 4].Value = "Unidad";
                worksheet.Cells[5, 5].Value = "Cantidad Solicitada";
                worksheet.Cells[5, 6].Value = "Comentarios";

                int fila = 6;

                if (listado.Count > 0)
                {
                    foreach (DtoConsumoNutricional obj in listado)
                    {
                        worksheet.Cells[fila, 1].Value = obj.codItem;
                        worksheet.Cells[fila, 2].Value = obj.grupo;

                        worksheet.Cells[fila, 3].Value = obj.nomItem;
                        worksheet.Cells[fila, 4].Value = obj.nomUnidad;
                        worksheet.Cells[fila, 5].Value = obj.cantidadsolicitada;
                        worksheet.Cells[fila, 6].Value = obj.comentarios;
                        fila++;
                    }
                }

                worksheet.Cells["A1:K10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 11])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }


                using (var cells = worksheet.Cells[4, 1, 4, 6])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                using (var cells = worksheet.Cells[5, 1, 5, 6])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }

        public PsConsumo coreActualizar(UsuarioActual usuarioActual, PsConsumo bean)
        {

            psConsumoDao.coreActualizar(usuarioActual, bean, bean.Estado);

            PsConsumoPk llave = new PsConsumoPk();


            llave.IdInstitucion = bean.IdInstitucion;
            llave.IdConsumo = bean.IdConsumo;
            psConsumoNutricionalDao.eliminardetalle(llave);

            foreach (PsConsumoNutricional deta in bean.listadetalle)
            {
                deta.IdConsumo = bean.IdConsumo;
                deta.IdConsumoNutricional = psConsumoNutricionalDao.generarCodigo(bean.IdInstitucion, bean.IdConsumo);
                if (UString.estaVacio(deta.Estado))
                    deta.Estado = ConstanteEstadoGenerico.ACTIVO;


                psConsumoNutricionalDao.coreInsertar(usuarioActual, deta, deta.Estado);


            }

            return bean;

        }



        public void coreEliminar(PsConsumoPk id)
        {
            psConsumoDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Int32? pIdConsumo)
        {
            PsConsumoPk llave = new PsConsumoPk();


            llave.IdInstitucion = pIdInstitucion;
            llave.IdConsumo = pIdConsumo;
            psConsumoNutricionalDao.eliminardetalle(llave);

            psConsumoDao.coreEliminar(pIdInstitucion, pIdConsumo);
        }


        public PsConsumo obtenerPorId(PsConsumoPk id)
        {


            PsConsumo bean = psConsumoDao.obtenerPorId(id.obtenerArreglo());

            bean.listadetalle = psConsumoNutricionalDao.listadonormal(id);

            return bean;

        }

        public PsConsumo solicitarObtenerporid(PsConsumoPk llave)
        {



            PsConsumo bean = psConsumoDao.obtenerPorId(llave.obtenerArreglo());




            return bean;
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroConsumo filtro)
        {
            return psConsumoDao.listarPaginacion(paginacion, filtro);
        }

        public PsConsumo coreAnular(UsuarioActual usuarioActual, PsConsumoPk id)
        {
            return psConsumoDao.coreAnular(usuarioActual, id);
        }

        public PsConsumo coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdConsumo)
        {
            return psConsumoDao.coreAnular(usuarioActual, pIdInstitucion, pIdConsumo);
        }
    }
}
