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
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.correo.dominio;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.covid.dao;

namespace net.royal.spring.programasocial.servicio.impl {

    public class PsBeneficiarioServicioImpl : GenericoServicioImpl, PsBeneficiarioServicio {

        private IServiceProvider servicioProveedor;
        private PsBeneficiarioDao psBeneficiarioDao;
        private IHostingEnvironment _hostingEnvironment;
        private PsEntidadDao psEntidadDao;
        private PsBeneficiarioIngresoDao psBeneficiarioIngresoDao;
        private PsBeneficiarioIngresoCausalDao psBeneficiarioIngresoCausalDao;
        private PsEntidadParienteDao psEntidadParienteDao;
        private PsEntidadDocumentoDao psEntidadDocumentoDao;
        private PsEntidadEquipamientoDao psEntidadEquipamientoDao;
        private PsEntidadSeguroSocialDao psEntidadSeguroSocialDao;
        private PsEntidadProgramaSocialDao psEntidadProgramaSocialDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private PaisDao paisDao;
        private SyCorreoServicio syCorreoServicio;
        private SyReporteServicio syReporteServicio;
        private PsInstitucionDao psInstitucionDao;
        private PsProgramaDao psProgramaDao;

        public PsBeneficiarioServicioImpl(IServiceProvider _servicioProveedor, IHostingEnvironment hostingEnvironment) {
            servicioProveedor = _servicioProveedor;
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
            psEntidadDao = servicioProveedor.GetService<PsEntidadDao>();
            psBeneficiarioIngresoDao = servicioProveedor.GetService<PsBeneficiarioIngresoDao>();
            psEntidadParienteDao = servicioProveedor.GetService<PsEntidadParienteDao>();
            psEntidadDocumentoDao = servicioProveedor.GetService<PsEntidadDocumentoDao>();
            psEntidadEquipamientoDao = servicioProveedor.GetService<PsEntidadEquipamientoDao>();
            psEntidadSeguroSocialDao = servicioProveedor.GetService<PsEntidadSeguroSocialDao>();
            psEntidadProgramaSocialDao = servicioProveedor.GetService<PsEntidadProgramaSocialDao>();
            psBeneficiarioIngresoCausalDao = servicioProveedor.GetService<PsBeneficiarioIngresoCausalDao>();
            maMiscelaneosdetalleDao = servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
            paisDao = servicioProveedor.GetService<PaisDao>();
            syCorreoServicio = servicioProveedor.GetService<SyCorreoServicio>();
            syReporteServicio = servicioProveedor.GetService<SyReporteServicio>();
            psInstitucionDao = servicioProveedor.GetService<PsInstitucionDao>();
            psProgramaDao = servicioProveedor.GetService<PsProgramaDao>();
            this._hostingEnvironment = hostingEnvironment;
        }

        public PsBeneficiario coreInsertar(UsuarioActual usuarioActual, PsBeneficiario bean) {
            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;
            return psBeneficiarioDao.coreInsertar(usuarioActual, bean, bean.Estado);
        }

        public PsBeneficiario coreActualizar(UsuarioActual usuarioActual, PsBeneficiario bean) {
            return psBeneficiarioDao.coreActualizar(usuarioActual, bean, bean.Estado);
        }

        public PsBeneficiario coreAnular(UsuarioActual usuarioActual, PsBeneficiarioPk id) {
            return psBeneficiarioDao.coreAnular(usuarioActual, id);
        }

        public PsBeneficiario coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario) {
            return psBeneficiarioDao.coreAnular(usuarioActual, pIdInstitucion, pIdBeneficiario);
        }

        public void coreEliminar(PsBeneficiarioPk id) {
            psBeneficiarioDao.coreEliminar(id);
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario) {
            psBeneficiarioDao.coreEliminar(pIdInstitucion, pIdBeneficiario);
        }


        public PsBeneficiario obtenerPorId(PsBeneficiarioPk id) {
            return psBeneficiarioDao.obtenerPorId(id);
        }

        public PsBeneficiario obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario) {
            return psBeneficiarioDao.obtenerPorId(pIdInstitucion, pIdBeneficiario);
        }

        public ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro) {
            return psBeneficiarioDao.listarPaginacion(paginacion, filtro);
        }

        public string Exportar(FiltroBeneficiario filtro) {
            ParametroPaginacionGenerico parametroPaginacionGenerico = this.listarPaginacion2(filtro.paginacion, filtro);
            List<List<String>> listado = (List<List<String>>)parametroPaginacionGenerico.ListaResultado;



            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = "Reporte Beneficiarios" + UFechaHora.obtenerNombreScat() + ".xlsx";

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

                worksheet.Cells[1, 1].Value = "Institución";
                worksheet.Cells[1, 2].Value = "Apellido Paterno";
                worksheet.Cells[1, 3].Value = "Apellido Materno";
                worksheet.Cells[1, 4].Value = "Nombres";
                worksheet.Cells[1, 5].Value = "Sexo";
                worksheet.Cells[1, 6].Value = "Nacimiento";
                worksheet.Cells[1, 7].Value = "Edad";
                worksheet.Cells[1, 8].Value = "País";
                worksheet.Cells[1, 9].Value = "Departamento";
                worksheet.Cells[1, 10].Value = "Provincia";
                worksheet.Cells[1, 11].Value = "Distrito";
                worksheet.Cells[1, 12].Value = "Documento";
                worksheet.Cells[1, 13].Value = "Acta de nacimiento";
                worksheet.Cells[1, 14].Value = "Certificado de Nacido Vivo";
                worksheet.Cells[1, 15].Value = "Partida de Bautizo";
                worksheet.Cells[1, 16].Value = "Tarjeta de Vacunación";
                worksheet.Cells[1, 17].Value = "Resultado Pelmatoscópico";
                worksheet.Cells[1, 18].Value = "Constancia de Notas";
                worksheet.Cells[1, 19].Value = "Epicrisis";

                worksheet.Cells[1, 20].Value = "Otros Documentos";
                worksheet.Cells[1, 21].Value = "Fecha Ingreso";
                worksheet.Cells[1, 22].Value = "Tiempo de permanencia";
                worksheet.Cells[1, 23].Value = "Violencia familiar";
                worksheet.Cells[1, 24].Value = "Maltrato físico";
                worksheet.Cells[1, 25].Value = "Maltrato psicologico";
                worksheet.Cells[1, 26].Value = "Maltrato sexual";
                worksheet.Cells[1, 27].Value = "Incapacidad o imposibilidad de controlar desproteccion familiar";
                worksheet.Cells[1, 28].Value = "Descuido o negligencia que pone en riesgo el desarrollo integral";
                worksheet.Cells[1, 29].Value = "Trabajo en situación de calle o que suponga una afectación de derechos";

                worksheet.Cells[1, 30].Value = "Por abandono en centro hospitalario";
                worksheet.Cells[1, 31].Value = "Por abandono en la via publica";
                worksheet.Cells[1, 32].Value = "Otras circunstancias";
                worksheet.Cells[1, 33].Value = "Institución";
                worksheet.Cells[1, 34].Value = "Situacion Legal";
                worksheet.Cells[1, 35].Value = "Residencia asignada";
                worksheet.Cells[1, 36].Value = "Comentarios adicionales de ingreso";
                worksheet.Cells[1, 37].Value = "Departamento";
                worksheet.Cells[1, 38].Value = "Provincia";
                worksheet.Cells[1, 39].Value = "Distrito";

                worksheet.Cells[1, 40].Value = "Dirección";
                worksheet.Cells[1, 41].Value = "Referencia";
                worksheet.Cells[1, 42].Value = "Tenencia";
                worksheet.Cells[1, 43].Value = "Material de construcción";
                worksheet.Cells[1, 44].Value = "Servicio de agua potable";
                worksheet.Cells[1, 45].Value = "Servicio de desague";
                worksheet.Cells[1, 46].Value = "Servicio de electricidad";
                worksheet.Cells[1, 47].Value = "Radio";
                worksheet.Cells[1, 48].Value = "TV";
                worksheet.Cells[1, 49].Value = "Refrigeradora";

                worksheet.Cells[1, 50].Value = "Cocina";
                worksheet.Cells[1, 51].Value = "Internet";
                worksheet.Cells[1, 52].Value = "Nª Telefono";
                worksheet.Cells[1, 53].Value = "Nombre del padre";
                worksheet.Cells[1, 54].Value = "Vive?";
                worksheet.Cells[1, 55].Value = "N° DNI";
                worksheet.Cells[1, 56].Value = "Ocupacion";
                worksheet.Cells[1, 57].Value = "Nombre de la madre";
                worksheet.Cells[1, 58].Value = "Vive?";
                worksheet.Cells[1, 59].Value = "N° DNI";

                worksheet.Cells[1, 60].Value = "Ocupacion";
                worksheet.Cells[1, 61].Value = "Otra Ocupación";
                worksheet.Cells[1, 62].Value = "Nombre del/la apoderado y/o familiares";
                worksheet.Cells[1, 63].Value = "N° DNI";
                worksheet.Cells[1, 64].Value = "Nombre del esposo/a";
                worksheet.Cells[1, 65].Value = "Ingreso Familiar (mensual)";

                worksheet.Cells[1, 66].Value = "Fecha Informe Nutricion";
                worksheet.Cells[1, 67].Value = "Periodo Nutricion";
                worksheet.Cells[1, 68].Value = "PESO";
                worksheet.Cells[1, 69].Value = "TALLA";
                worksheet.Cells[1, 70].Value = "IMC";
                worksheet.Cells[1, 71].Value = "PESO EDAD";
                worksheet.Cells[1, 72].Value = "TALLA EDAD";
                worksheet.Cells[1, 73].Value = "PESO TALLA";
                worksheet.Cells[1, 74].Value = "NOMBRE SUPLEMENTO";
                worksheet.Cells[1, 75].Value = "NOMBRE PERIMETRO";
                worksheet.Cells[1, 76].Value = "NOMBRE PRESION";
                worksheet.Cells[1, 77].Value = "ID VALORACION";
                worksheet.Cells[1, 78].Value = "ID TIPO DIETA";
                worksheet.Cells[1, 79].Value = "SUPLEMENTO NUTRICIONAL POR DIA";
                worksheet.Cells[1, 80].Value = "TIPO DIETA POR DIA";
                worksheet.Cells[1, 81].Value = "COMENTARIOS NUTRICION";
                worksheet.Cells[1, 82].Value = "VARIACION PESO";
                worksheet.Cells[1, 83].Value = "PERIODO SOCIO";
                worksheet.Cells[1, 84].Value = "FECHA INFORME SOCIO";
                worksheet.Cells[1, 85].Value = "COMENTARIOS SOCIO";
                worksheet.Cells[1, 86].Value = "CONFLICTOS";
                worksheet.Cells[1, 87].Value = "COMUNICACION";
                worksheet.Cells[1, 88].Value = "EMOCIONAL";
                worksheet.Cells[1, 89].Value = "COOPERACION";
                worksheet.Cells[1, 90].Value = "ASERTIVIDAD";
                worksheet.Cells[1, 91].Value = "EMPATIA";
                worksheet.Cells[1, 92].Value = "INTEGRACION";
                worksheet.Cells[1, 93].Value = "PARTICIPACION";
                worksheet.Cells[1, 94].Value = "FECHA INFORME CAPACIDAD";
                worksheet.Cells[1, 95].Value = "NOMBRE TIPO INSTITUCION";
                worksheet.Cells[1, 96].Value = "NOMBRE NIVEL";
                worksheet.Cells[1, 97].Value = "ID GRADO EDUCATIVO";
                worksheet.Cells[1, 98].Value = "ANIO RETRASO";
                worksheet.Cells[1, 99].Value = "COMENTARIO RETRASO";
                worksheet.Cells[1, 100].Value = "ID TIPO COMUNICACION";
                worksheet.Cells[1, 101].Value = "COMENTARIO RENDIMIENTO";
                worksheet.Cells[1, 102].Value = "COMENTARIO TALLERES";
                worksheet.Cells[1, 103].Value = "ID RIESGO CAIDA";
                worksheet.Cells[1, 104].Value = "NOMBRE HABILIDAD";
                worksheet.Cells[1, 105].Value = "ID EVALUAR OCUPACIONAL";
                worksheet.Cells[1, 106].Value = "COMENTARIO CAPACIDAD";
                worksheet.Cells[1, 107].Value = "PERIODO CAPACIDAD";
                worksheet.Cells[1, 108].Value = "GRADO DEPENDENCIA BARTHEL";
                worksheet.Cells[1, 109].Value = "GRADO DEPENDENCIA KATZ";
                worksheet.Cells[1, 110].Value = "PORCENTAJE GRADO BARTHEL";
                worksheet.Cells[1, 111].Value = "PORCENTAJE GRADO KATZ";
                worksheet.Cells[1, 112].Value = "ID ILETRADO";
                worksheet.Cells[1, 113].Value = "ID SAANEE";
                worksheet.Cells[1, 114].Value = "OCUPACION ANTERIOR";
                worksheet.Cells[1, 115].Value = "ID RIESGO CAIDA DETALLE";
                worksheet.Cells[1, 116].Value = "PERIODO SALUD";
                worksheet.Cells[1, 117].Value = "FECHA INFORME SALUD";
                worksheet.Cells[1, 118].Value = "HEMOGLOBINA";
                worksheet.Cells[1, 119].Value = "HEMOGLOBINA RESULTADO";
                worksheet.Cells[1, 120].Value = "ID TRATAMIENTO ANEMIA";
                worksheet.Cells[1, 121].Value = "ID DESCARTE TBC";
                worksheet.Cells[1, 122].Value = "ID DESCARTE SEROLOGICO";
                worksheet.Cells[1, 123].Value = "ID AYUDA MEDICA";
                worksheet.Cells[1, 124].Value = "COMENTARIOS";
                worksheet.Cells[1, 125].Value = "ID TBC";
                worksheet.Cells[1, 126].Value = "ID HTA";
                worksheet.Cells[1, 127].Value = "ID DIABETES";
                worksheet.Cells[1, 128].Value = "ID OSTEOARTROSIS";
                worksheet.Cells[1, 129].Value = "ID COGNITIVO";
                worksheet.Cells[1, 130].Value = "ID AFECTIVO";
                worksheet.Cells[1, 131].Value = "ID GRUPOSANGUINEO";
                worksheet.Cells[1, 132].Value = "ID FACTORERH";
                worksheet.Cells[1, 133].Value = "EDAD MENARQUIA";
                worksheet.Cells[1, 134].Value = "FECHA ULTIMA MESTRUACION";
                worksheet.Cells[1, 135].Value = "ID PRUEBA VIH";
                worksheet.Cells[1, 136].Value = "OTRAS ENFERMEDADES";
                worksheet.Cells[1, 137].Value = "Ayuda 1";
                worksheet.Cells[1, 138].Value = "Ayuda 2";
                worksheet.Cells[1, 139].Value = "ayuda 3";

                worksheet.Cells[1, 140].Value = "Ayuda Biomecánica 1";
                worksheet.Cells[1, 141].Value = "Ayuda Biomecánica 2";
                worksheet.Cells[1, 142].Value = "ayuda Biomecánica 3";

                worksheet.Cells[1, 143].Value = "Estado Ayuda Biomecánica 1";
                worksheet.Cells[1, 144].Value = "Estado Ayuda Biomecánica 2";
                worksheet.Cells[1, 145].Value = "Estado Ayuda Biomecánica 3";

                worksheet.Cells[1, 146].Value = "Diagnóstico 1";
                worksheet.Cells[1, 147].Value = "Diagnóstico 2";
                worksheet.Cells[1, 148].Value = "Diagnóstico 3";

                worksheet.Cells[1, 149].Value = "Discapacidad 1";
                worksheet.Cells[1, 150].Value = "Discapacidad 2";
                worksheet.Cells[1, 151].Value = "Discapacidad 3";
                worksheet.Cells[1, 152].Value = "Estado 1";
                worksheet.Cells[1, 153].Value = "Estado 2";
                worksheet.Cells[1, 154].Value = "Estado 3";
                worksheet.Cells[1, 155].Value = "Exámen 1";
                worksheet.Cells[1, 156].Value = "Resultado Exámen 1";
                worksheet.Cells[1, 157].Value = "Exámen 2";
                worksheet.Cells[1, 158].Value = "Resultado Exámen 2";
                worksheet.Cells[1, 159].Value = "Exámen 3";
                worksheet.Cells[1, 160].Value = "Resultado Exámen 3";
                worksheet.Cells[1, 161].Value = "Condición 1";
                worksheet.Cells[1, 162].Value = "Sub Condición 1";
                worksheet.Cells[1, 163].Value = "Condición 2";
                worksheet.Cells[1, 164].Value = "Sub Condición 2";
                worksheet.Cells[1, 165].Value = "Condición 3";
                worksheet.Cells[1, 166].Value = "Sub Condición 3";
            
                worksheet.Cells[1, 167].Value = "Terapia 1";
                worksheet.Cells[1, 168].Value = "Terapia 2";
                worksheet.Cells[1, 169].Value = "Terapia 3";
                worksheet.Cells[1, 170].Value = "Tratamiento 1";
                worksheet.Cells[1, 171].Value = "Tratamiento 2";
                worksheet.Cells[1, 172].Value = "Tratamiento 3";

                int fila = 2;

                if (listado.Count > 0) {
                    foreach (List<String> obj in listado) {
                        var col = 1;
                        foreach (var obj2 in obj) {
                            worksheet.Cells[fila, col].Value = obj2;
                            col++;
                        }
                        fila++;
                    }
                }

                worksheet.Cells["A1:FO10000"].AutoFitColumns();

                using (var cells = worksheet.Cells[1, 1, 1, 172]) {
                    cells.Style.Font.Bold = true;
                    cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                package.Save(); //Save the workbook.

                return URL;
            }
        }

        private ParametroPaginacionGenerico listarPaginacion2(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro) {
            return psBeneficiarioDao.listarPaginacion2(paginacion, filtro);
        }

        public PsEntidad registrarCompleto(UsuarioActual usuarioActual, PsEntidad bean) {
            var tempArea = bean.ingreso.IdArea;
            var reingresar = false;

            if (bean.FechaNacimiento.HasValue) {
                // Crear fechas
                DateTime nacimiento = bean.FechaNacimiento.Value;
                DateTime hoy = DateTime.Now;

                // Años
                int edadAnos = hoy.Year - nacimiento.Year;
                if (hoy.Month < nacimiento.Month || (hoy.Month == nacimiento.Month &&
                hoy.Day < nacimiento.Day))
                    edadAnos--;

                // Meses
                int edadMeses = hoy.Month - nacimiento.Month;
                if (hoy.Day < nacimiento.Day)
                    edadMeses--;
                if (edadMeses < 0)
                    edadMeses += 12;

                bean.EdadMeses = edadAnos * 12 + edadMeses;
                bean.Edad = edadAnos;
            }

            if (bean.IdEntidad != null) {
                if (psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk() { IdBeneficiario = bean.IdEntidad, IdInstitucion = bean.auxInstitucion }.obtenerArreglo()) != null) {
                    throw new UException("El beneficiario ya se encuentra registrado en esta institución");
                }
                reingresar = true;
                psEntidadDao.actualizar(bean);
            }
            else {
                bean.IdEntidad = psEntidadDao.generarCodigo();
                bean.CreacionFecha = DateTime.Now;
                bean.CreacionUsuario = usuarioActual.UsuarioLogin;
                bean.Estado = "A";
                psEntidadDao.registrar(bean);
            }



            PsBeneficiario psBeneficiario = new PsBeneficiario();
            psBeneficiario.IdPrograma = bean.auxPrograma;
            psBeneficiario.IdBeneficiario = bean.IdEntidad;//psBeneficiarioDao.generarCodigo(bean.auxInstitucion);
            psBeneficiario.IdInstitucion = bean.auxInstitucion;
            psBeneficiario.IdComponenteIngreso = 1;
            psBeneficiario.IdArea = tempArea;
            psBeneficiario.Estado = "ACT";
            psBeneficiario.CreacionFecha = DateTime.Now;
            psBeneficiario.CreacionUsuario = usuarioActual.UsuarioLogin;

            psBeneficiarioDao.registrar(psBeneficiario);

            PsBeneficiarioIngreso psBeneficiarioIngreso = bean.ingreso;
            psBeneficiarioIngreso.IdInstitucion = psBeneficiario.IdInstitucion;
            psBeneficiarioIngreso.IdBeneficiario = psBeneficiario.IdBeneficiario;
            psBeneficiarioIngreso.IdIngreso = 1;
            psBeneficiarioIngreso.IdArea = tempArea;
            psBeneficiarioIngreso.Estado = "A";
            psBeneficiarioIngreso.CreacionFecha = DateTime.Now;
            psBeneficiarioIngreso.CreacionUsuario = usuarioActual.UsuarioLogin;

            psBeneficiarioIngresoDao.registrar(psBeneficiarioIngreso);


            foreach (var item in bean.ingreso.listaCausal) {
                item.IdInstitucion = psBeneficiarioIngreso.IdInstitucion;
                item.IdBeneficiario = psBeneficiarioIngreso.IdBeneficiario;
                item.IdIngreso = psBeneficiarioIngreso.IdIngreso;

                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psBeneficiarioIngresoCausalDao.registrar(item);
            }

            if (reingresar) {
                this.actualizarCompleto(usuarioActual, bean);
                enviarCorreo(bean);
                return new PsEntidad();
            }

            foreach (var item in bean.listaDocumento) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadDocumentoDao.registrar(item);
            }
            foreach (var item in bean.listaEquipamiento) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadEquipamientoDao.registrar(item);
            }
            var cc = 1;
            foreach (var item in bean.listaPariente) {
                item.IdPariente = cc;
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadParienteDao.registrar(item);
                cc++;
            }
            foreach (var item in bean.listaPrograma) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadProgramaSocialDao.registrar(item);
            }
            foreach (var item in bean.listaSeguro) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadSeguroSocialDao.registrar(item);
            }

            enviarCorreo(bean);


            return new PsEntidad();
        }

        public void enviarCorreo(PsEntidad bean) {
            //envio de correo

            PsPrograma programa = psProgramaDao.obtenerPorId(new PsProgramaPk() { IdPrograma = bean.auxPrograma }.obtenerArreglo());
            PsInstitucion institucion = psInstitucionDao.obtenerPorId(bean.auxInstitucion);


            EmailConfiguracion configCorreo = syCorreoServicio.obtenerConfiguracion();

            List<ParametroPersistenciaGenerico> lstMetadata = new List<ParametroPersistenciaGenerico>();
            List<DtoCorreo> listaCorreos = new List<DtoCorreo>();
            List<Email> listaEmail = new List<Email>();
            DtoReporteParametro reportePk = new DtoReporteParametro("PS", "RB");



            foreach (String item in psBeneficiarioDao.listarCorreos(bean.auxInstitucion, "ING")) {
                listaCorreos.Add(new DtoCorreo(-1, null, item));
            }

            lstMetadata.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, bean.auxInstitucion));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_institucion_nombre", ConstanteUtil.TipoDato.String, institucion == null ? "" : institucion.Nombre));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_programa", ConstanteUtil.TipoDato.String, programa == null ? "" : programa.Nombre));
            lstMetadata.Add(new ParametroPersistenciaGenerico("p_beneficiario", ConstanteUtil.TipoDato.String, bean.Nombrecompleto));

            listaEmail = syReporteServicio.generarListaCorreo(reportePk, lstMetadata, listaCorreos);
            syCorreoServicio.enviarCorreInmediatoAsincrono(configCorreo, listaEmail);

            //fin envio de correos
        }

        public PsEntidad actualizarCompleto(UsuarioActual usuarioActual, PsEntidad bean) {

            if (bean.FechaNacimiento.HasValue) {
                // Crear fechas
                DateTime nacimiento = bean.FechaNacimiento.Value;
                DateTime hoy = DateTime.Now;

                // Años
                int edadAnos = hoy.Year - nacimiento.Year;
                if (hoy.Month < nacimiento.Month || (hoy.Month == nacimiento.Month &&
                hoy.Day < nacimiento.Day))
                    edadAnos--;

                // Meses
                int edadMeses = hoy.Month - nacimiento.Month;
                if (hoy.Day < nacimiento.Day)
                    edadMeses--;
                if (edadMeses < 0)
                    edadMeses += 12;

                bean.EdadMeses = edadAnos * 12 + edadMeses;
                bean.Edad = edadAnos;
            }
            bean.ModificacionFecha = DateTime.Now;
            bean.ModificacionUsuario = usuarioActual.UsuarioLogin;
            psEntidadDao.actualizar(bean);

            int IdUltimoIngreso = psBeneficiarioIngresoDao.ObtenerUltimoIngreso(bean.IdEntidad);

            PsBeneficiarioIngreso psBeneficiarioIngreso =
            psBeneficiarioIngresoDao.obtenerPorId(
                new PsBeneficiarioIngresoPk() {
                    IdBeneficiario = bean.IdEntidad,
                    IdInstitucion = bean.auxInstitucion,
                    IdIngreso = IdUltimoIngreso
                }.obtenerArreglo());
            if (psBeneficiarioIngreso == null) {
                psBeneficiarioIngreso = bean.ingreso;
                psBeneficiarioIngreso.IdInstitucion = bean.auxInstitucion;
                psBeneficiarioIngreso.IdBeneficiario = bean.IdEntidad;
                psBeneficiarioIngreso.IdIngreso = 1;
                psBeneficiarioIngreso.IdArea = bean.ingreso.IdArea;
                psBeneficiarioIngreso.Estado = "A";
                psBeneficiarioIngreso.CreacionFecha = DateTime.Now;
                psBeneficiarioIngreso.CreacionUsuario = usuarioActual.UsuarioLogin;

                psBeneficiarioIngresoDao.registrar(psBeneficiarioIngreso);
            }
            else {
                psBeneficiarioIngreso.FechaIngreso = bean.ingreso.FechaIngreso;
                psBeneficiarioIngreso.DiasPermanencia = bean.ingreso.DiasPermanencia;
                psBeneficiarioIngreso.IdSituacionLegal = bean.ingreso.IdSituacionLegal;
                psBeneficiarioIngreso.IdInstitucionDeriva = bean.ingreso.IdInstitucionDeriva;
                psBeneficiarioIngreso.IdArea = bean.ingreso.IdArea;
                psBeneficiarioIngreso.Comentarios = bean.ingreso.Comentarios;

                psBeneficiarioIngreso.ModificacionFecha = DateTime.Now;
                psBeneficiarioIngreso.ModificacionUsuario = usuarioActual.UsuarioLogin;
                psBeneficiarioIngresoDao.actualizar(psBeneficiarioIngreso);
            }

            //eliminar detalles

            psBeneficiarioDao.eliminarDetalles(bean.auxInstitucion, bean.IdEntidad, IdUltimoIngreso);

            foreach (var item in bean.ingreso.listaCausal) {
                item.IdInstitucion = psBeneficiarioIngreso.IdInstitucion;
                item.IdBeneficiario = psBeneficiarioIngreso.IdBeneficiario;
                item.IdIngreso = psBeneficiarioIngreso.IdIngreso;

                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psBeneficiarioIngresoCausalDao.registrar(item);
            }
            foreach (var item in bean.listaDocumento) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadDocumentoDao.registrar(item);
            }
            foreach (var item in bean.listaEquipamiento) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadEquipamientoDao.registrar(item);
            }
            var cc = 1;
            foreach (var item in bean.listaPariente) {
                item.IdPariente = cc;
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadParienteDao.registrar(item);
                cc++;
            }
            foreach (var item in bean.listaPrograma) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadProgramaSocialDao.registrar(item);
            }
            foreach (var item in bean.listaSeguro) {
                item.IdEntidad = bean.IdEntidad;
                item.CreacionFecha = DateTime.Now;
                item.CreacionUsuario = usuarioActual.UsuarioLogin;
                psEntidadSeguroSocialDao.registrar(item);
            }

            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk() { IdBeneficiario = bean.IdEntidad, IdInstitucion = bean.auxInstitucion }.obtenerArreglo());
            psBeneficiario.Estado = bean.EstadoBeneficiario;
            psBeneficiario.IdArea = bean.ingreso.IdArea;
            psBeneficiarioDao.actualizar(psBeneficiario);



            return new PsEntidad();
        }

        public PsEntidad obtenerCompleto(string institucion, Int32 idBeneficiario) {
            PsEntidad psEntidad = psEntidadDao.obtenerPorId(new PsEntidadPk() { IdEntidad = idBeneficiario }.obtenerArreglo());
            psEntidad.auxInstitucion = institucion;
            if (!UString.estaVacio(psEntidad.IdNacimientoUbigeo)) {
                psEntidad.auxUbigeo = paisDao.obtenerUbigeo(psEntidad.IdNacimientoUbigeo).Descripcion;
            }
            if (!UString.estaVacio(psEntidad.DomicilioIdUbigeo)) {
                psEntidad.auxUbigeoConocido = paisDao.obtenerUbigeo(psEntidad.DomicilioIdUbigeo).Descripcion;
            }

            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk() { IdBeneficiario = idBeneficiario, IdInstitucion = institucion }.obtenerArreglo());
            psEntidad.auxPrograma = psBeneficiario.IdPrograma;
            psEntidad.EstadoBeneficiario = psBeneficiario.Estado;

            int IdUltimoIngreso = psBeneficiarioIngresoDao.ObtenerUltimoIngreso(idBeneficiario);

            psEntidad.ingreso = psBeneficiarioIngresoDao.obtenerPorId(new PsBeneficiarioIngresoPk() { IdBeneficiario = idBeneficiario, IdInstitucion = institucion, IdIngreso = IdUltimoIngreso }.obtenerArreglo());
            if (psEntidad.ingreso == null) {
                psEntidad.ingreso = new PsBeneficiarioIngreso();
            }
            else {
                psEntidad.ingreso.listaCausal = psBeneficiarioIngresoCausalDao.listarCausal(institucion, idBeneficiario, IdUltimoIngreso);
                foreach (var item in psEntidad.ingreso.listaCausal) {
                    item.auxCausal = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "CINGAAM", item.IdCausal);
                    if (UString.estaVacio(item.auxCausal)) {
                        item.auxCausal = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "CINGNNA", item.IdCausal);
                    }
                }
            }
            //auxTipo
            psEntidad.listaDocumento = psEntidadDocumentoDao.listarBeneficiario(institucion, idBeneficiario);
            foreach (var item in psEntidad.listaDocumento) {
                item.auxTipo = maMiscelaneosdetalleDao.obtenerDescripcion("PS", psEntidad.auxPrograma == "AAM" ? "TIPDCAAM" : "TIPDCNNA", item.IdTipoDocumento);
            }
            //auxEquipamiento
            psEntidad.listaEquipamiento = psEntidadEquipamientoDao.listarBeneficiario(institucion, idBeneficiario);
            foreach (var item in psEntidad.listaEquipamiento) {
                item.auxEquipamiento = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "EQPBASICO", item.IdEquipamiento);
            }
            //auxParentesco
            psEntidad.listaPariente = psEntidadParienteDao.listarBeneficiario(institucion, idBeneficiario);
            foreach (var item in psEntidad.listaPariente) {
                item.auxParentesco = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "PARENFUN", item.IdParentesco);
            }
            //auxPrograma
            psEntidad.listaPrograma = psEntidadProgramaSocialDao.listarBeneficiario(institucion, idBeneficiario);
            foreach (var item in psEntidad.listaPrograma) {
                item.auxPrograma = maMiscelaneosdetalleDao.obtenerDescripcion("PS", psEntidad.auxPrograma == "AAM" ? "PROGSOAAM" : "PROGSOCIA", item.IdProgramaSocial);
            }
            //auxSeguro
            psEntidad.listaSeguro = psEntidadSeguroSocialDao.listarBeneficiario(institucion, idBeneficiario);
            foreach (var item in psEntidad.listaSeguro) {
                item.auxSeguro = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "SEGSAL", item.IdSeguroSocial);
            }

            return psEntidad;

        }

        public List<MaMiscelaneosdetalle> listarValoracionNutricional(Int32 IdBeneficiario, String IdInstitucion) {
            return psBeneficiarioDao.listarValoracionNutricional(IdBeneficiario, IdInstitucion);
        }

        public ParametroPaginacionGenerico listarBeneficiarios(ParametroPaginacionGenerico paginacion, FiltroBeneficiario filtro) {
            return psBeneficiarioDao.listarBeneficiarios(paginacion, filtro);
        }

        public DtoBeneficiario anularBeneficiario(DtoBeneficiario bean, UsuarioActual usuarioActual) {
            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk() { IdBeneficiario = bean.idBeneficiario, IdInstitucion = bean.idInstitucion }.obtenerArreglo());
            psBeneficiario.Estado = "ANU";
            psBeneficiarioDao.actualizar(psBeneficiario);
            return bean;
        }

        public PsEntidad obtenerDatosBasicos(PsEntidad bean) {
            if (UString.estaVacio(bean.Documento)) {
                bean.Documento = null;
            }
            if (UString.estaVacio(bean.ApellidoPaterno)) {
                bean.ApellidoPaterno = null;
            }
            if (UString.estaVacio(bean.Nombres)) {
                bean.Nombres = null;
            }

            PsEntidad psEntidad = psEntidadDao.obtenerDatos(bean);

            DtoPsBeneficiario dto = psBeneficiarioDao.obtenerPrograma(bean);


            if (psEntidad != null && dto != null) {



                psEntidad.auxPrograma = dto.idPrograma;
                psEntidad.auxInstitucion = dto.idInstitucion;
                if (!UString.estaVacio(psEntidad.IdNacimientoUbigeo)) {
                    psEntidad.auxUbigeo = paisDao.obtenerUbigeo(psEntidad.IdNacimientoUbigeo).Descripcion;
                }
                if (!UString.estaVacio(psEntidad.DomicilioIdUbigeo)) {
                    psEntidad.auxUbigeoConocido = paisDao.obtenerUbigeo(psEntidad.DomicilioIdUbigeo).Descripcion;
                }



                //auxTipo
                psEntidad.listaDocumento = psEntidadDocumentoDao.listarBeneficiario(bean.auxInstitucion, psEntidad.IdEntidad.Value);
                foreach (var item in psEntidad.listaDocumento) {
                    item.auxTipo = maMiscelaneosdetalleDao.obtenerDescripcion("PS", psEntidad.auxPrograma == "AAM" ? "TIPDCAAM" : "TIPDCNNA", item.IdTipoDocumento);
                }

                //auxEquipamiento
                psEntidad.listaEquipamiento = psEntidadEquipamientoDao.listarBeneficiario(bean.auxInstitucion, psEntidad.IdEntidad.Value);
                foreach (var item in psEntidad.listaEquipamiento) {
                    item.auxEquipamiento = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "EQPBASICO", item.IdEquipamiento);
                }
                //auxParentesco
                psEntidad.listaPariente = psEntidadParienteDao.listarBeneficiario(bean.auxInstitucion, psEntidad.IdEntidad.Value);
                foreach (var item in psEntidad.listaPariente) {
                    item.auxParentesco = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "PARENFUN", item.IdParentesco);
                }
                //auxPrograma
                psEntidad.listaPrograma = psEntidadProgramaSocialDao.listarBeneficiario(bean.auxInstitucion, psEntidad.IdEntidad.Value);
                foreach (var item in psEntidad.listaPrograma) {
                    item.auxPrograma = maMiscelaneosdetalleDao.obtenerDescripcion("PS", psEntidad.auxPrograma == "AAM" ? "PROGSOAAM" : "PROGSOCIA", item.IdProgramaSocial);
                }
                //auxSeguro
                psEntidad.listaSeguro = psEntidadSeguroSocialDao.listarBeneficiario(bean.auxInstitucion, psEntidad.IdEntidad.Value);
                foreach (var item in psEntidad.listaSeguro) {
                    item.auxSeguro = maMiscelaneosdetalleDao.obtenerDescripcion("PS", "SEGSAL", item.IdSeguroSocial);
                }
                return psEntidad;
            }
            else {
                return null;
            }




        }

        public List<DtoPrevencionSalud> ListarBeneficiariosSinEvaluacion(FiltroGraficos filtro) {
            return psBeneficiarioDao.ListarBeneficiariosSinEvaluacion(filtro);
        }

        public ParametroPaginacionGenerico paginacionBeneficiariosSinEvaluacion(ParametroPaginacionGenerico paginacion, FiltroGraficos filtro) {
            return psBeneficiarioDao.paginacionBeneficiariosSinEvaluacion(paginacion, filtro);
        }
    }
}
