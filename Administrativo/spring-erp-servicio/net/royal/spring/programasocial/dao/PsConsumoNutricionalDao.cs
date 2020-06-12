using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.web.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.rrhh.dominio.dto;

namespace net.royal.spring.programasocial.dao
{

    public interface PsConsumoNutricionalDao : GenericoDao<PsConsumoNutricional>
    {
        PsConsumoNutricional obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional);
        PsConsumoNutricional coreActualizar(UsuarioActual usuarioActual, PsConsumoNutricional bean, String estado);
        PsConsumoNutricional coreInsertar(UsuarioActual usuarioActual, PsConsumoNutricional bean, String estado);
        void coreEliminar(PsConsumoNutricionalPk id);
        void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdConsumo, Nullable<Int32> pIdConsumoNutricional);
      
        int generarCodigo(String pIdInstitucion, Nullable<Int32> pIdConsumo);

        List<DtoConsumoNutricional> listardetalle(PsConsumoPk llave);

        void eliminardetalle(PsConsumoPk llave);
        List<PsConsumoNutricional> listadonormal(PsConsumoPk llave);
    }
}
