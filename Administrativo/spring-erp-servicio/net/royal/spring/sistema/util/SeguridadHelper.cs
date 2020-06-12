using net.royal.spring.framework.core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace net.royal.spring.sistema.util
{
    public class SeguridadHelper
    {
        public static String springEncriptar(String texto)
        {
            String desencriptado = null;

            if (!String.IsNullOrEmpty(texto))
            {

                desencriptado = "";
                int plus = 1;

                for (int i = 0; i < texto.Length; i++)
                {
                    char caracter = texto[i];
                    int ascii = UString.convertChar2Ascii(caracter) + plus;
                    desencriptado = desencriptado + UString.convertAscii2Char(ascii);

                    plus++;
                }
            }
                return desencriptado;
        }
        public static String springDesencriptar(String password)
        {
            String encriptado = null;
            if (!String.IsNullOrEmpty(password))

            {

                encriptado = "";
                int minus = 1;

                for (int i = 0; i < password.Length; i++)
                {
                    char caracter = password[i];
                    int ascii = UString.convertChar2Ascii(caracter) - minus;
                    encriptado = encriptado + UString.convertAscii2Char(ascii);
                    minus++;
                }
                return encriptado;
            }
            return null;

            String letrita;

            Int32 int_Caracter = 0;
            Int32 int_Semilla = 0;
            Int32 int_Contador = 0;
            String str_Prueba;
            String str_Cifrado = "";

            if (UString.estaVacio(password))
                return password;

            password = password.Trim();

            for (int i = 0; i < password.Length; i++)
            {
                letrita = password.Substring(i, 1);

                int_Caracter = Encoding.Unicode.GetBytes(letrita)[0];
                int_Semilla = int_Contador + 1;
                int_Caracter -= int_Semilla;
                str_Prueba = Convert.ToString(Convert.ToChar(int_Caracter));
                str_Cifrado += Convert.ToChar(int_Caracter);
            }
            return str_Cifrado;
        }
    }
}
