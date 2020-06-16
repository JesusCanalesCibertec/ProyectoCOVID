using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface HrEncuestaplantilladetalleDao : GenericoDao<HrEncuestaplantilladetalle>
    {
        HrEncuestaplantilladetalle obtenerPorId(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta);
        HrEncuestaplantilladetalle coreActualizar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean);
        HrEncuestaplantilladetalle coreInsertar(UsuarioActual usuarioActual, HrEncuestaplantilladetalle bean);
        void coreEliminar(HrEncuestaplantilladetallePk id);
        void coreEliminar(Nullable<Int32> pPlantilla, Nullable<Int32> pPregunta);

        List<HrEncuestaplantilladetalle> listarPreguntas(int plantilla);
    }
        
}
