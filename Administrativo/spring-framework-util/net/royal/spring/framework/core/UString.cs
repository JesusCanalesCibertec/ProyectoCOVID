using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UString
    {
        public static String trimNotNull(String variable)
        {
            if (variable == null || variable.Length == 0)
                return "";
            return variable.Trim();
        }

        public static String obtenerValorCadenaSinNulo(String variable)
        {
            if (variable == null || variable.Length == 0)
                return ""; 
            return variable;
        }

        public static Boolean estaVacio(String valor)
        {
            if (valor != null)
            {
                if (valor.Trim().Equals("null"))
                {
                    return true;
                }

                if (!(valor.Trim().Equals("")))
                {
                    return false;
                }

                
            }

            return true;
        }

        public static String Mayusculas(String variable)
        {
            if (variable == null || variable.Length == 0)
                return null;
            if (estaVacio(variable))
                return null;

            return variable.ToUpper().Trim();
        }


        public static String[] split(String text, String delemeter)
        {
            if (UString.estaVacio(text))
            {
                return new String[0];
            }
            Char delimiter = Char.Parse(delemeter);
            return text.Split(delimiter);
        }
        public static List<String> arregloCadenaToList(String[] arregloCadena)
        {
            List<String> lst = new List<string>();

            if (arregloCadena == null)
                return lst;

            for (int i = 0; i < arregloCadena.Length; i++)
            {
                lst.Add(arregloCadena[i]);
            }

            return lst;
        }

        public static int convertChar2Ascii(char caracter)
        {
            for (int i = 0; i < ASCII_CHAR.Length; i++)
            {
                if (ASCII_CHAR[i] == caracter)
                {
                    return ASCII_DEC[i];
                }
            }

            return (int)caracter;
        }

        public static char convertAscii2Char(int ascii)
        {
            for (int i = 0; i < ASCII_DEC.Length; i++)
            {
                if (ASCII_DEC[i] == ascii)
                {
                    return ASCII_CHAR[i];
                }
            }
            return (char)ascii;
        }

        public static char[] ASCII_CHAR = { '!', '"', '#', '$', '%', '&',
            '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3',
            '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g',
            'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', '€', '‚', 'ƒ',
            '„', '…', '†', '‡', 'ˆ', '‰', 'Š', '‹', 'Œ', 'Ž', '‘', '’', '“',
            '”', '•', '–', '—', '˜', '™', 'š', '›', 'œ', 'ž', 'Ÿ', '¡', '¢',
            '£', '¤', '¥', '¦', '§', '¨', '©', 'ª', '«', '¬', '®', '¯', '°',
            '±', '²', '³', '´', 'µ', '¶', '·', '¸', '¹', 'º', '»', '¼', '½',
            '¾', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê',
            'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×',
            'Ø', 'Ù', 'Ú', 'Û', 'Ü', 'Ý', 'Þ', 'ß', 'à', 'á', 'â', 'ã', 'ä',
            'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ð', 'ñ',
            'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ',
            'ÿ',

    };


        public static int[] ASCII_DEC = { 33, 34, 35, 36, 37, 38, 39, 40, 41,
            42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58,
            59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75,
            76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92,
            93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107,
            108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120,
            121, 122, 123, 124, 125, 126, 128, 130, 131, 132, 133, 134, 135,
            136, 137, 138, 139, 140, 142, 145, 146, 147, 148, 149, 150, 151,
            152, 153, 154, 155, 156, 158, 159, 161, 162, 163, 164, 165, 166,
            167, 168, 169, 170, 171, 172, 174, 175, 176, 177, 178, 179, 180,
            181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193,
            194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206,
            207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219,
            220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232,
            233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245,
            246, 247, 248, 249, 250, 251, 252, 253, 254, 255 };

        public static List<String> listaLimpiarDuplicadosNulos(List<String> listaCorreos)
        {

            if (listaCorreos == null)
            {
                return listaCorreos;
            }
            List<String> lst = new List<string>();

            foreach (String elemento in listaCorreos)
            {
                if (!estaVacio(elemento))
                    lst.Add(elemento);
            }
            listaCorreos = lst;
            lst = new List<string>();
            Boolean flgEncontrado = false;
            foreach (String elemento in listaCorreos)
            {
                flgEncontrado = false;
                foreach (String correo in lst)
                {
                    if (elemento.ToUpper().Equals(correo.ToUpper()))
                    {
                        flgEncontrado = true;
                        break;
                    }
                }
                if (!flgEncontrado)
                    lst.Add(elemento);
                flgEncontrado = false;
            }

            return lst;
        }

        public static string obtenerNombreMonto(Decimal monto)
        {

            if (monto == 0)
                return " CERO ";

            string letras = string.Empty;
            List<String> L = new List<String>(57) {""
            ,"UN","DOS","TRES","CUATRO","CINCO","SEIS","SIETE","OCHO","NUEVE" ,""
            ,"CIENTO ","DOSCIENTOS ","TRESCIENTOS ","CUATROCIENTOS ","QUINIENTOS ","SEISCIENTOS ","SETECIENTOS ","OCHOCIENTOS ","NOVECIENTOS "," "
            ,"DIECI","VEINTI","TREINTA Y ","CUARENTA Y ","CINCUENTA Y ","SESENTA Y ","SETENTA Y ","OCHENTA Y ","NOVENTA Y ",""
            ,"UN","DOS","TRES","CUATRO","CINCO","SEIS","SIETE","OCHO","NUEVE",""
            ,"DIEZ","VEINTE","TREINTA","CUARENTA","CINCUENTA","SESENTA","SETENTA","OCHENTA","NOVENTA",""
            ,"ONCE","DOCE","TRECE","CATORCE","QUINCE","CIEN" };



            int indicew = 0;
            int numeroi = 0;
            int pivot = 0;
            int resta = 0;
            int numeroy = 0;
            int numeroip = 0;
            int workv = 0;

            string w_decimals = string.Empty;
            Decimal w_number;

            w_number = Decimal.Round(monto, 2);
            numeroy = (int)Math.Truncate(monto);
            letras = "";
            //w_decimals = (w_number - (int)Math.Truncate(w_number)) + "";
            w_decimals = (w_number + "").Split(".")[1];

            //Loop de grupos centena+decena+unidades
            int j = 1;
            while (j <= 3)
            {
                pivot = 100;
                numeroi = 0;


                if (j == 1)
                    if (numeroy > 999999)
                        numeroi = (int)(Math.Truncate(numeroy / 1000000.0));

                if (j == 2)
                    if (numeroy > 999)
                        numeroi = (int)(Math.Truncate(numeroy / 1000.0));


                if (j == 3)
                    if (numeroy > 0)
                        numeroi = numeroy;

                numeroip = numeroi;

                if (numeroi > 0)
                {
                    for (int indice = 10; indice < 31; indice += 10)
                    {
                        if (numeroi >= pivot)
                        {
                            //para decenas enteros 10, 20,30....
                            workv = (int)(Math.Truncate(numeroi / 10.0));
                            if (workv * 10 == numeroi && pivot == 10)
                            {
                                indicew = (int)(40 + numeroi / pivot);
                                workv = (int)(Math.Truncate(numeroi / (decimal)pivot));
                                resta = workv * pivot;
                            }

                            else
                            {
                                if (numeroi < 16 && numeroi > 10)
                                {
                                    //para numeros entre 11 y 15
                                    indicew = 40 + numeroi;
                                    resta = numeroi;
                                }
                                else
                                {
                                    indicew = (int)(Math.Truncate(indice + numeroi / (decimal)pivot));
                                    //Para millones y miles se usa arreglo del 1-9, 
                                    //para unidades 31-39	
                                    if (j < 3 && indice == 30)
                                        indicew = indicew - 30;
                                    workv = (int)(Math.Truncate(numeroi / (decimal)pivot));
                                    resta = workv * pivot;
                                }

                            }

                            //Excepcion 100
                            if (numeroi == 100)
                                indicew = 56;
                            letras = letras + L[(int)(indicew)];
                            numeroi = numeroi - resta;
                        }

                        pivot = (int)(pivot / 10);
                    }


                    if (j == 1)
                    {
                        numeroy = numeroy - (numeroip * 1000000);
                        letras = letras + " MILLON ";
                        if (numeroip > 1)
                        {
                            letras = letras + " ES ";
                        }
                        letras = letras + "";
                    }

                    if (j == 2)
                    {
                        numeroy = numeroy - (numeroip * 1000);
                        letras = letras + " MIL ";
                    }
                }

                j += 1;
            }

            letras = letras + " Y " + w_decimals + " / 100 ";
            return letras;
        }

    }
}
