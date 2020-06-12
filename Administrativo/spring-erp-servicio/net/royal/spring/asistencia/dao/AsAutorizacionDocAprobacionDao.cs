using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.asistencia.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.planilla.dominio;
using net.royal.spring.sistema.dominio.dto;

namespace net.royal.spring.planilla.dao
{

    public interface AsAutorizacionDocAprobacionDao : GenericoDao<AsAutorizacionDocAprobacion>
    {
        List<DtoProcesoSeguimiento> listarSeguiemiento(Int32 NumeroProceso);
        void registrar(UsuarioActual usuarioActual, AsAutorizacion bean, String comentario);
    }
}
