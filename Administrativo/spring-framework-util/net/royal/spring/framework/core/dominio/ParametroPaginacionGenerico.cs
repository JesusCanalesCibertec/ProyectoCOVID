using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static net.royal.spring.framework.core.dominio.ConstanteDatos;

namespace net.royal.spring.framework.core.dominio
{
    public class ParametroPaginacionGenerico : DominioOrden
    {
        public Int32? RegistroInicio { get; set; }
        public Int32? CantidadRegistrosDevolver { get; set; }
        public Int32? RegistroEncontrados { get; set; }
        public IList ListaResultado { get; set; }

        public ParametroPaginacionGenerico()
        {
            RegistroInicio = 0;
        }
        public ParametroPaginacionGenerico(Int32 _registroInicio, Int32 _cantidadRegistrosDevolver,
            String _atributoOrdenar, SORT_ORDER _direccionOrdenAtributo)
        {
            RegistroInicio = _registroInicio;
            CantidadRegistrosDevolver = _cantidadRegistrosDevolver;
            AtributoOrdenar = _atributoOrdenar;
            //DireccionOrdenAtributo = _direccionOrdenAtributo;
        }
    }
}
