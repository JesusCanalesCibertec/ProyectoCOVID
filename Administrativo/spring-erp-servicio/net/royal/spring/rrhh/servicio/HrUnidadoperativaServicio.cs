using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrUnidadoperativaServicio : GenericoServicio
    {
        HrUnidadoperativa obtenerPorId(HrUnidadoperativaPk hrUnidadoperativaPk);
        void registrar(HrUnidadoperativa hrUnidadoperativa);
        void actualizar(HrUnidadoperativa hrUnidadoperativa);
        void eliminar(HrUnidadoperativa hrUnidadoperativa);
        List<HrUnidadoperativa> listarTodos();
        List<HrUnidadoperativa> listarBusqueda(DtoFiltro filtroPaginacionJefatura);
        List<HrUnidadoperativa> listarActivos();
        List<HrUnidadoperativa> listarJerarquico(String unidadOperativa);
    }
}
