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
using System.Linq;

namespace CargaExcel
{
    public class Program
    {
        public static List<PsEntidad> LISTA_GENERAL = new List<PsEntidad>();
        public static int script = 1;
        public static int contadorMiscelaneos = 9999;
        public static int inicioEntidad = 1;
        public static int inicioNutricion = 1;
        public static int inicioSalud = 1;
        public static int inicioCapacidad = 1;
        public static int inicioSocio = 1;

        public static int inicioConsumo = 1;

        public static bool escribir = true;


        static void Main(string[] args)
        {


            //ejecutarCargaItemsUpdate();
            //ejecutarCargaFunComponentes();
            //ejecutarCargaItems();
            //ejecutarCargaConsumo();
            ejecutarCargaFurh();

        }

        static void ejecutarCargaFurh()
        {
            List<MaMiscelaneosdetalle> maMiscelaneosdetalles = new List<MaMiscelaneosdetalle>();
            SqlConnection conn = new SqlConnection("Data Source=VM-RSNT-SRV-23\\SQL2014BDDEV;Initial Catalog=Spring_Canevaro_Cloud;User ID=desarrollo; Password=desarrollont,.");

            conn.Open();

            var codigos = UtilCast.generarCodigos(conn);
            inicioEntidad = codigos[0];

            var ruta = @"C:\Users\ApesteguiA\Downloads\New folder\FURH - Datos Personal.xlsx";
            var hoja = 1;
            var inicioNumero = 7;
            var inicioLetra = 1;
            var filas = 85;
            UtilCast.procesarFurh(conn, hoja, inicioNumero, inicioLetra, filas, ruta);

        }
        static void ejecutarCargaConsumo()
        {
            //DESAMPARADOS

            var ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\HAD\GRUPO NUTRICIONAL HAD- FUNDACION.xlsx";
            var hoja = 1;
            var inicioNumero = 9;
            var inicioLetra = 1;
            var filas = 46;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "DESAM", "F");


            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\HAD\GRUPO NUTRICIONAL HAD.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 107;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "DESAM", "O");

            //CANEVARO

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\IRC\GRUPO NUTRICIONAL IRC - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 98;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "CANEV", "F");

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\IRC\GRUPO NUTRICIONAL IRC.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 44;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "CANEV", "O");


            //INMACULADA

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\LI\GRUPO NUTRICIONAL LI - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 98;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "INMAC", "F");

            //CARIDAD

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\MC\GRUPO NUTRICIONAL MC - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 35;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "CARID", "F");


            //PUERICULTORIO

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\PPA\GRUPO NUTRICIONAL PPA - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 136;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "PURIC", "F");

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\PPA\GRUPO NUTRICIONAL PPA.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 71;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "PURIC", "O");


            //SAN FRANCISCO

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\SFA\GRUPO NUTRICIONAL SFA - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 91;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "FRASI", "F");

            //SAN VICENTE

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\SVP\GRUPO NUTRICIONAL SVP - FUNDACION.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 152;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "SVPAU", "F");

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\documentos\WBS 5 FASES\FASE II\FUN Y COMPONENTES\Consumos Instituciones\SVP\GRUPO NUTRICIONAL SVP.xlsx";
            hoja = 1;
            inicioNumero = 9;
            inicioLetra = 1;
            filas = 69;
            UtilCast.procesarPedido(hoja, inicioNumero, inicioLetra, filas, ruta, new DateTime(2018, 12, 24), "SVPAU", "O");

            return;
        }
        static void ejecutarCargaItems()
        {
            var ruta = @"C:\Users\ApesteguiA\Downloads\New folder\MAESTRO DE ALIMENTOS (1).xlsx";
            var hoja = 1;
            var inicioNumero = 196;
            var inicioLetra = 3;
            var filas = 281;
            UtilCast.procesarItems(hoja, inicioNumero, inicioLetra, filas, ruta);
        }
        static void ejecutarCargaItemsUpdate()
        {
            var ruta = @"C:\Users\ApesteguiA\Downloads\New folder\ali.xlsx";
            var hoja = 1;
            var inicioNumero = 2;
            var inicioLetra = 1;
            var filas = 474;
            UtilCast.procesarCoeficiente(hoja, inicioNumero, inicioLetra, filas, ruta);
        }
        static void ejecutarCargaFunComponentes()
        {
            List<MaMiscelaneosdetalle> maMiscelaneosdetalles = new List<MaMiscelaneosdetalle>();
            SqlConnection conn = new SqlConnection("Data Source=VM-RSNT-SRV-23\\SQL2014BDDEV;Initial Catalog=Spring_Canevaro_Cloud;User ID=desarrollo; Password=desarrollont,.");

            conn.Open();

            var codigos = UtilCast.generarCodigos(conn);
            inicioEntidad = codigos[0];
            inicioNutricion = codigos[1];
            inicioSalud = codigos[2];
            inicioCapacidad = codigos[3];
            inicioSocio = codigos[4];

            var ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN SAN VICENTE FINAL.xlsx";
            var hoja = 1;
            var inicioNumero = 8;
            var inicioLetra = 4;
            var filas = 142;
            contadorMiscelaneos = 9999;
            LISTA_GENERAL.AddRange(AAM.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "SVPAU", maMiscelaneosdetalles));

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN ASILO HERMANITAS DESAMPARADOS FINAL.xlsx";
            inicioNumero = 8;
            inicioLetra = 3;
            filas = 308;
            contadorMiscelaneos = 8999;

            LISTA_GENERAL.AddRange(AAM.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "DESAM", maMiscelaneosdetalles));

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN ALBERGUE CANEVARO FINAL.xlsx";
            inicioNumero = 8;
            inicioLetra = 3;
            filas = 331;
            contadorMiscelaneos = 7999;

            LISTA_GENERAL.AddRange(AAM.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "CANEV", maMiscelaneosdetalles));

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN PUERICULTORIO PEREZ ARANIBAR FINAL.xlsx";
            inicioNumero = 9;
            inicioLetra = 3;
            filas = 217;
            contadorMiscelaneos = 6999;
            LISTA_GENERAL.AddRange(NNA.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "PURIC", maMiscelaneosdetalles));

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN LA INMACULADA FINAL.xlsx";
            inicioNumero = 7;
            inicioLetra = 3;
            filas = 172;
            contadorMiscelaneos = 5999;
            LISTA_GENERAL.AddRange(NNA.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "INMAC", maMiscelaneosdetalles));


            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\FUN\FUN SAN FRANCISCO DE ASIS FINAL.xlsx";
            inicioNumero = 9;
            inicioLetra = 3;
            filas = 163;
            contadorMiscelaneos = 4999;
            LISTA_GENERAL.AddRange(NNA.procesarBeneficiarios(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "FRASI", maMiscelaneosdetalles));

            /*componentes (evaluaciones)
            */

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Comptes SAN VICENTE DE PAUL OFICIAL.xlsx";
            hoja = 1;
            inicioNumero = 5;
            inicioLetra = 2;
            filas = 142;
            AAM.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "SVPAU", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 142;
            AAM.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "SVPAU", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 2;
            filas = 142;
            AAM.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "SVPAU", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 2;
            filas = 142;
            AAM.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "SVPAU", maMiscelaneosdetalles);

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Comptes HAD OFICIAL.xlsx";
            hoja = 1;
            inicioNumero = 5;
            inicioLetra = 1;
            filas = 308;
            AAM.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "DESAM", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 308;
            AAM.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "DESAM", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 308;
            AAM.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "DESAM", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 308;
            AAM.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "DESAM", maMiscelaneosdetalles);


            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Comptes  ALBERGUE CANEVARO OFICIAL.xlsx";
            hoja = 1;
            inicioNumero = 5;
            inicioLetra = 1;
            filas = 331;
            AAM.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "CANEV", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 331;
            AAM.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "CANEV", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 331;
            AAM.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "CANEV", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 331;
            AAM.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "AAM", "CANEV", maMiscelaneosdetalles);


            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Componentes PEREZ ARANIBAR OFICIAL.xlsx";
            hoja = 1;
            inicioNumero = 6;
            inicioLetra = 1;
            filas = 217;
            NNA.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "PURIC", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 217;
            NNA.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "PURIC", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 217;
            NNA.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "PURIC", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 217;
            NNA.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "PURIC", maMiscelaneosdetalles);



            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Componentes LA INMACULADA OFICIAL_F.xlsx";
            hoja = 1;
            inicioNumero = 6;
            inicioLetra = 1;
            filas = 172;
            NNA.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "INMAC", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 172;
            NNA.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "INMAC", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 172;
            NNA.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "INMAC", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 172;
            NNA.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "INMAC", maMiscelaneosdetalles);

            ruta = @"D:\Fabrica\Desarrollo\ANGULAR\CANEVARO\CargaExcel\Datos\Original\componentes\Componentes SAN FRANCISCO DE ASIS OFICIAL.xlsx";
            hoja = 1;
            inicioNumero = 5;
            inicioLetra = 1;
            filas = 163;
            NNA.procesarNutricion(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "FRASI", maMiscelaneosdetalles);

            hoja = 2;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 163;
            NNA.procesarSalud(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "FRASI", maMiscelaneosdetalles);

            hoja = 3;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 163;
            NNA.procesarCapacidades(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "FRASI", maMiscelaneosdetalles);

            hoja = 4;
            inicioNumero = 7;
            inicioLetra = 1;
            filas = 163;
            NNA.procesarSocioambiental(conn, hoja, inicioNumero, inicioLetra, filas, ruta, "NNA", "FRASI", maMiscelaneosdetalles);

            conn.Close();

            //leer todos los archivos y escribir uno general
            if (Program.escribir)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\0_MISCELANEOS.sql"))
                {
                    maMiscelaneosdetalles.ForEach(x =>
                    {
                        file.WriteLine(UtilCast.queryMiscelaneo(x));
                    });
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\carga\ZZ_GENERAL.sql"))
                {

                    List<String> inst = new List<string>() { "SVPAU", "DESAM", "CANEV", "PURIC", "INMAC", "FRASI" };
                    List<String> comps = new List<string>() { "NUTRI", "SALUD", "CAPAC", "SOCIO" };


                    using (System.IO.StreamReader f1 = new System.IO.StreamReader(@"D:\carga\0_MISCELANEOS.sql"))
                    {
                        file.WriteLine(f1.ReadToEnd());
                    }

                    var c = 1;

                    foreach (var ins in inst)
                    {
                        using (System.IO.StreamReader f1 = new System.IO.StreamReader(@"D:\carga\" + c + "_" + ins + ".sql"))
                        {
                            Console.WriteLine(@"D:\carga\" + c + "_" + ins + ".sql");
                            file.WriteLine(f1.ReadToEnd());
                        }
                        c++;
                    }

                    foreach (var ins in inst)
                    {
                        foreach (var cc in comps)
                        {
                            using (System.IO.StreamReader f2 = new System.IO.StreamReader(@"D:\carga\" + c + "_" + ins + "_" + cc + ".sql"))
                            {
                                Console.WriteLine(@"D:\carga\" + c + "_" + ins + "_" + cc + ".sql");
                                file.WriteLine(f2.ReadToEnd());
                            }
                            c++;
                        }
                    }

                    file.WriteLine("UPDATE sgseguridadsys.PS_ENTIDAD SET APELLIDO_MATERNO = ''  WHERE APELLIDO_MATERNO IS NULL");
                    file.WriteLine("UPDATE sgseguridadsys.PS_ENTIDAD SET APELLIDO_PATERNO = ''  WHERE APELLIDO_PATERNO IS NULL");

                    Console.WriteLine(c);

                }
            }

            Console.ReadLine();
        }

        public static String MENSAJES = "";
    }
}
