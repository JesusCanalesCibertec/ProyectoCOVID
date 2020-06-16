using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.core.dao;
using net.royal.spring.core.servicio;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core.dominio.dto;

namespace net.royal.spring.core.servicio.impl
{

public class PersonamastServicioImpl : GenericoServicioImpl, PersonamastServicio {

        private IServiceProvider servicioProveedor;
        private PersonamastDao personamastDao;

        public PersonamastServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            personamastDao = servicioProveedor.GetService<PersonamastDao>();
        }

        public Personamast actualizar(Personamast personamast)
        {
            personamastDao.actualizar(personamast);
            return personamast;
        }

        public void eliminar(PersonamastPk personamastPk)
        {
            Personamast personamast = personamastDao.obtenerPorId(personamastPk);
            personamastDao.eliminar(personamast);
        }

        public ParametroPaginacionGenerico listarBusqueda(ParametroPaginacionGenerico paginacion, FiltroPaginacionPersona filtro)
        {
            throw new NotImplementedException();
        }

        public Personamast obtenerPorId(PersonamastPk personamastPk)
        {
            return personamastDao.obtenerPorId(personamastPk);
        }

        public Personamast registrar(Personamast personamast)
        {
            personamastDao.registrar(personamast);
            return personamast;
        }

        public string obtenerNombrePorPk(PersonamastPk pk)
        {
            return personamastDao.obtenerNombrePorPk(pk);
        }

        public DtoTabla obtenerNombrePorJefeUnidadOperativa(string unidadoperativa)
        {
            return personamastDao.obtenerNombrePorJefeUnidadOperativa(unidadoperativa);
        }

        public bool esJefePorUnidadOperativa(int? personaId)
        {
            return personamastDao.esJefePorUnidadOperativa(personaId);
        }
    }
}
