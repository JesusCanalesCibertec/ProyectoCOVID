using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.dao
{

    public interface PersonamastDao : GenericoDao<Personamast>
    {
        List<Personamast> listarPorCorreoElectronico(String codigousuario);
        String obtenerNombrePorPk(PersonamastPk pk);
        Personamast obtenerPorId(PersonamastPk personamastPk);
        List<Personamast> obtenerPorCorreoElectronico(String correoElectronicoEnviado);
        bool esJefePorUnidadOperativa(int? personaId);
        DtoTabla obtenerNombrePorJefeUnidadOperativa(string unidadoperativa);
    }
}
