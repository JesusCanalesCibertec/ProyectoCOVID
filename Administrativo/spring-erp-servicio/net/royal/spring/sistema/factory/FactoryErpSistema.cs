using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.servicio.impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace net.royal.spring.sistema.factory
{
    public class FactoryErpSistema
    {

        private ParametrosmastServicio _ParametrosmastServicio;
        private IServiceProvider _provider;

        public ParametrosmastServicio getParametrosmastServicio() {
            //_ParametrosmastServicio = _provider.GetService<ParametrosmastServicio>();
            return _ParametrosmastServicio;
        }
    }
}
