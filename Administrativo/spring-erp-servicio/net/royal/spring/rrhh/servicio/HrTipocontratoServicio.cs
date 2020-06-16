using System;
using System.Collections.Generic;
using System.Text;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrTipocontratoServicio : GenericoServicio
    {
        HrTipocontrato obtenerPorId(HrTipocontratoPk hrTipocontratoPk);
        void registrar(HrTipocontrato hrTipocontrato);
        void actualizar(HrTipocontrato hrTipocontrato);
        void eliminar(HrTipocontrato hrTipocontrato);
        List<HrTipocontrato> listarTodos();
        List<HrTipocontrato> listar(DtoFiltro filtro);
        List<HrTipocontrato> listarActivos();
    }
}
