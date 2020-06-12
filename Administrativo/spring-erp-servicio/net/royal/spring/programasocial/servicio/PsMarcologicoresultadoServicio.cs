using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.servicio
{

    public interface PsMarcologicoresultadoServicio : GenericoServicio {
        
        PsMarcologicoresultado obtenerPorId(PsMarcologicoresultadoPk llave);
        PsMarcologicoresultado coreActualizar(UsuarioActual usuarioActual, PsMarcologicoresultado bean);
        PsMarcologicoresultado coreInsertar(UsuarioActual usuarioActual, PsMarcologicoresultado bean);
        void eliminar(string pIdMarcologico);
        
    }
}
