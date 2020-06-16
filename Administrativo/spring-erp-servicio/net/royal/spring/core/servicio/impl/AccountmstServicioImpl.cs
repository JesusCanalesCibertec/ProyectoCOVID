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

public class AccountmstServicioImpl : GenericoServicioImpl, AccountmstServicio {

        private IServiceProvider servicioProveedor;
        private AccountmstDao accountmstDao;

        public AccountmstServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            accountmstDao = servicioProveedor.GetService<AccountmstDao>();
}

        public List<Accountmst> listarTodos()
        {
            return accountmstDao.listarTodos();
        }
    }
}
