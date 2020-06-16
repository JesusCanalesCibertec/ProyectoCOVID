using net.royal.spring.framework.correo.dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace net.royal.spring.sistema.servicio
{
    public interface SyCorreoServicio
    {
        EmailConfiguracion obtenerConfiguracion();

        void enviarCorreInmediato(EmailConfiguracion config, List<Email> listaEmail);

        void enviarCorreInmediatoAsincrono(EmailConfiguracion config, List<Email> listaEmail);
    }
}
