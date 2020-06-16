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
    public class AAM
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

                nutricion.Estado = "A";
                nutricion.IdOrigen = "EVA";

                if (evaluado)
                {

                    var mes = nutricion.FechaInforme.Value.Month;
                    nutricion.IdPeriodo = nutricion.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");


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
                        nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 6) * 100.0m;
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }

                    //nutricion.IdPerimetroPierna = UtilCast.valueToString(sheet, beginRow, beginCol + 8);
                    nutricion.IdPresionMensual = UtilCast.valueToString(sheet, beginRow, beginCol + 9);


                    //pantorrilla 10

                    var pantorrilla = UtilCast.valueToString(sheet, beginRow, beginCol + 10);

                    switch (pantorrilla)
                    {
                        case "Disminuido":
                            nutricion.IdPerimetroPierna = "DIS";
                            break;
                        case "Normal":
                            nutricion.IdPerimetroPierna = "NOR";
                            break;
                        case null:
                            nutricion.IdPerimetroPierna = null;
                            break;
                        default:
                            throw new Exception("per pierna no contemplado");
                    }

                    var valoracion = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                    switch (valoracion)
                    {
                        case "Adelgazado moderado":
                            nutricion.IdValoracion = "03";
                            break;
                        case "Sobrepeso":
                            nutricion.IdValoracion = "04";
                            break;
                        case "Adelgazado severo":
                            nutricion.IdValoracion = "02";
                            break;
                        case "Normal":
                            nutricion.IdValoracion = "01";
                            break;
                        case "Obesidad":
                            nutricion.IdValoracion = "05";
                            break;
                        case null:
                            nutricion.IdValoracion = null;
                            break;
                        default:
                            throw new Exception("valoracion no contemplado");
                    }

                    var tipoDieta = UtilCast.valueToString(sheet, beginRow, beginCol + 13);

                    switch (tipoDieta)
                    {
                        case "Nutrioterapeutico":
                            nutricion.IdTipoDieta = "NUTRI";
                            break;
                        case "Normal":
                            nutricion.IdTipoDieta = "NORM";
                            break;
                        case "Ninguno":
                            nutricion.IdTipoDieta = "NINGU";
                            break;
                        case "Regular":
                            nutricion.IdTipoDieta = "REGU";
                            break;
                        case null:
                            nutricion.IdTipoDieta = null;
                            break;
                        default:
                            throw new Exception("IdTipoDieta no contemplado");
                    }

                    try
                    {
                        nutricion.TipoDietaPorDia = UtilCast.valueToInt(sheet, beginRow, beginCol + 14);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }


                    var suple = UtilCast.valueToString(sheet, beginRow, beginCol + 15);

                    switch (suple)
                    {
                        case "Formula enteral completa":
                            nutricion.IdSuplementoNutricional = "001";
                            break;
                        case "Formula enteral especifica":
                            nutricion.IdSuplementoNutricional = "003";
                            break;
                        case "Formula enteral polimerica completa":
                            nutricion.IdSuplementoNutricional = "003";
                            break;
                        case null:
                            nutricion.IdSuplementoNutricional = null;
                            break;
                        default:
                            throw new Exception("IdSuplementoNutricional no contemplado");
                    }

                    try
                    {
                        nutricion.SuplementoNutricionalPorDia = UtilCast.valueToInt(sheet, beginRow, beginCol + 16);
                    }
                    catch (Exception e)
                    {
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                        continue;
                    }


                    nutricion.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 17);

                    nutricion.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 18);

                    if (UString.estaVacio(nutricion.Evaluado))
                    {
                        nutricion.Evaluado = "";
                    }

                    var pesoAnterior = entidad.nutricion.Peso;

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
                        //nutricion.VariacionPeso = calculosNutricion[5] as decimal?;
                    }
                    if (pesoAnterior.HasValue && entidad.nutricion.Peso.HasValue)
                    {
                        nutricion.VariacionPeso = Math.Round(((entidad.nutricion.Peso - pesoAnterior) / pesoAnterior * 100).Value, 2);
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

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\99_ERRORES_" + institucion + "_NUTRI.txt"))
            {
                file.WriteLine(Program.MENSAJES);
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


                    //FORMATIVO 5, 6, 7, 8, 9

                    var formativoCantidad = UtilCast.valueToInt(sheet, beginRow, beginCol + 9);
                    {
                        if (formativoCantidad == null)
                        {
                            formativoCantidad = 0;
                        }
                        var excelente1 = UtilCast.valueToString(sheet, beginRow, beginCol + 5);
                        var bueno1 = UtilCast.valueToString(sheet, beginRow, beginCol + 6);
                        var regular1 = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 8);

                        var nota = "";

                        if (excelente1 == "x" || excelente1 == "X")
                        {
                            nota = "EX";
                        }
                        else if (bueno1 == "x" || bueno1 == "X")
                        {
                            nota = "BU";
                        }
                        else if (regular1 == "x" || regular1 == "X")
                        {
                            nota = "RE";
                        }
                        else if (na == "x" || na == "X")
                        {
                            nota = "NA";
                        }
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 10, IdNota = nota, Cantidad = formativoCantidad, CreacionUsuario = "CARGAMASIVA" });
                    }

                    //FISICO    10, 11, 12, 13, 14

                    var formativoFisico = UtilCast.valueToInt(sheet, beginRow, beginCol + 14);
                    {
                        if (formativoFisico == null)
                        {
                            formativoFisico = 0;
                        }
                        var excelente = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                        var bueno = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                        var regular = UtilCast.valueToString(sheet, beginRow, beginCol + 12);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 8);

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
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 13, IdNota = nota, Cantidad = formativoFisico, CreacionUsuario = "CARGAMASIVA" });
                    }
                    //COGNITIVO 15, 16, 17, 18, 19
                    var formativoCognitivo = UtilCast.valueToInt(sheet, beginRow, beginCol + 19);
                    {
                        if (formativoCognitivo == null)
                        {
                            formativoCognitivo = 0;
                        }
                        var excelente = UtilCast.valueToString(sheet, beginRow, beginCol + 15);
                        var bueno = UtilCast.valueToString(sheet, beginRow, beginCol + 16);
                        var regular = UtilCast.valueToString(sheet, beginRow, beginCol + 17);
                        var na = UtilCast.valueToString(sheet, beginRow, beginCol + 8);

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
                        capacidad.listaTaller.Add(new PsCapacidadTaller() { IdInstitucion = capacidad.IdInstitucion, IdBeneficiario = capacidad.IdBeneficiario, IdCapacidad = capacidad.IdCapacidad, IdTaller = 14, IdNota = nota, Cantidad = formativoCognitivo, CreacionUsuario = "CARGAMASIVA" });
                    }

                    capacidad.ComentarioTalleres = UtilCast.valueToString(sheet, beginRow, beginCol + 20);

                    var si = UtilCast.valueToString(sheet, beginRow, beginCol + 21);
                    var no = UtilCast.valueToString(sheet, beginRow, beginCol + 22);
                    var empro = UtilCast.valueToString(sheet, beginRow, beginCol + 23);
                    var noapl = UtilCast.valueToString(sheet, beginRow, beginCol + 24);

                    if (si == "x" || si == "X")
                    {
                        capacidad.IdHabilidadOcupacional = "SI";
                    }
                    else if (empro == "x" || empro == "X")
                    {
                        capacidad.IdHabilidadOcupacional = "PRO";
                    }
                    else if (no == "x" || no == "X")
                    {
                        capacidad.IdHabilidadOcupacional = "NO";
                    }
                    else if (noapl == "x" || noapl == "X")
                    {
                        capacidad.IdHabilidadOcupacional = "NOAP";
                    }

                    capacidad.Katz1 = UtilCast.valueToString(sheet, beginRow, beginCol + 26) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 26) == "X" ? 1 : 0;
                    capacidad.Katz2 = UtilCast.valueToString(sheet, beginRow, beginCol + 28) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 28) == "X" ? 1 : 0;
                    capacidad.Katz3 = UtilCast.valueToString(sheet, beginRow, beginCol + 30) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 30) == "X" ? 1 : 0;
                    capacidad.Katz4 = UtilCast.valueToString(sheet, beginRow, beginCol + 32) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 32) == "X" ? 1 : 0;
                    capacidad.Katz5 = UtilCast.valueToString(sheet, beginRow, beginCol + 34) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 34) == "X" ? 1 : 0;
                    capacidad.Katz6 = UtilCast.valueToString(sheet, beginRow, beginCol + 36) == "x" || UtilCast.valueToString(sheet, beginRow, beginCol + 36) == "X" ? 1 : 0;

                    var cal = UtilCast.calcularKatz(capacidad);

                    capacidad.PorcentajeGradoKatz = cal.PorcentajeGradoKatz;
                    capacidad.GradoDependenciaKatz = cal.GradoDependenciaKatz;


                    if (institucion == "SVPAU")
                    {
                        var caidaDetalle = UtilCast.valueToString(sheet, beginRow, beginCol + 38);

                        if (caidaDetalle == "MODERADO" || caidaDetalle == "Moderado")
                        {
                            capacidad.IdRiesgoCaidaDetalle = "MO";
                            capacidad.IdRiesgoCaida = "SI";
                        }
                        else if (caidaDetalle == "LEVE" || caidaDetalle == "Leve")
                        {
                            capacidad.IdRiesgoCaidaDetalle = "LE";
                            capacidad.IdRiesgoCaida = "SI";
                        }
                        else if (caidaDetalle == "ALTO" || caidaDetalle == "Alto")
                        {
                            capacidad.IdRiesgoCaidaDetalle = "AL";
                            capacidad.IdRiesgoCaida = "SI";
                        }
                        else if (caidaDetalle == "NO EVALUABLE" || caidaDetalle == "No evaluable")
                        {
                            capacidad.IdRiesgoCaidaDetalle = null;
                            capacidad.IdRiesgoCaida = "NO";
                        }
                        else if (caidaDetalle == null)
                        {
                            capacidad.IdRiesgoCaida = "NO";
                            capacidad.IdRiesgoCaidaDetalle = null;
                        }
                        else
                        {
                            throw new Exception("riesgo no mapeado");
                        }
                    }
                    else
                    {

                        var caida = UtilCast.valueToString(sheet, beginRow, beginCol + 38);

                        if (caida == "SI")
                        {
                            capacidad.IdRiesgoCaida = "SI";
                        }
                        else if (caida == "NO")
                        {
                            capacidad.IdRiesgoCaida = "NO";
                        }
                    }


                    /*var caidaDetalle = UtilCast.valueToString(sheet, beginRow, beginCol + 39);

                    if (caidaDetalle == "MODERADO")
                    {
                        capacidad.IdRiesgoCaidaDetalle = "MO";
                    }
                    else if (caidaDetalle == "LEVE")
                    {
                        capacidad.IdRiesgoCaidaDetalle = "LE";
                    }
                    else if (caidaDetalle == "ALTO")
                    {
                        capacidad.IdRiesgoCaidaDetalle = "AL";
                    }
                    else if (caidaDetalle == null)
                    {
                        capacidad.IdRiesgoCaidaDetalle = null;
                    }
                    else
                    {
                        throw new Exception("riesgo no mapeado");
                    }*/

                    capacidad.ComentarioCapacidad = UtilCast.valueToString(sheet, beginRow, beginCol + 39);

                    capacidad.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 40);

                    if (UString.estaVacio(capacidad.Evaluado))
                    {
                        capacidad.Evaluado = "";
                    }

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


                try
                {
                    socio.FechaInforme = UtilCast.valueToDateTime(sheet, beginRow, beginCol + 4);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " con error de fecha => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                socio.IdOrigen = "EVA";
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
                        socio.IdIntegracion = "EXCE";
                    }
                    else if (integra2 == "x" || integra2 == "X")
                    {
                        socio.IdIntegracion = "BUEN";
                    }
                    else if (integra3 == "x" || integra3 == "X")
                    {
                        socio.IdIntegracion = "REGU";
                    }
                    else if (integra4 == "x" || integra4 == "X")
                    {
                        socio.IdIntegracion = "NAPL";
                    }

                    var participacion1 = UtilCast.valueToString(sheet, beginRow, beginCol + 9);
                    var participacion2 = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                    var participacion3 = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                    var participacion4 = UtilCast.valueToString(sheet, beginRow, beginCol + 12);

                    if (participacion1 == "x" || participacion1 == "X")
                    {
                        socio.IdCooperacion = "EXCE";
                    }
                    else if (participacion2 == "x" || participacion2 == "X")
                    {
                        socio.IdCooperacion = "BUEN";
                    }
                    else if (participacion3 == "x" || participacion3 == "X")
                    {
                        socio.IdCooperacion = "REGU";
                    }
                    else if (participacion4 == "x" || participacion4 == "X")
                    {
                        socio.IdCooperacion = "NAPL";
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
                        socio.IdParticipacion = "EXCE";
                    }
                    else if (participacion22 == "x" || participacion22 == "X")
                    {
                        socio.IdParticipacion = "BUEN";
                    }
                    else if (participacion23 == "x" || participacion23 == "X")
                    {
                        socio.IdParticipacion = "REGU";
                    }
                    else if (participacion24 == "x" || participacion24 == "X")
                    {
                        socio.IdParticipacion = "NAPL";
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
                salud.IdOrigen = "EVA";
                salud.IdInstitucion = institucion;
                salud.IdBeneficiario = entidad.IdEntidad;
                salud.IdSalud = Program.inicioSalud;
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
                }

                var mes = salud.FechaInforme.Value.Month;
                salud.IdPeriodo = salud.FechaInforme.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");

                if (evaluado)
                {





                    //inicio componentes examenes

                    var ex1 = UtilCast.valueToString(sheet, beginRow, beginCol + 5);
                    if (ex1 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEMO", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex1 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEMO", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex2 = UtilCast.valueToString(sheet, beginRow, beginCol + 6);
                    if (ex2 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "GLUC", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex2 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "GLUC", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex3 = UtilCast.valueToString(sheet, beginRow, beginCol + 7);
                    if (ex3 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "UREA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex3 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "UREA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex4 = UtilCast.valueToString(sheet, beginRow, beginCol + 8);
                    if (ex4 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEPA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex4 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEPA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex5 = UtilCast.valueToString(sheet, beginRow, beginCol + 9);
                    if (ex5 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "LIPI", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex5 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "LIPI", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex6 = UtilCast.valueToString(sheet, beginRow, beginCol + 10);
                    if (ex6 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "TIRO", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex6 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "TIRO", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex7 = UtilCast.valueToString(sheet, beginRow, beginCol + 11);
                    if (ex7 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "B12", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex7 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "B12", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex8 = UtilCast.valueToString(sheet, beginRow, beginCol + 12);
                    if (ex8 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PSA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex8 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PSA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }



                    //NUEVOS
                    var ex9 = UtilCast.valueToString(sheet, beginRow, beginCol + 13);
                    if (ex9 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "ELEC", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex9 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "ELEC", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex10 = UtilCast.valueToString(sheet, beginRow, beginCol + 14);
                    if (ex10 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "FOLA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex10 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "FOLA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex11 = UtilCast.valueToString(sheet, beginRow, beginCol + 15);
                    if (ex11 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HIER", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex11 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HIER", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex12 = UtilCast.valueToString(sheet, beginRow, beginCol + 16);
                    if (ex12 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "ORIN", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex12 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "ORIN", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex13 = UtilCast.valueToString(sheet, beginRow, beginCol + 17);
                    if (ex13 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "POTA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex13 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "POTA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ex14 = UtilCast.valueToString(sheet, beginRow, beginCol + 18);
                    if (ex14 == "Alterado")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PARA", IdResultado = "ALT", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    else if (ex14 == "Normal")
                    {
                        salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PARA", IdResultado = "NOR", IdInstitucion = salud.IdInstitucion, IdBeneficiario = salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    //FIN NUEVOS


                    //fin componentes examenes

                    var otrosExamenes = UtilCast.valueToString(sheet, beginRow, beginCol + 19);



                    //CORRELACION OK


                    if (!UString.estaVacio(otrosExamenes))
                    {
                        // si no esta vacio, bsucar en los miscelaneos
                        Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene EXAMEN AUXILIAR => " + otrosExamenes + System.Environment.NewLine;
                    }

                    salud.IdDescarteSerologico = UtilCast.valueToString(sheet, beginRow, beginCol + 20) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 20) == "NO" ? "SI" : null;
                    salud.IdTbc = UtilCast.valueToString(sheet, beginRow, beginCol + 21) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 21) == "NO" ? "SI" : null;
                    salud.IdHta = UtilCast.valueToString(sheet, beginRow, beginCol + 22) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 22) == "NO" ? "SI" : null;
                    salud.IdDiabetes = UtilCast.valueToString(sheet, beginRow, beginCol + 23) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 23) == "NO" ? "SI" : null;
                    salud.IdOsteoatrosis = UtilCast.valueToString(sheet, beginRow, beginCol + 24) == "SI" ? "SI" : UtilCast.valueToString(sheet, beginRow, beginCol + 24) == "NO" ? "SI" : null;

                    salud.OtrasEnfermedades = UtilCast.valueToString(sheet, beginRow, beginCol + 25);



                    //inicio sensorial

                    var visionNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 26);
                    if (visionNormal == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "NORM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionMode = UtilCast.valueToString(sheet, beginRow, beginCol + 27);
                    if (visionMode == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "MODE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionGrave = UtilCast.valueToString(sheet, beginRow, beginCol + 28);
                    if (visionGrave == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "GRAV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var visionCegu = UtilCast.valueToString(sheet, beginRow, beginCol + 29);
                    if (visionCegu == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "CEGE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 30);
                    if (audioNormal == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "AUN", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioLeve = UtilCast.valueToString(sheet, beginRow, beginCol + 31);
                    if (audioLeve == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPL", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioMode = UtilCast.valueToString(sheet, beginRow, beginCol + 32);
                    if (audioMode == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioSevera = UtilCast.valueToString(sheet, beginRow, beginCol + 33);
                    if (audioSevera == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "SEVE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var audioProfunda = UtilCast.valueToString(sheet, beginRow, beginCol + 34);
                    if (audioProfunda == "x")
                    {
                        salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "PROF", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    //fin sensorial


                    var diagExtras = UtilCast.valueToString(sheet, beginRow, beginCol + 35);


                    if (!UString.estaVacio(diagExtras))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene Diagnóstico Médico (listado múltiple de preferencia check de selección) => " + diagExtras + System.Environment.NewLine;
                    }


                    var gene = UtilCast.valueToString(sheet, beginRow, beginCol + 36);

                    if (gene == "x" || gene == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRGEN", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var neuro = UtilCast.valueToString(sheet, beginRow, beginCol + 37);

                    if (neuro == "x" || neuro == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRNEU", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var psiq = UtilCast.valueToString(sheet, beginRow, beginCol + 38);

                    if (psiq == "x" || psiq == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "CRPSI", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var odo = UtilCast.valueToString(sheet, beginRow, beginCol + 39);

                    if (odo == "x" || odo == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "ODO", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var agu = UtilCast.valueToString(sheet, beginRow, beginCol + 40);

                    if (agu == "x" || agu == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "AGU", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var quiru = UtilCast.valueToString(sheet, beginRow, beginCol + 41);

                    if (quiru == "x" || quiru == "X")
                    {
                        salud.listaTratamiento.Add(new PsSaludTratamiento() { IdTratamiento = "QUI", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }


                    var otrotra = UtilCast.valueToString(sheet, beginRow, beginCol + 42);

                    if (!UString.estaVacio(otrotra))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otro tratamiento => " + otrotra + System.Environment.NewLine;
                    }

                    var pfeiffer = UtilCast.valueToString(sheet, beginRow, beginCol + 43);

                    switch (pfeiffer)
                    {
                        case "No deterioro cognitivo":
                            salud.IdCognitivo = "NDCO";
                            break;
                        case "Deterioro cognitivo leve":
                            salud.IdCognitivo = "DCL";
                            break;
                        case "Deterioro cognitivo moderado":
                            salud.IdCognitivo = "DCM";
                            break;
                        case "Deterioro cognitivo severo":
                            salud.IdCognitivo = "DCS";
                            break;
                        default:
                            break;
                    }

                    var yesavage = UtilCast.valueToString(sheet, beginRow, beginCol + 44);

                    switch (yesavage)
                    {
                        case "Con manifestaciones depresivas":
                            salud.IdAfectivo = "DEP";
                            break;
                        case "Sin manifestaciones depresivas":
                            salud.IdAfectivo = "SIN";
                            break;
                        default:
                            break;
                    }


                    var est1 = UtilCast.valueToString(sheet, beginRow, beginCol + 45);

                    if (est1 == "x" || est1 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDS", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est2 = UtilCast.valueToString(sheet, beginRow, beginCol + 46);

                    if (est2 == "x" || est2 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEA", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est3 = UtilCast.valueToString(sheet, beginRow, beginCol + 47);

                    if (est3 == "x" || est3 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est4 = UtilCast.valueToString(sheet, beginRow, beginCol + 48);

                    if (est4 == "x" || est4 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "SDEAC", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est5 = UtilCast.valueToString(sheet, beginRow, beginCol + 49);

                    if (est5 == "x" || est5 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDS", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est6 = UtilCast.valueToString(sheet, beginRow, beginCol + 50);

                    if (est6 == "x" || est6 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDEA", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }



                    var est7 = UtilCast.valueToString(sheet, beginRow, beginCol + 51);

                    if (est7 == "x" || est7 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDS", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }

                    var est8 = UtilCast.valueToString(sheet, beginRow, beginCol + 52);

                    if (est8 == "x" || est8 == "X")
                    {
                        salud.listaEstado.Add(new PsSaludEstado() { IdEstado = "CDEA", IdSalud = salud.IdSalud, IdBeneficiario = salud.IdBeneficiario, IdInstitucion = salud.IdInstitucion });
                    }



                    beginCol = beginCol + 2;











                    var td1 = UtilCast.valueToString(sheet, beginRow, beginCol + 51);
                    if (td1 == "x" || td1 == "X")
                    {
                        salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var td2 = UtilCast.valueToString(sheet, beginRow, beginCol + 52);
                    if (td2 == "x" || td2 == "X")
                    {
                        salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MENTA", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var td3 = UtilCast.valueToString(sheet, beginRow, beginCol + 53);
                    if (td3 == "x" || td3 == "X")
                    {
                        salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "SENSO", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var td4 = UtilCast.valueToString(sheet, beginRow, beginCol + 54);
                    if (td4 == "x" || td4 == "X")
                    {
                        salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MULTI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    //inicio biomeca

                    var baston = UtilCast.valueToString(sheet, beginRow, beginCol + 55);
                    if (baston != null)
                    {
                        if (baston.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 56);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "BAST", IdEstadoAyuda = es });
                        }
                    }
                    var muletas = UtilCast.valueToString(sheet, beginRow, beginCol + 57);
                    if (muletas != null)
                    {
                        if (muletas.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 58);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "MULE", IdEstadoAyuda = es });
                        }
                    }
                    var andador = UtilCast.valueToString(sheet, beginRow, beginCol + 59);
                    if (andador != null)
                    {
                        if (andador.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 60);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "ANDA", IdEstadoAyuda = es });
                        }
                    }

                    var silla = UtilCast.valueToString(sheet, beginRow, beginCol + 61);
                    if (silla != null)
                    {
                        if (silla.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 62);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "RUED", IdEstadoAyuda = es });
                        }
                    }

                    var gafas = UtilCast.valueToString(sheet, beginRow, beginCol + 63);
                    if (gafas != null)
                    {
                        if (gafas.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 64);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "GAFA", IdEstadoAyuda = es });
                        }
                    }

                    var implante = UtilCast.valueToString(sheet, beginRow, beginCol + 65);
                    if (implante != null)
                    {
                        if (implante.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 66);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "IMPL", IdEstadoAyuda = es });
                        }
                    }

                    var audi = UtilCast.valueToString(sheet, beginRow, beginCol + 67);
                    if (audi != null)
                    {
                        if (audi.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 68);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "AUDI", IdEstadoAyuda = es });
                        }
                    }

                    var neurol = UtilCast.valueToString(sheet, beginRow, beginCol + 69);
                    if (neurol != null)
                    {
                        if (neurol.ToUpper() == "X")
                        {
                            var es = UtilCast.valueToString(sheet, beginRow, beginCol + 70);
                            es = es == "R" ? "RE" : es == "M" ? "MA" : es == "B" ? "BU" : "BU";
                            salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud, IdTipoAyuda = "RUNE", IdEstadoAyuda = es });
                        }
                    }
                    //fin biomeca

                    var otroBio = UtilCast.valueToString(sheet, beginRow, beginCol + 71);

                    if (!UString.estaVacio(otroBio))
                    {
                        //  Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra ayuda biomecanica => " + otroBio + System.Environment.NewLine;
                    }


                    //tipo ayuda 47

                    var ta1 = UtilCast.valueToString(sheet, beginRow, beginCol + 72);
                    if (ta1 == "x" || ta1 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "TRAQ", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var ta2 = UtilCast.valueToString(sheet, beginRow, beginCol + 73);
                    if (ta2 == "x" || ta2 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "GAST", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var ta3 = UtilCast.valueToString(sheet, beginRow, beginCol + 74);
                    if (ta3 == "x" || ta3 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "FOLE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ta4 = UtilCast.valueToString(sheet, beginRow, beginCol + 75);
                    if (ta4 == "x" || ta4 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "NASO", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var ta5 = UtilCast.valueToString(sheet, beginRow, beginCol + 76);
                    if (ta5 == "x" || ta5 == "X")
                    {
                        salud.listaAyuda.Add(new PsSaludAyuda() { IdAyuda = "PRDE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    //tipo ayuda 52
                    var otroTipoAyu = UtilCast.valueToString(sheet, beginRow, beginCol + 77);

                    if (!UString.estaVacio(otroTipoAyu))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra Tipo de Ayuda Medico => " + otroTipoAyu + System.Environment.NewLine;
                    }




















                    //FALTA  HACER EL DETALLE DE TIPO DE AYUDA MEDICO COMO TABLA

                    var t1 = UtilCast.valueToString(sheet, beginRow, beginCol + 78);
                    if (t1 == "x" || t1 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var t2 = UtilCast.valueToString(sheet, beginRow, beginCol + 79);
                    if (t2 == "x" || t2 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "OCUP", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var t3 = UtilCast.valueToString(sheet, beginRow, beginCol + 80);
                    if (t3 == "x" || t3 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "LENG", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }
                    var t4 = UtilCast.valueToString(sheet, beginRow, beginCol + 81);
                    if (t4 == "x" || t4 == "X")
                    {
                        salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "PISC", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = salud.IdSalud });
                    }

                    var toTRO = UtilCast.valueToString(sheet, beginRow, beginCol + 82);

                    if (!UString.estaVacio(toTRO))
                    {
                        //Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + apepat + " " + apemat + ", " + nombre + " tiene otra terapia => " + toTRO + System.Environment.NewLine;

                    }
                    salud.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 91);

                    salud.Evaluado = UtilCast.valueToString(sheet, beginRow, beginCol + 92);

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
                                        Program.MENSAJES = Program.MENSAJES + "Error con el distrito para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine;
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
                                Program.MENSAJES = Program.MENSAJES + "Error con la provincia para " + entidad.IdEntidad + " de " + institucion + System.Environment.NewLine;
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
                var doc2 = UtilCast.valueToString(sheet, beginRow, beginCol + 13);
                var doc3 = UtilCast.valueToString(sheet, beginRow, beginCol + 14);

                if (doc1 != null)
                {
                    if (doc1.ToUpper() == "SI" || doc1.ToUpper() == "X")
                    {
                        entidad.listaDocumento.Add(new PsEntidadDocumento() { IdTipoDocumento = "AN", IdEntidad = entidad.IdEntidad });
                    }
                }

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

                var estadoCivil = UtilCast.valueToString(sheet, beginRow, beginCol + 15);

                switch (estadoCivil)
                {
                    case "Soltero (a)":
                    case "Soltero(a)":
                        entidad.IdEstadoCivil = "S";
                        break;
                    case "Casado (a)":
                    case "Casado(a)":
                        entidad.IdEstadoCivil = "C";
                        break;
                    case "Viudo (a)":
                    case "Viudo(a)":
                        entidad.IdEstadoCivil = "V";
                        break;
                    case "Divorsiado (a)":
                    case "Divorsiado(a)":
                    case "Divorciado(a)":
                        entidad.IdEstadoCivil = "D";
                        break;
                    case "Conviviente":
                        entidad.IdEstadoCivil = "E";
                        break;
                    case null:
                        entidad.IdEstadoCivil = null;
                        break;
                    default:
                        Program.MENSAJES = Program.MENSAJES + "No se encontro valor para el estado civil " + estadoCivil + System.Environment.NewLine;
                        break;
                }

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

                    if (causa1 != null)
                    {
                        if (causa1.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "POBR" });
                        }
                    }

                    if (causa2 != null)
                    {
                        if (causa2.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "RISO" });
                        }
                    }

                    if (causa3 != null)
                    {
                        if (causa3.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "ABAN" });
                        }
                    }

                    if (causa4 != null)
                    {
                        if (causa4.ToUpper() == "X")
                        {
                            entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = "IVOL" });
                        }
                    }

                    if (causa5 != null)
                    {
                        if (!UString.estaVacio(causa5))
                        {
                            var encontrado = maMiscelaneosdetalles.Find(x => x.Aplicacioncodigo == "PS" && x.Compania == "999999" && x.Codigotabla == (programa == "AAM" ? "CINGAAM" : "CINGNNA") && x.Descripcionlocal.ToUpper() == causa5.ToUpper());
                            if (encontrado != null)
                            {
                                entidad.ingreso.listaCausal.Add(new PsBeneficiarioIngresoCausal() { IdInstitucion = institucion, IdBeneficiario = entidad.IdEntidad, IdIngreso = entidad.ingreso.IdIngreso, IdCausal = encontrado.Codigoelemento });
                            }
                            else
                            {
                                Program.contadorMiscelaneos--;
                                maMiscelaneosdetalles.Add(new MaMiscelaneosdetalle() { Aplicacioncodigo = "PS", Compania = "999999", Descripcionlocal = causa5, Codigotabla = programa == "AAM" ? "CINGAAM" : "CINGNNA", Codigoelemento = "" + Program.contadorMiscelaneos });
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
                            case "Fiscalia":
                                entidad.ingreso.IdInstitucionDeriva = "FISC";
                                break;
                            case "Comisaria":
                                entidad.ingreso.IdInstitucionDeriva = "COMI";
                                break;
                            case "Hospital":
                                entidad.ingreso.IdInstitucionDeriva = "HOSP";
                                break;
                            case "Juzgado":
                                entidad.ingreso.IdInstitucionDeriva = "JUZG";
                                break;
                            case "MIMP":
                                entidad.ingreso.IdInstitucionDeriva = "MIMP";
                                break;
                            case "Familia":
                                entidad.ingreso.IdInstitucionDeriva = "FAMI";
                                break;
                            default:
                                break;
                        }
                    }

                    var legal = UtilCast.valueToString(sheet, beginRow, beginCol + 25);

                    switch (legal)
                    {
                        case "SI- Interdicción":
                            entidad.ingreso.IdSituacionLegal = "INTE";
                            break;
                        case "SI - Curatela voluntaria":
                            entidad.ingreso.IdSituacionLegal = "CURA";
                            break;
                        case "Representacion Propia":
                            entidad.ingreso.IdSituacionLegal = "PROP";
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

                entidad.ApoderadoNombre = UtilCast.valueToString(sheet, beginRow, beginCol + 36);
                entidad.ApoderadoNroDocumento = UtilCast.valueToString(sheet, beginRow, beginCol + 37);

                entidad.flgFamiliares = UtilCast.valueToString(sheet, beginRow, beginCol + 35) == "SI" ? "S" : "N";
                entidad.ApoderadoEsposoa = UtilCast.valueToString(sheet, beginRow, beginCol + 38);
                entidad.flgPensionista = UtilCast.valueToString(sheet, beginRow, beginCol + 39) == "SI" ? "S" : "N";


                try
                {
                    entidad.IngresoMensual = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 40);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                entidad.ComentariosReferenciaFamiliar = UtilCast.valueToString(sheet, beginRow, beginCol + 41);


                var gradoInstruccion = UtilCast.valueToString(sheet, beginRow, beginCol + 42);
                var gradoInstruccionIletrado = UtilCast.valueToString(sheet, beginRow, beginCol + 43);

                if (!UString.estaVacio(gradoInstruccionIletrado))
                {
                    if (gradoInstruccionIletrado.ToUpper() == "SI" || gradoInstruccionIletrado.ToUpper() == "X")
                    {
                        entidad.IdNivelAcademico = "IL";
                    }
                }
                else
                {
                    switch (gradoInstruccion)
                    {
                        case "Primaria":
                            entidad.IdNivelAcademico = "PR";
                            break;
                        case "Secundaria":
                            entidad.IdNivelAcademico = "SE";
                            break;
                        case "Tecnica":
                            entidad.IdNivelAcademico = "TE";
                            break;
                        case "Universitaria":
                            entidad.IdNivelAcademico = "UN";
                            break;
                        default:
                            break;
                    }
                }


                entidad.Telefono2 = UtilCast.valueToString(sheet, beginRow, beginCol + 45);

                var conadis = UtilCast.valueToString(sheet, beginRow, beginCol + 46);

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


                var flagSIS = UtilCast.valueToString(sheet, beginRow, beginCol + 47);
                if (flagSIS == "SI")
                {
                    var numero = UtilCast.valueToString(sheet, beginRow, beginCol + 48);
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "SIS", SeguroSocial = numero });
                }

                var flagSEG = UtilCast.valueToString(sheet, beginRow, beginCol + 49);
                if (flagSEG == "SI")
                {
                    var numero = UtilCast.valueToString(sheet, beginRow, beginCol + 50);
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "SESO", SeguroSocial = numero });
                }

                var flagPRI = UtilCast.valueToString(sheet, beginRow, beginCol + 52);
                if (flagPRI == "SI")
                {
                    entidad.listaSeguro.Add(new PsEntidadSeguroSocial() { IdEntidad = entidad.IdEntidad, IdSeguroSocial = "PRIV" });
                }

                var flagPension65 = UtilCast.valueToString(sheet, beginRow, beginCol + 53);
                if (flagPension65 != null)
                {
                    if (flagPension65.ToUpper() == "SI" || flagPension65.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "PE65" });
                    }

                }

                var flagContigo = UtilCast.valueToString(sheet, beginRow, beginCol + 54);
                if (flagContigo != null)
                {
                    if (flagContigo.ToUpper() == "SI" || flagContigo.ToUpper() == "X")
                    {
                        entidad.listaPrograma.Add(new PsEntidadProgramaSocial() { IdEntidad = entidad.IdEntidad, IdProgramaSocial = "CONT" });
                    }
                }

                entidad.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 55);

                //COMPONENTE NUTRICION INICIO 56-62
                entidad.nutricion.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.nutricion.IdBeneficiario = entidad.IdEntidad;
                entidad.nutricion.IdNutricion = Program.inicioNutricion;
                var mes = entidad.ingreso.FechaIngreso.Value.Month;
                entidad.nutricion.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.nutricion.FechaInforme = entidad.ingreso.FechaIngreso.Value;


                try
                {
                    entidad.nutricion.Peso = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 56);
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                try
                {
                    if (entidad.nutricion.IdInstitucion == "DESAM")
                    {
                        entidad.nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 57);

                    }
                    else
                    {
                        entidad.nutricion.Talla = UtilCast.valueToDecimal(sheet, beginRow, beginCol + 57) * 100.0m;

                    }
                }
                catch (Exception e)
                {
                    Program.MENSAJES = Program.MENSAJES + "El beneficiario => " + entidad.Nombrecompleto + " con error de numero => " + e.Message + System.Environment.NewLine;
                    continue;
                }

                entidad.nutricion.IdPerimetroPierna = UtilCast.valueToString(sheet, beginRow, beginCol + 59);
                entidad.nutricion.IdPresionMensual = UtilCast.valueToString(sheet, beginRow, beginCol + 60);

                //TO-DO evaluar cuando no vengan valores
                entidad.nutricion.IdValoracion = UtilCast.valueToString(sheet, beginRow, beginCol + 61);

                entidad.nutricion.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 62);
                entidad.nutricion.Estado = "A";
                entidad.nutricion.IdOrigen = "INI";
                entidad.nutricion.Evaluado = "";
                //calcular
                if (entidad.nutricion.Peso.HasValue && entidad.nutricion.Talla.HasValue && entidad.FechaNacimiento.HasValue)
                {
                    var edades = UtilCast.calcularEdad(entidad.FechaNacimiento, entidad.nutricion.FechaInforme.Value);
                    Object[] calculosNutricion = UtilCast.obtenerCalculosNutricion(conn, entidad, edades[0], edades[1]);
                    entidad.nutricion.Imc = calculosNutricion[1] as string;
                    entidad.nutricion.TallaEdad = calculosNutricion[2] as string;
                    entidad.nutricion.PesoEdad = calculosNutricion[3] as string;
                    entidad.nutricion.PesoTalla = calculosNutricion[4] as string;
                }
                Program.inicioNutricion++;
                //COMPONENTE NUTRICION FIN

                //COMPONENTE CAPACIDAD INICIO 63-74
                entidad.capacidad.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.capacidad.IdBeneficiario = entidad.IdEntidad;
                entidad.capacidad.IdCapacidad = Program.inicioCapacidad;
                Program.inicioCapacidad++;

                entidad.capacidad.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.capacidad.FechaInforme = entidad.ingreso.FechaIngreso.Value;

                //44 es ocupacion anterior
                entidad.capacidad.OcupacionAnterior = UtilCast.valueToString(sheet, beginRow, beginCol + 44);

                var riesgoCaida = UtilCast.valueToString(sheet, beginRow, beginCol + 76);

                switch (riesgoCaida)
                {
                    case "SI":
                    case "Si":
                    case "si":
                        entidad.capacidad.IdRiesgoCaida = "SI";
                        break;
                    default:
                        entidad.capacidad.IdRiesgoCaida = "NO";
                        break;
                }

                var caidaDetalle = UtilCast.valueToString(sheet, beginRow, beginCol + 77);

                if (caidaDetalle == "MODERADO")
                {
                    entidad.capacidad.IdRiesgoCaidaDetalle = "MO";
                }
                else if (caidaDetalle == "LEVE")
                {
                    entidad.capacidad.IdRiesgoCaidaDetalle = "LE";
                }
                else if (caidaDetalle == "ALTO")
                {
                    entidad.capacidad.IdRiesgoCaidaDetalle = "AL";
                }
                else if (caidaDetalle == "NO EVALUABLE")
                {
                    entidad.capacidad.IdRiesgoCaidaDetalle = null;
                }
                else if (caidaDetalle == null)
                {
                    entidad.capacidad.IdRiesgoCaidaDetalle = null;
                }
                else
                {
                    throw new Exception("riesgo no mapeado");
                }


                entidad.capacidad.Katz1 = UtilCast.valueToString(sheet, beginRow, beginCol + 64) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 64) == "X" ? 1 : 0;
                entidad.capacidad.Katz2 = UtilCast.valueToString(sheet, beginRow, beginCol + 66) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 66) == "X" ? 1 : 0;
                entidad.capacidad.Katz3 = UtilCast.valueToString(sheet, beginRow, beginCol + 68) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 68) == "X" ? 1 : 0;
                entidad.capacidad.Katz4 = UtilCast.valueToString(sheet, beginRow, beginCol + 70) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 70) == "X" ? 1 : 0;
                entidad.capacidad.Katz5 = UtilCast.valueToString(sheet, beginRow, beginCol + 72) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 72) == "X" ? 1 : 0;
                entidad.capacidad.Katz6 = UtilCast.valueToString(sheet, beginRow, beginCol + 74) == "x" ? 0 : UtilCast.valueToString(sheet, beginRow, beginCol + 74) == "X" ? 1 : 0;

                DtoCapacidad dto = UtilCast.calcularKatz(entidad.capacidad);

                entidad.capacidad.GradoDependenciaKatz = dto.GradoDependenciaKatz;
                entidad.capacidad.PorcentajeGradoKatz = dto.PorcentajeGradoKatz;

                entidad.capacidad.IdOrigen = "INI";
                entidad.capacidad.Estado = "A";
                entidad.capacidad.Evaluado = "";
                //COMPONENTE CAPACIDAD FIN 63-74

                //COMPONENTE SALUD INICIO 78 -
                entidad.salud.IdOrigen = "INI";
                entidad.salud.IdInstitucion = entidad.ingreso.IdInstitucion;
                entidad.salud.IdBeneficiario = entidad.IdEntidad;
                entidad.salud.IdSalud = Program.inicioSalud;
                Program.inicioSalud++;
                entidad.salud.IdPeriodo = entidad.ingreso.FechaIngreso.Value.ToString("yyyy") + (mes >= 6 ? "02" : "01");
                entidad.salud.FechaInforme = entidad.ingreso.FechaIngreso.Value;

                var baston = UtilCast.valueToString(sheet, beginRow, beginCol + 78);
                if (baston != null)
                {
                    if (baston.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "BAST", IdEstadoAyuda = "BU" });
                    }
                }
                var andador = UtilCast.valueToString(sheet, beginRow, beginCol + 79);
                if (andador != null)
                {
                    if (andador.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "ANDA", IdEstadoAyuda = "BU" });
                    }
                }

                var muletas = UtilCast.valueToString(sheet, beginRow, beginCol + 80);
                if (muletas != null)
                {
                    if (muletas.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "MULE", IdEstadoAyuda = "BU" });
                    }
                }

                var silla = UtilCast.valueToString(sheet, beginRow, beginCol + 81);
                if (silla != null)
                {
                    if (silla.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "RUED", IdEstadoAyuda = "BU" });
                    }
                }

                var gafas = UtilCast.valueToString(sheet, beginRow, beginCol + 82);
                if (gafas != null)
                {
                    if (gafas.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "GAFA", IdEstadoAyuda = "BU" });
                    }
                }

                var implante = UtilCast.valueToString(sheet, beginRow, beginCol + 83);
                if (implante != null)
                {
                    if (implante.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "IMPL", IdEstadoAyuda = "BU" });
                    }
                }

                var audi = UtilCast.valueToString(sheet, beginRow, beginCol + 84);
                if (audi != null)
                {
                    if (audi.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "AUDI", IdEstadoAyuda = "BU" });
                    }
                }

                var neuro = UtilCast.valueToString(sheet, beginRow, beginCol + 85);
                if (neuro != null)
                {
                    if (neuro.ToUpper() == "X")
                    {
                        entidad.salud.listaBiomecanica.Add(new PsSaludBiomecanica() { IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud, IdTipoAyuda = "RUNE", IdEstadoAyuda = "BU" });
                    }
                }

                var sangre = UtilCast.valueToString(sheet, beginRow, beginCol + 86);

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

                var rh = UtilCast.valueToString(sheet, beginRow, beginCol + 87);
                entidad.salud.IdFactorRh = rh == "Negativo" ? "N" : rh == "Positivo" ? "P" : null;

                var ex1 = UtilCast.valueToString(sheet, beginRow, beginCol + 88);
                if (ex1 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEMO", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex1 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEMO", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex2 = UtilCast.valueToString(sheet, beginRow, beginCol + 89);
                if (ex2 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "GLUC", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex2 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "GLUC", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex3 = UtilCast.valueToString(sheet, beginRow, beginCol + 90);
                if (ex3 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "UREA", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex3 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "UREA", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex4 = UtilCast.valueToString(sheet, beginRow, beginCol + 91);
                if (ex4 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEPA", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex4 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "HEPA", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex5 = UtilCast.valueToString(sheet, beginRow, beginCol + 92);
                if (ex5 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "LIPI", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex5 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "LIPI", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex6 = UtilCast.valueToString(sheet, beginRow, beginCol + 93);
                if (ex6 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "TIRO", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex6 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "TIRO", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex7 = UtilCast.valueToString(sheet, beginRow, beginCol + 94);
                if (ex7 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "B12", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex7 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "B12", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var ex8 = UtilCast.valueToString(sheet, beginRow, beginCol + 95);
                if (ex8 == "Alterado")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PSA", IdResultado = "ALT", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }
                else if (ex8 == "Normal")
                {
                    entidad.salud.listaExamen.Add(new PsSaludExamen() { IdExamen = "PSA", IdResultado = "NOR", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                entidad.salud.IdDescarteSerologico = UtilCast.valueToString(sheet, beginRow, beginCol + 96) == "Tratamiento -Si" ? "SI" : "NO";

                var tbc = UtilCast.valueToString(sheet, beginRow, beginCol + 97);

                if (tbc == "Negativo" || tbc == "NO" || tbc == "No")
                {
                    entidad.salud.IdTbc = "NO";
                }
                else if (tbc == "Positivo" || tbc == "SI" || tbc == "Si" || tbc == "Tratamiento -Si")
                {
                    entidad.salud.IdTbc = "SI";
                }

                var hta = UtilCast.valueToString(sheet, beginRow, beginCol + 98);

                if (hta == "SI")
                {
                    entidad.salud.IdHta = "SI";
                }
                else if (hta == "NO")
                {
                    entidad.salud.IdHta = "NO";
                }
                else if (hta == "Tratamiento -Si")
                {
                    entidad.salud.IdHta = "SI";
                }
                else if (hta == "Tratamiento -No")
                {
                    entidad.salud.IdHta = "NO";
                }
                else if (hta == "No")
                {
                    entidad.salud.IdHta = "NO";
                }

                var diab = UtilCast.valueToString(sheet, beginRow, beginCol + 99);

                if (diab == "SI")
                {
                    entidad.salud.IdDiabetes = "SI";
                }
                else if (diab == "NO")
                {
                    entidad.salud.IdDiabetes = "NO";
                }
                else if (diab == "Tratamiento -Si")
                {
                    entidad.salud.IdDiabetes = "SI";
                }
                else if (diab == "Tratamiento -No")
                {
                    entidad.salud.IdDiabetes = "NO";
                }
                else if (diab == "No")
                {
                    entidad.salud.IdDiabetes = "NO";
                }


                //nuevo
                var osteo = UtilCast.valueToString(sheet, beginRow, beginCol + 100);

                if (osteo == "SI")
                {
                    entidad.salud.IdOsteoatrosis = "SI";
                }
                else if (osteo == "NO")
                {
                    entidad.salud.IdOsteoatrosis = "NO";
                }
                else if (osteo == "Tratamiento -Si")
                {
                    entidad.salud.IdOsteoatrosis = "SI";
                }
                else if (osteo == "Tratamiento -No")
                {
                    entidad.salud.IdOsteoatrosis = "NO";
                }
                else if (osteo == "No")
                {
                    entidad.salud.IdOsteoatrosis = "NO";
                }

                beginCol = beginCol + 1;
                //nuevo

                var visionNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 100);
                if (visionNormal == "x" || visionNormal == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "NORM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionMode = UtilCast.valueToString(sheet, beginRow, beginCol + 101);
                if (visionMode == "x" || visionMode == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "MODE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionGrave = UtilCast.valueToString(sheet, beginRow, beginCol + 102);
                if (visionGrave == "x" || visionGrave == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "GRAV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var visionCegu = UtilCast.valueToString(sheet, beginRow, beginCol + 103);
                if (visionCegu == "x" || visionCegu == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "OFTA", IdSubcondicion = "CEGE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioNormal = UtilCast.valueToString(sheet, beginRow, beginCol + 104);
                if (audioNormal == "x" || audioNormal == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "AUN", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioLeve = UtilCast.valueToString(sheet, beginRow, beginCol + 105);
                if (audioLeve == "x" || audioLeve == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPL", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioMode = UtilCast.valueToString(sheet, beginRow, beginCol + 106);
                if (audioMode == "x" || audioMode == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "HIPM", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioSevera = UtilCast.valueToString(sheet, beginRow, beginCol + 107);
                if (audioSevera == "x" || audioSevera == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "SEVE", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var audioProfunda = UtilCast.valueToString(sheet, beginRow, beginCol + 108);
                if (audioProfunda == "x" || audioProfunda == "X")
                {
                    entidad.salud.listaSubcondicion.Add(new PsSaludSubcondicion() { IdCondicion = "AUDI", IdSubcondicion = "PROF", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }


                //diagnosticos

                var d1 = UtilCast.valueToString(sheet, beginRow, beginCol + 109);
                if (d1 == "x" || d1 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "I", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d2 = UtilCast.valueToString(sheet, beginRow, beginCol + 110);
                if (d2 == "x" || d2 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "II", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d3 = UtilCast.valueToString(sheet, beginRow, beginCol + 111);
                if (d3 == "x" || d3 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "III", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d4 = UtilCast.valueToString(sheet, beginRow, beginCol + 112);
                if (d4 == "x" || d4 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "IV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d5 = UtilCast.valueToString(sheet, beginRow, beginCol + 113);
                if (d5 == "x" || d5 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "IX", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d6 = UtilCast.valueToString(sheet, beginRow, beginCol + 114);
                if (d6 == "x" || d6 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "X", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d7 = UtilCast.valueToString(sheet, beginRow, beginCol + 115);
                if (d7 == "x" || d7 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d8 = UtilCast.valueToString(sheet, beginRow, beginCol + 116);
                if (d8 == "x" || d8 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d9 = UtilCast.valueToString(sheet, beginRow, beginCol + 117);
                if (d9 == "x" || d9 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XIII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d10 = UtilCast.valueToString(sheet, beginRow, beginCol + 118);
                if (d10 == "x" || d10 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XIV", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var d11 = UtilCast.valueToString(sheet, beginRow, beginCol + 119);
                if (d11 == "x" || d11 == "X")
                {
                    entidad.salud.listaDiagnostico.Add(new PsSaludDiagnostico() { IdDiagnostico = "XVII", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var pfeiffer = UtilCast.valueToString(sheet, beginRow, beginCol + 141);

                switch (pfeiffer)
                {
                    case "No deterioro cognitivo":
                        entidad.salud.IdCognitivo = "NDCO";
                        break;
                    case "Deterioro cognitivo leve":
                        entidad.salud.IdCognitivo = "DCL";
                        break;
                    case "Deterioro cognitivo moderado":
                        entidad.salud.IdCognitivo = "DCM";
                        break;
                    case "Deterioro cognitivo severo":
                        entidad.salud.IdCognitivo = "DCS";
                        break;
                    default:
                        break;
                }

                var yesavage = UtilCast.valueToString(sheet, beginRow, beginCol + 142);

                switch (yesavage)
                {
                    case "Con manifestaciones depresivas":
                        entidad.salud.IdAfectivo = "DEP";
                        break;
                    case "Sin manifestaciones depresivas":
                        entidad.salud.IdAfectivo = "SIN";
                        break;
                    default:
                        break;
                }

                var t1 = UtilCast.valueToString(sheet, beginRow, beginCol + 143);
                if (t1 == "x" || t1 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t2 = UtilCast.valueToString(sheet, beginRow, beginCol + 144);
                if (t2 == "x" || t2 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "OCUP", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t3 = UtilCast.valueToString(sheet, beginRow, beginCol + 145);
                if (t3 == "x" || t3 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "LENG", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var t4 = UtilCast.valueToString(sheet, beginRow, beginCol + 146);
                if (t4 == "x" || t4 == "X")
                {
                    entidad.salud.listaTerapia.Add(new PsSaludTerapia() { IdTerapia = "PISC", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var disc = false;

                var td1 = UtilCast.valueToString(sheet, beginRow, beginCol + 147);
                if (td1 == "x" || td1 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "FISI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var td2 = UtilCast.valueToString(sheet, beginRow, beginCol + 148);
                if (td2 == "x" || td2 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MENTA", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var td3 = UtilCast.valueToString(sheet, beginRow, beginCol + 149);
                if (td3 == "x" || td3 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "SENSO", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                var td4 = UtilCast.valueToString(sheet, beginRow, beginCol + 150);
                if (td4 == "x" || td4 == "X")
                {
                    disc = true;
                    entidad.salud.listaDiscapacidad.Add(new PsSaludDiscapacidad() { IdDiscapacidad = "MULTI", IdInstitucion = entidad.salud.IdInstitucion, IdBeneficiario = entidad.salud.IdBeneficiario, IdSalud = entidad.salud.IdSalud });
                }

                if (disc)
                {
                    entidad.IdDiscapacidad = "S";
                }
                else
                {
                    entidad.IdDiscapacidad = "N";
                }

                entidad.salud.Comentarios = UtilCast.valueToString(sheet, beginRow, beginCol + 151);


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
                entidad.beneficiario.IdComponenteSocioAmbiental = null;//vicente no tiene socioambiental
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
