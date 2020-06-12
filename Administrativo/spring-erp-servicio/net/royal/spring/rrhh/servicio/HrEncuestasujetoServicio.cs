using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.servicio;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.servicio
{

    public interface HrEncuestasujetoServicio : GenericoServicio
    {
        HrEncuestasujeto obtenerPorId(HrEncuestasujetoPk id);
        HrEncuestasujeto coreActualizar(UsuarioActual usuarioActual, HrEncuestasujeto bean);
        HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto bean);
        void coreEliminar(HrEncuestasujetoPk id);
        void coreEliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String periodo);
    }
}
