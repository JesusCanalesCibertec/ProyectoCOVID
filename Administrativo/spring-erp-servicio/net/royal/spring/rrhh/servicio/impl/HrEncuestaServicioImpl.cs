using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.rrhh.dao;
using net.royal.spring.rrhh.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace net.royal.spring.rrhh.servicio.impl
{

    public class HrEncuestaServicioImpl : GenericoServicioImpl, HrEncuestaServicio
    {

        private IServiceProvider servicioProveedor;
        private HrEncuestaDao hrEncuestaDao;
        private HrEncuestadetalleDao hrEncuestadetalleDao;
        private HrEncuestapreguntavaloresDao hrEncuestapreguntavaloresDao;
        private HrEncuestasujetoDao hrEncuestasujetoDao;
        private HrEncuestapreguntaDao hrEncuestapreguntaDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private MaMiscelaneosheaderDao maMiscelaneosheaderDao;
        private IHostingEnvironment _hostingEnvironment;
        private AfemstDao afemstDao;

        public HrEncuestaServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            hrEncuestaDao = servicioProveedor.GetService<HrEncuestaDao>();
            hrEncuestadetalleDao = servicioProveedor.GetService<HrEncuestadetalleDao>();
            hrEncuestapreguntavaloresDao = servicioProveedor.GetService<HrEncuestapreguntavaloresDao>();
            hrEncuestasujetoDao = servicioProveedor.GetService<HrEncuestasujetoDao>();
            hrEncuestapreguntaDao = servicioProveedor.GetService<HrEncuestapreguntaDao>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            maMiscelaneosheaderDao = servicioProveedor.GetService<MaMiscelaneosheaderDao>();
            afemstDao = servicioProveedor.GetService<AfemstDao>();
            this._hostingEnvironment = hostingEnvironment;

        }

        public HrEncuesta coreInsertar(UsuarioActual usuarioActual, HrEncuesta bean)
        {
            return hrEncuestaDao.coreInsertar(usuarioActual, bean);
        }

        public HrEncuesta coreActualizar(UsuarioActual usuarioActual, HrEncuesta bean)
        {
            return hrEncuestaDao.coreActualizar(usuarioActual, bean);
        }


        public void coreEliminar(HrEncuestaPk id)
        {
            hrEncuestaDao.coreEliminar(id);
        }

        public void coreEliminar(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            hrEncuestaDao.coreEliminar(pCompanyowner, pPeriodo, pSecuencia);
        }


        public HrEncuesta obtenerPorId(HrEncuestaPk id)
        {
            return hrEncuestaDao.obtenerPorId(id.obtenerArreglo());
        }

        public HrEncuesta obtenerPorId(String pCompanyowner, String pPeriodo, Nullable<Int32> pSecuencia)
        {
            return hrEncuestaDao.obtenerPorId(pCompanyowner, pPeriodo, pSecuencia);
        }

        public ParametroPaginacionGenerico listar(FiltroPaginacionEncuesta filtroPaginacion)
        {
            return hrEncuestaDao.listar(filtroPaginacion);
        }

        public DtoEncuesta obtenerPlantilla(HrEncuestaPk hrEncuestaPk)
        {
            var dto = new DtoEncuesta();
            var seleccionar = new SelectItem() { label = " -- Seleccionar -- ", value = null };

            var cabecera = this.obtenerPorId(hrEncuestaPk);
            if (cabecera != null)
            {
                dto.secuencia = cabecera.Secuencia;
                dto.periodo = cabecera.Periodo;
                dto.titulo = cabecera.Titulo;
                dto.compania = cabecera.Companyowner;
                dto.desde = cabecera.Fechainicio;
                dto.hasta = cabecera.Fechafin;
                dto.observaciones = cabecera.Observaciones;
                dto.instrucciones = cabecera.Instrucciones;
                dto.idPrograma = cabecera.idPrograma;
                dto.tipoDescripcion = cabecera.Tipo;

                var preguntas = hrEncuestadetalleDao.obtenerPreguntas(hrEncuestaPk);
                var areas = hrEncuestadetalleDao.obtenerAreas(hrEncuestaPk);

                foreach (var pregunta in preguntas)
                {
                    pregunta.valores = hrEncuestapreguntavaloresDao.obtenerValores(new HrEncuestapreguntaPk() { Pregunta = pregunta.pregunta });

                    var areaActual = (areas as List<DtoTabla>).Find(x => x.Codigo == UString.trimNotNull(pregunta.area));

                    if (areaActual == null)
                    {
                        areaActual = new DtoTabla() { Codigo = "OTRO", Descripcion = "OTROS" };
                    }

                    var areaIngresada = (dto.areas as List<DtoArea>).Find(x => x.codigo == areaActual.Codigo);

                    if (areaIngresada == null)
                    {
                        var nuevaArea = new DtoArea() { codigo = areaActual.Codigo, nombre = areaActual.Descripcion, grupo = pregunta.grupo };
                        nuevaArea.preguntas.Add(pregunta);
                        dto.areas.Add(nuevaArea);
                        continue;
                    }

                    areaIngresada.preguntas.Add(pregunta);

                    if (cabecera.Tipo == "GUIAIN")
                    {
                        var grupoActual = dto.grupos.Find(x => x.codigo == pregunta.grupo);
                        if (grupoActual == null)
                        {
                            dto.grupos.Add(new DtoGrupo() { codigo = pregunta.grupo, nombre = pregunta.grupo == "F" ? "Funcional" : "Estructural", areas = new List<DtoArea>() });
                        }
                    }
                }



                if (!UString.estaVacio(cabecera.idMiscelaneoHeader1))
                {
                    var cab = maMiscelaneosheaderDao.obtenerPorId(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader1 });
                    dto.titulo1 = cab.Descripcionlocal;

                    var l1 = maMiscelaneosdetalleDao.listarActivos(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader1 });
                    dto.listaMiscelaneo1.Add(seleccionar);

                    foreach (var item in l1)
                    {
                        dto.listaMiscelaneo1.Add(new SelectItem() { label = item.Descripcionlocal, value = item.Codigoelemento.Trim() });
                    }
                }

                if (!UString.estaVacio(cabecera.idMiscelaneoHeader2))
                {
                    var cab = maMiscelaneosheaderDao.obtenerPorId(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader2 });
                    dto.titulo2 = cab.Descripcionlocal;

                    var l1 = maMiscelaneosdetalleDao.listarActivos(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader2 });
                    dto.listaMiscelaneo2.Add(seleccionar);
                    foreach (var item in l1)
                    {
                        dto.listaMiscelaneo2.Add(new SelectItem() { label = item.Descripcionlocal, value = item.Codigoelemento.Trim() });
                    }
                }

                if (!UString.estaVacio(cabecera.idMiscelaneoHeader3))
                {
                    var cab = maMiscelaneosheaderDao.obtenerPorId(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader3 });
                    dto.titulo3 = cab.Descripcionlocal;

                    var l1 = maMiscelaneosdetalleDao.listarActivos(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader3 });
                    dto.listaMiscelaneo3.Add(seleccionar);
                    foreach (var item in l1)
                    {
                        dto.listaMiscelaneo3.Add(new SelectItem() { label = item.Descripcionlocal, value = item.Codigoelemento.Trim() });
                    }
                }

                if (!UString.estaVacio(cabecera.idMiscelaneoHeader4))
                {
                    var cab = maMiscelaneosheaderDao.obtenerPorId(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader4 });
                    dto.titulo4 = cab.Descripcionlocal;

                    var l1 = maMiscelaneosdetalleDao.listarActivos(new MaMiscelaneosheaderPk() { Aplicacioncodigo = "PS", Compania = "999999", Codigotabla = cabecera.idMiscelaneoHeader4 });
                    dto.listaMiscelaneo4.Add(seleccionar);
                    foreach (var item in l1)
                    {
                        dto.listaMiscelaneo4.Add(new SelectItem() { label = item.Descripcionlocal, value = item.Codigoelemento.Trim() });
                    }

                }

                return dto;
            }

            throw new UException("No se encuentra la plantilla seleccionada");
        }

        public DtoEncuesta solicitudRegistrar(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual)
        {
            //actualizar muestra
            HrEncuesta hrEncuesta = this.obtenerPorId(new HrEncuestaPk() { Companyowner = dtoEncuesta.compania, Periodo = dtoEncuesta.periodo, Secuencia = dtoEncuesta.secuencia });
            hrEncuesta.Muestra = UInteger.esCeroOrNulo(hrEncuesta.Muestra) ? 1 : hrEncuesta.Muestra.Value + 1;
            hrEncuestaDao.actualizar(hrEncuesta);
            //insertar en HR_ENCUESTASUJETO
            var sujeto = hrEncuestasujetoDao.autogenerarSujetoPorEncuesta(hrEncuesta as HrEncuestaPk);
            var ahoa = DateTime.Now;
            foreach (var area in dtoEncuesta.areas)
            {
                foreach (var pregunta in area.preguntas)
                {
                    HrEncuestasujeto hrEncuestasujeto = new HrEncuestasujeto();
                    hrEncuestasujeto.Secuencia = dtoEncuesta.secuencia;
                    hrEncuestasujeto.Periodo = dtoEncuesta.periodo;
                    hrEncuestasujeto.Companyowner = dtoEncuesta.compania;
                    hrEncuestasujeto.Orden = pregunta.orden;

                    hrEncuestasujeto.Tipo = pregunta.tipo;
                    hrEncuestasujeto.Valor = pregunta.valor;
                    hrEncuestasujeto.Pregunta = pregunta.pregunta;
                    hrEncuestasujeto.Observacion = pregunta.comentario;

                    hrEncuestasujeto.Sujeto = sujeto;
                    hrEncuestasujeto.Edad = dtoEncuesta.edad;
                    hrEncuestasujeto.Numero = dtoEncuesta.numero;
                    hrEncuestasujeto.Sexo = dtoEncuesta.sexo;

                    hrEncuestasujeto.miscelaneo1 = dtoEncuesta.miscelaneo1;
                    hrEncuestasujeto.miscelaneo2 = dtoEncuesta.miscelaneo2;
                    hrEncuestasujeto.miscelaneo3 = dtoEncuesta.miscelaneo3;
                    hrEncuestasujeto.miscelaneo4 = dtoEncuesta.miscelaneo4;

                    hrEncuestasujeto.idInstitucion = dtoEncuesta.institucion;
                    hrEncuestasujeto.idInstitucionArea = dtoEncuesta.idInstitucionArea;
                    //en caso sea de satisfaccion el programa ya es elegido
                    if (dtoEncuesta.tipoDescripcion == "SATIS")
                    {
                        hrEncuestasujeto.idPrograma = hrEncuesta.idPrograma;
                    }
                    else
                    {
                        hrEncuestasujeto.idPrograma = dtoEncuesta.idPrograma;
                    }
                    hrEncuestasujeto.idComponente = dtoEncuesta.idComponente;
                    hrEncuestasujeto.TipoGuiain = dtoEncuesta.tipoGuia;
                    hrEncuestasujeto.afe = dtoEncuesta.afe;

                    //sino marco la pregunta no se guarda
                    if (UString.estaVacio(pregunta.valor) && UString.estaVacio(pregunta.comentario))
                    {
                        continue;
                    }
                    hrEncuestasujetoDao.coreInsertar(usuarioActual, hrEncuestasujeto, ahoa);
                }
            }

            return dtoEncuesta;
        }

        public ParametroPaginacionGenerico listarSujetos(FiltroPaginacionSujeto filtroPaginacion)
        {
            return hrEncuestaDao.listarSujetos(filtroPaginacion);
        }

        public DtoEncuesta obtenerEncuesta(HrEncuestaPk hrEncuestaPk, int? pSujeto)
        {
            var seleccionar = new SelectItem() { label = " -- Seleccionar -- ", value = null };

            var cabecera = this.obtenerPorId(hrEncuestaPk);

            var plantilla = this.obtenerPlantilla(hrEncuestaPk);

            foreach (var area in plantilla.areas)
            {
                foreach (var pregunta in area.preguntas)
                {
                    var preguntaSujeto = hrEncuestasujetoDao.obtenerPorId(new HrEncuestasujetoPk() { Pregunta = pregunta.pregunta, Secuencia = hrEncuestaPk.Secuencia, Sujeto = pSujeto, Companyowner = hrEncuestaPk.Companyowner, Periodo = hrEncuestaPk.Periodo }.obtenerArreglo());
                    if (preguntaSujeto != null)
                    {
                        plantilla.sexo = preguntaSujeto.Sexo;
                        plantilla.edad = preguntaSujeto.Edad;
                        plantilla.numero = preguntaSujeto.Numero;

                        plantilla.miscelaneo1 = preguntaSujeto.miscelaneo1;
                        plantilla.miscelaneo2 = preguntaSujeto.miscelaneo2;
                        plantilla.miscelaneo3 = preguntaSujeto.miscelaneo3;
                        plantilla.miscelaneo4 = preguntaSujeto.miscelaneo4;

                        plantilla.institucion = preguntaSujeto.idInstitucion;
                        plantilla.idInstitucionArea = preguntaSujeto.idInstitucionArea;
                        plantilla.idComponente = preguntaSujeto.idComponente;
                        plantilla.idPrograma = preguntaSujeto.idPrograma;


                        plantilla.tipoGuia = preguntaSujeto.TipoGuiain;
                        plantilla.afe = preguntaSujeto.afe;



                        pregunta.sujeto = preguntaSujeto.Sujeto;

                        if (!UString.estaVacio(preguntaSujeto.Valor) && (pregunta.tipo == "M" || pregunta.tipo == "N"))
                        {
                            foreach (var valor in pregunta.valores)
                            {
                                var valores = preguntaSujeto.Valor.Split(",");

                                var esta = false;

                                foreach (var v in valores)
                                {
                                    if (v == valor.value.ToString())
                                    {
                                        esta = true;
                                    }
                                }

                                valor.internalValue = esta ? true : false;
                            }
                        }
                        else
                        {
                            pregunta.valor = preguntaSujeto.Valor;
                            pregunta.comentario = preguntaSujeto.Observacion;
                        }
                    }
                }
            }


            if (!UString.estaVacio(plantilla.afe))
            {
                plantilla.afeNombre = afemstDao.obtenerPorId(new AfemstPk() { Afe = plantilla.afe }.obtenerArreglo()).Localname;
            }

            return plantilla;
        }

        public DtoEncuesta solicitudActualizar(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual)
        {
            foreach (var area in dtoEncuesta.areas)
            {
                foreach (var pregunta in area.preguntas)
                {
                    var preguntaSujeto = hrEncuestasujetoDao.obtenerPorId(new HrEncuestasujetoPk() { Pregunta = pregunta.pregunta, Secuencia = dtoEncuesta.secuencia, Sujeto = pregunta.sujeto, Companyowner = dtoEncuesta.compania, Periodo = dtoEncuesta.periodo }.obtenerArreglo());

                    preguntaSujeto.Valor = pregunta.valor;
                    preguntaSujeto.Observacion = pregunta.comentario;
                    preguntaSujeto.Numero = dtoEncuesta.numero;

                    preguntaSujeto.Edad = "" + dtoEncuesta.edad;
                    preguntaSujeto.Sexo = dtoEncuesta.sexo;

                    preguntaSujeto.miscelaneo1 = dtoEncuesta.miscelaneo1;
                    preguntaSujeto.miscelaneo2 = dtoEncuesta.miscelaneo2;
                    preguntaSujeto.miscelaneo3 = dtoEncuesta.miscelaneo3;
                    preguntaSujeto.miscelaneo4 = dtoEncuesta.miscelaneo4;

                    preguntaSujeto.idComponente = dtoEncuesta.idComponente;
                    preguntaSujeto.TipoGuiain = dtoEncuesta.tipoGuia;
                    preguntaSujeto.afe = dtoEncuesta.afe;
                    preguntaSujeto.idInstitucionArea = dtoEncuesta.idInstitucionArea;

                    hrEncuestasujetoDao.coreActualizar(usuarioActual, preguntaSujeto);
                }
            }
            return dtoEncuesta;
        }

        public HrEncuesta obtenerCompleto(HrEncuestaPk pk)
        {
            var bean = hrEncuestaDao.obtenerPorId(pk.obtenerArreglo());
            bean.listaPreguntas = hrEncuestadetalleDao.listarPorEncuesta(pk);
            foreach (var item in bean.listaPreguntas)
            {
                var pregunta = hrEncuestapreguntaDao.obtenerPorId(new HrEncuestapreguntaPk() { Pregunta = item.Pregunta }.obtenerArreglo());
                item.auxPregunta = pregunta.Descripcion;
                if (!UString.estaVacio(pregunta.Tipoencuesta))
                {
                    switch (pregunta.Tipoencuesta)
                    {
                        case "CLIMA":
                            item.auxArea = maMiscelaneosdetalleDao.obtenerDescripcion("HR", "AREASCLIMA", pregunta.Area);
                            break;
                        case "GUIAIN":
                            item.auxArea = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "ARGOINF", pregunta.Area);
                            break;
                        case "SATIS":
                            item.auxArea = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "AREAENCSAT", pregunta.Area);
                            break;
                        default:
                            break;
                    }
                }
            }

            return bean;
        }

        public HrEncuesta solicitudRegistrarBean(HrEncuesta bean, UsuarioActual usuarioActual)
        {
            bean.Periodo = bean.Periodo.Replace("-", "");
            bean.Secuencia = hrEncuestaDao.generarSecuencia(bean as HrEncuestaPk);
            hrEncuestaDao.coreInsertar(usuarioActual, bean);

            foreach (var item in bean.listaPreguntas)
            {
                item.Companyowner = bean.Companyowner;
                item.Periodo = bean.Periodo;
                item.Secuencia = bean.Secuencia;
                hrEncuestadetalleDao.coreInsertar(usuarioActual, item);
            }
            return bean;
        }

        public HrEncuesta solicitudActualizarBean(HrEncuesta bean, UsuarioActual usuarioActual)
        {
            hrEncuestaDao.coreActualizar(usuarioActual, bean);

            hrEncuestadetalleDao.eliminarPorEncuesta(bean as HrEncuestaPk);

            foreach (var item in bean.listaPreguntas)
            {
                item.Companyowner = bean.Companyowner;
                item.Periodo = bean.Periodo;
                item.Secuencia = bean.Secuencia;
                hrEncuestadetalleDao.coreInsertar(usuarioActual, item);
            }
            return bean;
        }

        public DtoEncuesta actualizarEjecucion(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual)
        {
            var hrencuesta = hrEncuestaDao.obtenerPorId(new HrEncuestaPk() { Companyowner = dtoEncuesta.compania, Periodo = dtoEncuesta.periodo, Secuencia = dtoEncuesta.secuencia }.obtenerArreglo());
            if (hrencuesta != null)
            {
                hrencuesta.Fechadesde = dtoEncuesta.desde;
                hrencuesta.Fechahasta = dtoEncuesta.hasta;
                hrencuesta.Estado = "E";
                hrEncuestaDao.coreActualizar(usuarioActual, hrencuesta);
            }
            return dtoEncuesta;
        }

        public DtoEncuesta anularEncuesta(DtoEncuesta dtoEncuesta, UsuarioActual usuarioActual)
        {
            var hrencuesta = hrEncuestaDao.obtenerPorId(new HrEncuestaPk() { Companyowner = dtoEncuesta.compania, Periodo = dtoEncuesta.periodo, Secuencia = dtoEncuesta.secuencia }.obtenerArreglo());
            if (hrencuesta != null)
            {
                hrencuesta.Estado = "R";
                hrEncuestaDao.coreActualizar(usuarioActual, hrencuesta);
            }
            return dtoEncuesta;
        }

        public List<DtoEncuestaAnalisis> analizarEncuesta(FiltroEncuestaAnalisis filtro)
        {
            return hrEncuestaDao.analizarEncuesta(filtro);
        }

        public string exportarEncuesta(FiltroEncuestaAnalisis filtro)
        {
            List<DtoEncuestaAnalisis> listado = hrEncuestaDao.analizarEncuesta(filtro);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Analisis de Encuesta " + UFechaHora.obtenerNombreScat() + ".xlsx";

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
                worksheet.Cells[1, 1].Value = "¨Área";
                worksheet.Cells[1, 2].Value = "Pregunta";
                worksheet.Cells[1, 3].Value = "Respuesta";
                worksheet.Cells[1, 4].Value = "Cantidad";

                int fila = 2;

                if (listado.Count > 0)
                {
                    foreach (DtoEncuestaAnalisis obj in listado)
                    {
                        worksheet.Cells[fila, 1].Value = obj.area;
                        worksheet.Cells[fila, 2].Value = obj.pregunta;
                        worksheet.Cells[fila, 3].Value = obj.respuesta;
                        worksheet.Cells[fila, 4].Value = obj.cantidad;
                        fila++;
                    }
                }

                worksheet.Cells["A1:D10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 4])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }

        public DtoEncuesta anularSujeto(DtoEncuestaSujeto dto)
        {
            return hrEncuestasujetoDao.anularSujeto(dto);
        }

        public DtoEncuesta copiar(HrEncuestaPk pk)
        {
            return hrEncuestasujetoDao.copiar(pk);
        }

        public ParametroPaginacionGenerico consulta(FiltroPaginacionEncuesta filtroPaginacion)
        {
            return hrEncuestaDao.consulta(filtroPaginacion);
        }
    }
}
