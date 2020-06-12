using System;
using System.Collections.Generic;
using System.Text;

using net.royal.spring.framework.web.servicio;
using net.royal.spring.asistencia.dominio;

namespace net.royal.spring.asistencia.servicio
{

    public interface AsTipohorarioServicio : GenericoServicio
    {
        List<AsTipohorario> listarTodos();
        AsTipohorario obtenerPorId(AsTipohorarioPk pk);
        string[] listarHorasSemana(int? tipohorario);
    }
}
