using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.servicio;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.rrhh.dominio.dtoscomponente;

namespace net.royal.spring.programasocial.servicio.impl
{

public class PsRasServicioImpl : GenericoServicioImpl, PsRasServicio
{

        private IServiceProvider servicioProveedor;
        //private PsProgramaDao psProgramaDao;

        public PsRasServicioImpl(IServiceProvider _servicioProveedor) {
            servicioProveedor = _servicioProveedor;
            //psProgramaDao = servicioProveedor.GetService<PsProgramaDao>();
        }

        public DtoComponenteRas actualizar(UsuarioActual usuarioActual, DtoComponenteRas bean)
        {
            throw new NotImplementedException();
        }

        public DtoComponenteRas anular(UsuarioActual usuarioActual, String idInstitucion, Nullable<Int32> idBeneficiario, Nullable<Int32> idAtencion)
        {
            throw new NotImplementedException();
        }

        public List<DtoComponenteRas> consultar(UsuarioActual usuarioActual, FiltroRas filtro)
        {
            throw new NotImplementedException();
        }

        public DtoComponenteRas insertar(UsuarioActual usuarioActual, DtoComponenteRas bean)
        {
            throw new NotImplementedException();
        }

        public DtoComponenteRas obtenerPorId(String idInstitucion, Nullable<Int32> idBeneficiario, Nullable<Int32> idAtencion)
        {
            throw new NotImplementedException();
        }
    }
}
