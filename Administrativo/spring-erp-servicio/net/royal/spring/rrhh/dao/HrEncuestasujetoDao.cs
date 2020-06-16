using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEncuestasujetoDao : GenericoDao<HrEncuestasujeto>
    {
        HrEncuestasujeto coreActualizar(UsuarioActual usuarioActual, HrEncuestasujeto bean);
        HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto bean);
        void coreEliminar(HrEncuestasujetoPk id);
        void coreEliminar(Nullable<Int32> pSecuencia, Nullable<Int32> pSujeto, Nullable<Int32> pPregunta, String pCompanyowner, String periodo);
        int autogenerarSujetoPorEncuesta(HrEncuestaPk hrEncuestaPk);
        HrEncuestasujeto coreInsertar(UsuarioActual usuarioActual, HrEncuestasujeto hrEncuestasujeto, DateTime ahoa);
        DtoEncuesta anularSujeto(DtoEncuestaSujeto dto);
        DtoEncuesta copiar(HrEncuestaPk pk);
    }
}
