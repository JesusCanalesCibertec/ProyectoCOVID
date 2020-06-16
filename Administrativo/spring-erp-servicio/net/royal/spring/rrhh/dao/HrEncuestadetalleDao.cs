using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEncuestadetalleDao : GenericoDao<HrEncuestadetalle>
    {
        HrEncuestadetalle obtenerPorId(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta);
        HrEncuestadetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestadetalle bean);
        HrEncuestadetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestadetalle bean);
        void coreEliminar(HrEncuestadetallePk id);
        void coreEliminar(Nullable<Int32> pSecuencia,Nullable<Int32> pPregunta);
        IList<DtoPreguntas> obtenerPreguntas(HrEncuestaPk hrEncuestaPk);
        IList<DtoTabla> obtenerAreas(HrEncuestaPk hrEncuestaPk);
        List<HrEncuestadetalle> listarPorEncuesta(HrEncuestaPk pk);
        void eliminarPorEncuesta(HrEncuestaPk hrEncuestaPk);
    }
}
