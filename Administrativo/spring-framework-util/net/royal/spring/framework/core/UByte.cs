using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UByte
    {
        public static String convertirString(byte[] arreglo)
        {
            if (arreglo == null)
                return null;
            return Encoding.UTF8.GetString(arreglo);
        }
        public static byte[] convertirByte(String texto)
        {
            if (texto == null)
                return null;
            return Encoding.UTF8.GetBytes(texto);
        }
    }
}
