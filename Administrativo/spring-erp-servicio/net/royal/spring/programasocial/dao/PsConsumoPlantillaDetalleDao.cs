using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio;

namespace net.royal.spring.rrhh.dao
{

    public interface PsConsumoPlantillaDetalleDao : GenericoDao<PsConsumoPlantillaDetalle>
    {
        PsConsumoPlantillaDetalle obtenerPorId(String pidInstitucion, Nullable<Int32> pPlantilla, String pidItem);
        PsConsumoPlantillaDetalle coreActualizar(UsuarioActual usuarioActual, PsConsumoPlantillaDetalle bean);
        PsConsumoPlantillaDetalle coreInsertar(UsuarioActual usuarioActual, PsConsumoPlantillaDetalle bean);
        void coreEliminar(PsConsumoPlantillaDetallePk id);
        void coreEliminar(String pidInstitucion, Nullable<Int32> pPlantilla, String pidItem);

        List<PsConsumoPlantillaDetalle> listarDetalle(String pidInstitucion, int plantilla);
    }
        
}
