using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.servicio.impl;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.servicio;
using net.royal.spring.sistema.dominio;
using net.royal.spring.core.servicio;
using net.royal.spring.framework.core;

namespace net.royal.spring.sistema.servicio.impl
{

    public class AplicacionesmastServicioImpl : GenericoServicioImpl, AplicacionesmastServicio
    {

        private IServiceProvider servicioProveedor;
        private AplicacionesmastDao aplicacionesmastDao;
        private ParametrosmastDao parametrosmastDao;

        public AplicacionesmastServicioImpl(IServiceProvider _servicioProveedor)
        {
            servicioProveedor = _servicioProveedor;
            aplicacionesmastDao = servicioProveedor.GetService<AplicacionesmastDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
        }

        public List<Aplicacionesmast> ListarActivos()
        {
            return aplicacionesmastDao.ListarActivos();
        }

        public string obtenerPeriodoContable(string aplicacionCodigo)
        {
            String tipoPeriodoVoucher;
            String ultimoperiodocontable = null;

            tipoPeriodoVoucher = parametrosmastDao.obtenerValorCadena("999999", "WH", "TPERIODVOU");
            if (tipoPeriodoVoucher == null)
                tipoPeriodoVoucher = "";

            Aplicacionesmast obj = aplicacionesmastDao.obtenerPorId(new AplicacionesmastPk() { Aplicacioncodigo = aplicacionCodigo }.obtenerArreglo());

            if (!tipoPeriodoVoucher.Equals("S"))
            {
                DateTime today = DateTime.Today;

                try
                {
                    ultimoperiodocontable = UFechaHora.convertirFechaCadena(today, "yyyy");
                    ultimoperiodocontable = ultimoperiodocontable + UFechaHora.convertirFechaCadena(today, "MM");
                }
                catch (Exception e)
                {

                }

                if (obj != null)
                {
                    if (obj.Ultimoperiodocontable != null)
                    {
                        if (!obj.Ultimoperiodocontable.Equals(ultimoperiodocontable))
                        {
                            /*
                             * obj.setUltimoperiodocontable(ultimoperiodocontable);
                             * this.update(obj);
                             */
                        }
                    }
                }

                if (aplicacionCodigo.Equals("AP"))
                {
                    Aplicacionesmast objCO = aplicacionesmastDao.obtenerPorId(new AplicacionesmastPk() { Aplicacioncodigo = "CO" }.obtenerArreglo());
                    if (!objCO.Ultimoperiodocontable.Equals(ultimoperiodocontable))
                    {
                        /*
                         * objCO.setUltimoperiodocontable(ultimoperiodocontable);
                         * this.update(objCO);
                         */
                    }
                }
            }

            obj = aplicacionesmastDao.obtenerPorId(new AplicacionesmastPk() { Aplicacioncodigo = aplicacionCodigo }.obtenerArreglo());

            if (obj == null)
            {
                obj = new Aplicacionesmast();
            }
            return obj.Ultimoperiodocontable;
        }
    }
}
