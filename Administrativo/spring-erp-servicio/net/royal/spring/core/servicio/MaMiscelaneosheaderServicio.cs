using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.core.servicio
{

public interface MaMiscelaneosheaderServicio : GenericoServicio {

        MaMiscelaneosheader obtenerPorId(MaMiscelaneosheaderPk id);
        MaMiscelaneosheader obtenerPorId(Nullable<Int32> pPregunta);
        MaMiscelaneosheader coreActualizar(UsuarioActual usuarioActual, MaMiscelaneosheader bean);
        MaMiscelaneosheader coreInsertar(UsuarioActual usuarioActual, MaMiscelaneosheader bean);
        void coreEliminar(MaMiscelaneosheaderPk id);
        void coreEliminar(Nullable<Int32> pPregunta);
        ParametroPaginacionGenerico listarPaginacion(ParametroPaginacionGenerico paginacion, FiltroMiscelaneosheader filtro);
        MaMiscelaneosheader registrarPregunta(UsuarioActual usuarioActual, MaMiscelaneosheader bean);
        MaMiscelaneosheader solicitudObtenerPorId(string aplicacion, string compania, string codigo);
        MaMiscelaneosheader actualizarMiscelaneo(UsuarioActual usuarioActual, MaMiscelaneosheader bean);
        void eliminarPregunta(int? pregunta);

    }
}
