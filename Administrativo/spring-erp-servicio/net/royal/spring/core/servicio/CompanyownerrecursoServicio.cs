using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.dto;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.core.servicio
{

    public interface CompanyownerrecursoServicio : GenericoServicio {
        DtoRecursoConfiguracionResultado obtenerRecursoEnServidor(DtoRecursoParametro reporte);
        Companyownerrecurso registrar(UsuarioActual usuarioActual, Companyownerrecurso bean);
        Companyownerrecurso actualizar(UsuarioActual usuarioActual, Companyownerrecurso bean);
        IList<Companyownerrecurso> listarPorRecurso(String tipoRecurso);
        void eliminarPorTipoRecurso(string v);

        Companyownerrecurso obtenerPorId(CompanyownerrecursoPk pk);
        
    }
}
