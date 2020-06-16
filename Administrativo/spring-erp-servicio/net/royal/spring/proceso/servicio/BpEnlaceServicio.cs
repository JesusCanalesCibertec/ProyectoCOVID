using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;

namespace net.royal.spring.proceso.servicio
{

    public interface BpEnlaceServicio : GenericoServicio
    {
        DtoEnlace validarEnlace(DtoEnlace enlace);

        DtoEnlace generarEnlace(DtoEnlace enlace);

        BpEnlace obtenerPorId(BpEnlacePk pk);

        BpEnlace actualizar(BpEnlace bean);
    }
}
