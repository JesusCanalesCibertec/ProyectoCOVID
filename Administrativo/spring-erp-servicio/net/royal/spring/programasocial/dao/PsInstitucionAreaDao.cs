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

    public interface PsInstitucionAreaDao : GenericoDao<PsInstitucionArea>
    {
        
        PsInstitucionArea coreActualizar(UsuarioActual usuarioActual, PsInstitucionArea bean, String estado);
        PsInstitucionArea coreInsertar(UsuarioActual usuarioActual, PsInstitucionArea bean, String estado);
        List<PsInstitucionArea> listadoPorPrograma(string pIdInstitucion, String idPrograma);
        String obtenercadena(string pIdInstitucion, string pIdNombre);
        String obtenercodigo(string pIdInstitucion, string pIdArea);
        List<PsInstitucionArea> listado(string pIdInstitucion);
        List<PsInstitucionArea> listarTodo();

        PsInstitucionArea coreAnular(UsuarioActual usuarioActual, PsInstitucionAreaPk id);
        PsInstitucionArea coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, String pIdArea);
    }
}
