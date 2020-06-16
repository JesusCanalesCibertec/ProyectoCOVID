using net.royal.spring.framework.constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.core.dominio
{
    public class ParametroPersistenciaGenerico


    {
        public String Campo { get; set; }

        public ConstanteUtil.TipoDato Clase { get; set; }

        public Object Valor { get; set; }

        public ParametroPersistenciaGenerico(String _Campo, ConstanteUtil.TipoDato _Clase, Object _Valor) {
            Campo = _Campo;
            Clase = _Clase;
            Valor = _Valor;
        }

        public ParametroPersistenciaGenerico(string v) { }
    }
}
