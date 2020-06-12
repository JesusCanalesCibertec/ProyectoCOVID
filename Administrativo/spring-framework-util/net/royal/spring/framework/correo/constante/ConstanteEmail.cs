using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.correo.constante
{
    public class ConstanteEmail
    {
        public enum tipo_configuracion
        {
            ARCHIVO, PARAMETROS, FUNCIONES
        };
        public enum tipo_destino
        {
            TO, CC, BCC
        };
}
}
