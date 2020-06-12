using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace net.royal.spring.framework.core
{
    public class UFile
    {
        public static String getSeparador()
        {
            //return Path.PathSeparator.ToString();
            return Path.DirectorySeparatorChar.ToString();
        }

        public static byte[] obtenerArregloByte(String ruta)
        {
            if (UString.estaVacio(ruta))
                return null;
            if (!File.Exists(ruta))
                return null;
            return File.ReadAllBytes(ruta);
        }

        public static String extraerExtension(String fileName)
        {
            String extension = "";
            if (fileName == null)
                return null;
            int i = fileName.LastIndexOf('.');
            if (i > 0)
            {
                extension = fileName.Substring(i + 1);
            }
            return extension;
        }
    }
}
