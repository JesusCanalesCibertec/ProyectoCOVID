using net.royal.spring.core.dominio;
using net.royal.spring.framework.core;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.programasocial.dominio.dtos;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CargaExcel
{
    public class NNA
    {
        public static void procesarNutricion(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, String prog, String institucion, List<MaMiscelaneosdetalle> maMiscelaneosdetalles)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga NUTRICION para => " + institucion);

            List<PsNutricion> listaNutricion = new List<PsNutricion>();
            List<String> listaUpdates = new List<String>();

            var programa = prog;

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsEntidad entidad = null;
                List<PsEntidad> entidades = null;

                var nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                var apepat = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                var apemat = UtilCast.valueToString(sheet, beginRow, beginCol + 2);

                //obtener la entidad>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                entidades = Program.LISTA_GENERAL.FindAll(x => x.ingreso.IdInstitucion == institucion && x.ApellidoPaterno == apepat && x.ApellidoMaterno == apemat && x.Nombres == nombre);

                if (entidades.Count == 0)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " no tiene FUN" + System.Environment.NewLine;
                    continue;
                }
                if (entidades.Count > 1)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene mas de 1 FUN" + System.Environment.NewLine;
                    continue;
                }

                entidad = entidades[0];

                var nutricion = new PsNutricion();

                nutricion.IdInstitucion = institucion;
                nutricion.IdBeneficiario = entidad.IdEntidad;
                nutricion.IdNutricion = Program.inicioNutricion;
                try
                {
                    nutricion.FechaInforme = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 4);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }
                var evaluado = true;

                nutricion.IdOrigen = "EVA";
                if (!nutricion.FechaInforme.HasValue)
                {
                    //evaluado = false;
                    //nutricion.Evaluado = "S";
                    nutricion.FechaInforme = new DateTime(2018, 12, 20);
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " no tiene fecha de ingreso para el componente NUTRICION se pone la fecha 20/12/2018 " + System.Environment.NewLine;
                }

                var mes = nutricion.FechaInforme.Value.Month;
                nutricion.IdPeriodo = nutricion.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");

                nutricion.Estado = "A";
                nutricion.IdOrigen = "EVA";

                if (evaluado)
                {



                    try
                    {
                        nutricion.Peso = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 5);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }

                    try
                    {
                        if (nutricion.IdInstitucion == "PURIC")
                        {
                            nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 6);
                        }
                        else
                        {
                            nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 6) * 100.0m;
                        }
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }



                    var valoracion = UtilCast.valueToString(sheet, beginRow, beginCol + 11);

                    switch (valoracion)
                    {
                        case "DNT (Talla)":
                        case "DNT(Talla)":
                            nutricion.IdValoracion = "03";
                            break;
                        case "Riesgo DNT(Peso)":
                        case "Riesgo de DNT (Peso)":
                            nutricion.IdValoracion = "04";
                            break;
                        case "DNT(Peso)":
                        case "DNT (Peso)":
                            nutricion.IdValoracion = "02";
                            break;
                        case "Normal":
                            nutricion.IdValoracion = "01";
                            break;
                        case "Riesgo DNT(Talla)":
                        case "Riesgo de DNT (Talla)":
                            nutricion.IdValoracion = "05";
                            break;
                        case "Sobrepeso":
                        case "Sobrepeso ":
                            nutricion.IdValoracion = "06";
                            break;
                        case "Obesidad":
                        case "obesidad":
                            nutricion.IdValoracion = "07";
                            break;
                        case "Riesgo de Sobrepeso":
                        case "Riesgo de sobrepeso":
                            nutricion.IdValoracion = "08";
                            break;
                        case null:
                            nutricion.IdValoracion = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra valoracion => " + valoracion + System.Environment.NewLine;
                            break;

                    }

                    var tipoDieta = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                    switch (tipoDieta)
                    {
                        case "Alta en fibra":
                            nutricion.IdTipoDieta = "ALFI";
                            break;
                        case "Blanda":
                            nutricion.IdTipoDieta = "BLAN";
                            break;
                        case "Hiperproteica":
                            nutricion.IdTipoDieta = "HIPER";
                            break;
                        case "Hipocalórico":
                            nutricion.IdTipoDieta = "HOCAL";
                            break;
                        case "Hipercalórico":
                            nutricion.IdTipoDieta = "HRCAL";
                            break;
                        case "Regular":
                            nutricion.IdTipoDieta = "REGU";
                            break;
                        case null:
                            nutricion.IdTipoDieta = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra IdTipoDieta => " + tipoDieta + System.Environment.NewLine;
                            break;
                    }

                    try
                    {
                        nutricion.TipoDietaPorDia = UtilCast.valueToInt(sheet, beginRow, beginCol + 13);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }

                    var suple = UtilCast.valueToString(sheet, beginRow, beginCol + 14);

                    switch (suple)
                    {
                        case "Formula enteral completa":
                        case "Formula enteral polimerica completa":
                        case "Formula enteral polimerica completa ":
                            nutricion.IdSuplementoNutricional = "001";
                            break;
                        case "Formula enteral especifica":
                            nutricion.IdSuplementoNutricional = "003";
                            break;
                        case "Modulo proteico ":
                        case "Modulo proteico":
                            nutricion.IdSuplementoNutricional = "004";
                            break;
                        case "Suplemento de hierro (+ácido fólico)":
                            nutricion.IdSuplementoNutricional = "002";
                            break;
                        case null:
                            nutricion.IdSuplementoNutricional = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra IdSuplementoNutricional => " + suple + System.Environment.NewLine;
                            break;
                    }

                    try
                    {
                        nutricion.SuplementoNutricionalPorDia = UtilCast.valueToInt(sheet, beginRow, beginCol + 15);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }

                    nutricion.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 16);

                    nutricion.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 17);

                    if (UString.estaVacio(nutricion.Evaluado))
                    {
                        nutricion.Evaluado = "";
                    }

                    nutricion.Estado = "A";
                    nutricion.IdOrigen = "EVA";

                    entidad.nutricion.Peso = nutricion.Peso;
                    entidad.nutricion.Talla = nutricion.Talla;

                    if (nutricion.Peso.HasValue && nutricion.Talla.HasValue && entidad.FechaNacimiento.HasValue)
                    {
                        var edades = UtilCast.calcularEdad(entidad.FechaNacimiento, nutricion.FechaInforme.Value);

                        Object[] calculosNutricion = UtilCast.obtenerCalculosNutricion(conn, entidad, edades[0], edades[1]);
                        nutricion.Imc = calculosNutricion[1] as string;
                        nutricion.TallaEdad = calculosNutricion[2] as string;
                        nutricion.PesoEdad = calculosNutricion[3] as string;
                        nutricion.PesoTalla = calculosNutricion[4] as string;
                        nutricion.VariacionPeso = calculosNutricion[5] as decimal?;
                    }
                    //fin relleno de campos de nutricion

                }
                listaNutricion.Add(nutricion);

                if (entidad.nutricion.IdNutricion == null)
                {
                    listaUpdates.Add("update sgseguridadsys.PS_BENEFICIARIO set ID_COMPONENTE_NUTRICION = " + nutricion.IdNutricion + " where ID_INSTITUCION = '" + nutricion.IdInstitucion + "'and ID_BENEFICIARIO = " + nutricion.IdBeneficiario);
                }

                Program.inicioNutricion++;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\" + Program.script + "_" + institucion + "_NUTRI.sql"))
            {
                Program.script++;
                listaNutricion.ForEach(x => file.WriteLine(UtilCast.queryNutricion(x)));
                listaUpdates.ForEach(x => file.WriteLine(x));
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + "_NUTRI.txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }

            Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga NUTRICION para => " + institucion);

        }
        public static void procesarCapacidades(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, String prog, String institucion, List<MaMiscelaneosdetalle> maMiscelaneosdetalles)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga CAPACIDADES para => " + institucion);

            List<PsCapacidad> listaCapacidades = new List<PsCapacidad>();
            List<String> listaUpdates = new List<String>();

            var programa = prog;

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsEntidad entidad = null;
                List<PsEntidad> entidades = null;

                var nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                var apepat = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                var apemat = UtilCast.valueToString(sheet, beginRow, beginCol + 2);

                //obtener la entidad>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                entidades = Program.LISTA_GENERAL.FindAll(x => x.ingreso.IdInstitucion == institucion && x.ApellidoPaterno == apepat && x.ApellidoMaterno == apemat && x.Nombres == nombre);

                if (entidades.Count == 0)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " no tiene FUN" + System.Environment.NewLine;
                    continue;
                }
                if (entidades.Count > 1)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene mas de 1 FUN" + System.Environment.NewLine;
                    continue;
                }

                entidad = entidades[0];

                var capacidad = new PsCapacidad();
                capacidad.IdOrigen = "EVA";
                capacidad.IdInstitucion = institucion;
                capacidad.IdBeneficiario = entidad.IdEntidad;
                capacidad.IdCapacidad = Program.inicioCapacidad;
                capacidad.Estado = "A";
                Program.inicioCapacidad++;

                var fe = UtilCast.valueToString(sheet, beginRow, beginCol + 4);

                if (fe == "FALLECIDO")
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " FALLECIDO en fecha ingreso" + System.Environment.NewLine;
                    continue;
                }

                if (fe == "EGRESO")
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " EGRESO en fecha ingreso" + System.Environment.NewLine;
                    continue;
                }

                try
                {
                    capacidad.FechaInforme = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 4);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                var evaluado = true;

                if (!capacidad.FechaInforme.HasValue)
                {
                    //evaluado = false;
                    //capacidad.Evaluado = "S";
                    capacidad.FechaInforme = new DateTime(2018, 12, 20);
                }

                var mes = capacidad.FechaInforme.Value.Month;
                capacidad.IdPeriodo = capacidad.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");

                if (evaluado)
                {


                    var tipoi = UtilCast.valueToString(sheet, beginRow, beginCol + 5);

                    switch (tipoi)
                    {
                        case "Alternativa (CEBA)":
                        case "Alternativa":
                        case "CEBA":
                            capacidad.IdTipoInstitucion = "ALT";
                            break;
                        case "CETPRO":
                            capacidad.IdTipoInstitucion = "CET";
                            break;
                        case "Especial (CEBE)":
                        case "Especializada":
                            capacidad.IdTipoInstitucion = "ESP";
                            break;
                        case "Ninguna":
                        case "Ninguno":
                        case null:
                            capacidad.IdTipoInstitucion = "NIN";
                            break;
                        case "Regular":
                        case "REGULAR":
                            capacidad.IdTipoInstitucion = "REG";
                            break;
                        case "Regular Inclusiva":
                            capacidad.IdTipoInstitucion = "REI";
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "Error con el IdTipoInstitucion " + tipoi + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            break;
                    }


                    var nivel = UtilCast.valueToString(sheet, beginRow, beginCol + 6);

                    switch (nivel)
                    {
                        case "Inicial":
                        case "INICIAL":
                        case "INICAL":
                            capacidad.IdNivel = "INIC";
                            break;
                        case "Primaria":
                        case "primaria":
                            capacidad.IdNivel = "PRIM";
                            break;
                        case "SECUNDARIA":
                        case "Secundaria":
                            capacidad.IdNivel = "SECU";
                            break;
                        case "Aprestamiento":
                            capacidad.IdNivel = "APRE";
                            break;
                        case null:
                        case "Regular":
                            capacidad.IdNivel = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "Error con el NIVEL " + nivel + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            break;
                    }

                    var grado = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                    switch (grado)
                    {
                        case "3 años":
                            capacidad.IdGradoEducativo = "3ANIO";
                            break;
                        case "4 años":
                            capacidad.IdGradoEducativo = "4ANIO";
                            break;
                        case "5 años":
                        case "5to añito":
                            capacidad.IdGradoEducativo = "5ANIO";
                            break;

                        case "1 Grado":
                        case "1er grado":
                        case "1 er grado":
                        case "1er gfrado":
                        case "1er grado ":
                        case "1ER GRADO":
                            capacidad.IdGradoEducativo = "1GRA";
                            break;
                        case "2 Grado":
                        case "2do grado":
                        case "2dogrado":
                            capacidad.IdGradoEducativo = "2GRA";
                            break;
                        case "3 Grado":
                        case "3er grado":
                        case "3er  grado":
                            capacidad.IdGradoEducativo = "3GRA";
                            break;
                        case "4 Grado":
                        case "4to grado":
                            capacidad.IdGradoEducativo = "4GRA";
                            break;
                        case "5 Grado":
                        case "5to grado":
                            capacidad.IdGradoEducativo = "5GRA";
                            break;
                        case "6 Grado":
                        case "6to grado":
                            capacidad.IdGradoEducativo = "6GRA";
                            break;

                        case "1er Año":
                        case "1er año":
                        case "1ER Año":
                            capacidad.IdGradoEducativo = "1ER";
                            break;
                        case "2do Año":
                        case "2do año":
                            capacidad.IdGradoEducativo = "2DO";
                            break;
                        case "3er Año":
                        case "3er año":
                        case "3er años":
                            capacidad.IdGradoEducativo = "3ER";
                            break;
                        case "4to Año":
                        case "4to año":
                            capacidad.IdGradoEducativo = "4TO";
                            break;
                        case "5to Año":
                        case "5to año":
                            capacidad.IdGradoEducativo = "5TO";
                            break;
                        case null:
                        case "No escolaridad":
                            capacidad.IdGradoEducativo = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "Error con el GRADO " + grado + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            break;
                    }

                    var idNotaLogi = UtilCast.valueToString(sheet, beginRow, beginCol + 8);
                    //idNotaLogi = UString.estaVacio(idNotaLogi) ? "N" : idNotaLogi;
                    idNotaLogi = idNotaLogi == "No Aplica" ? "N" : idNotaLogi;

                    capacidad.listaCursos.Add(new PsCapacidadCurso() { IdNota = idNotaLogi, IdCurso = "MATE", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdCapacidad = capacidad.IdCapacidad });

                    var idNotaComu = UtilCast.valueToString(sheet, beginRow, beginCol + 9);
                    //idNotaComu = UString.estaVacio(idNotaComu) ? "N" : idNotaComu;
                    idNotaComu = idNotaComu == "No Aplica" ? "N" : idNotaComu;

                    capacidad.listaCursos.Add(new PsCapacidadCurso() { IdNota = idNotaComu, IdCurso = "COMU", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdCapacidad = capacidad.IdCapacidad });


                    var idNotaLect = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                    //idNotaLect = UString.estaVacio(idNotaLect) ? "N" : idNotaLect;
                    idNotaLect = idNotaLect == "No Aplica" ? "N" : idNotaLect;

                    capacidad.listaCursos.Add(new PsCapacidadCurso() { IdNota = idNotaLect, IdCurso = "LECT", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdCapacidad = capacidad.IdCapacidad });


                    var idNotaAmbi = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                    //idNotaAmbi = UString.estaVacio(idNotaAmbi) ? "N" : idNotaAmbi;
                    idNotaAmbi = idNotaAmbi == "No Aplica" ? "N" : idNotaAmbi;

                    capacidad.listaCursos.Add(new PsCapacidadCurso() { IdNota = idNotaAmbi, IdCurso = "AMBI", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdCapacidad = capacidad.IdCapacidad });


                    var idNotaSoci = UtilCast.valueToString(sheet, beginRow, beginCol + 12);
                    //idNotaSoci = UString.estaVacio(idNotaSoci) ? "N" : idNotaSoci;
                    idNotaSoci = idNotaSoci == "No Aplica" ? "N" : idNotaSoci;

                    capacidad.listaCursos.Add(new PsCapacidadCurso() { IdNota = idNotaSoci, IdCurso = "SOCI", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdCapacidad = capacidad.IdCapacidad });

                    capacidad.ComentarioRendimiento = UtilCast.valueToString(sheet, beginRow, beginCol + 13);

                    var formaComunicacion = UtilCast.valueToString(sheet, beginRow, beginCol + 14);

                    switch (formaComunicacion)
                    {

                        case "Alternativa":
                            capacidad.IdTipoComunicacion = "ALT";
                            break;
                        case "BIMODAL ":
                        case "bimodal":
                        case "Bimodal":
                        case "BIMODAL":
                        case "ORAL/GESTUAL":
                            capacidad.IdTipoComunicacion = "BIM";
                            break;
                        case "ORAL ":
                        case "Oral / Verbal":
                        case "ORAL/VERBAL":
                        case "Oral/verbal":
                        case "Oral/Verbal":
                        case "Oral":
                        case "ORAL":
                            capacidad.IdTipoComunicacion = "ORA";
                            break;
                        case "Señas":
                            capacidad.IdTipoComunicacion = "SEN";
                            break;
                        case null:
                            capacidad.IdTipoComunicacion = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene tipo comunicacion " + formaComunicacion + System.Environment.NewLine;
                            break;
                    }

                    if (entidad.FechaNacimiento.HasValue && capacidad.IdTipoInstitucion != "NIN")
                    {
                        capacidad.AnioRetraso = UtilCast.calcularAnioRetraso(conn, capacidad.IdGradoEducativo, entidad.FechaNacimiento, null);// "[PENDIENTE CARGA]";

                    }
                    else
                    {
                        capacidad.AnioRetraso = null;
                    }


                    capacidad.ComentarioRetraso = UtilCast.valueToString(sheet, beginRow, beginCol + 16);

                    //FORMATIVO 17, 18, 19, 20, 21

                    var formativoCantidad = UtilCast.valueToInt(sheet, beginRow, beginCol + 21);
                    {
                        if (formativoCantidad == null)
                        {
                            formativoCantidad = 0;
                        }
                        var excelente = UtilCast.valueToString(sheet, beginRow, beginCol + 17);
                        var bueno = UtilCast.valueToString(sheet, beginRow, beginCol + 18);
                        var regular = UtilCast.valueToString(sheet, beginRow, beginCol + 19);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 19);

                        var nota = "";

                        if (excelente == "x" || excelente == "X")
                        {
                            nota = "EX";
                        }
                        else if (bueno == "x" || bueno == "X")
                        {
                            nota = "BU";
                        }
                        else if (regular == "x" || regular == "X")
                        {
                            nota = "RE";
                        }
                        else if (na == "x" || na == "X")
                        {
                            nota = "NA";
                        }
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 10, IdNota = nota, Cantidad = formativoCantidad, CreacionUsuario = "CARGAMASIVA" });
                    }

                    //FISICO    22, 23, 24, 25, 26

                    var formativoFisico = UtilCast.valueToInt(sheet, beginRow, beginCol + 26);
                    {
                        if (formativoFisico == null)
                        {
                            formativoFisico = 0;
                        }
                        var excelente = UtilCast.valueToString(sheet, beginRow, beginCol + 22);
                        var bueno = UtilCast.valueToString(sheet, beginRow, beginCol + 23);
                        var regular = UtilCast.valueToString(sheet, beginRow, beginCol + 24);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 25);

                        var nota = "";

                        if (excelente == "x" || excelente == "X")
                        {
                            nota = "EX";
                        }
                        else if (bueno == "x" || bueno == "X")
                        {
                            nota = "BU";
                        }
                        else if (regular == "x" || regular == "X")
                        {
                            nota = "RE";
                        }
                        else if (na == "x" || na == "X")
                        {
                            nota = "NA";
                        }
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 11, IdNota = nota, Cantidad = formativoFisico, CreacionUsuario = "CARGAMASIVA" });
                    }
                    //COGNITIVO 27, 28, 29, 30, 31
                    var formativoCognitivo = UtilCast.valueToInt(sheet, beginRow, beginCol + 31);
                    {
                        if (formativoCognitivo == null)
                        {
                            formativoCognitivo = 0;
                        }
                        var excelente = UtilCast.valueToString(sheet, beginRow, beginCol + 27);
                        var bueno = UtilCast.valueToString(sheet, beginRow, beginCol + 28);
                        var regular = UtilCast.valueToString(sheet, beginRow, beginCol + 29);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 30);

                        var nota = "";

                        if (excelente == "x" || excelente == "X")
                        {
                            nota = "EX";
                        }
                        else if (bueno == "x" || bueno == "X")
                        {
                            nota = "BU";
                        }
                        else if (regular == "x" || regular == "X")
                        {
                            nota = "RE";
                        }
                        else if (na == "x" || na == "X")
                        {
                            nota = "NA";
                        }
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 12, IdNota = nota, Cantidad = formativoCognitivo, CreacionUsuario = "CARGAMASIVA" });
                    }

                    capacidad.ComentarioTalleres = UtilCast.valueToString(sheet, beginRow, beginCol + 32);

                    //inicio bartel

                    //115

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 35) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 35) == "X")
                    {
                        capacidad.Barthel1 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 34) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 34) == "X")
                    {
                        capacidad.Barthel1 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 33) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 33) == "X")
                    {
                        capacidad.Barthel1 = 0;
                    }


                    //Trasladarse        OK 4

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 39) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 39) == "X")
                    {
                        capacidad.Barthel8 = 15;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 38) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 38) == "X")
                    {
                        capacidad.Barthel8 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 37) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 37) == "X")
                    {
                        capacidad.Barthel8 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 36) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 36) == "X")
                    {
                        capacidad.Barthel8 = 0;
                    }

                    //Arreglarse         OK 2

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 41) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 41) == "X")
                    {
                        capacidad.Barthel4 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 40) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 40) == "X")
                    {
                        capacidad.Barthel4 = 0;
                    }

                    //Uso del Retrete    OK 3


                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 44) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 44) == "X")
                    {
                        capacidad.Barthel7 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 43) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 43) == "X")
                    {
                        capacidad.Barthel7 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 42) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 42) == "X")
                    {
                        capacidad.Barthel7 = 0;
                    }

                    //Lavarse(baño)      OK 2

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 46) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 46) == "X")
                    {
                        capacidad.Barthel2 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 45) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 45) == "X")
                    {
                        capacidad.Barthel2 = 0;
                    }

                    //Deambulación       OK 4

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 50) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 50) == "X")
                    {
                        capacidad.Barthel9 = 15;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 49) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 49) == "X")
                    {
                        capacidad.Barthel9 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 48) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 48) == "X")
                    {
                        capacidad.Barthel9 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 47) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 47) == "X")
                    {
                        capacidad.Barthel9 = 0;
                    }

                    //Escaleras         OK 3

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 53) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 54) == "X")
                    {
                        capacidad.Barthel10 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 52) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 52) == "X")
                    {
                        capacidad.Barthel10 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 51) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 51) == "X")
                    {
                        capacidad.Barthel10 = 0;
                    }

                    //Vestido            OK 3

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 56) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 56) == "X")
                    {
                        capacidad.Barthel3 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 55) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 55) == "X")
                    {
                        capacidad.Barthel3 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 54) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 54) == "X")
                    {
                        capacidad.Barthel3 = 0;
                    }

                    //Deposición         OK 3

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 59) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 59) == "X")
                    {
                        capacidad.Barthel5 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 58) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 58) == "X")
                    {
                        capacidad.Barthel5 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 57) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 57) == "X")
                    {
                        capacidad.Barthel5 = 0;
                    }

                    //Micción            OK 3

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 62) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 62) == "X")
                    {
                        capacidad.Barthel6 = 10;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 61) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 61) == "X")
                    {
                        capacidad.Barthel6 = 5;
                    }
                    else if (UtilCast.valueToString(sheet, beginRow, beginCol + 60) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 60) == "X")
                    {
                        capacidad.Barthel6 = 0;
                    }

                    capacidad.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 64);
                    if (UString.estaVacio(capacidad.Evaluado))
                    {
                        capacidad.Evaluado = "";
                    }

                    DtoCapacidad dto = UtilCast.calcularBarthel(capacidad);

                    capacidad.GradoDependenciaBarthel = dto.GradoDependenciaBarthel;
                    capacidad.PorcentajeGradoBarthel = dto.PorcentajeGradoBarthel;

                    //fin bartel

                    //fin relleno de campos de capacidad
                }

                listaCapacidades.Add(capacidad);

                if (entidad.capacidad.IdCapacidad == null)
                {
                    listaUpdates.Add("update sgseguridadsys.PS_BENEFICIARIO set ID_COMPONENTE_CAPACIDAD = " + capacidad.IdCapacidad + " where ID_INSTITUCION = '" + capacidad.IdInstitucion + "'and ID_BENEFICIARIO = " + capacidad.IdBeneficiario);
                }
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\" + Program.script + "_" + institucion + "_CAPAC.sql"))
            {
                Program.script++;
                listaCapacidades.ForEach(x => file.WriteLine(UtilCast.queryCapacidad(x)));
                listaUpdates.ForEach(x => file.WriteLine(x));
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + "_CAPAC.txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }

            Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga CAPACIDADES para => " + institucion);
        }
        public static void procesarSocioambiental(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, String prog, String institucion, List<MaMiscelaneosdetalle> maMiscelaneosdetalles)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga SOCIOAMBIENTAL para => " + institucion);

            List<PsSocioAmbiental> listaSocio = new List<PsSocioAmbiental>();
            List<String> listaUpdates = new List<String>();

            var programa = prog;

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsEntidad entidad = null;
                List<PsEntidad> entidades = null;

                var nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                var apepat = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                var apemat = UtilCast.valueToString(sheet, beginRow, beginCol + 2);

                //obtener la entidad>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                entidades = Program.LISTA_GENERAL.FindAll(x => x.ingreso.IdInstitucion == institucion && x.ApellidoPaterno == apepat && x.ApellidoMaterno == apemat && x.Nombres == nombre);

                if (entidades.Count == 0)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " no tiene FUN" + System.Environment.NewLine;
                    continue;
                }
                if (entidades.Count > 1)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene mas de 1 FUN" + System.Environment.NewLine;
                    continue;
                }

                entidad = entidades[0];

                var socio = new PsSocioAmbiental();

                socio.IdInstitucion = institucion;
                socio.IdBeneficiario = entidad.IdEntidad;
                socio.IdSocioAmbiental = Program.inicioSocio;
                socio.IdOrigen = "EVA";



                if (UtilCast.valueToString(sheet, beginRow, beginCol + 4) == "EN ATENCION EN INTEGRANDO FAMILIAS")
                {
                    //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " esta EN ATENCION EN INTEGRANDO FAMILIAS para el componente SOCIOAMBIENTAL se pone la fecha de hoy " + DateTime.Today + System.Environment.NewLine;
                    continue;
                }
                try
                {

                    if (UtilCast.valueToString(sheet, beginRow, beginCol + 4) == "EGRESADA" || UtilCast.valueToString(sheet, beginRow, beginCol + 4) == "EGRESADO")
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " esta EGRESADO para el componente SOCIOAMBIENTAL se pone la fecha de hoy " + DateTime.Today + System.Environment.NewLine;
                        //continue;
                        socio.FechaInforme = new DateTime(2018, 12, 20);

                    }
                    else
                    {
                        socio.FechaInforme = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 4);

                    }
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                var evaluado = true;

                if (!socio.FechaInforme.HasValue)
                {
                    //evaluado = false;
                    //socio.Evaluado = "S";
                    socio.FechaInforme = new DateTime(2018, 12, 20);
                }

                var mes = socio.FechaInforme.Value.Month;
                socio.IdPeriodo = socio.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");


                if (evaluado)
                {

                    var integra1 = UtilCast.valueToString(sheet, beginRow, beginCol + 5);
                    var integra2 = UtilCast.valueToString(sheet, beginRow, beginCol + 6);
                    var integra3 = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                    var integra4 = UtilCast.valueToString(sheet, beginRow, beginCol + 8);

                    if (integra1 == "x" || integra1 == "X")
                    {
                        socio.IdConflictos = "EXCE";
                    }
                    else if (integra2 == "x" || integra2 == "X")
                    {
                        socio.IdConflictos = "BUEN";
                    }
                    else if (integra3 == "x" || integra3 == "X")
                    {
                        socio.IdConflictos = "REGU";
                    }
                    else if (integra4 == "x" || integra4 == "X")
                    {
                        socio.IdConflictos = "NAPL";
                    }

                    var participacion1 = UtilCast.valueToString(sheet, beginRow, beginCol + 9);
                    var participacion2 = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                    var participacion3 = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                    var participacion4 = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                    if (participacion1 == "x" || participacion1 == "X")
                    {
                        socio.IdEmocional = "EXCE";
                    }
                    else if (participacion2 == "x" || participacion2 == "X")
                    {
                        socio.IdEmocional = "BUEN";
                    }
                    else if (participacion3 == "x" || participacion3 == "X")
                    {
                        socio.IdEmocional = "REGU";
                    }
                    else if (participacion4 == "x" || participacion4 == "X")
                    {
                        socio.IdEmocional = "NAPL";
                    }

                    var asertividad1 = UtilCast.valueToString(sheet, beginRow, beginCol + 13);
                    var asertividad2 = UtilCast.valueToString(sheet, beginRow, beginCol + 14);
                    var asertividad3 = UtilCast.valueToString(sheet, beginRow, beginCol + 15);
                    var asertividad4 = UtilCast.valueToString(sheet, beginRow, beginCol + 16);

                    if (asertividad1 == "x" || asertividad1 == "X")
                    {
                        socio.IdAsertavidad = "EXCE";
                    }
                    else if (asertividad2 == "x" || asertividad2 == "X")
                    {
                        socio.IdAsertavidad = "BUEN";
                    }
                    else if (asertividad3 == "x" || asertividad3 == "X")
                    {
                        socio.IdAsertavidad = "REGU";
                    }
                    else if (asertividad4 == "x" || asertividad4 == "X")
                    {
                        socio.IdAsertavidad = "NAPL";
                    }

                    var comunicacion1 = UtilCast.valueToString(sheet, beginRow, beginCol + 17);
                    var comunicacion2 = UtilCast.valueToString(sheet, beginRow, beginCol + 18);
                    var comunicacion3 = UtilCast.valueToString(sheet, beginRow, beginCol + 19);
                    var comunicacion4 = UtilCast.valueToString(sheet, beginRow, beginCol + 20);

                    if (comunicacion1 == "x" || comunicacion1 == "X")
                    {
                        socio.IdComunicacion = "EXCE";
                    }
                    else if (comunicacion2 == "x" || comunicacion2 == "X")
                    {
                        socio.IdComunicacion = "BUEN";
                    }
                    else if (comunicacion3 == "x" || comunicacion3 == "X")
                    {
                        socio.IdComunicacion = "REGU";
                    }
                    else if (comunicacion4 == "x" || comunicacion4 == "X")
                    {
                        socio.IdComunicacion = "NAPL";
                    }

                    var participacion21 = UtilCast.valueToString(sheet, beginRow, beginCol + 21);
                    var participacion22 = UtilCast.valueToString(sheet, beginRow, beginCol + 22);
                    var participacion23 = UtilCast.valueToString(sheet, beginRow, beginCol + 23);
                    var participacion24 = UtilCast.valueToString(sheet, beginRow, beginCol + 24);

                    if (participacion21 == "x" || participacion21 == "X")
                    {
                        socio.IdCooperacion = "EXCE";
                    }
                    else if (participacion22 == "x" || participacion22 == "X")
                    {
                        socio.IdCooperacion = "BUEN";
                    }
                    else if (participacion23 == "x" || participacion23 == "X")
                    {
                        socio.IdCooperacion = "REGU";
                    }
                    else if (participacion24 == "x" || participacion24 == "X")
                    {
                        socio.IdCooperacion = "NAPL";
                    }

                    var empatia1 = UtilCast.valueToString(sheet, beginRow, beginCol + 25);
                    var empatia2 = UtilCast.valueToString(sheet, beginRow, beginCol + 26);
                    var empatia3 = UtilCast.valueToString(sheet, beginRow, beginCol + 27);
                    var empatia4 = UtilCast.valueToString(sheet, beginRow, beginCol + 28);

                    if (empatia1 == "x" || empatia1 == "X")
                    {
                        socio.IdEmpatia = "EXCE";
                    }
                    else if (empatia2 == "x" || empatia2 == "X")
                    {
                        socio.IdEmpatia = "BUEN";
                    }
                    else if (empatia3 == "x" || empatia3 == "X")
                    {
                        socio.IdEmpatia = "REGU";
                    }
                    else if (empatia4 == "x" || empatia4 == "X")
                    {
                        socio.IdEmpatia = "NAPL";
                    }

                    socio.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 29);
                    socio.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 30);
                    if (UString.estaVacio(socio.Evaluado))
                    {
                        socio.Evaluado = "";
                    }

                    //fin relleno de campos de capacidad
                }
                listaSocio.Add(socio);

                if (entidad.capacidad.IdCapacidad == null)
                {
                    listaUpdates.Add("update sgseguridadsys.PS_BENEFICIARIO set ID_COMPONENTE_SOCIO_AMBIENTAL = " + socio.IdSocioAmbiental + " where ID_INSTITUCION = '" + socio.IdInstitucion + "'and ID_BENEFICIARIO = " + socio.IdBeneficiario);
                }

                Program.inicioSocio++;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\" + Program.script + "_" + institucion + "_SOCIO.sql"))
            {
                Program.script++;
                listaSocio.ForEach(x => file.WriteLine(UtilCast.querySocio(x)));
                listaUpdates.ForEach(x => file.WriteLine(x));
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + "_SOCIO.txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }

            Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga SOCIOAMBIENTAL para => " + institucion);
        }
        public static void procesarSalud(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, String prog, String institucion, List<MaMiscelaneosdetalle> maMiscelaneosdetalles)
        {
            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga SALUD para => " + institucion);

            List<PsSalud> listaSalud = new List<PsSalud>();
            List<String> listaUpdates = new List<String>();

            var programa = prog;

            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {
                beginCol = inicioColumnaDatos - 1;

                PsEntidad entidad = null;
                List<PsEntidad> entidades = null;

                var nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                var apepat = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                var apemat = UtilCast.valueToString(sheet, beginRow, beginCol + 2);

                //obtener la entidad>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                entidades = Program.LISTA_GENERAL.FindAll(x => x.ingreso.IdInstitucion == institucion && x.ApellidoPaterno == apepat && x.ApellidoMaterno == apemat && x.Nombres == nombre);

                if (entidades.Count == 0)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " no tiene FUN" + System.Environment.NewLine;
                    continue;
                }
                if (entidades.Count > 1)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene mas de 1 FUN" + System.Environment.NewLine;
                    continue;
                }

                entidad = entidades[0];

                var salud = new PsSalud();

                salud.IdInstitucion = institucion;
                salud.IdBeneficiario = entidad.IdEntidad;
                salud.IdSalud = Program.inicioSalud;
                salud.IdOrigen = "EVA";
                salud.Estado = "A";
                Program.inicioSalud++;

                try
                {
                    salud.FechaInforme = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 4);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                var evaluado = true;
                if (!salud.FechaInforme.HasValue)
                {
                    //evaluado = false;
                    //salud.Evaluado = "S";
                    salud.FechaInforme = new DateTime(2018, 12, 20);
                    //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " no tiene fecha de ingreso para el componente SALUD se pone la fecha 20/12/2018 " + System.Environment.NewLine;
                }

                var mes = salud.FechaInforme.Value.Month;
                salud.IdPeriodo = salud.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");


                if (evaluado)
                {
                    try
                    {
                        salud.Hemoglobina = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 5);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }


                    var temp = new PsEntidad();
                    temp.IdSexo = entidad.IdSexo;
                    temp.salud.Hemoglobina = salud.Hemoglobina;
                    temp.Edad = entidad.Edad;
                    temp.EdadMeses = entidad.EdadMeses;
                    temp.FechaNacimiento = entidad.FechaNacimiento;

                    var edades = UtilCast.calcularEdad(entidad.FechaNacimiento, salud.FechaInforme.Value);
                    var datos = UtilCast.obtenerCalculosNutricion(conn, temp, edades[0], edades[1]);

                    salud.HemoglobinaResultado = datos[0] as string;

                    salud.IdTratamientoAnemia = UtilCast.valueToString(sheet, beginRow, beginCol + 7);

                    salud.IdDescarteSerologico = UtilCast.valueToString(sheet, beginRow, beginCol + 8) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 8) == "NO" ? "SI" : null;
                    salud.IdDescarteTbc = UtilCast.valueToString(sheet, beginRow, beginCol + 9) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 9) == "NO" ? "SI" : null;



                    //inicio sensorial

                    var visionNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                    if (visionNormal == "x" || visionNormal == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "NORM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionMode = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                    if (visionMode == "x" || visionMode == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "MODE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionGrave = UtilCast.valueToString(sheet, beginRow, beginCol + 12);
                    if (visionGrave == "x" || visionGrave == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "GRAV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionCegu = UtilCast.valueToString(sheet, beginRow, beginCol + 13);
                    if (visionCegu == "x" || visionCegu == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "CEGE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 14);
                    if (audioNormal == "x" || audioNormal == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "AUN", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioLeve = UtilCast.valueToString(sheet, beginRow, beginCol + 15);
                    if (audioLeve == "x" || audioLeve == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPL", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioMode = UtilCast.valueToString(sheet, beginRow, beginCol + 16);
                    if (audioMode == "x" || audioMode == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioSevera = UtilCast.valueToString(sheet, beginRow, beginCol + 17);
                    if (audioSevera == "x" || audioSevera == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "SEVE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioProfunda = UtilCast.valueToString(sheet, beginRow, beginCol + 18);
                    if (audioProfunda == "x" || audioProfunda == "X")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "PROF", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    //fin sensorial


                    var diagExtras = UtilCast.valueToString(sheet, beginRow, beginCol + 19);

                    if (!UString.estaVacio(diagExtras))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " Diagnóstico Médico (listado múltiple de preferencia check de selección)- Atención Interna => " + diagExtras + System.Environment.NewLine;
                    }



                    var gene = UtilCast.valueToString(sheet, beginRow, beginCol + 20);

                    if (gene == "x" || gene == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRGEN", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var neuro = UtilCast.valueToString(sheet, beginRow, beginCol + 21);

                    if (neuro == "x" || neuro == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRNEU", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var psiq = UtilCast.valueToString(sheet, beginRow, beginCol + 22);

                    if (psiq == "x" || psiq == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRPSI", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var odo = UtilCast.valueToString(sheet, beginRow, beginCol + 23);

                    if (odo == "x" || odo == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "ODO", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var agu = UtilCast.valueToString(sheet, beginRow, beginCol + 24);

                    if (agu == "x" || agu == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "AGU", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var quiru = UtilCast.valueToString(sheet, beginRow, beginCol + 25);

                    if (quiru == "x" || quiru == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "QUI", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }


                    var otrotra = UtilCast.valueToString(sheet, beginRow, beginCol + 26);

                    if (!UString.estaVacio(otrotra))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otro tratamiento interno=> " + otrotra + System.Environment.NewLine;
                    }



                    var est1 = UtilCast.valueToString(sheet, beginRow, beginCol + 27);

                    if (est1 == "x" || est1 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDS", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est2 = UtilCast.valueToString(sheet, beginRow, beginCol + 28);

                    if (est2 == "x" || est2 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEA", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est3 = UtilCast.valueToString(sheet, beginRow, beginCol + 29);

                    if (est3 == "x" || est3 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est4 = UtilCast.valueToString(sheet, beginRow, beginCol + 30);

                    if (est4 == "x" || est4 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEAC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }




                    var est5 = UtilCast.valueToString(sheet, beginRow, beginCol + 31);

                    if (est5 == "x" || est5 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDS", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est6 = UtilCast.valueToString(sheet, beginRow, beginCol + 32);

                    if (est6 == "x" || est6 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDEA", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est7 = UtilCast.valueToString(sheet, beginRow, beginCol + 33);

                    if (est7 == "x" || est7 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDEC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est8 = UtilCast.valueToString(sheet, beginRow, beginCol + 34);

                    if (est8 == "x" || est8 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDEAC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }


                    beginCol = beginCol + 2;



                    //inicio biomeca

                    var baston = UtilCast.valueToString(sheet, beginRow, beginCol + 33);
                    if (baston != null)
                    {
                        if (baston.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 34);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "BAST", IdEstadoAyuda = es });
                        }
                    }
                    var muletas = UtilCast.valueToString(sheet, beginRow, beginCol + 35);
                    if (muletas != null)
                    {
                        if (muletas.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 36);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "MULE", IdEstadoAyuda = es });
                        }
                    }
                    var andador = UtilCast.valueToString(sheet, beginRow, beginCol + 37);
                    if (andador != null)
                    {
                        if (andador.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 38);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "ANDA", IdEstadoAyuda = es });
                        }
                    }

                    var silla = UtilCast.valueToString(sheet, beginRow, beginCol + 39);
                    if (silla != null)
                    {
                        if (silla.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 40);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "RUED", IdEstadoAyuda = es });
                        }
                    }

                    var gafas = UtilCast.valueToString(sheet, beginRow, beginCol + 41);
                    if (gafas != null)
                    {
                        if (gafas.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 42);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "GAFA", IdEstadoAyuda = es });
                        }
                    }

                    var implante = UtilCast.valueToString(sheet, beginRow, beginCol + 43);
                    if (implante != null)
                    {
                        if (implante.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 44);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "IMPL", IdEstadoAyuda = es });
                        }
                    }

                    var audi = UtilCast.valueToString(sheet, beginRow, beginCol + 45);
                    if (audi != null)
                    {
                        if (audi.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 46);
                            switch (es)
                            {
                                case "R":
                                case "REGULAR":
                                case "Regular":
                                    es = "RE";
                                    break;
                                case "B":
                                case "BUENO":
                                case "Bueno":
                                    es = "BU";
                                    break;
                                case "M":
                                case "MALO":
                                case "Malo":
                                    es = "BU";
                                    break;
                                case null:
                                    es = "BU";
                                    break;
                                default:
                                    break;
                            }
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "AUDI", IdEstadoAyuda = es });
                        }
                    }

                    var otroBio = UtilCast.valueToString(sheet, beginRow, beginCol + 47);

                    if (!UString.estaVacio(otroBio))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra ayuda biomecanica => " + otroBio + System.Environment.NewLine;
                    }
                    //tipo ayuda 47

                    var ta1 = UtilCast.valueToString(sheet, beginRow, beginCol + 48);
                    if (ta1 == "x" || ta1 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "TRAQ", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var ta2 = UtilCast.valueToString(sheet, beginRow, beginCol + 49);
                    if (ta2 == "x" || ta2 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "GAST", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var ta3 = UtilCast.valueToString(sheet, beginRow, beginCol + 50);
                    if (ta3 == "x" || ta3 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "FOLE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ta4 = UtilCast.valueToString(sheet, beginRow, beginCol + 51);
                    if (ta4 == "x" || ta4 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "NASO", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ta5 = UtilCast.valueToString(sheet, beginRow, beginCol + 52);
                    if (ta5 == "x" || ta5 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "PRDE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    //tipo ayuda 52
                    var otroTipoAyu = UtilCast.valueToString(sheet, beginRow, beginCol + 53);

                    if (!UString.estaVacio(otroTipoAyu))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra Tipo de Ayuda Medico => " + otroTipoAyu + System.Environment.NewLine;
                    }

                    var t1 = UtilCast.valueToString(sheet, beginRow, beginCol + 54);
                    if (t1 == "x" || t1 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var t2 = UtilCast.valueToString(sheet, beginRow, beginCol + 55);
                    if (t2 == "x" || t2 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "OCUP", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var t3 = UtilCast.valueToString(sheet, beginRow, beginCol + 56);
                    if (t3 == "x" || t3 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "LENG", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var t32 = UtilCast.valueToString(sheet, beginRow, beginCol + 57);
                    if (t32 == "x" || t32 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "APRE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var t4 = UtilCast.valueToString(sheet, beginRow, beginCol + 58);
                    if (t4 == "x" || t4 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "PISC", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var toTRO = UtilCast.valueToString(sheet, beginRow, beginCol + 59);

                    if (!UString.estaVacio(toTRO))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra terapia => " + toTRO + System.Environment.NewLine;
                    }

                    salud.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 68);

                    salud.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 69);

                    if (UString.estaVacio(salud.Evaluado))
                    {
                        salud.Evaluado = "";
                    }

                    //fin relleno de campos de salud
                }

                listaSalud.Add(salud);

                if (entidad.salud.IdSalud == null)
                {
                    listaUpdates.Add("update sgseguridadsys.PS_BENEFICIARIO set ID_COMPONENTE_SALUD = " + salud.IdSalud + " where ID_INSTITUCION = '" + salud.IdInstitucion + "'and ID_BENEFICIARIO = " + salud.IdBeneficiario);
                }
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\" + Program.script + "_" + institucion + "_SALUD.sql"))
            {
                Program.script++;
                listaSalud.ForEach(x => file.WriteLine(UtilCast.querySalud(x)));
                listaUpdates.ForEach(x => file.WriteLine(x));
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + "_SALUD.txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }

            Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga SALUD para => " + institucion);
        }

        public static List<PsEntidad> procesarBeneficiarios(SqlConnection conn, int hoja, int inicioFilaDatos, int inicioColumnaDatos, int filas, string ruta, String prog, String inst, List<MaMiscelaneosdetalle> maMiscelaneosdetalles)
        {

            Console.WriteLine(">>>>>>>>>>>>>> Iniciando carga para => " + inst);

            var programa = prog;
            var institucion = inst;


            var package = new ExcelPackage(new FileInfo(ruta));
            ExcelWorksheet sheet = package.Workbook.Worksheets[hoja - 1];

            List<PsEntidad> entidades = new List<PsEntidad>();

            var beginRow = inicioFilaDatos;
            var beginCol = inicioColumnaDatos - 1;

            var endRow = inicioFilaDatos + filas - 1;

            for (; beginRow < endRow; beginRow++)
            {

                beginCol = inicioColumnaDatos - 1;

                PsEntidad entidad = new PsEntidad();
                entidad.IdEntidad = Program.inicioEntidad;
                entidad.salud.IdSalud = Program.inicioSalud;
                Program.inicioSalud++;

                Program.inicioEntidad++;

                entidad.ApellidoPaterno = UtilCast.valueToString(sheet, beginRow, beginCol + 1);
                entidad.ApellidoMaterno = UtilCast.valueToString(sheet, beginRow, beginCol + 2);
                entidad.Nombres = UtilCast.valueToString(sheet, beginRow, beginCol + 3);
                entidad.Nombrecompleto = entidad.ApellidoPaterno + " " + entidad.ApellidoMaterno + ", " + entidad.Nombres;

                var sexo = UtilCast.valueToString(sheet, beginRow, beginCol + 4);
                entidad.IdSexo = sexo == "Masculino" ? "M" : sexo == "Femenino" ? "F" : null;

                try
                {
                    entidad.FechaNacimiento = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 5);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }


                var edad = UtilCast.calcularEdad(entidad.FechaNacimiento, DateTime.Now);
                entidad.Edad = edad[0];
                entidad.EdadMeses = edad[1];

                var pais = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                switch (pais)
                {
                    case "PERÚ":
                        entidad.IdNacimientoPais = "001";
                        break;
                    case "PERU":
                        entidad.IdNacimientoPais = "001";
                        break;
                    case null:
                        entidad.IdNacimientoPais = null;
                        break;
                    default:
                        entidad.IdNacimientoPais = UtilCast.obtenerPais(conn, pais);
                        break;
                }

                entidad.IdNacimientoPais = entidad.IdNacimientoPais == "Per" ? "001" : entidad.IdNacimientoPais;

                String ubigeo = null;
                var departamento = UtilCast.valueToString(sheet, beginRow, beginCol + 8);
                if (departamento != null)
                {
                    departamento = UtilCast.obtenerDepartamento(conn, departamento);
                    if (departamento != "[error]")
                    {
                        ubigeo = ubigeo + departamento;

                        var provincia = UtilCast.valueToString(sheet, beginRow, beginCol + 9);
                        if (departamento != null && provincia != null)
                        {
                            provincia = UtilCast.obtenerProvincia(conn, departamento, provincia);
                            if (provincia != "[error]")
                            {
                                ubigeo = ubigeo + provincia;

                                var distrito = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                                if (departamento != null && provincia != null && distrito != null)
                                {
                                    var di = UtilCast.obtenerDistrito(conn, departamento, provincia, distrito);
                                    if (di != "[error]")
                                    {
                                        ubigeo = ubigeo + di;
                                    }
                                    else
                                    {
                                        ubigeo = null;
                                        Program.MENSAJES = Program.MENSAJES + "Error con el distrito para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                                    }
                                }
                                else
                                {
                                    ubigeo = ubigeo + "00";
                                }
                            }
                            else
                            {
                                ubigeo = null;
                                Program.MENSAJES = Program.MENSAJES + "Error con la provincia para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            }

                        }
                        else
                        {
                            ubigeo = ubigeo + "0000";
                        }
                    }
                    else
                    {
                        ubigeo = null;
                        Program.MENSAJES = Program.MENSAJES + "Error con el departamento para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                    }
                }
                entidad.IdNacimientoUbigeo = ubigeo;
                entidad.Documento = UtilCast.valueToString(sheet, beginRow, beginCol + 11);

                if (entidad.Documento != null)
                {
                    entidad.Documento = entidad.Documento.PadLeft(8, '0');
                }

                entidad.IdTipoDocumento = "D";
                //OTROS DOCUMENTOS
                var doc1 = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                beginCol = beginCol + 1;
                var docNV = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                beginCol = beginCol + 1;
                var docPB = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                beginCol = beginCol + 1;
                var docTV = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                beginCol = beginCol + 1;
                var docRP = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                beginCol = beginCol + 1;
                var docCN = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                var doc2 = UtilCast.valueToString(sheet, beginRow, beginCol + 13);
                var doc3 = UtilCast.valueToString(sheet, beginRow, beginCol + 14);

                if (doc1 != null)
                {
                    if (doc1.ToUpper() == "SI" || doc1.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "AN", IdEntidad = entidad.IdEntidad });
                    }
                }
                //inicio docs de ninos
                if (docNV != null)
                {
                    if (docNV.ToUpper() == "SI" || docNV.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "CN", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (docPB != null)
                {
                    if (docPB.ToUpper() == "SI" || docPB.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "PB", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (docTV != null)
                {
                    if (docTV.ToUpper() == "SI" || docTV.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "TV", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (docRP != null)
                {
                    if (docRP.ToUpper() == "SI" || docRP.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "RP", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (docCN != null)
                {
                    if (docCN.ToUpper() == "SI" || docCN.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "CNO", IdEntidad = entidad.IdEntidad });
                    }
                }

                //fin docs de ninos
                if (doc2 != null)
                {
                    if (doc2.ToUpper() == "SI" || doc2.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "EP", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (doc3 != null)
                {
                    if (!UString.estaVacio(doc3))
                    {
                        var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == (programa == "AAM" ? "TIPDCAAM" : "TIPDCNNA") && x.Descripcionlocal.ToUpper() == doc3.ToUpper());
                        if (encontrado != null)
                        {
                            entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = encontrado.Codigoelemento, IdEntidad = entidad.IdEntidad });
                        }
                        else
                        {
                            Program.contadorMiscelaneos--;
                            maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = doc3, Codigotabla = programa == "AAM" ? "TIPDCAAM" : "TIPDCNNA", Codigoelemento = "" + Program.contadorMiscelaneos });
                            entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = Program.contadorMiscelaneos.ToString(), IdEntidad = entidad.IdEntidad });
                        }
                    }
                }

                beginCol = beginCol - 1;

                //ingreso
                try
                {
                    entidad.ingreso.FechaIngreso = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 16);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }


                if (!entidad.ingreso.FechaIngreso.HasValue)
                {
                    entidad.ingreso.FechaIngreso = new DateTime(2018, 12, 20);
                    //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " no tiene fecha de ingreso, se pone la fecha 20/12/2018" + System.Environment.NewLine;
                }

                if (entidad.ingreso.FechaIngreso.HasValue)
                {
                    //armar ingreso
                    entidad.ingreso.DiasPermanencia = (DateTime.Today - entidad.ingreso.FechaIngreso.Value).Days;
                    entidad.ingreso.IdIngreso = 1;
                    entidad.ingreso.IdInstitucion = institucion;
                    entidad.ingreso.IdBeneficiario = entidad.IdEntidad;
                    //OTROS DOCUMENTOS
                    var causa1 = UtilCast.valueToString(sheet, beginRow, beginCol + 18);
                    var causa2 = UtilCast.valueToString(sheet, beginRow, beginCol + 19);
                    var causa3 = UtilCast.valueToString(sheet, beginRow, beginCol + 20);
                    var causa4 = UtilCast.valueToString(sheet, beginRow, beginCol + 21);
                    var causa5 = UtilCast.valueToString(sheet, beginRow, beginCol + 22);

                    var causa6 = UtilCast.valueToString(sheet, beginRow, beginCol + 23);
                    var causa7 = UtilCast.valueToString(sheet, beginRow, beginCol + 24);
                    var causa8 = UtilCast.valueToString(sheet, beginRow, beginCol + 25);
                    var causa9 = UtilCast.valueToString(sheet, beginRow, beginCol + 26);
                    var causa10 = UtilCast.valueToString(sheet, beginRow, beginCol + 27);

                    beginCol = beginCol + 5;

                    if (causa1 != null)
                    {
                        if (causa1.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "VFAM" });
                        }
                    }

                    if (causa2 != null)
                    {
                        if (causa2.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "MFIS" });
                        }
                    }

                    if (causa3 != null)
                    {
                        if (causa3.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "MPSI" });
                        }
                    }

                    if (causa4 != null)
                    {
                        if (causa4.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "MSEX" });
                        }
                    }

                    //NUEVOS NINOS

                    if (causa5 != null)
                    {
                        if (causa5.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "DESFM" });
                        }
                    }

                    if (causa6 != null)
                    {
                        if (causa6.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "RDIN" });
                        }
                    }

                    if (causa7 != null)
                    {
                        if (causa7.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "TADR" });
                        }
                    }

                    if (causa8 != null)
                    {
                        if (causa8.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "ACEH" });
                        }
                    }

                    if (causa9 != null)
                    {
                        if (causa9.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "AVIP" });
                        }
                    }

                    //FIN NUEVOS NINOS

                    if (causa10 != null)
                    {
                        if (!UString.estaVacio(causa10))
                        {
                            var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == (programa == "AAM" ? "CINGAAM" : "CINGNNA") && x.Descripcionlocal.ToUpper() == causa10.ToUpper());
                            if (encontrado != null)
                            {
                                entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = encontrado.Codigoelemento });
                            }
                            else
                            {
                                Program.contadorMiscelaneos--;
                                maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = causa10, Codigotabla = programa == "AAM" ? "CINGAAM" : "CINGNNA", Codigoelemento = "" + Program.contadorMiscelaneos });
                                entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = Program.contadorMiscelaneos.ToString() });
                            }
                        }
                    }


                    var deriva = UtilCast.valueToString(sheet, beginRow, beginCol + 23);
                    var derivaOtro = UtilCast.valueToString(sheet, beginRow, beginCol + 24);

                    if (!UString.estaVacio(derivaOtro))
                    {
                        var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == (programa == "AAM" ? "INSDERI" : "INSDAAM") && x.Descripcionlocal.ToUpper() == derivaOtro.ToUpper());
                        if (encontrado != null)
                        {
                            entidad.ingreso.IdInstitucionDeriva = encontrado.Codigoelemento;
                        }
                        else
                        {
                            Program.contadorMiscelaneos--;
                            maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = derivaOtro, Codigotabla = programa == "AAM" ? "INSDERI" : "INSDAAM", Codigoelemento = "" + Program.contadorMiscelaneos });
                            entidad.ingreso.IdInstitucionDeriva = Program.contadorMiscelaneos.ToString();
                        }
                    }
                    else
                    {
                        switch (deriva)
                        {
                            case "DPE-Lima Centro":
                                entidad.ingreso.IdInstitucionDeriva = "DPLC";
                                break;
                            case "DPE-Lima Norte-Callao":
                                entidad.ingreso.IdInstitucionDeriva = "DPLN";
                                break;
                            case "DPE-Lima Este":
                                entidad.ingreso.IdInstitucionDeriva = "DPLE";
                                break;
                            case null:
                            case "OTROS":
                                entidad.ingreso.IdInstitucionDeriva = null;
                                break;
                            default:
                                Program.MENSAJES = Program.MENSAJES + "La institucion deriva " + deriva + " => de " + entidad.Nombrecompleto + " no se encuentra, considerar poner el campo en la columna otros" + System.Environment.NewLine;
                                break;
                        }
                    }

                    var legal = UtilCast.valueToString(sheet, beginRow, beginCol + 25);

                    switch (legal)
                    {
                        case "SI - Investigación Tutelar":
                            entidad.ingreso.IdSituacionLegal = "INTU";
                            break;
                        case "SI- Auto de abandono":
                            entidad.ingreso.IdSituacionLegal = "AABAN";
                            break;
                        default:
                            break;
                    }

                    var resi = UtilCast.valueToString(sheet, beginRow, beginCol + 26);
                    entidad.ingreso.IdArea = UtilCast.obtenerArea(conn, entidad.ingreso.IdInstitucion, resi);
                    entidad.ingreso.IdArea = entidad.ingreso.IdArea == null ? "SN" : entidad.ingreso.IdArea;
                    entidad.ingreso.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 27);

                }
                else
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " no tiene fecha de ingreso" + System.Environment.NewLine;
                }



                //domicilio actual
                String ubigeoA = null;
                var departamentoA = UtilCast.valueToString(sheet, beginRow, beginCol + 28);
                if (departamentoA != null)
                {
                    departamentoA = UtilCast.obtenerDepartamento(conn, departamentoA);
                    if (departamentoA != "[error]")
                    {
                        ubigeoA = ubigeoA + departamentoA;

                        var provinciaA = UtilCast.valueToString(sheet, beginRow, beginCol + 29);
                        if (departamentoA != null && provinciaA != null)
                        {
                            provinciaA = UtilCast.obtenerProvincia(conn, departamentoA, provinciaA);
                            if (provinciaA != "[error]")
                            {
                                ubigeoA = ubigeoA + provinciaA;

                                var distritoA = UtilCast.valueToString(sheet, beginRow, beginCol + 30);
                                if (departamentoA != null && provinciaA != null && distritoA != null)
                                {
                                    var diA = UtilCast.obtenerDistrito(conn, departamentoA, provinciaA, distritoA);
                                    if (diA != "[error]")
                                    {
                                        ubigeoA = ubigeoA + diA;
                                    }
                                    else
                                    {
                                        ubigeoA = null;
                                        Program.MENSAJES = Program.MENSAJES + "Error con el distrito para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                                    }
                                }
                                else
                                {
                                    ubigeoA = ubigeoA + "00";
                                }
                            }
                            else
                            {
                                ubigeoA = null;
                                Program.MENSAJES = Program.MENSAJES + "Error con la provincia para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            }

                        }
                        else
                        {
                            ubigeoA = ubigeoA + "0000";
                        }
                    }
                    else
                    {
                        ubigeoA = null;
                        Program.MENSAJES = Program.MENSAJES + "Error con el departamento para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                    }
                }

                entidad.DomicilioIdUbigeo = ubigeoA;
                entidad.DomicilioDireccion = UtilCast.valueToString(sheet, beginRow, beginCol + 31);
                entidad.DomicilioReferencia = UtilCast.valueToString(sheet, beginRow, beginCol + 32);


                //tenencia
                var tenencia = UtilCast.valueToString(sheet, beginRow, beginCol + 33);
                var tenenciaO = UtilCast.valueToString(sheet, beginRow, beginCol + 34);

                if (!UString.estaVacio(tenenciaO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "TENENCINNA" && x.Descripcionlocal.ToUpper() == tenenciaO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.IdTenencia = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = tenenciaO, Codigotabla = "TENENCINNA", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.IdTenencia = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (tenencia)
                    {
                        case "ALQUILADA":
                        case "Aquilada":
                            entidad.IdTenencia = "ALQ";
                            break;
                        case "Propia":
                            entidad.IdTenencia = "PRO";
                            break;
                        case "Alojada":
                        case "alojado":
                            entidad.IdTenencia = "ALO";
                            break;
                        case null:
                        case "Otras":
                            entidad.IdTenencia = null;
                            break;
                        default:
                            throw new Exception("tenencia no mapeada");
                    }
                }
                //fin tenencia


                //material
                var material = UtilCast.valueToString(sheet, beginRow, beginCol + 35);
                var materialO = UtilCast.valueToString(sheet, beginRow, beginCol + 36);

                if (!UString.estaVacio(materialO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "MATECONST" && x.Descripcionlocal.ToUpper() == materialO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.IdMaterialConstruccion = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = materialO, Codigotabla = "MATECONST", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.IdMaterialConstruccion = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (material)
                    {
                        case "Adobe":
                            entidad.IdMaterialConstruccion = "ALQ";
                            break;
                        case "Ladrillo":
                        case "ladrillo":
                        case "Ladrillos":
                            entidad.IdMaterialConstruccion = "PRO";
                            break;
                        case "Mixto":
                            entidad.IdMaterialConstruccion = "AJD";
                            break;
                        case "OTROS":
                        case null:
                            entidad.IdMaterialConstruccion = null;
                            break;
                        default:
                            throw new Exception("MATERIAL no mapeado");
                    }
                }
                //fin material


                //AGUA
                var agua = UtilCast.valueToString(sheet, beginRow, beginCol + 37);
                var aguaO = UtilCast.valueToString(sheet, beginRow, beginCol + 38);

                if (agua == "Cisterna")
                {
                    aguaO = agua;
                }

                if (!UString.estaVacio(aguaO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "SERAGUAPO" && x.Descripcionlocal.ToUpper() == aguaO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.IdServicioAgua = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = aguaO, Codigotabla = "SERAGUAPO", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.IdServicioAgua = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (agua)
                    {
                        case "Red pública":
                        case "Red Pública":
                        case "Red Publica":
                        case "RED PUBLICA":
                            entidad.IdServicioAgua = "PUB";
                            break;
                        case null:
                        case "OTROS":
                            entidad.IdServicioAgua = null;
                            break;
                        default:
                            throw new Exception("AGUA no mapeado");
                    }
                }
                //fin AGUA


                //desague
                var desa = UtilCast.valueToString(sheet, beginRow, beginCol + 39);
                var desaO = UtilCast.valueToString(sheet, beginRow, beginCol + 40);

                if (!UString.estaVacio(desaO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "SERDESAGU" && x.Descripcionlocal.ToUpper() == desaO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.IdServicioDesague = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = desaO, Codigotabla = "SERDESAGU", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.IdServicioDesague = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (desa)
                    {
                        case "Red Pública":
                        case "Red Publica":
                        case "RED PUBLICA":
                            entidad.IdServicioDesague = "PUB";
                            break;
                        case "Pozo Tratamiento":
                            entidad.IdServicioDesague = "POZ";
                            break;
                        case null:
                            entidad.IdServicioDesague = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "Error con el tipo de servicio desague " + desa + System.Environment.NewLine;
                            break;
                    }
                }
                //fin desague

                //elec
                var elec = UtilCast.valueToString(sheet, beginRow, beginCol + 41);
                var elecO = UtilCast.valueToString(sheet, beginRow, beginCol + 42);

                if (!UString.estaVacio(elecO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "SERELECTR" && x.Descripcionlocal.ToUpper() == elecO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.IdServicioElectricidad = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = elecO, Codigotabla = "SERELECTR", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.IdServicioElectricidad = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (elec)
                    {
                        case "Medidor común":
                        case "Red Pública":
                        case "MEDIDOR COMUN":
                        case "Pozo Tratamiento":
                            entidad.IdServicioElectricidad = "COMU";
                            break;
                        case "Medidor propio":
                        case "Medidor Propio":
                            entidad.IdServicioElectricidad = "PROP";
                            break;
                        case "NINGUNO":
                        case null:
                            entidad.IdServicioElectricidad = null;
                            break;
                        default:
                            Program.MENSAJES = Program.MENSAJES + "Error con el electricidad " + elec + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                            break;
                    }
                }
                //fin elec 41-42


                //EQUIPAMIENTO BASICO 43 - 47

                var radio = UtilCast.valueToString(sheet, beginRow, beginCol + 43);
                var tv = UtilCast.valueToString(sheet, beginRow, beginCol + 44);
                var refri = UtilCast.valueToString(sheet, beginRow, beginCol + 45);
                var coci = UtilCast.valueToString(sheet, beginRow, beginCol + 46);
                var inter = UtilCast.valueToString(sheet, beginRow, beginCol + 47);

                if (radio != null)
                {
                    if (radio.ToUpper() == "SI" || radio.ToUpper() == "X")
                    {
                        entidad.listaEquipamiento.Add(new PsEntidadEquipamiento() { IdEquipamiento = "RAD", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (tv != null)
                {
                    if (tv.ToUpper() == "SI" || tv.ToUpper() == "X")
                    {
                        entidad.listaEquipamiento.Add(new PsEntidadEquipamiento() { IdEquipamiento = "TV", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (refri != null)
                {
                    if (refri.ToUpper() == "SI" || refri.ToUpper() == "X")
                    {
                        entidad.listaEquipamiento.Add(new PsEntidadEquipamiento() { IdEquipamiento = "REF", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (coci != null)
                {
                    if (coci.ToUpper() == "SI" || coci.ToUpper() == "X")
                    {
                        entidad.listaEquipamiento.Add(new PsEntidadEquipamiento() { IdEquipamiento = "COC", IdEntidad = entidad.IdEntidad });
                    }
                }

                if (inter != null)
                {
                    if (inter.ToUpper() == "SI" || inter.ToUpper() == "X")
                    {
                        entidad.listaEquipamiento.Add(new PsEntidadEquipamiento() { IdEquipamiento = "INTE", IdEntidad = entidad.IdEntidad });
                    }
                }

                //EQUIPAMIENTO BASICO


                entidad.Telefono1 = UtilCast.valueToString(sheet, beginRow, beginCol + 48);

                if (entidad.Telefono1 != null)
                {
                    if (entidad.Telefono1.Length > 11)
                    {
                        Program.MENSAJES = Program.MENSAJES + "Error con el telefono " + entidad.Telefono1 + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine;
                        entidad.Telefono1 = null;
                    }
                }

                entidad.PadreNombre = UtilCast.valueToString(sheet, beginRow, beginCol + 49);
                entidad.PadreIdVive = UtilCast.valueToString(sheet, beginRow, beginCol + 50) == "SI" ? "S" : "N";
                entidad.PadreNroDocumento = UtilCast.valueToString(sheet, beginRow, beginCol + 51);
                //ocupacion padre
                var ocupaP = UtilCast.valueToString(sheet, beginRow, beginCol + 52);
                var ocupaPO = UtilCast.valueToString(sheet, beginRow, beginCol + 53);

                if (!UString.estaVacio(ocupaPO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "OCUPAFUN" && x.Descripcionlocal.ToUpper() == ocupaPO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.PadreIdOcupacion = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = ocupaPO, Codigotabla = "OCUPAFUN", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.PadreIdOcupacion = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (ocupaP)
                    {
                        case "Otro":
                        case null:
                        case "X":
                            entidad.PadreIdOcupacion = null;
                            break;
                        case "Obrero":
                        case "obrero":
                            entidad.PadreIdOcupacion = "OBR";
                            break;
                        case "Ambulante":
                            entidad.PadreIdOcupacion = "AMB";
                            break;
                        case "comerciante":
                        case "Comerciante":
                        case "Comerciantes":
                            entidad.PadreIdOcupacion = "COM";
                            break;
                        case "Profesional":
                            entidad.PadreIdOcupacion = "PRO";
                            break;
                        case "Técnico":
                            entidad.PadreIdOcupacion = "TEC";
                            break;
                        case "Desocupado":
                            entidad.PadreIdOcupacion = "DES";
                            break;
                        default:
                            throw new Exception("OCUPACION no mapeado");
                    }
                }
                //ocupacion padre


                entidad.MadreNombre = UtilCast.valueToString(sheet, beginRow, beginCol + 54);
                entidad.MadreIdVive = UtilCast.valueToString(sheet, beginRow, beginCol + 55) == "SI" ? "S" : "N";
                entidad.MadreNroDocumento = UtilCast.valueToString(sheet, beginRow, beginCol + 56);
                //ocupacion madre
                var ocupaM = UtilCast.valueToString(sheet, beginRow, beginCol + 57);
                var ocupaMO = UtilCast.valueToString(sheet, beginRow, beginCol + 58);

                if (!UString.estaVacio(ocupaMO))
                {
                    var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == "OCUPAFUN" && x.Descripcionlocal.ToUpper() == ocupaMO.ToUpper());
                    if (encontrado != null)
                    {
                        entidad.MadreIdOcupacion = encontrado.Codigoelemento;
                    }
                    else
                    {
                        Program.contadorMiscelaneos--;
                        maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = ocupaMO, Codigotabla = "OCUPAFUN", Codigoelemento = "" + Program.contadorMiscelaneos });
                        entidad.MadreIdOcupacion = Program.contadorMiscelaneos.ToString();
                    }
                }
                else
                {
                    switch (ocupaM)
                    {
                        case "Otro":
                        case "Otros":
                        case "OTRO":
                        case null:
                            entidad.MadreIdOcupacion = null;
                            break;
                        case "Profesional":
                            entidad.MadreIdOcupacion = "PRO";
                            break;
                        case "Técnico":
                            entidad.MadreIdOcupacion = "TEC";
                            break;
                        case "Desocupado":
                            entidad.MadreIdOcupacion = "DES";
                            break;
                        case "comerciante":
                        case "Comerciante":
                            entidad.MadreIdOcupacion = "COM";
                            break;
                        case "Obrero":
                            entidad.MadreIdOcupacion = "OBR";
                            break;
                        case "Ambulante":
                            entidad.MadreIdOcupacion = "AMB";
                            break;
                        default:
                            throw new Exception("OCUPACION no mapeado");
                    }
                }
                //ocupacion madre

                entidad.ApoderadoNombre = UtilCast.valueToString(sheet, beginRow, beginCol + 59);
                entidad.ApoderadoNroDocumento = UtilCast.valueToString(sheet, beginRow, beginCol + 60);
                entidad.ApoderadoEsposoa = UtilCast.valueToString(sheet, beginRow, beginCol + 61);
                try
                {
                    entidad.IngresoMensual = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 62);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }



                // 8 familiares 63 + 16

                var secuenciaFamiliar = 1;

                var p1Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 63);
                var p1Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 64));

                if (!UString.estaVacio(p1Pare) && !UString.estaVacio(p1Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p1Pare, Pariente = p1Nombre });
                    secuenciaFamiliar++;
                }

                var p2Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 65);
                var p2Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 66));
                if (!UString.estaVacio(p2Pare) && !UString.estaVacio(p2Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p2Pare, Pariente = p2Nombre });
                    secuenciaFamiliar++;
                }

                var p3Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 67);
                var p3Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 68));
                if (!UString.estaVacio(p3Pare) && !UString.estaVacio(p3Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p3Pare, Pariente = p3Nombre });
                    secuenciaFamiliar++;
                }

                var p4Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 69);
                var p4Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 70));
                if (!UString.estaVacio(p4Pare) && !UString.estaVacio(p4Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p4Pare, Pariente = p4Nombre });
                    secuenciaFamiliar++;
                }

                var p5Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 71);
                var p5Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 72));
                if (!UString.estaVacio(p5Pare) && !UString.estaVacio(p5Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p5Pare, Pariente = p5Nombre });
                    secuenciaFamiliar++;
                }

                var p6Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 73);
                var p6Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 74));
                if (!UString.estaVacio(p6Pare) && !UString.estaVacio(p6Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p6Pare, Pariente = p6Nombre });
                    secuenciaFamiliar++;
                }

                var p7Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 75);
                var p7Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 76));
                if (!UString.estaVacio(p7Pare) && !UString.estaVacio(p7Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p7Pare, Pariente = p7Nombre });
                    secuenciaFamiliar++;
                }

                var p8Nombre = UtilCast.valueToString(sheet, beginRow, beginCol + 77);
                var p8Pare = UtilCast.parentesco(UtilCast.valueToString(sheet, beginRow, beginCol + 78));
                if (!UString.estaVacio(p8Pare) && !UString.estaVacio(p8Nombre))
                {
                    entidad.listaPariente.Add(new PsEntidadPariente() { IdEntidad = entidad.IdEntidad, IdPariente = secuenciaFamiliar, IdParentesco = p8Pare, Pariente = p8Nombre });
                    secuenciaFamiliar++;
                }

                // 8 familiares

                entidad.ComentariosReferenciaFamiliar = UtilCast.valueToString(sheet, beginRow, beginCol + 79);

                //FALTA

                var nivel = UtilCast.valueToString(sheet, beginRow, beginCol + 80);

                switch (nivel)
                {
                    case "Inicial":
                    case "INICIAL":
                    case "INICAL":
                        entidad.capacidad.IdNivel = "INIC";
                        break;
                    case "Primaria":
                    case "primaria":
                        entidad.capacidad.IdNivel = "PRIM";
                        break;
                    case "Secundaria":
                    case "SECUNDARIA":
                        entidad.capacidad.IdNivel = "SECU";
                        break;
                    case "Aprestamiento":
                        entidad.capacidad.IdNivel = "APRE";
                        break;
                    case "Regular":
                    case null:
                        entidad.capacidad.IdNivel = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el NIVEL " + nivel + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                var grado = UtilCast.valueToString(sheet, beginRow, beginCol + 81);
                switch (grado)
                {
                    case "3 años":
                        entidad.capacidad.IdGradoEducativo = "3ANIO";
                        break;
                    case "4 años":
                        entidad.capacidad.IdGradoEducativo = "4ANIO";
                        break;
                    case "5 años":
                    case "5 años ":
                    case "5to añito":
                        entidad.capacidad.IdGradoEducativo = "5ANIO";
                        break;

                    case "1er Grado":
                    case "1er grado ":
                    case "1 er grado":
                    case "1er grado":
                    case "1ER GRADO":
                        entidad.capacidad.IdGradoEducativo = "1GRA";
                        break;
                    case "2do Grado":
                    case "2do grado":
                    case "2dogrado":
                        entidad.capacidad.IdGradoEducativo = "2GRA";
                        break;
                    case "3er Grado":
                    case "3er grado":
                    case "3er  grado":
                        entidad.capacidad.IdGradoEducativo = "3GRA";
                        break;
                    case "4to Grado":
                    case "4to grado":
                        entidad.capacidad.IdGradoEducativo = "4GRA";
                        break;
                    case "5to Grado":
                    case "5to grado":
                        entidad.capacidad.IdGradoEducativo = "5GRA";
                        break;
                    case "6to Grado":
                    case "6to grado":
                        entidad.capacidad.IdGradoEducativo = "6GRA";
                        break;

                    case "1er. Año":
                    case "1er Año":
                    case "1er año":
                        entidad.capacidad.IdGradoEducativo = "1ER";
                        break;
                    case "2do Año":
                    case "2do año":
                        entidad.capacidad.IdGradoEducativo = "2DO";
                        break;
                    case "3er Año":
                    case "3er año":
                        entidad.capacidad.IdGradoEducativo = "3ER";
                        break;
                    case "4to Año":
                    case "4to año":
                    case "4to. Año":
                        entidad.capacidad.IdGradoEducativo = "4TO";
                        break;
                    case "5to Año":
                    case "5to año":
                        entidad.capacidad.IdGradoEducativo = "5TO";
                        break;
                    case "No escolaridad":
                    case null:
                        entidad.capacidad.IdGradoEducativo = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el GRADO " + grado + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }


                var tipoi = UtilCast.valueToString(sheet, beginRow, beginCol + 82);

                switch (tipoi)
                {
                    case "Alternativa (CEBA)":
                    case "Alternativa":
                    case "CEBA":
                        entidad.capacidad.IdTipoInstitucion = "ALT";
                        break;
                    case "CETPRO":
                        entidad.capacidad.IdTipoInstitucion = "CET";
                        break;
                    case "Especializada":
                    case "ESPECIALIZADA":
                    case "Especial (CEBE)":
                        entidad.capacidad.IdTipoInstitucion = "ESP";
                        break;
                    case "Ninguna":
                    case "Ninguno":
                    case null:
                        entidad.capacidad.IdTipoInstitucion = "NIN";
                        break;
                    case "Regular":
                        entidad.capacidad.IdTipoInstitucion = "REG";
                        break;
                    case "Regular Inclusiva":
                        entidad.capacidad.IdTipoInstitucion = "REI";
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el IdTipoInstitucion " + tipoi + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }
                entidad.capacidad.IdIletrado = UtilCast.valueToString(sheet, beginRow, beginCol + 83);
                entidad.capacidad.IdIletrado = entidad.capacidad.IdIletrado == null ? "NO" : entidad.capacidad.IdIletrado.ToUpper().Trim() == "SI" ? "SI" : "NO";
                entidad.capacidad.IdSaanee = UtilCast.valueToString(sheet, beginRow, beginCol + 84);
                entidad.capacidad.IdSaanee = entidad.capacidad.IdSaanee == null ? "NO" : entidad.capacidad.IdSaanee.ToUpper().Trim() == "SI" ? "SI" : "NO";


                entidad.socioAmbiental.Evaluado = "";
                entidad.socioAmbiental.IdOrigen = "INI";
                entidad.socioAmbiental.Estado = "A";
                entidad.socioAmbiental.FechaInforme = entidad.ingreso.FechaIngreso;
                entidad.socioAmbiental.IdInstitucion = institucion;
                entidad.socioAmbiental.IdBeneficiario = entidad.IdEntidad;
                entidad.socioAmbiental.IdSocioAmbiental = Program.inicioSocio;

                var conflicto = UtilCast.valueToString(sheet, beginRow, beginCol + 85);
                switch (conflicto)
                {
                    case "Alto":
                        entidad.socioAmbiental.IdConflictos = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdConflictos = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdConflictos = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdConflictos = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el conflicto " + conflicto + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }


                var auto = UtilCast.valueToString(sheet, beginRow, beginCol + 86 + 2);
                switch (auto)
                {
                    case "Alto":
                        entidad.socioAmbiental.IdEmocional = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdEmocional = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdEmocional = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdEmocional = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el autocontrol " + auto + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                var asert = UtilCast.valueToString(sheet, beginRow, beginCol + 87 + 4);
                switch (asert)
                {
                    case "Alto":
                        entidad.socioAmbiental.IdAsertavidad = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdAsertavidad = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdAsertavidad = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdAsertavidad = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el asertivo " + asert + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                var comu = UtilCast.valueToString(sheet, beginRow, beginCol + 88 + 6);
                switch (comu)
                {
                    case "Alto":
                    case "ALto":
                        entidad.socioAmbiental.IdComunicacion = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdComunicacion = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdComunicacion = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdComunicacion = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el comunicacion " + comu + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                var coop = UtilCast.valueToString(sheet, beginRow, beginCol + 89 + 8);
                switch (coop)
                {
                    case "Alto":
                        entidad.socioAmbiental.IdCooperacion = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdCooperacion = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdCooperacion = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdCooperacion = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el cooperacion " + coop + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                var empa = UtilCast.valueToString(sheet, beginRow, beginCol + 90 + 10);
                switch (empa)
                {
                    case "Alto":
                        entidad.socioAmbiental.IdEmpatia = "EXCE";
                        break;
                    case "Medio":
                        entidad.socioAmbiental.IdEmpatia = "BUEN";
                        break;
                    case "Bajo":
                        entidad.socioAmbiental.IdEmpatia = "REGU";
                        break;
                    case null:
                        entidad.socioAmbiental.IdEmpatia = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "Error con el empatia " + empa + " para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine; ;
                        break;
                }

                Program.inicioSocio++;

                beginCol = beginCol + 12;

                //FALTA

                var conadis = UtilCast.valueToString(sheet, beginRow, beginCol + 92);

                switch (conadis)
                {
                    case "Discapacidad Severa (Amarillo)":
                        entidad.IdCarnetConadis = "DAMA";
                        break;
                    case "Discapacidad Leve\\Mode (Azul)":
                        entidad.IdCarnetConadis = "DAZU";
                        break;
                    case "Ninguno":
                        entidad.IdCarnetConadis = null;
                        break;
                    case "En tramite":
                        entidad.IdCarnetConadis = "TRAM";
                        break;
                    default:
                        break;
                }



                var flagSIS = UtilCast.valueToString(sheet, beginRow, beginCol + 93);
                if (flagSIS == "SI")
                {
                    var numero = UtilCast.valueToString(sheet, beginRow, beginCol + 94);
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "SIS", SeguroSocial = numero });
                }

                var flagSEG = UtilCast.valueToString(sheet, beginRow, beginCol + 95);
                if (flagSEG == "SI")
                {
                    var numero = UtilCast.valueToString(sheet, beginRow, beginCol + 96);
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "SESO", SeguroSocial = numero });
                }

                var flagPRI = UtilCast.valueToString(sheet, beginRow, beginCol + 98);
                if (flagPRI == "SI")
                {
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "PRIV" });
                }


                var flagPension65 = UtilCast.valueToString(sheet, beginRow, beginCol + 99);
                if (flagPension65 != null)
                {
                    if (flagPension65.ToUpper() == "SI" || flagPension65.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "QAWA" });
                    }
                }

                var flagContigo = UtilCast.valueToString(sheet, beginRow, beginCol + 100);
                if (flagContigo != null)
                {
                    if (flagContigo.ToUpper() == "SI" || flagContigo.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "CMAS" });
                    }
                }

                var flagPension652 = UtilCast.valueToString(sheet, beginRow, beginCol + 101);
                if (flagPension652 != null)
                {
                    if (flagPension652.ToUpper() == "SI" || flagPension652.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "CONT" });
                    }
                }

                var flagContigo2 = UtilCast.valueToString(sheet, beginRow, beginCol + 102);
                if (flagContigo2 != null)
                {
                    if (flagContigo2.ToUpper() == "SI" || flagContigo2.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "VALE" });
                    }
                }

                entidad.socioAmbiental.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 103);

                //COMPONENTE NUTRICION INICIO 104
                entidad.nutricion.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.nutricion.IdBeneficiario = entidad.IdEntidad;
                entidad.nutricion.IdNutricion = Program.inicioNutricion;
                var mes = entidad.ingreso.FechaIngreso.Value.Month;
                entidad.nutricion.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.nutricion.FechaInforme = entidad.ingreso.FechaIngreso.Value;

                try
                {
                    entidad.nutricion.Peso = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 104);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                try
                {
                    if (entidad.nutricion.IdInstitucion == "PURIC" || entidad.nutricion.IdInstitucion == "INMAC")
                    {
                        entidad.nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 105);
                        if (entidad.nutricion.IdInstitucion == "INMAC")
                        {
                            if (entidad.nutricion.Talla.HasValue)
                            {
                                if (entidad.nutricion.Talla.Value < 10)
                                {
                                    entidad.nutricion.Talla = entidad.nutricion.Talla * 100;
                                }
                            }
                        }
                    }
                    else
                    {
                        entidad.nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 105) * 100.0m;
                    }
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }


                //TO-DO evaluar cuando no vengan valores
                entidad.nutricion.IdValoracion = UtilCast.valueToString(sheet, beginRow, beginCol + 110);

                entidad.nutricion.Estado = "A";
                entidad.nutricion.IdOrigen = "INI";
                entidad.nutricion.Evaluado = "";

                Program.inicioNutricion++;
                //COMPONENTE NUTRICION FIN

                //COMPONENTE CAPACIDAD INICIO 63-74
                entidad.capacidad.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.capacidad.IdBeneficiario = entidad.IdEntidad;
                entidad.capacidad.IdCapacidad = Program.inicioCapacidad;
                Program.inicioCapacidad++;
                entidad.capacidad.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.capacidad.FechaInforme = entidad.ingreso.FechaIngreso.Value;
                entidad.socioAmbiental.IdPeriodo = entidad.capacidad.IdPeriodo;

                var disc = false;

                var td1 = UtilCast.valueToString(sheet, beginRow, beginCol + 111);
                if (td1 == "x" || td1 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "FISI", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdSalud = entidad.salud.IdSalud });
                }

                var td2 = UtilCast.valueToString(sheet, beginRow, beginCol + 112);
                if (td2 == "x" || td2 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MENTA", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdSalud = entidad.salud.IdSalud });
                }

                var td3 = UtilCast.valueToString(sheet, beginRow, beginCol + 113);
                if (td3 == "x" || td3 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "SENSO", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdSalud = entidad.salud.IdSalud });
                }

                var td4 = UtilCast.valueToString(sheet, beginRow, beginCol + 114);
                if (td4 == "x" || td4 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MULTI", IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdSalud = entidad.salud.IdSalud });
                }

                if (disc)
                {
                    entidad.IdDiscapacidad = "S";
                }
                else
                {
                    entidad.IdDiscapacidad = "N";
                }

                //115

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 117) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 117) == "X")
                {
                    entidad.capacidad.Barthel1 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 116) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 116) == "X")
                {
                    entidad.capacidad.Barthel1 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 115) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 115) == "X")
                {
                    entidad.capacidad.Barthel1 = 0;
                }


                //Trasladarse        OK 4

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 121) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 121) == "X")
                {
                    entidad.capacidad.Barthel8 = 15;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 120) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 120) == "X")
                {
                    entidad.capacidad.Barthel8 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 119) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 119) == "X")
                {
                    entidad.capacidad.Barthel8 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 118) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 118) == "X")
                {
                    entidad.capacidad.Barthel8 = 0;
                }

                //Arreglarse         OK 2

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 123) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 123) == "X")
                {
                    entidad.capacidad.Barthel4 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 122) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 122) == "X")
                {
                    entidad.capacidad.Barthel4 = 0;
                }

                //Uso del Retrete    OK 3


                if (UtilCast.valueToString(sheet, beginRow, beginCol + 126) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 126) == "X")
                {
                    entidad.capacidad.Barthel7 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 125) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 125) == "X")
                {
                    entidad.capacidad.Barthel7 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 124) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 124) == "X")
                {
                    entidad.capacidad.Barthel7 = 0;
                }

                //Lavarse(baño)      OK 2

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 128) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 128) == "X")
                {
                    entidad.capacidad.Barthel2 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 127) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 127) == "X")
                {
                    entidad.capacidad.Barthel2 = 0;
                }

                //Deambulación       OK 4

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 132) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 132) == "X")
                {
                    entidad.capacidad.Barthel9 = 15;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 131) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 131) == "X")
                {
                    entidad.capacidad.Barthel9 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 130) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 130) == "X")
                {
                    entidad.capacidad.Barthel9 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 129) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 129) == "X")
                {
                    entidad.capacidad.Barthel9 = 0;
                }

                //Escaleras         OK 3

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 135) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 135) == "X")
                {
                    entidad.capacidad.Barthel10 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 134) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 134) == "X")
                {
                    entidad.capacidad.Barthel10 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 133) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 133) == "X")
                {
                    entidad.capacidad.Barthel10 = 0;
                }

                //Vestido            OK 3

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 138) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 138) == "X")
                {
                    entidad.capacidad.Barthel3 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 137) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 137) == "X")
                {
                    entidad.capacidad.Barthel3 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 136) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 136) == "X")
                {
                    entidad.capacidad.Barthel3 = 0;
                }

                //Deposición         OK 3

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 141) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 141) == "X")
                {
                    entidad.capacidad.Barthel5 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 140) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 140) == "X")
                {
                    entidad.capacidad.Barthel5 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 139) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 139) == "X")
                {
                    entidad.capacidad.Barthel5 = 0;
                }

                //Micción            OK 3

                if (UtilCast.valueToString(sheet, beginRow, beginCol + 144) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 144) == "X")
                {
                    entidad.capacidad.Barthel6 = 10;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 143) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 143) == "X")
                {
                    entidad.capacidad.Barthel6 = 5;
                }
                else if (UtilCast.valueToString(sheet, beginRow, beginCol + 142) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 142) == "X")
                {
                    entidad.capacidad.Barthel6 = 0;
                }

                DtoCapacidad dto = UtilCast.calcularBarthel(entidad.capacidad);

                entidad.capacidad.GradoDependenciaBarthel = dto.GradoDependenciaBarthel;
                entidad.capacidad.PorcentajeGradoBarthel = dto.PorcentajeGradoBarthel;
                entidad.capacidad.IdOrigen = "INI";
                entidad.capacidad.Estado = "A";
                entidad.capacidad.Evaluado = "";

                //COMPONENTE CAPACIDAD FIN -145

                //COMPONENTE SALUD INICIO 146 
                entidad.salud.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.salud.IdBeneficiario = entidad.IdEntidad;
                entidad.salud.IdOrigen = "INI";
                entidad.salud.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.salud.FechaInforme = entidad.ingreso.FechaIngreso.Value;

                var baston = UtilCast.valueToString(sheet, beginRow, beginCol + 146);
                if (baston != null)
                {
                    if (baston.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "BAST", IdEstadoAyuda = "BU" });
                    }
                }
                var andador = UtilCast.valueToString(sheet, beginRow, beginCol + 147);
                if (andador != null)
                {
                    if (andador.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "ANDA", IdEstadoAyuda = "BU" });
                    }
                }

                var muletas = UtilCast.valueToString(sheet, beginRow, beginCol + 148);
                if (muletas != null)
                {
                    if (muletas.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "MULE", IdEstadoAyuda = "BU" });
                    }
                }

                var silla = UtilCast.valueToString(sheet, beginRow, beginCol + 149);
                if (silla != null)
                {
                    if (silla.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "RUED", IdEstadoAyuda = "BU" });
                    }
                }

                var gafas = UtilCast.valueToString(sheet, beginRow, beginCol + 150);
                if (gafas != null)
                {
                    if (gafas.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "GAFA", IdEstadoAyuda = "BU" });
                    }
                }

                var implante = UtilCast.valueToString(sheet, beginRow, beginCol + 151);
                if (implante != null)
                {
                    if (implante.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "IMPL", IdEstadoAyuda = "BU" });
                    }
                }

                var audi = UtilCast.valueToString(sheet, beginRow, beginCol + 152);
                if (audi != null)
                {
                    if (audi.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "AUDI", IdEstadoAyuda = "BU" });
                    }
                }


                //153 Edad de Menarquia
                try
                {
                    entidad.salud.EdadMenarquia = UtilCast.valueToInt(sheet, beginRow, beginCol + 153);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                try
                {
                    entidad.salud.FechaUltimaMestruacion = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 154);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }


                var sangre = UtilCast.valueToString(sheet, beginRow, beginCol + 157);

                switch (sangre)
                {
                    case "A":
                        entidad.salud.IdGrupoSanguineo = "1";
                        break;
                    case "B":
                        entidad.salud.IdGrupoSanguineo = "5";
                        break;
                    case "O":
                        entidad.salud.IdGrupoSanguineo = "7";
                        break;
                    case "AB":
                        entidad.salud.IdGrupoSanguineo = "3";
                        break;
                    default:
                        break;
                }

                var rh = UtilCast.valueToString(sheet, beginRow, beginCol + 158);
                entidad.salud.IdFactorRh = rh == "Negativo" ? "N" : rh == "Positivo" ? "P" : null;

                try
                {
                    entidad.salud.Hemoglobina = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 159);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }



                //calcular
                if (entidad.nutricion.Peso.HasValue && entidad.nutricion.Talla.HasValue && entidad.FechaNacimiento.HasValue)
                {
                    var edades = UtilCast.calcularEdad(entidad.FechaNacimiento, entidad.nutricion.FechaInforme.Value);

                    Object[] calculosNutricion = UtilCast.obtenerCalculosNutricion(conn, entidad, edades[0], edades[1]);
                    entidad.salud.HemoglobinaResultado = calculosNutricion[0] as string;
                    entidad.nutricion.Imc = calculosNutricion[1] as string;
                    entidad.nutricion.TallaEdad = calculosNutricion[2] as string;
                    entidad.nutricion.PesoEdad = calculosNutricion[3] as string;
                    entidad.nutricion.PesoTalla = calculosNutricion[4] as string;
                }

                var vih = UtilCast.valueToString(sheet, beginRow, beginCol + 161);
                entidad.salud.IdPruebaVIH = vih == "SI" ? "S" : "N";

                entidad.salud.IdDescarteSerologico = UtilCast.valueToString(sheet, beginRow, beginCol + 162) == "Tratamiento -Si" ? "SI" : "NO";

                entidad.salud.IdTbc = UtilCast.valueToString(sheet, beginRow, beginCol + 163);



                var visionNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 164);
                if (visionNormal == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "NORM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionMode = UtilCast.valueToString(sheet, beginRow, beginCol + 165);
                if (visionMode == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "MODE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionGrave = UtilCast.valueToString(sheet, beginRow, beginCol + 166);
                if (visionGrave == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "GRAV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionCegu = UtilCast.valueToString(sheet, beginRow, beginCol + 167);
                if (visionCegu == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "CEGE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 168);
                if (audioNormal == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "AUN", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioLeve = UtilCast.valueToString(sheet, beginRow, beginCol + 169);
                if (audioLeve == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPL", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioMode = UtilCast.valueToString(sheet, beginRow, beginCol + 170);
                if (audioMode == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioSevera = UtilCast.valueToString(sheet, beginRow, beginCol + 171);
                if (audioSevera == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "SEVE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioProfunda = UtilCast.valueToString(sheet, beginRow, beginCol + 172);
                if (audioProfunda == "x")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "PROF", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }



                //195 parasitaria




                //diagnosticos

                var d1 = UtilCast.valueToString(sheet, beginRow, beginCol + 195);
                if (d1 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "I", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d3 = UtilCast.valueToString(sheet, beginRow, beginCol + 196);
                if (d3 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "III", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d4 = UtilCast.valueToString(sheet, beginRow, beginCol + 197);
                if (d4 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "IV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d5 = UtilCast.valueToString(sheet, beginRow, beginCol + 198);
                if (d5 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "IX", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d6 = UtilCast.valueToString(sheet, beginRow, beginCol + 199);
                if (d6 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "X", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d7 = UtilCast.valueToString(sheet, beginRow, beginCol + 200);
                if (d7 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d8 = UtilCast.valueToString(sheet, beginRow, beginCol + 201);
                if (d8 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d9 = UtilCast.valueToString(sheet, beginRow, beginCol + 202);
                if (d9 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XIII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d10 = UtilCast.valueToString(sheet, beginRow, beginCol + 203);
                if (d10 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XIV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d11 = UtilCast.valueToString(sheet, beginRow, beginCol + 204);
                if (d11 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XVI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d12 = UtilCast.valueToString(sheet, beginRow, beginCol + 205);
                if (d12 == "x")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XVII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }




                var t1 = UtilCast.valueToString(sheet, beginRow, beginCol + 207);
                if (t1 == "x" || t1 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t2 = UtilCast.valueToString(sheet, beginRow, beginCol + 208);
                if (t2 == "x" || t2 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "OCUP", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t3 = UtilCast.valueToString(sheet, beginRow, beginCol + 209);
                if (t3 == "x" || t3 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "LENG", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t4 = UtilCast.valueToString(sheet, beginRow, beginCol + 210);
                if (t4 == "x" || t4 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "PISC", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }



                entidad.salud.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 211);
                //COMPONENTE SALUD FIN

                //BENEFICIARIO
                entidad.beneficiario.IdInstitucion = institucion;
                entidad.beneficiario.IdPrograma = programa;
                entidad.beneficiario.IdBeneficiario = entidad.IdEntidad;
                entidad.beneficiario.IdArea = entidad.ingreso.IdArea;
                entidad.beneficiario.IdComponenteIngreso = 1;
                entidad.beneficiario.IdComponenteNutricion = entidad.nutricion.IdNutricion;
                entidad.beneficiario.IdComponenteSalud = entidad.salud.IdSalud;
                entidad.beneficiario.IdComponenteCapacidad = entidad.capacidad.IdCapacidad;
                entidad.beneficiario.IdComponenteSocioAmbiental = entidad.socioAmbiental.IdSocioAmbiental;
                entidad.beneficiario.Estado = "ACT";

                entidades.Add(entidad);
            }

            //Program.MENSAJES = "";

            Console.WriteLine(">>>>>>>>>>>>>> Finalizando carga para => " + inst);


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\" + Program.script + "_" + institucion + ".sql"))
            {
                Program.script++;

                file.WriteLine("-->>>>>>>>>>>>>>>>>>>>>>>> INICIO <<<<<<<<<<<<<<<<<<<<<<<<<");

                //insertar ps_entidad
                entidades.ForEach(x => file.WriteLine(UtilCast.queryEntidad(x)));

                //insertar detalles de entidad
                entidades.ForEach(x => { x.listaDocumento.ForEach(y => file.WriteLine(UtilCast.queryDocumento(y))); });
                entidades.ForEach(x => { x.listaSeguro.ForEach(y => file.WriteLine(UtilCast.querySeguro(y))); });
                entidades.ForEach(x => { x.listaPrograma.ForEach(y => file.WriteLine(UtilCast.queryPrograma(y))); });
                entidades.ForEach(x => { x.listaEquipamiento.ForEach(y => file.WriteLine(UtilCast.queryEquipamiento(y))); });
                entidades.ForEach(x => { x.listaPariente.ForEach(y => file.WriteLine(UtilCast.queryPariente(y))); });

                //insertar ps_beneficiario
                entidades.ForEach(x => file.WriteLine(UtilCast.queryBeneficiario(x.beneficiario)));

                //insertar ps_beneficiarioinrgeso
                entidades.ForEach(x => file.WriteLine(UtilCast.queryIngreso(x.ingreso)));

                //insertar ps_beneficiarioingresocausal
                entidades.ForEach(x => { x.ingreso.listaCausal.ForEach(y => file.WriteLine(UtilCast.queryCausal(y))); });

                //insertar ps_nutricion
                entidades.ForEach(x => file.WriteLine(UtilCast.queryNutricion(x.nutricion)));

                //insertar ps_salud
                entidades.ForEach(x => file.WriteLine(UtilCast.querySalud(x.salud)));

                //insertar ps_capacidad
                entidades.ForEach(x => file.WriteLine(UtilCast.queryCapacidad(x.capacidad)));

                //insertar ps_socio_ambiental
                entidades.ForEach(x => file.WriteLine(UtilCast.querySocio(x.socioAmbiental)));

                file.WriteLine("-->>>>>>>>>>>>>>>>>>>>>>>> FIN <<<<<<<<<<<<<<<<<<<<<<<<<");
            }

            if (Program.MENSAJES.Length > 0)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + ".txt"))
                {
                    file.WriteLine(Program.MENSAJES);
                }

            }
            return entidades;
        }
    }
}
