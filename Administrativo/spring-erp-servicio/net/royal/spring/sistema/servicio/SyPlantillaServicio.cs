using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.correo.dominio;

namespace net.royal.spring.sistema.servicio
{

    public interface SyPlantillaServicio : GenericoServicio {
        SyPlantilla obtenerPorId(SyPlantillaPk syPlantillaPk);
        
        void eliminar(SyPlantillaPk syPlantillaPk);

        List<SyPlantilla> listarTodos();

        List<Email> generarListaCorreo(SyPlantillaPk syPlantillaPk, List<ParametroPersistenciaGenerico> lstMetadata,
                List<String> listaCorreos);

        ParametroPaginacionGenerico listarPorPaginacion(ParametroPaginacionGenerico paginacion, SyPlantilla filtro);

        SyPlantilla obtenerPorIdString(SyPlantillaPk pkC);

        void registrar(UsuarioActual usuarioActual, SyPlantilla bean);

        void actualizar(UsuarioActual usuarioActual, SyPlantilla bean);
    }
}
