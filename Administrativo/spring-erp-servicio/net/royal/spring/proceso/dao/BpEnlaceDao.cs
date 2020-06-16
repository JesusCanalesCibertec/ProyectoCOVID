using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.proceso.dominio;

namespace net.royal.spring.proceso.dao
{

    public interface BpEnlaceDao : GenericoDao<BpEnlace>
    {
        BpEnlace obtenerPorId(BpEnlacePk pk);

        DtoEnlace generarEnlace(DtoEnlace enlace);
    }
}
