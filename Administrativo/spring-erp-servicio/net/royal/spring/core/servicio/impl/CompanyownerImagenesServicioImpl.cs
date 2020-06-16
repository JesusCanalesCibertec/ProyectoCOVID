using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;

namespace net.royal.spring.core.servicio.impl
{

public class CompanyownerImagenesServicioImpl : GenericoServicioImpl, CompanyownerImagenesServicio {

        private IServiceProvider servicioProveedor;
        private CompanyownerImagenesDao companyownerImagenesDao;

        public CompanyownerImagenesServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            companyownerImagenesDao = servicioProveedor.GetService<CompanyownerImagenesDao>();
}

}
}
