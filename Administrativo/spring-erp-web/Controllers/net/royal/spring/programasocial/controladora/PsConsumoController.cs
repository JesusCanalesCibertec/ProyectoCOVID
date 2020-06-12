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
using net.royal.spring.framework.web.controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net.royal.spring.rrhh.dominio.dto;
using System.IO;
using OfficeOpenXml;

namespace net.royal.spring.programasocial.controladora
{

    [Route("api/spring/programasocial/[controller]")]
    public class PsConsumoController : SecuredBaseController
    {

        private IServiceProvider servicioProveedor;
        private PsConsumoServicio psConsumoServicio;

        public PsConsumoController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            psConsumoServicio = servicioProveedor.GetService<PsConsumoServicio>();
        }

        [HttpPost("[action]")]
        public PsConsumo registrar([FromBody]PsConsumo bean)
        {
            return psConsumoServicio.coreInsertar(_usuarioActual, bean);
        }

        [HttpPost("[action]")]
        public PsConsumo actualizar([FromBody]PsConsumo bean)
        {
            return psConsumoServicio.coreActualizar(_usuarioActual, bean);
        }


        [HttpPost("[action]")]
        public DtoConsumo eliminar([FromBody] DtoConsumo bean)
        {
            psConsumoServicio.coreEliminar(bean.codInstitucion, bean.codConsumo);
            return bean;
        }

        [HttpPost("[action]")]
        public PsConsumo solicitarObtenerporid([FromBody] PsConsumoPk llave)
        {
            return psConsumoServicio.solicitarObtenerporid(llave);
        }



        [HttpPost("[action]")]
        public ParametroPaginacionGenerico listarPaginacion([FromBody] FiltroConsumo filtro)
        {
            return psConsumoServicio.listarPaginacion(filtro.paginacion, filtro);

        }

        [HttpPost("[action]")]
        public PsConsumo obtenerPorId([FromBody] PsConsumoPk id)
        {
            return psConsumoServicio.obtenerPorId(id);
        }

        [HttpPost("[action]")]
        public JsonResult exportar([FromBody] DtoConsumo bean)
        {


            return Json(new { url = psConsumoServicio.Exportar(bean) });
        }


        [HttpGet("[action]")]
        public PsConsumo anular(String pIdInstitucion, Nullable<Int32> pIdConsumo)
        {
            return psConsumoServicio.coreAnular(_usuarioActual, pIdInstitucion, pIdConsumo);
        }


        [HttpPost("[action]")]
        public List<DtoConsumoNutricional> importar([FromBody] DtoConsumoCarga bean)
        {
            List<DtoConsumoNutricional> lista = new List<DtoConsumoNutricional>();
            //create a new Excel package in a memorystream
            var inicial = 6;
            try
            {
                using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(bean.excel)))
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    //loop all worksheets7

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                    for (; inicial < 1000000; inicial++)
                    {
                        //add the cell data to the List
                        if (worksheet.Cells[inicial, 1].Value != null)
                        {
                            DtoConsumoNutricional dto = new DtoConsumoNutricional();

                            dto.codItem = (worksheet.Cells[inicial, 1].Value.ToString());

                            if (worksheet.Cells[inicial, 2].Value != null)
                            {
                                dto.nomItem = (worksheet.Cells[inicial, 2].Value.ToString());
                            }

                            if (worksheet.Cells[inicial, 4].Value != null)
                            {
                                dto.nomUnidad = (worksheet.Cells[inicial, 4].Value.ToString());
                            }

                            if (worksheet.Cells[inicial, 5].Value != null)
                            {
                                dto.cantidadsolicitada = (Convert.ToDecimal(worksheet.Cells[inicial, 5].Value.ToString()));
                            }
                            lista.Add(dto);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new UException("No se pudo cargar el archivo, error en el formato del archivo");
            }

            return lista;
        }
    }
}
