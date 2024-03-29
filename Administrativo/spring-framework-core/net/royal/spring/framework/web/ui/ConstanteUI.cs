﻿using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.framework.web.ui
{
    public class ConstanteUI
    {
        public static String MSG_ACCION_REQUERIDA = "No se envío la acción solicitada";

        /** para cuando se quiere ver los listados desde los menus**/
        public static String ACCION_SOLICITADA_LISTAR = "LISTAR";

        /** desde la venta de listado se da click en boton nuevo **/
        public static String ACCION_SOLICITADA_NUEVO = "NUEVO";

        /** desde la ventana de listado se desea modificar la informacion de un registro **/
        public static String ACCION_SOLICITADA_EDITAR = "EDITAR";

        /** desde la ventana de listado se desea anular un registro en forma logica**/
        public static String ACCION_SOLICITADA_ANULAR = "ANULAR";

        /** desde la ventana de listado se desea eliminar fisicamente un registro **/
        public static String ACCION_SOLICITADA_ELIMINAR = "ELIMINAR";
    }
}
