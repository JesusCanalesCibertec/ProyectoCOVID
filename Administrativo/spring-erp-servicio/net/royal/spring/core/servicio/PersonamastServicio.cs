using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio
{

    public interface PersonamastServicio : GenericoServicio {
        Personamast obtenerPorId(PersonamastPk personamastPk);
        Personamast registrar(Personamast personamast);
        Personamast actualizar(Personamast personamast);
        void eliminar(PersonamastPk personamastPk);
        ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion,
                FiltroPaginacionPersona filtro);
        string obtenerNombrePorPk(PersonamastPk pk);
        bool esJefePorUnidadOperativa(int? personaId);
        DtoTabla obtenerNombrePorJefeUnidadOperativa(string unidadoperativa);
    }
}
