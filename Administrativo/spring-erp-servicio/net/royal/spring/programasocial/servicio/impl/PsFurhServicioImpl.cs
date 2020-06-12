using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio.dtoscomponente;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace net.royal.spring.programasocial.servicio.impl
{

    public class PsFurhServicioImpl : GenericoServicioImpl, PsFurhServicio
    {

        private IServiceProvider servicioProveedor;
        private PsEntidadDao psEntidadDao;
        private PsEmpleadoDao psEmpleadoDao;
        private IHostingEnvironment _hostingEnvironment;
        private PsInstitucionDao psInstitucionDao;

        public PsFurhServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment)
        {
            servicioProveedor = _servicioProveedor;
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
            this._hostingEnvironment = hostingEnvironment;
            psEmpleadoDao = servicioProveedor.GetService<PsEmpleadoDao>();
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
        }

        public DtoComponenteFurh actualizar(UsuarioActual usuarioActual, DtoComponenteFurh bean)
        {

            PsEntidad psEntidad = this.psEntidadDao.obtenerPorId(new PsEntidadPk(bean.IdEmpleado).obtenerArreglo());
            psEntidad.ApellidoMaterno = bean.ApellidoMaterno;
            psEntidad.ApellidoPaterno = bean.ApellidoPaterno;
            psEntidad.Nombres = bean.Nombres;
            psEntidad.FechaNacimiento = bean.Fechanacimiento;
            psEntidad.IdAreaTrabajo = bean.IdAreaTrabajo;
            psEntidad.Correo1 = bean.Correo1;
            psEntidad.CreacionFecha = DateTime.Now;
            psEntidad.Estado = bean.Estado;
            psEntidad.IdDiscapacidad = bean.IdDiscapacidad;
            psEntidad.IdEntidad = bean.IdEntidad;
            psEntidad.IdEspecialidadAcademica = bean.IdEspecialidadAcademica;
            psEntidad.IdNivelAcademico = bean.IdNivelAcademico;
            psEntidad.IdSexo = bean.IdSexo;
            psEntidad.Nombrecompleto = traerNombreCompleto(bean);
            psEntidad.Documento = bean.Documento;
            psEntidad.IdTipoDocumento = bean.IdTipoDocumento;
            psEntidad.Comentarios = bean.comentarios;
            this.psEntidadDao.actualizar(psEntidad);

            PsEmpleado psEmpleado = this.psEmpleadoDao.obtenerPorId(new PsEmpleadoPk(bean.IdInstitucion, bean.IdEmpleado).obtenerArreglo());
            psEmpleado.IdEmpleado = bean.IdEmpleado;
            psEmpleado.FechaIngreso = bean.FechaIngreso;
            psEmpleado.IdInstitucion = bean.IdInstitucion;
            psEmpleado.Estado = bean.Estado;
            psEmpleado.CodigoUsuario = bean.CodigoUsuario;
            this.psEmpleadoDao.actualizar(psEmpleado);

            bean.IdEntidad = psEntidad.IdEntidad;
            bean.IdEmpleado = psEmpleado.IdEmpleado;

            return bean;
        }

        public DtoComponenteFurh anular(UsuarioActual usuarioActual, string idInstitucion, int? idEmpleado)
        {
            PsEntidad psEntidad = this.psEntidadDao.obtenerPorId(new PsEntidadPk(idEmpleado).obtenerArreglo());
            PsEmpleado psEmpleado = this.psEmpleadoDao.obtenerPorId(new PsEmpleadoPk(idInstitucion, idEmpleado).obtenerArreglo());

            psEntidad.Estado = "I";
            psEmpleado.Estado = "I";
            this.psEntidadDao.actualizar(psEntidad);
            this.psEmpleadoDao.actualizar(psEmpleado);

            DtoComponenteFurh bean = new DtoComponenteFurh();
            bean.IdEmpleado = idEmpleado;

            return bean;
        }

        public List<DtoComponenteFurh> consultar(string idInstitucion, FiltroFurh filtro)
        {
            throw new NotImplementedException();
        }

        public DtoComponenteFurh insertar(UsuarioActual usuarioActual, DtoComponenteFurh bean)
        {
            PsEntidad psEntidad = new PsEntidad();
            psEntidad.ApellidoMaterno = bean.ApellidoMaterno;
            psEntidad.ApellidoPaterno = bean.ApellidoPaterno;
            psEntidad.Nombres = bean.Nombres;
            psEntidad.FechaNacimiento = bean.Fechanacimiento;
            psEntidad.Correo1 = bean.Correo1;
            psEntidad.CreacionFecha = DateTime.Now;
            psEntidad.IdGrupoSanguineo = bean.IdGrupoSanguineo;
            psEntidad.IdFactorRh = bean.IdFactorRh;
            psEntidad.Estado = bean.Estado;
            psEntidad.IdAreaTrabajo = bean.IdAreaTrabajo;
            psEntidad.IdDiscapacidad = bean.IdDiscapacidad;
            psEntidad.IdEntidad = this.psEntidadDao.generarCodigo();
            psEntidad.IdEspecialidadAcademica = bean.IdEspecialidadAcademica;
            psEntidad.IdNivelAcademico = bean.IdNivelAcademico;
            psEntidad.IdSexo = bean.IdSexo;
            psEntidad.Nombrecompleto = traerNombreCompleto(bean);
            psEntidad.Documento = bean.Documento;
            psEntidad.IdTipoDocumento = bean.IdTipoDocumento;
            psEntidad.Comentarios = bean.comentarios;
            this.psEntidadDao.registrar(psEntidad);

            PsEmpleado psEmpleado = new PsEmpleado();
            psEmpleado.IdEmpleado = psEntidad.IdEntidad; //this.psEmpleadoDao.generarCodigo();
            psEmpleado.FechaIngreso = bean.FechaIngreso;
            psEmpleado.IdInstitucion = bean.IdInstitucion;
            psEmpleado.Estado = "A";
            psEmpleado.CodigoUsuario = bean.CodigoUsuario;
            this.psEmpleadoDao.registrar(psEmpleado);

            bean.IdEntidad = psEntidad.IdEntidad;
            bean.IdEmpleado = psEmpleado.IdEmpleado;

            return bean;
        }

        public string traerNombreCompleto(DtoComponenteFurh bean)
        {
            String nombre = "", paterno = "", materno = "";


            if (!String.IsNullOrEmpty(bean.Nombres))
            {
                nombre = (bean.Nombres.Trim() != null ? bean.Nombres.Trim() : "");
            }


            if (!String.IsNullOrEmpty(bean.ApellidoPaterno))
            {
                paterno = (bean.ApellidoPaterno.Trim() != null ? bean.ApellidoPaterno.Trim() : "");
            }
            else
            {
                paterno = "";
            }

            if (!String.IsNullOrEmpty(bean.ApellidoMaterno))
            {
                materno = (bean.ApellidoMaterno.Trim() != null ? bean.ApellidoMaterno.Trim() : "");
            }
            else
            {
                materno = "";
            }


            return (paterno + " " + materno + ", " + nombre);


        }

        public DtoComponenteFurh obtenerPorId(string idInstitucion, int? idEmpleado)
        {

            PsEntidad bean = this.psEntidadDao.obtenerPorId(new PsEntidadPk(idEmpleado).obtenerArreglo());
            DtoComponenteFurh psEntidad = new DtoComponenteFurh();

            psEntidad.ApellidoMaterno = bean.ApellidoMaterno;
            psEntidad.ApellidoPaterno = bean.ApellidoPaterno;
            psEntidad.Nombres = bean.Nombres;
            psEntidad.Fechanacimiento = bean.FechaNacimiento;
            psEntidad.Correo1 = bean.Correo1;
            psEntidad.Estado = bean.Estado;
            psEntidad.IdAreaTrabajo = bean.IdAreaTrabajo;
            psEntidad.IdDiscapacidad = bean.IdDiscapacidad;
            psEntidad.IdEntidad = bean.IdEntidad;
            psEntidad.IdEspecialidadAcademica = bean.IdEspecialidadAcademica;
            psEntidad.IdNivelAcademico = bean.IdNivelAcademico;
            psEntidad.IdGrupoSanguineo = bean.IdGrupoSanguineo;
            psEntidad.IdFactorRh = bean.IdFactorRh;
            psEntidad.IdSexo = bean.IdSexo;
            psEntidad.Nombrecompleto = bean.Nombrecompleto;
            psEntidad.Documento = bean.Documento;
            psEntidad.IdTipoDocumento = bean.IdTipoDocumento;
            psEntidad.comentarios = bean.Comentarios;
            PsEmpleado psEmpleado = this.psEmpleadoDao.obtenerPorId(new PsEmpleadoPk(idInstitucion, idEmpleado).obtenerArreglo());

            psEntidad.IdEmpleado = psEmpleado.IdEmpleado;
            psEntidad.FechaIngreso = psEmpleado.FechaIngreso;
            psEntidad.IdInstitucion = psEmpleado.IdInstitucion;
            psEntidad.Estado = psEmpleado.Estado;
            psEntidad.CodigoUsuario = psEmpleado.CodigoUsuario;


            psEntidad.IdInstitucionNombre = this.psInstitucionDao.obtenerPorId(new PsInstitucionPk(idInstitucion).obtenerArreglo()).Nombre;

            return psEntidad;
        }

        public ParametroPaginacionGenerico listarFurh(ParametroPaginacionGenerico paginacion, DtoComponenteFurh filtro)
        {
            return psEntidadDao.listarFurh(paginacion, filtro);
        }

        public string Exportar(DtoComponenteFurh filtro)
        {

            List<DtoComponenteFurh> listarFurh = psEntidadDao.exportarFurh(filtro);

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Furh" + UFechaHora.obtenerNombreScat() + ".xlsx";

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

                worksheet.Cells[1, 1].Value = "Institución";
                worksheet.Cells[1, 2].Value = "Apellido Paterno";
                worksheet.Cells[1, 3].Value = "Apellido Materno";
                worksheet.Cells[1, 4].Value = "Nombres";

                worksheet.Cells[1, 5].Value = "Tipo documento";
                worksheet.Cells[1, 6].Value = "Documento";
                worksheet.Cells[1, 7].Value = "Sexo";
                worksheet.Cells[1, 8].Value = "Fecha Nacimiento";
                worksheet.Cells[1, 9].Value = "Correo";
                worksheet.Cells[1, 10].Value = "Discapacidad";
                worksheet.Cells[1, 11].Value = "Fecha Ingreso";
                worksheet.Cells[1, 12].Value = "Área Trabajo";
                worksheet.Cells[1, 13].Value = "Nivel Académico";
                worksheet.Cells[1, 14].Value = "Especialidad Académica";
                worksheet.Cells[1, 15].Value = "Codigo Usuario";
                worksheet.Cells[1, 16].Value = "Estado";

                int fila = 2;

                if (listarFurh.Count > 0)
                {
                    foreach (DtoComponenteFurh obj in listarFurh)
                    {
                        worksheet.Cells[fila, 1].Value = obj.IdInstitucionNombre;
                        worksheet.Cells[fila, 2].Value = obj.ApellidoPaterno;
                        worksheet.Cells[fila, 3].Value = obj.ApellidoMaterno;
                        worksheet.Cells[fila, 4].Value = obj.Nombres;
                        worksheet.Cells[fila, 5].Value = obj.IdTipoDocumento;
                        worksheet.Cells[fila, 6].Value = obj.Documento;
                        worksheet.Cells[fila, 7].Value = obj.IdSexo;
                        worksheet.Cells[fila, 8].Value = obj.FechaNacimiento.HasValue ? obj.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "";
                        worksheet.Cells[fila, 9].Value = obj.Correo1;
                        worksheet.Cells[fila, 10].Value = obj.IdDiscapacidad;
                        worksheet.Cells[fila, 11].Value = obj.FechaIngreso.HasValue ? obj.FechaIngreso.Value.ToString("dd/MM/yyyy") : "";
                        worksheet.Cells[fila, 12].Value = obj.IdAreaTrabajo;
                        worksheet.Cells[fila, 13].Value = obj.IdNivelAcademicoNombre;
                        worksheet.Cells[fila, 14].Value = obj.IdEspecialidadAcademicaNombre;
                        worksheet.Cells[fila, 15].Value = obj.CodigoUsuario;
                        worksheet.Cells[fila, 16].Value = obj.Estado;
                        fila++;
                    }
                }

                worksheet.Cells["A1:Q10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 16])
                {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }

        public List<DtoComponenteFurh> consultar(UsuarioActual usuarioActual, FiltroFurh filtro)
        {
            throw new NotImplementedException();
        }
    }
}
