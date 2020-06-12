using net.royal.spring.core.dominio;
using net.royal.spring.framework.core;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using static CargaExcel.Program;

namespace CargaExcel
{
    public class UtilCast
    {
        public static void procesarCoeficiente(int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga ITEM UPDATE");

            List<PsItem> listaNutricion = new List<PsItem>();

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsItem entidad = new PsItem();

                entidad.IdItem = UtilCast.valueToString(sheet, beginRow, beginCol + 1).PadLeft(6, '0');
                entidad.Coeficiente = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 9);

                listaNutricion.Add(entidad);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\ITEM_UPDATE.sql"))
            {
                listaNutricion.ForEach(x => file.WriteLine(UtilCast.queryItemUpdate(x)));
            }

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga UPDATE");
        }

        private static string queryItemUpdate(PsItem x)
        {
            var query = "UPDATE sgseguridadsys.PS_ITEM SET COEFICIENTE = " + x.Coeficiente + " WHERE ID_ITEM = " + stringToString(x.IdItem) + System.Environment.NewLine;
            return query;
        }

        public static void procesarPedido(int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, DateTime fechaInforme, string institucion, string aporte)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga CONSUMO");

            PsConsumo consumo = new PsConsumo();
            consumo.IdInstitucion = institucion;
            consumo.IdConsumo = Program.inicioConsumo;
            consumo.Estado = "A";
            consumo.Fechainicio = fechaInforme;
            consumo.Fechafin = null;
            consumo.Descripcion = null;
            consumo.Aportante = aporte;

            var detalle = 1;

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsConsumoNutricional entidad = new PsConsumoNutricional();

                entidad.IdInstitucion = institucion;
                entidad.IdConsumo = consumo.IdConsumo;
                entidad.IdConsumoNutricional = detalle;

                if (UtilCast.valueToInt(sheet, beginRow, beginCol + 1) == null)
                {
                    throw new Exception("no tiene codigo");
                }

                entidad.IdItem = (UtilCast.valueToInt(sheet, beginRow, beginCol + 1) + "").PadLeft(6, '0');
                entidad.Cantidadsolicitada = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 6);

                if (!entidad.Cantidadsolicitada.HasValue)
                {
                    continue;
                }
                if (entidad.Cantidadsolicitada == 0)
                {
                    continue;
                }



                entidad.Comentarios = null;
                entidad.Estado = "A";

                consumo.listadetalle.Add(entidad);

                detalle++;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\PEDIDO_" + institucion + "_" + aporte + ".sql"))
            {
                file.WriteLine(UtilCast.queryConsumo(consumo));
                Program.inicioConsumo++;
            }

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga CONSUMO");
        }

        internal static void procesarFurh(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta)
        {

            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga FURH ");

            List<PsEmpleado> listaEmpleados = new List<PsEmpleado>();

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsEmpleado entidad = new PsEmpleado();
                entidad.IdEmpleado = Program.inicioEntidad;
                entidad.psEntidad.IdEntidad = Program.inicioEntidad;
                entidad.IdInstitucion = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                entidad.psEntidad.ApellidoPaterno = UtilCast.valueToString(sheet, beginRow, beginCol + 4);
                entidad.psEntidad.ApellidoMaterno = UtilCast.valueToString(sheet, beginRow, beginCol + 5);
                entidad.psEntidad.Nombres = UtilCast.valueToString(sheet, beginRow, beginCol + 6);
                entidad.psEntidad.Nombrecompleto = entidad.psEntidad.ApellidoPaterno + " " + entidad.psEntidad.ApellidoMaterno + ", " + entidad.psEntidad.Nombres;
                entidad.psEntidad.Correo1 = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                entidad.psEntidad.FechaNacimiento = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 8);
                entidad.psEntidad.IdSexo = UtilCast.valueToString(sheet, beginRow, beginCol + 10);

                entidad.psEntidad.IdDiscapacidad = UtilCast.valueToString(sheet, beginRow, beginCol + 11);

                entidad.FechaIngreso = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 14);

                entidad.psEntidad.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 16);


                switch (entidad.psEntidad.IdDiscapacidad.Trim())
                {
                    case "Si":
                        entidad.psEntidad.IdDiscapacidad = "SI";
                        break;
                    case "No":
                        entidad.psEntidad.IdDiscapacidad = "NO";
                        break;
                    default:
                        entidad.psEntidad.IdDiscapacidad = null;
                        break;
                }

                switch (entidad.psEntidad.IdSexo.Trim())
                {
                    case "Femenino":
                        entidad.psEntidad.IdSexo = "F";
                        break;
                    case "Masculino":
                        entidad.psEntidad.IdSexo = "M";
                        break;
                    default:
                        entidad.psEntidad.IdSexo = null;
                        break;
                }

                entidad.psEntidad.IdNivelAcademico = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                switch (entidad.psEntidad.IdNivelAcademico.Trim())
                {
                    case "Profesional":
                        entidad.psEntidad.IdNivelAcademico = "PRO";
                        break;
                    case "Secundaria completa":
                        entidad.psEntidad.IdNivelAcademico = "SEC";
                        break;
                    case "Técnico":
                        entidad.psEntidad.IdNivelAcademico = "TEC";
                        break;

                }

                entidad.IdInstitucion = UtilCast.valueToString(sheet, beginRow, beginCol + 1);

                switch (entidad.IdInstitucion.Trim())
                {
                    case "Puericultorio Perez Aranibar":
                        entidad.IdInstitucion = "PURIC";
                        break;
                    case "CARGG Canevaro":
                        entidad.IdInstitucion = "CANEV";
                        break;
                    case "CARG San Vicente de Paúl":
                        entidad.IdInstitucion = "SVPAU";
                        break;
                    case "CEBE 07 La Inmaculada":
                        entidad.IdInstitucion = "INMAC";
                        break;
                    case "CEBE 09 San Francisco de Asís":
                        entidad.IdInstitucion = "FRASI";
                        break;
                    case "Asilo Hnitas Ancianos Desamparados":
                        entidad.IdInstitucion = "DESAM";
                        break;

                }
                //clase
                entidad.psEntidad.IdEspecialidadAcademica = UtilCast.valueToString(sheet, beginRow, beginCol + 13);

                switch (entidad.psEntidad.IdEspecialidadAcademica.Trim())
                {
                    case "Asistente de cocina":
                        entidad.psEntidad.IdEspecialidadAcademica = "ACOCI";
                        break;
                    case "Auxiliar de cocina":
                        entidad.psEntidad.IdEspecialidadAcademica = "AUCOC";
                        break;
                    case "Docente":
                        entidad.psEntidad.IdEspecialidadAcademica = "DOCEN";
                        break;
                    case "Enfermera":
                        entidad.psEntidad.IdEspecialidadAcademica = "ENFE";
                        break;
                    case "Medico":
                        entidad.psEntidad.IdEspecialidadAcademica = "MEDI";
                        break;
                    case "Nutricionista":
                        entidad.psEntidad.IdEspecialidadAcademica = "NUTR";
                        break;
                    case "odontologo":
                        entidad.psEntidad.IdEspecialidadAcademica = "ODON";
                        break;
                    case "Otros":
                        entidad.psEntidad.IdEspecialidadAcademica = "OTRO";
                        break;
                    case "Psicologo":
                        entidad.psEntidad.IdEspecialidadAcademica = "PSIC";
                        break;
                    case "Psiquiatra":
                        entidad.psEntidad.IdEspecialidadAcademica = "PSIQ";
                        break;
                    case "Terapeuta":
                        entidad.psEntidad.IdEspecialidadAcademica = "TERA";
                        break;
                    case "Trabajadora social":
                        entidad.psEntidad.IdEspecialidadAcademica = "TRSOC";
                        break;
                }

                //unidad medida

                entidad.Estado = "A";

                listaEmpleados.Add(entidad);

                Program.inicioEntidad++;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\FURH.sql"))
            {
                listaEmpleados.ForEach(x => file.WriteLine(UtilCast.queryEmpleado(x)));
            }

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga FURH");

        }

        public static void procesarItems(int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga ITEM ");

            List<PsItem> listaNutricion = new List<PsItem>();

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsItem entidad = new PsItem();

                entidad.IdItem = UtilCast.valueToString(sheet, beginRow, beginCol + 1).PadLeft(6, '0');
                entidad.IdGrupo = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                entidad.IdClase = UtilCast.valueToString(sheet, beginRow, beginCol + 2);
                entidad.Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 4);
                entidad.IdUnidadMedida = UtilCast.valueToString(sheet, beginRow, beginCol + 5);

                if (!UString.estaVacio(entidad.IdUnidadMedida))
                {
                    entidad.IdUnidadMedida = entidad.IdUnidadMedida.Trim();
                }
                //grupo
                switch (entidad.IdGrupo)
                {
                    case "CARNE DE RES":
                        entidad.IdGrupo = "01";
                        break;
                    case "AVES":
                        entidad.IdGrupo = "02";
                        break;
                    case "PESCADO":
                        entidad.IdGrupo = "03";
                        break;
                    case "FRUTAS":
                        entidad.IdGrupo = "04";
                        break;
                    case "VEGETALES":
                        entidad.IdGrupo = "05";
                        break;
                    case "EMBUTIDOS":
                        entidad.IdGrupo = "06";
                        break;
                    case "OTROS":
                        entidad.IdGrupo = "07";
                        break;
                    default:
                        throw new Exception();
                }
                //clase
                switch (entidad.IdClase)
                {
                    case "Cárnicos":
                        entidad.IdClase = "CAR";
                        break;
                    case "Frutas":
                        entidad.IdClase = "FRU";
                        break;
                    case "Verduras":
                    case "Vegetales":
                        entidad.IdClase = "VER";
                        break;
                    case "Otros":
                    case "Otros ":
                        entidad.IdClase = "OTR";
                        break;
                    case "Lácteos y Huevos":
                        entidad.IdClase = "LAC";
                        break;
                    case "Aceites y Grasas":
                        entidad.IdClase = "ACE";
                        break;
                    case "Cereales":
                        entidad.IdClase = "CER";
                        break;
                    default:
                        throw new Exception();
                }

                //unidad medida

                entidad.Estado = "A";
                entidad.IdTipoItem = "ALI";

                listaNutricion.Add(entidad);
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\ITEM.sql"))
            {
                listaNutricion.ForEach(x => file.WriteLine(UtilCast.queryItem(x)));
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_ITEM.txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }

            Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga ITEM");

        }
        public static String querySalud(PsSalud bean)
        {
            var query = "insert into sgseguridadsys.PS_SALUD(EVALUADO, ID_ORIGEN, ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_PERIODO,FECHA_INFORME, ID_GRUPOSANGUINEO, ID_FACTORERH, ID_DESCARTE_SEROLOGICO, ID_TBC, ID_HTA, ID_DIABETES, ID_COGNITIVO, ID_AFECTIVO, COMENTARIOS, EDAD_MENARQUIA, FECHA_ULTIMA_MESTRUACION, HEMOGLOBINA, HEMOGLOBINA_RESULTADO, Id_PRUEBA_VIH ) values (" + UtilCast.stringToString(bean.Evaluado) + "," + UtilCast.stringToString(bean.IdOrigen) + "," + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdSalud) + "," + UtilCast.stringToString(bean.IdPeriodo) + "," + UtilCast.datetimeToString(bean.FechaInforme) + "," + UtilCast.stringToString(bean.IdGrupoSanguineo) + "," + UtilCast.stringToString(bean.IdFactorRh) + "," + UtilCast.stringToString(bean.IdDescarteSerologico) + "," + UtilCast.stringToString(bean.IdTbc) + "," + UtilCast.stringToString(bean.IdHta) + "," + UtilCast.stringToString(bean.IdDiabetes) + "," + UtilCast.stringToString(bean.IdCognitivo) + "," + UtilCast.stringToString(bean.IdAfectivo) + "," + UtilCast.stringToString(bean.Comentarios) + "," + UtilCast.intToString(bean.EdadMenarquia) + "," + UtilCast.datetimeToString(bean.FechaUltimaMestruacion) + "," + UtilCast.decimalToString(bean.Hemoglobina) + "," + UtilCast.stringToString(bean.HemoglobinaResultado) + "," + UtilCast.stringToString(bean.IdPruebaVIH) + ")" + System.Environment.NewLine;
            //query = query + "GO;" + System.Environment.NewLine;

            bean.listaBiomecanica.ForEach(bio =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_BIOMECANICA(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_TIPO_AYUDA, ID_ESTADO_AYUDA) values (" + UtilCast.stringToString(bio.IdInstitucion) + "," + UtilCast.intToString(bio.IdBeneficiario) + "," + UtilCast.intToString(bio.IdSalud) + "," + UtilCast.stringToString(bio.IdTipoAyuda) + "," + UtilCast.stringToString(bio.IdEstadoAyuda) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaExamen.ForEach(exa =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_EXAMEN(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_EXAMEN, ID_RESULTADO) values (" + UtilCast.stringToString(exa.IdInstitucion) + "," + UtilCast.intToString(exa.IdBeneficiario) + "," + UtilCast.intToString(exa.IdSalud) + "," + UtilCast.stringToString(exa.IdExamen) + "," + UtilCast.stringToString(exa.IdResultado) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaSubcondicion.ForEach(sub =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_SUBCONDICION(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_CONDICION, ID_SUBCONDICION) values (" + UtilCast.stringToString(sub.IdInstitucion) + "," + UtilCast.intToString(sub.IdBeneficiario) + "," + UtilCast.intToString(sub.IdSalud) + "," + UtilCast.stringToString(sub.IdCondicion) + "," + UtilCast.stringToString(sub.IdSubcondicion) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            /*
            bean.listaDiagnostico.ForEach(dia =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_DIAGNOSTICO(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_DIAGNOSTICO) values (" + UtilCast.stringToString(dia.IdInstitucion) + "," + UtilCast.intToString(dia.IdBeneficiario) + "," + UtilCast.intToString(dia.IdSalud) + "," + UtilCast.stringToString(dia.IdDiagnostico) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });
            */

            bean.listaTerapia.ForEach(ter =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_TERAPIA(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_TERAPIA) values (" + UtilCast.stringToString(ter.IdInstitucion) + "," + UtilCast.intToString(ter.IdBeneficiario) + "," + UtilCast.intToString(ter.IdSalud) + "," + UtilCast.stringToString(ter.IdTerapia) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaDiscapacidad.ForEach(dis =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_DISCAPACIDAD(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_DISCAPACIDAD) values (" + UtilCast.stringToString(dis.IdInstitucion) + "," + UtilCast.intToString(dis.IdBeneficiario) + "," + UtilCast.intToString(dis.IdSalud) + "," + UtilCast.stringToString(dis.IdDiscapacidad) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaEstado.ForEach(est =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_ESTADO(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_ESTADO) values (" + UtilCast.stringToString(est.IdInstitucion) + "," + UtilCast.intToString(est.IdBeneficiario) + "," + UtilCast.intToString(est.IdSalud) + "," + UtilCast.stringToString(est.IdEstado) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaTratamiento.ForEach(tra =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_TRATAMIENTO(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_TRATAMIENTO) values (" + UtilCast.stringToString(tra.IdInstitucion) + "," + UtilCast.intToString(tra.IdBeneficiario) + "," + UtilCast.intToString(tra.IdSalud) + "," + UtilCast.stringToString(tra.IdTratamiento) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });

            bean.listaAyuda.ForEach(est =>
            {
                query = query + "insert into sgseguridadsys.PS_SALUD_AYUDA(ID_INSTITUCION, ID_BENEFICIARIO, ID_SALUD, ID_AYUDA) values (" + UtilCast.stringToString(est.IdInstitucion) + "," + UtilCast.intToString(est.IdBeneficiario) + "," + UtilCast.intToString(est.IdSalud) + "," + UtilCast.stringToString(est.IdAyuda) + ")" + System.Environment.NewLine;
                //query = query + "GO;" + System.Environment.NewLine;
            });


            return query;
        }

        public static String queryBeneficiario(PsBeneficiario bean)
        {
            var query = "insert into sgseguridadsys.PS_BENEFICIARIO(ID_INSTITUCION, ID_BENEFICIARIO, ID_PROGRAMA, ID_AREA, ID_COMPONENTE_INGRESO, ID_COMPONENTE_NUTRICION, ID_COMPONENTE_SALUD, ID_COMPONENTE_CAPACIDAD, ID_COMPONENTE_SOCIO_AMBIENTAL, ESTADO) values (" + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.stringToString(bean.IdPrograma) + "," + UtilCast.stringToString(bean.IdArea) + "," + UtilCast.intToString(bean.IdComponenteIngreso) + "," + UtilCast.intToString(bean.IdComponenteNutricion) + "," + UtilCast.intToString(bean.IdComponenteSalud) + "," + UtilCast.intToString(bean.IdComponenteCapacidad) + "," + UtilCast.intToString(bean.IdComponenteSocioAmbiental) + "," + UtilCast.stringToString(bean.Estado) + ")";// + System.Environment.NewLine;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       //query = query + "GO;";
            return query;
        }

        public static String queryEmpleado(PsEmpleado bean)
        {
            var entidad = bean.psEntidad;
            var query = "insert into sgseguridadsys.PS_ENTIDAD(COMENTARIOS, ID_ENTIDAD, APELLIDO_PATERNO, APELLIDO_MATERNO, NOMBRES, NOMBRECOMPLETO, CORREO1, FECHA_NACIMIENTO, ID_SEXO, ID_DISCAPACIDAD, ID_NIVEL_ACADEMICO, ID_ESPECIALIDAD_ACADEMICA, ESTADO) values (" + UtilCast.stringToString(entidad.Comentarios) + "," + UtilCast.intToString(entidad.IdEntidad) + "," + UtilCast.stringToString(entidad.ApellidoPaterno) + "," + UtilCast.stringToString(entidad.ApellidoMaterno) + "," + UtilCast.stringToString(entidad.Nombres) + "," + UtilCast.stringToString(entidad.Nombrecompleto) + "," + UtilCast.stringToString(entidad.Correo1) + "," + UtilCast.datetimeToString(entidad.FechaNacimiento) + "," + UtilCast.stringToString(entidad.IdSexo) + "," + UtilCast.stringToString(entidad.IdDiscapacidad) + "," + UtilCast.stringToString(entidad.IdNivelAcademico) + "," + UtilCast.stringToString(entidad.IdEspecialidadAcademica) + "," + "'A'" + ")";// + System.Environment.NewLine;

            query = query + "insert into sgseguridadsys.PS_EMPLEADO(ID_INSTITUCION, ID_EMPLEADO, FECHA_INGRESO, ESTADO) values (" + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdEmpleado) + "," + UtilCast.datetimeToString(bean.FechaIngreso) + "," + "'A'" + ")" + System.Environment.NewLine;

            return query;
        }


        public static String queryCapacidad(PsCapacidad bean)
        {
            var query = "insert into sgseguridadsys.PS_CAPACIDAD(ID_RIESGO_CAIDA_DETALLE, ID_TIPO_COMUNICACION, ID_HABILIDAD_OCUPACIONAL, COMENTARIO_RETRASO, COMENTARIO_RENDIMIENTO, COMENTARIO_TALLERES, ANIO_RETRASO, COMENTARIO_CAPACIDAD,ID_INSTITUCION, ID_BENEFICIARIO, ID_CAPACIDAD, FECHA_INFORME, ID_RIESGO_CAIDA,KATZ1,KATZ2,KATZ3,KATZ4,KATZ5,KATZ6, ID_PERIODO, GRADO_DEPENDENCIA_KATZ, PORCENTAJE_GRADO_KATZ, ID_ORIGEN, EVALUADO, ESTADO, OCUPACION_ANTERIOR, ID_NIVEL, ID_GRADO_EDUCATIVO, ID_TIPO_INSTITUCION, ID_ILETRADO, ID_SAANEE, Barthel1, Barthel2, Barthel3, Barthel4, Barthel5, Barthel6, Barthel7, Barthel8, Barthel9, Barthel10, GRADO_DEPENDENCIA_BARTHEL, PORCENTAJE_GRADO_BARTHEL) values (" + UtilCast.stringToString(bean.IdRiesgoCaidaDetalle) + "," + UtilCast.stringToString(bean.IdTipoComunicacion) + "," + UtilCast.stringToString(bean.IdHabilidadOcupacional) + "," + UtilCast.stringToString(bean.ComentarioRetraso) + "," + UtilCast.stringToString(bean.ComentarioRendimiento) + "," + UtilCast.stringToString(bean.ComentarioTalleres) + "," + UtilCast.stringToString(bean.AnioRetraso) + "," + UtilCast.stringToString(bean.ComentarioCapacidad) + "," + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdCapacidad) + "," + UtilCast.datetimeToString(bean.FechaInforme) + "," + UtilCast.stringToString(bean.IdRiesgoCaida) + "," + UtilCast.intToString(bean.Katz1) + "," + UtilCast.intToString(bean.Katz2) + "," + UtilCast.intToString(bean.Katz3) + "," + UtilCast.intToString(bean.Katz4) + "," + UtilCast.intToString(bean.Katz5) + "," + UtilCast.intToString(bean.Katz6) + "," + UtilCast.stringToString(bean.IdPeriodo) + "," + UtilCast.stringToString(bean.GradoDependenciaKatz) + "," + UtilCast.decimalToString(bean.PorcentajeGradoKatz) + "," + UtilCast.stringToString(bean.IdOrigen) + "," + UtilCast.stringToString(bean.Evaluado) + "," + UtilCast.stringToString(bean.Estado) + "," + UtilCast.stringToString(bean.OcupacionAnterior) + "," + UtilCast.stringToString(bean.IdNivel) + "," + UtilCast.stringToString(bean.IdGradoEducativo) + "," + UtilCast.stringToString(bean.IdTipoInstitucion) + "," + UtilCast.stringToString(bean.IdIletrado) + "," + UtilCast.stringToString(bean.IdSaanee) + "," + UtilCast.intToString(bean.Barthel1) + "," + UtilCast.intToString(bean.Barthel2) + "," + UtilCast.intToString(bean.Barthel3) + "," + UtilCast.intToString(bean.Barthel4) + "," + UtilCast.intToString(bean.Barthel5) + "," + UtilCast.intToString(bean.Barthel6) + "," + UtilCast.intToString(bean.Barthel7) + "," + UtilCast.intToString(bean.Barthel8) + "," + UtilCast.intToString(bean.Barthel9) + "," + UtilCast.intToString(bean.Barthel10) + "," + UtilCast.stringToString(bean.GradoDependenciaBarthel) + "," + UtilCast.decimalToString(bean.PorcentajeGradoBarthel) + ")" + System.Environment.NewLine; ;
            bean.listaCursos.ForEach(cur =>
            {
                query = query + "insert into sgseguridadsys.PS_CAPACIDAD_CURSO(ID_INSTITUCION, ID_BENEFICIARIO, ID_CAPACIDAD, ID_CURSO, ID_NOTA) values (" + UtilCast.stringToString(cur.IdInstitucion) + "," + UtilCast.intToString(cur.IdBeneficiario) + "," + UtilCast.intToString(cur.IdCapacidad) + "," + UtilCast.stringToString(cur.IdCurso) + "," + UtilCast.stringToString(cur.IdNota) + ")" + System.Environment.NewLine;
            });
            bean.listaTaller.ForEach(cur =>
            {
                query = query + "insert into sgseguridadsys.PS_CAPACIDAD_TALLER(ID_INSTITUCION, ID_BENEFICIARIO, ID_CAPACIDAD, ID_TALLER, ID_NOTA, CANTIDAD, CREACION_USUARIO) values (" + UtilCast.stringToString(cur.IdInstitucion) + "," + UtilCast.intToString(cur.IdBeneficiario) + "," + UtilCast.intToString(cur.IdCapacidad) + "," + UtilCast.intToString(cur.IdTaller) + "," + UtilCast.stringToString(cur.IdNota) + "," + UtilCast.intToString(cur.Cantidad) + "," + UtilCast.stringToString(cur.CreacionUsuario) + ")" + System.Environment.NewLine;
            });
            return query;
        }

        public static String querySocio(PsSocioAmbiental bean)
        {
            var query = "insert into sgseguridadsys.PS_SOCIO_AMBIENTAL(ID_INTEGRACION, ID_PARTICIPACION, ID_INSTITUCION, ID_BENEFICIARIO, ID_SOCIO_AMBIENTAL, ID_PERIODO, FECHA_INFORME, ID_CONFLICTOS, ID_COMUNICACION, ID_EMOCIONAL, ID_COOPERACION, ID_ASERTAVIDAD, ID_EMPATIA, COMENTARIOS, ESTADO, ID_ORIGEN, EVALUADO) values (" + UtilCast.stringToString(bean.IdIntegracion) + "," + UtilCast.stringToString(bean.IdParticipacion) + "," + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdSocioAmbiental) + "," + UtilCast.stringToString(bean.IdPeriodo) + "," + UtilCast.datetimeToString(bean.FechaInforme) + "," + UtilCast.stringToString(bean.IdConflictos) + "," + UtilCast.stringToString(bean.IdComunicacion) + "," + UtilCast.stringToString(bean.IdEmocional) + "," + UtilCast.stringToString(bean.IdCooperacion) + "," + UtilCast.stringToString(bean.IdAsertavidad) + "," + UtilCast.stringToString(bean.IdEmpatia) + "," + UtilCast.stringToString(bean.Comentarios) + "," + UtilCast.stringToString(bean.Estado) + "," + UtilCast.stringToString(bean.IdOrigen) + "," + UtilCast.stringToString(bean.Evaluado) + ")";
            return query;
        }

        public static String queryConsumo(PsConsumo bean)
        {
            var query = "insert into sgseguridadsys.PS_CONSUMO(ID_PERIODO, ID_INSTITUCION, ID_CONSUMO, ESTADO, FECHA_INICIO, FECHA_FIN, DESCRIPCION, APORTANTE) values ('S1'," + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdConsumo) + "," + UtilCast.stringToString(bean.Estado) + "," + UtilCast.datetimeToString(bean.Fechainicio) + "," + UtilCast.datetimeToString(bean.Fechafin) + "," + UtilCast.stringToString(bean.Descripcion) + "," + UtilCast.stringToString(bean.Aportante) + ")" + System.Environment.NewLine; ;
            bean.listadetalle.ForEach(cur =>
            {
                query = query + "insert into sgseguridadsys.PS_CONSUMO_NUTRICIONAL(ID_INSTITUCION, ID_CONSUMO, ID_CONSUMO_NUTRICIONAL, ID_ITEM, CANTIDAD_SOLICITADA, COMENTARIOS, ESTADO) values (" + UtilCast.stringToString(cur.IdInstitucion) + "," + UtilCast.intToString(cur.IdConsumo) + "," + UtilCast.intToString(cur.IdConsumoNutricional) + "," + UtilCast.stringToString(cur.IdItem) + "," + UtilCast.decimalToString(cur.Cantidadsolicitada) + "," + UtilCast.stringToString(cur.Comentarios) + ",'A')" + System.Environment.NewLine;
            });
            return query;
        }

        public static String queryItem(PsItem bean)
        {
            var query = "insert into sgseguridadsys.PS_ITEM(ID_ITEM, ID_TIPO_ITEM, NOMBRE, ID_UNIDAD_MEDIDA, ID_CLASE, ID_GRUPO, ESTADO) values (" + UtilCast.stringToString(bean.IdItem) + "," + UtilCast.stringToString(bean.IdTipoItem) + "," + UtilCast.stringToString(bean.Nombre) + "," + UtilCast.stringToString(bean.IdUnidadMedida) + "," + UtilCast.stringToString(bean.IdClase) + "," + UtilCast.stringToString(bean.IdGrupo) + "," + UtilCast.stringToString(bean.Estado) + ")";
            return query;
        }

        public static String queryNutricion(PsNutricion bean)
        {
            var query = "insert into sgseguridadsys.PS_NUTRICION(VARIACION_PESO,ID_INSTITUCION, ID_BENEFICIARIO, ID_NUTRICION, ID_PERIODO, FECHA_INFORME, PESO, TALLA, IMC, PESO_EDAD, TALLA_EDAD, PESO_TALLA, ID_VALORACION, COMENTARIOS, ESTADO, ID_PERIMETRO_PIERNA, ID_PRESION_MENSUAL, ID_ORIGEN, EVALUADO, ID_SUPLEMENTO_NUTRICIONAL, SUPLEMENTO_NUTRICIONAL_POR_DIA, ID_TIPO_DIETA, TIPO_DIETA_POR_DIA) values(" + UtilCast.decimalToString(bean.VariacionPeso) + "," + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdNutricion) + "," + UtilCast.stringToString(bean.IdPeriodo) + "," + UtilCast.datetimeToString(bean.FechaInforme) + "," + UtilCast.decimalToString(bean.Peso) + "," + UtilCast.decimalToString(bean.Talla) + "," + UtilCast.stringToString(bean.Imc) + "," + UtilCast.stringToString(bean.PesoEdad) + "," + UtilCast.stringToString(bean.TallaEdad) + "," + UtilCast.stringToString(bean.PesoTalla) + "," + UtilCast.stringToString(bean.IdValoracion) + "," + UtilCast.stringToString(bean.Comentarios) + "," + "'A'" + "," + UtilCast.stringToString(bean.IdPerimetroPierna) + "," + UtilCast.stringToString(bean.IdPresionMensual) + "," + UtilCast.stringToString(bean.IdOrigen) + "," + UtilCast.stringToString(bean.Evaluado) + "," + UtilCast.stringToString(bean.IdSuplementoNutricional) + "," + UtilCast.intToString(bean.SuplementoNutricionalPorDia) + "," + UtilCast.stringToString(bean.IdTipoDieta) + "," + UtilCast.intToString(bean.TipoDietaPorDia) + ")";//+ System.Environment.NewLine;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           //query = query + "GO;";
            return query;
        }

        public static String queryEntidad(PsEntidad bean)
        {
            var query = "INSERT INTO sgseguridadsys.PS_ENTIDAD(NOMBRECOMPLETO,ID_TIPO_DOCUMENTO, ID_ENTIDAD, APELLIDO_PATERNO, APELLIDO_MATERNO, NOMBRES, ID_SEXO, FECHA_NACIMIENTO, EDAD, EDAD_MESES, ID_NACIMIENTO_PAIS, ID_NACIMIENTO_UBIGEO, DOCUMENTO, ID_ESTADO_CIVIL, DOMICILIO_ID_UBIGEO, DOMICILIO_DIRECCION, DOMICILIO_REFERENCIA, APODERADO_NOMBRE, APODERADO_NRO_DOCUMENTO, FLG_FAMILIARES, APODERADO_ESPOSOA, FLG_PENSIONISTA, INGRESO_MENSUAL, COMENTARIOS_REFERENCIA_FAMILIAR, ID_NIVEL_ACADEMICO, TELEFONO2, ID_CARNET_CONADIS, COMENTARIOS, ID_DISCAPACIDAD, ID_TENENCIA,ID_MATERIAL_CONSTRUCCION,ID_SERVICIO_AGUA,ID_SERVICIO_DESAGUE,ID_SERVICIO_ELECTRICIDAD,TELEFONO1,PADRE_NOMBRE,PADRE_ID_VIVE,PADRE_NRO_DOCUMENTO,PADRE_ID_OCUPACION,MADRE_NOMBRE,MADRE_ID_VIVE,MADRE_NRO_DOCUMENTO,MADRE_ID_OCUPACION) VALUES (" + UtilCast.stringToString(bean.Nombrecompleto) + "," + UtilCast.stringToString(bean.IdTipoDocumento) + "," + UtilCast.intToString(bean.IdEntidad) + "," + UtilCast.stringToString(bean.ApellidoPaterno) + "," + UtilCast.stringToString(bean.ApellidoMaterno) + "," + UtilCast.stringToString(bean.Nombres) + "," + UtilCast.stringToString(bean.IdSexo) + "," + UtilCast.datetimeToString(bean.FechaNacimiento) + "," + UtilCast.intToString(bean.Edad) + "," + UtilCast.intToString(bean.EdadMeses) + "," + UtilCast.stringToString(bean.IdNacimientoPais) + "," + UtilCast.stringToString(bean.IdNacimientoUbigeo) + "," + UtilCast.stringToString(bean.Documento) + "," + UtilCast.stringToString(bean.IdEstadoCivil) + "," + UtilCast.stringToString(bean.DomicilioIdUbigeo) + "," + UtilCast.stringToString(bean.DomicilioDireccion) + "," + UtilCast.stringToString(bean.DomicilioReferencia) + "," + UtilCast.stringToString(bean.ApoderadoNombre) + "," + UtilCast.stringToString(bean.ApoderadoNroDocumento) + "," + UtilCast.stringToString(bean.flgFamiliares) + "," + UtilCast.stringToString(bean.ApoderadoEsposoa) + "," + UtilCast.stringToString(bean.flgPensionista) + "," + UtilCast.decimalToString(bean.IngresoMensual) + "," + UtilCast.stringToString(bean.ComentariosReferenciaFamiliar) + "," + UtilCast.stringToString(bean.IdNivelAcademico) + "," + UtilCast.stringToString(bean.Telefono2) + "," + UtilCast.stringToString(bean.IdCarnetConadis) + "," + UtilCast.stringToString(bean.Comentarios) + "," + UtilCast.stringToString(bean.IdDiscapacidad) + "," + UtilCast.stringToString(bean.IdTenencia) + "," + UtilCast.stringToString(bean.IdMaterialConstruccion) + "," + UtilCast.stringToString(bean.IdServicioAgua) + "," + UtilCast.stringToString(bean.IdServicioDesague) + "," + UtilCast.stringToString(bean.IdServicioElectricidad) + "," + UtilCast.stringToString(bean.Telefono1) + "," + UtilCast.stringToString(bean.PadreNombre) + "," + UtilCast.stringToString(bean.PadreIdVive) + "," + UtilCast.stringToString(bean.PadreNroDocumento) + "," + UtilCast.stringToString(bean.PadreIdOcupacion) + "," + UtilCast.stringToString(bean.MadreNombre) + "," + UtilCast.stringToString(bean.MadreIdVive) + "," + UtilCast.stringToString(bean.MadreNroDocumento) + "," + UtilCast.stringToString(bean.MadreIdOcupacion) + ")";
            return query;
        }
        public static String queryDocumento(PsEntidadDocumento bean)
        {
            var query = "insert into sgseguridadsys.PS_ENTIDAD_DOCUMENTO(ID_ENTIDAD, ID_TIPO_DOCUMENTO) values (" + UtilCast.intToString(bean.IdEntidad) + "," + UtilCast.stringToString(bean.IdTipoDocumento) + ")";//+ System.Environment.NewLine;
                                                                                                                                                                                                                     //query = query + "GO;";
            return query;
        }

        public static String queryMiscelaneo(MaMiscelaneosdetalle bean)
        {
            var query = "insert into MA_MiscelaneosDetalle(AplicacionCodigo, Compania, CodigoElemento, CodigoTabla, Estado, DescripcionLocal) values(" + UtilCast.stringToString(bean.Aplicacioncodigo) + "," + UtilCast.stringToString(bean.Compania) + "," + UtilCast.stringToString(bean.Codigoelemento) + "," + UtilCast.stringToString(bean.Codigotabla) + ",'A'," + UtilCast.stringToString(bean.Descripcionlocal) + ")";//+ System.Environment.NewLine;
                                                                                                                                                                                                                                                                                                                                                                                                                               //query = query + "GO;";
            return query;
        }

        public static String queryIngreso(PsBeneficiarioIngreso bean)
        {
            var query = "insert into sgseguridadsys.PS_BENEFICIARIO_INGRESO(ID_INSTITUCION, ID_BENEFICIARIO, ID_INGRESO, FECHA_INGRESO, DIAS_PERMANENCIA, ID_SITUACION_LEGAL, ID_INSTITUCION_DERIVA, ID_AREA, COMENTARIOS ,ESTADO) values (" + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdIngreso) + "," + UtilCast.datetimeToString(bean.FechaIngreso) + "," + UtilCast.intToString(bean.DiasPermanencia) + "," + UtilCast.stringToString(bean.IdSituacionLegal) + "," + UtilCast.stringToString(bean.IdInstitucionDeriva) + "," + UtilCast.stringToString(bean.IdArea) + "," + UtilCast.stringToString(bean.Comentarios) + "," + "'A'" + ")";// + System.Environment.NewLine;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      //query = query + "GO;";
            return query;
        }

        public static String queryCausal(PsBeneficiarioIngresoCausal bean)
        {
            var query = "insert into sgseguridadsys.PS_BENEFICIARIO_INGRESO_CAUSAL(ID_INSTITUCION, ID_BENEFICIARIO, ID_INGRESO, ID_CAUSAL) values (" + UtilCast.stringToString(bean.IdInstitucion) + "," + UtilCast.intToString(bean.IdBeneficiario) + "," + UtilCast.intToString(bean.IdIngreso) + "," + UtilCast.stringToString(bean.IdCausal) + ")";//+ System.Environment.NewLine;

            return query;
        }

        public static String queryEquipamiento(PsEntidadEquipamiento bean)
        {
            var query = "insert into sgseguridadsys.PS_ENTIDAD_EQUIPAMIENTO(ID_EQUIPAMIENTO, ID_ENTIDAD) values (" + UtilCast.stringToString(bean.IdEquipamiento) + "," + UtilCast.intToString(bean.IdEntidad) + ")";
            return query;
        }

        public static String queryPariente(PsEntidadPariente bean)
        {
            var query = "insert into sgseguridadsys.PS_ENTIDAD_PARIENTE(ID_ENTIDAD, ID_PARIENTE, ID_PARENTESCO, PARIENTE) values (" + UtilCast.intToString(bean.IdEntidad) + "," + UtilCast.intToString(bean.IdPariente) + "," + UtilCast.stringToString(bean.IdParentesco) + "," + UtilCast.stringToString(bean.Pariente) + ")";
            return query;
        }

        public static String queryPrograma(PsEntidadProgramaSocial bean)
        {
            var query = "insert into sgseguridadsys.PS_ENTIDAD_PROGRAMA_SOCIAL (ID_ENTIDAD, ID_PROGRAMA_SOCIAL) values (" + UtilCast.intToString(bean.IdEntidad) + "," + UtilCast.stringToString(bean.IdProgramaSocial) + ")";// + System.Environment.NewLine;
                                                                                                                                                                                                                              //query = query + "GO;";
            return query;
        }

        public static String querySeguro(PsEntidadSeguroSocial bean)
        {
            var query = "insert into sgseguridadsys.PS_ENTIDAD_SEGURO_SOCIAL(ID_ENTIDAD, ID_SEGURO_SOCIAL, SEGURO_SOCIAL) values (" + UtilCast.intToString(bean.IdEntidad) + "," + UtilCast.stringToString(bean.IdSeguroSocial) + "," + UtilCast.stringToString(bean.SeguroSocial) + ")";// + System.Environment.NewLine;
                                                                                                                                                                                                                                                                                         //query = query + "GO;";
            return query;
        }

        public static String calcularAnioRetraso(SqlConnection conn, String grado, DateTime? nacimiento, Int32? persona)
        {
            SqlCommand command = new SqlCommand("select sgseguridadsys.FN_CALCULAR_RETRASO_EDUCATIVO(@pIdGradoAcademico, @pFechaNaciomiento, null)", conn);

            command.Parameters.AddWithValue("@pIdGradoAcademico", grado == null ? "" : grado);
            command.Parameters.AddWithValue("@pFechaNaciomiento", nacimiento.HasValue ? nacimiento : new DateTime(1900, 1, 1));
            command.Parameters.AddWithValue("@pIdPersona", persona.HasValue ? persona : -1);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            return null;
        }

        public static Object[] obtenerCalculosNutricion(SqlConnection conn, PsEntidad value, Int32? edad, Int32? meses)
        {
            Object[] valores = new Object[6];
            SqlCommand command = new SqlCommand("sgseguridadsys.SNp_NUTRICION_CALCULOS_CARGA", conn);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@v_SEXO", value.IdSexo);
            command.Parameters.AddWithValue("@v_EDAD_ANIO", edad.HasValue ? edad : -1);
            command.Parameters.AddWithValue("@v_EDAD_MESES", meses.HasValue ? meses : -1);
            command.Parameters.AddWithValue("@v_FECHANAC", value.FechaNacimiento.HasValue ? value.FechaNacimiento.Value : new DateTime(1900, 1, 1));
            command.Parameters.AddWithValue("@pPeso", value.nutricion.Peso.HasValue ? value.nutricion.Peso : -1);
            command.Parameters.AddWithValue("@pTalla", value.nutricion.Talla.HasValue ? value.nutricion.Talla : -1);
            command.Parameters.AddWithValue("@pHemoglobina", value.salud.Hemoglobina.HasValue ? value.salud.Hemoglobina : -1);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        valores[0] = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        valores[1] = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        valores[2] = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        valores[3] = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        valores[4] = reader.GetString(4);
                    if (!reader.IsDBNull(5))
                        valores[5] = reader.GetDecimal(5);
                }
            }

            return valores;
        }

        public static Int32[] generarCodigos(SqlConnection conn)
        {
            Int32[] codigos = new Int32[5];
            SqlCommand command = new SqlCommand("select (select isnull(max(ID_ENTIDAD), 0)+1 from sgseguridadsys.PS_ENTIDAD ),( select isnull(max(ID_NUTRICION ), 0)+1from sgseguridadsys.PS_NUTRICION ),( select isnull(max(ID_SALUD ), 0)+1 from sgseguridadsys.PS_SALUD ),( select isnull(max(ID_CAPACIDAD ), 0)+1 from sgseguridadsys.PS_CAPACIDAD),( select isnull(max(ID_SOCIO_AMBIENTAL ), 0)+1 from sgseguridadsys.PS_SOCIO_AMBIENTAL)", conn);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        codigos[0] = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        codigos[1] = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                        codigos[2] = reader.GetInt32(2);
                    if (!reader.IsDBNull(3))
                        codigos[3] = reader.GetInt32(3);
                    if (!reader.IsDBNull(4))
                        codigos[4] = reader.GetInt32(4);
                }
            }

            return codigos;
        }

        public static String obtenerPais(SqlConnection conn, String value)
        {
            SqlCommand command = new SqlCommand("select top 1 rtrim(Pais) from Pais where DescripcionCorta = @pais", conn);
            command.Parameters.AddWithValue("@pais", value);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            if (!UString.estaVacio(value))
            {
                return value.Substring(0, 3);//MENSAJES = MENSAJES + "No se encontro valor para el país " + value + System.Environment.NewLine;
            }

            return null;
        }

        public static String obtenerDepartamento(SqlConnection conn, String value)
        {

            if (value != null)
            {
                value = value.Replace("Cuzco", "Cusco");
                value = value.Replace("Huánuco", "HUANUCO");
                value = value.Replace("Junín", "JUNIN");
                value = value.Replace(" Trujillo", "Trujillo");

                switch (value)
                {
                    case "NO LLENAN":
                    case " Arica":
                    case " Belgrado":
                    case " Madrid":
                    case "Hungria":
                    case "Irlanda":
                    case "Fleury":
                    case "China":
                    case "Burgos":
                        return null;

                    default:
                        break;
                }


            }

            SqlCommand command = new SqlCommand("select rtrim(Departamento) from Departamento where DescripcionCorta = @dep", conn);
            command.Parameters.AddWithValue("@dep", value);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            if (!UString.estaVacio(value))
            {
                //MENSAJES = MENSAJES + "No se encontro valor para el departamento " + value + System.Environment.NewLine;
                return "[error]";
            }

            return null;
        }
        public static string obtenerArea(SqlConnection conn, String value, String value2)
        {



            if (value2 != null)
            {
                if (value2== "NO LLENAN")
                {
                    return null;
                }
                if (value2 == "No aplica")
                {
                    return null;
                }
                value2 = value2.Replace("Pabellon de Hombres", "Pabellón de Varones");
                value2 = value2.Replace("Pabellon de Mujeres", "Pabellón de Mujeres");
                value2 = value2.Replace("Enfermería Mujeres", "Enfermeria de Mujeres");
                value2 = value2.Replace("Enfermeria de Hombres", "Enfermeria de Varones");
                value2 = value2.Replace("Pabellon Especiales (El Carmen)", "Pabellón el Carmen");
                value2 = value2.Replace("Pabellon de Sacerdotes", "Pabellón de Sacerdotes");
            }
            else
            {
                return null;
            }

            SqlCommand command = new SqlCommand("select rtrim(ID_AREA) from sgseguridadsys.PS_INSTITUCION_AREA  where ID_INSTITUCION = @ins and NOMBRE = @val", conn);
            command.Parameters.AddWithValue("@ins", value);
            command.Parameters.AddWithValue("@val", value2);
            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            if (!UString.estaVacio(value2))
            {
                Program.MENSAJES = Program.MENSAJES + "No se encontro valor para la residencia" + value2 + System.Environment.NewLine;
            }

            return null;
        }
        public static string obtenerProvincia(SqlConnection conn, String value, String value2)
        {

            if (value2 != null)
            {
                value2 = value2.Replace("Breña", "BRENA");
            }

            SqlCommand command = new SqlCommand("select rtrim(Provincia) from Provincia where Departamento = @dep and DescripcionCorta = @val", conn);
            command.Parameters.AddWithValue("@dep", value);
            command.Parameters.AddWithValue("@val", value2);
            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            if (!UString.estaVacio(value2))
            {
                return "[error]";
                //MENSAJES = MENSAJES + "No se encontro valor para la Provincia " + value2 + System.Environment.NewLine;
            }

            return null;
        }
        public static string obtenerDistrito(SqlConnection conn, String value, String value2, String value3)
        {

            switch (value3)
            {
                case "CHOSICA":
                    value3 = "LURIGANCHO";
                    break;
                case "SMP":
                    value3 = "SAN MARTIN DE PORRES";
                    break;
                case "SURCO":
                    value3 = "SURCO - LIMA";
                    break;
                case "Breña":
                    value3 = "BRENA";
                    break;
                case "BREÑA":
                    value3 = "BRENA";
                    break;
                case "VMT":
                    value3 = "VILLA MARIA DEL TRIU";
                    break;
                case "SJL":
                    value3 = "SAN JUAN DE LURIGANC";
                    break;
                case "SJM":
                    value3 = "SAN JUAN DE MIRAFLOR";
                    break;
                default:
                    break;
            }

            SqlCommand command = new SqlCommand("select rtrim(CodigoPostal) from ZonaPostal where Departamento = @dep and Provincia = @pro and DescripcionCorta = @val", conn);
            command.Parameters.AddWithValue("@dep", value);
            command.Parameters.AddWithValue("@pro", value2);
            command.Parameters.AddWithValue("@val", value3);

            command.ExecuteNonQuery();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                        return reader.GetString(0);
                }
            }

            if (!UString.estaVacio(value3))
            {
                return "[error]";
                //MENSAJES = MENSAJES + "No se encontro valor para el distrito " + value3 + System.Environment.NewLine;
            }

            return null;
        }

        public static string datetimeToString(Nullable<DateTime> dateTime)
        {
            return (dateTime.HasValue ? "'" + dateTime.Value.ToString("yyyy-MM-dd") + "'" : "null");
        }
        public static string stringToString(String value)
        {
            return (value != null ? "'" + value + "'" : "null");
        }
        public static string intToString(Int32? value)
        {
            return (value.HasValue ? value.ToString() : "null");
        }
        public static string decimalToString(Decimal? value)
        {
            return (value.HasValue ? value.ToString() : "null");
        }
        public static string doubleToString(Double? value)
        {
            return (value.HasValue ? value.ToString() : "null");
        }

        public static Nullable<DateTime> valueToDateTime(ExcelWorksheet worksheet, int row, int col)
        {
            var value = worksheet.Cells[row, col].Value;
            if (value == null)
            {
                return null;
            }
            worksheet.Cells[row, col].Style.Numberformat.Format = "MM/dd/yyyy";
            value = worksheet.Cells[row, col].Value;

            if (value != null)
            {
                if (value.ToString().Length == 4)
                {
                    Program.MENSAJES = Program.MENSAJES + "Error con la fecha [row-col] [" + row + " - " + col + "]" + System.Environment.NewLine;
                    return new DateTime(Convert.ToInt32(value), 1, 1);
                }
                if (value.ToString() == "OCTUBRE")
                {
                    Program.MENSAJES = Program.MENSAJES + "Error con la fecha [row-col] [" + row + " - " + col + "]" + System.Environment.NewLine;
                    return null;
                }
            }

            return value == null ? null as DateTime? : Convert.ToDateTime(value.ToString());
        }
        public static Int32? valueToInt(ExcelWorksheet worksheet, int row, int col)
        {
            var value = valueFromFila(worksheet, row, col);
            if (value != null)
            {
                if (value.ToString() == "X" || value.ToString() == "SI")
                {
                    return null;
                }
            }
            return value == null ? null as Int32? : Convert.ToInt32(value.ToString());
        }
        public static Int32?[] calcularEdad(DateTime? value, DateTime referencia)
        {
            var valores = new Int32?[2];
            if (value.HasValue)
            {
                DateTime nacimiento = value.Value;
                DateTime hoy = referencia;

                int edadAnos = hoy.Year - nacimiento.Year;
                if (hoy.Month < nacimiento.Month || (hoy.Month == nacimiento.Month &&
                hoy.Day < nacimiento.Day))
                    edadAnos--;
                int edadMeses = hoy.Month - nacimiento.Month;
                if (hoy.Day < nacimiento.Day)
                    edadMeses--;
                if (edadMeses < 0)
                    edadMeses += 12;
                valores[0] = edadAnos;
                valores[1] = edadAnos * 12 + edadMeses;
            }
            return valores;
        }
        public static Decimal? valueToDecimal(ExcelWorksheet worksheet, int row, int col)
        {
            var value = valueFromFila(worksheet, row, col);
            return value == null ? null as Decimal? : Convert.ToDecimal(value.ToString());
        }
        public static Double? valueToDouble(ExcelWorksheet worksheet, int row, int col)
        {
            var value = valueFromFila(worksheet, row, col);
            return value == null ? null as Double? : Convert.ToDouble(value.ToString());
        }
        public static String valueToString(ExcelWorksheet worksheet, int row, int col)
        {
            var value = valueFromFila(worksheet, row, col);
            return value == null ? null : value.ToString();
        }
        public static Object valueFromFila(ExcelWorksheet worksheet, int row, int col)
        {
            return worksheet.Cells[row, col].Value;
        }
        public static DtoCapacidad calcularBarthel(PsCapacidad bean)
        {

            Nullable<Decimal> Porcentaje = 0;
            var tiene = false;
            DtoCapacidad retorno = new DtoCapacidad();

            if (bean.Barthel1.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel1;
            }
            if (bean.Barthel2.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel2;
            }
            if (bean.Barthel3.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel3;
            }
            if (bean.Barthel4.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel4;
            }
            if (bean.Barthel5.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel5;
            }
            if (bean.Barthel6.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel6;
            }
            if (bean.Barthel7.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel7;
            }
            if (bean.Barthel8.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel8;
            }
            if (bean.Barthel9.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel9;
            }
            if (bean.Barthel10.HasValue)
            {
                tiene = true;
                Porcentaje = Porcentaje + bean.Barthel10;
            }

            if (!tiene)
            {
                return retorno;
            }

            retorno.PorcentajeGradoBarthel = Porcentaje;

            if (retorno.PorcentajeGradoBarthel >= 0 && retorno.PorcentajeGradoBarthel <= 20)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Total";
            }

            if (retorno.PorcentajeGradoBarthel >= 20 && retorno.PorcentajeGradoBarthel <= 35)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Grave";
            }

            if (retorno.PorcentajeGradoBarthel >= 40 && retorno.PorcentajeGradoBarthel <= 55)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Moderado";
            }

            if (retorno.PorcentajeGradoBarthel >= 60 && retorno.PorcentajeGradoBarthel <= 99)
            {
                retorno.GradoDependenciaBarthel = "Dependiente leve";
            }

            if (retorno.PorcentajeGradoBarthel >= 100)
            {
                retorno.GradoDependenciaBarthel = "Independiente";
            }


            return retorno;
        }
        public static DtoCapacidad calcularKatz(PsCapacidad bean)
        {
            Nullable<Decimal> Porcentaje = 0; ;
            DtoCapacidad retorno = new DtoCapacidad();

            if (bean.Katz1 != null)
            {
                Porcentaje = Porcentaje + bean.Katz1;
            }
            if (bean.Katz2 != null)
            {
                Porcentaje = Porcentaje + bean.Katz2;
            }
            if (bean.Katz3 != null)
            {
                Porcentaje = Porcentaje + bean.Katz3;
            }
            if (bean.Katz4 != null)
            {
                Porcentaje = Porcentaje + bean.Katz4;
            }
            if (bean.Katz5 != null)
            {
                Porcentaje = Porcentaje + bean.Katz5;
            }
            if (bean.Katz6 != null)
            {
                Porcentaje = Porcentaje + bean.Katz6;
            }

            retorno.PorcentajeGradoKatz = Porcentaje;

            if (retorno.PorcentajeGradoKatz == 0)
            {
                retorno.GradoDependenciaKatz = "Independiente";
            }
            else if (retorno.PorcentajeGradoKatz >= 1 && retorno.PorcentajeGradoKatz <= 5)
            {
                retorno.GradoDependenciaKatz = "Dependiente Parcial";
            }
            else if (retorno.PorcentajeGradoKatz == 6)
            {
                retorno.GradoDependenciaKatz = "Dependiente Total";
            }


            return retorno;
        }

        public static String parentesco(String pare)
        {
            switch (pare)
            {
                case "Tio(a)":
                    return "TIO";
                case "Abuelo(a)":
                case "abuelo":
                    return "ABU";
                case "Primo(a)":
                    return "PRI";
                case "Hermano(a)":
                case "Hermana":
                    return "HER";
                case null:
                case "Otros":
                    return null;
                default:
                    Program.MENSAJES = Program.MENSAJES + "Error con el parentesco " + pare + System.Environment.NewLine; ;
                    return null;
            }
        }
    }
}
