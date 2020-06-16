using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsMarcologicoresultadoDao : GenericoDao<PsMarcologicoresultado>
    {
        
        PsMarcologicoresultado coreActualizar(UsuarioActual usuarioActual, PsMarcologicoresultado bean, String estado);
        PsMarcologicoresultado coreInsertar(UsuarioActual usuarioActual, PsMarcologicoresultado bean, String estado);
        

        //String obtenercadena(string pIdNombre);
        //String obtenercodigo(string pIdModologico);

    }
}
