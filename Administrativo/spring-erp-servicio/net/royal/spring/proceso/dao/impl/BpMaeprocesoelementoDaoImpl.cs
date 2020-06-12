using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.proceso.dao;
using net.royal.spring.proceso.dominio;
using net.royal.spring.framework.core.dominio;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpMaeprocesoelementoDaoImpl : GenericoDaoImpl<BpMaeprocesoelemento>, BpMaeprocesoelementoDao
    {
        private IServiceProvider servicioProveedor;

        public BpMaeprocesoelementoDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpmaeprocesoelemento")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpMaeprocesoelemento> listarElementosIniciales(string idProceso)
        {
            List<BpMaeprocesoelemento> lstRetorno = new List<BpMaeprocesoelemento>();

            List<ParametroPersistenciaGenerico> parameters = new List<ParametroPersistenciaGenerico>();
            parameters.Add(new ParametroPersistenciaGenerico("p_id_proceso", ConstanteUtil.TipoDato.String, idProceso));

            List<DtoProcesoElemento> lst = new List<DtoProcesoElemento>();

            _reader = this.listarPorQuery("bpmaeprocesoelemento.listarElementosIniciales", parameters);

            while (_reader.Read())
            {
                var bean = new DtoProcesoElemento();
                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.idElemento = _reader.GetString(1);
                lst.Add(bean);
            }

            this.dispose();

            if (lst == null)
                return lstRetorno;

            if (lst.Count == 0)
                return lstRetorno;

            foreach (DtoProcesoElemento bean in lst)
            {
                BpMaeprocesoelemento e = this.obtenerPorId(new BpMaeprocesoelementoPk(bean.idProceso, bean.idElemento).obtenerArreglo());
                lstRetorno.Add(e);
            }

            return lstRetorno;
        }

        public BpMaeprocesoelemento obtenerPorIdConfiguracion(BpMaeprocesoelementoPk bpMaeProcesoElementoPk, Int32? idTransaccionPadre, List<BpProcesoconexion> listaConexionesPermitidas)
        {
            Boolean flgConfiguracionGuardar = false;
            Boolean flgConfiguracionConexion = false;
            int contadorElementoPermitidos = 0;
            int contadorElementoConexion = 0;
            BpMaeprocesoelemento bean = obtenerPorId(bpMaeProcesoElementoPk.obtenerArreglo());

            if (bean == null)
                return null;

            bean.auxFlgPuedeActualizar = UBoolean.validarFlag(bean.FlagPuedeActualizar);

            if (listaConexionesPermitidas.Count == 0)
                bean.auxFlgPuedeConexiones = false;
            else
                bean.auxFlgPuedeConexiones = true;

            if (!idTransaccionPadre.HasValue)
            {
                return bean;
            }

            if (idTransaccionPadre == 0)
            {
                return bean;
            }

            BpTransaccion bpTransaccion = servicioProveedor.GetService<BpTransaccionDao>().obtenerPorId(new BpTransaccionPk(idTransaccionPadre).obtenerArreglo());

            if (bpTransaccion == null)
                return bean;

            List<BpMaeprocesoelementoconfiguracion> lst = servicioProveedor.GetService<BpMaeprocesoelementoconfiguracionDao>()
                    .listarPorElemento(bpMaeProcesoElementoPk);

            foreach (BpMaeprocesoelementoconfiguracion bpMaeProcesoElementoConfiguracion in lst)
            {
                if (bpMaeProcesoElementoConfiguracion.IdTipoConfiguracion
                        .Equals(BpMaeprocesoelementoconfiguracion.TC_ELEMENTO_EXTERNO_PERMITE_GUARDAR)
                        && bean.auxFlgPuedeActualizar)
                {
                    flgConfiguracionGuardar = true;
                    if (bpTransaccion.ActualIdProceso
                            .Equals(bpMaeProcesoElementoConfiguracion.DependenciaIdProceso)
                            && bpTransaccion.ActualIdElemento
                                    .Equals(bpMaeProcesoElementoConfiguracion.DependenciaIdElemento))
                    {
                        contadorElementoPermitidos++;
                    }
                }

                if (bpMaeProcesoElementoConfiguracion.IdTipoConfiguracion
                        .Equals(BpMaeprocesoelementoconfiguracion.TC_ELEMENTO_EXTERNO_RESTRINGE_CONEXIONES))
                {
                    flgConfiguracionConexion = true;
                    if (bpTransaccion.ActualIdProceso
                            .Equals(bpMaeProcesoElementoConfiguracion.DependenciaIdProceso)
                            && bpTransaccion.ActualIdElemento
                                    .Equals(bpMaeProcesoElementoConfiguracion.DependenciaIdElemento))
                    {
                        contadorElementoConexion++;
                    }
                }
            }

            if (flgConfiguracionGuardar)
            {
                bean.auxFlgPuedeActualizar = false;
                if (contadorElementoPermitidos > 0)
                {
                    bean.auxFlgPuedeActualizar = true;
                }
            }


            if (flgConfiguracionConexion && bean.auxFlgPuedeConexiones)
            {
                if (contadorElementoConexion > 0)
                {
                    bean.auxFlgPuedeConexiones = false;
                }
            }

            return bean;
        }

        public List<DtoBpMaeprocesoelemento> listado(string codigo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpMaeprocesoelemento> lst = new List<DtoBpMaeprocesoelemento>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, codigo));


            _reader = this.listarPorQuery("bpmaeprocesoelemento.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpMaeprocesoelemento bean = new DtoBpMaeprocesoelemento();

                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomProceso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.idElemento = _reader.GetString(2);

                if (!_reader.IsDBNull(3))
                    bean.idUnico = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.nomElemento = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.idTipoElemento = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.nomTipoElemento = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.idEstado = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.nomEstado = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.idRol = _reader.GetString(9);

                if (!_reader.IsDBNull(10))
                    bean.nomRol = _reader.GetString(10);

                if (!_reader.IsDBNull(11))
                    bean.idNivelSeg = _reader.GetString(11);

                if (!_reader.IsDBNull(12))
                    bean.Icono = _reader.GetString(12);

                if (!_reader.IsDBNull(13))
                    bean.Avance = _reader.GetInt32(13);

                if (!_reader.IsDBNull(14))
                    bean.Dias = _reader.GetInt32(14);

                if (!_reader.IsDBNull(15))
                    bean.IdColor = _reader.GetString(15);

                if (!_reader.IsDBNull(16))
                    bean.NomColor = _reader.GetString(16);

                if (!_reader.IsDBNull(17))
                    bean.RutaIcono = _reader.GetString(17);

                if (!_reader.IsDBNull(18))
                    bean.PosX = _reader.GetInt32(18);

                if (!_reader.IsDBNull(19))
                    bean.PosY = _reader.GetInt32(19);



                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public string obtenercadena(string pIdProceso, string pNombre)
        {
            IQueryable<BpMaeprocesoelemento> query = this.getCriteria();
            query = query.Where(p => p.IdProceso == pIdProceso && p.Nombre == pNombre);

            List<BpMaeprocesoelemento> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Nombre;
            return null;
        }

        public string obtenercodigo(string pIdProceso, string pIdelemento)
        {
            IQueryable<BpMaeprocesoelemento> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdElemento == pIdelemento
                );

            List<BpMaeprocesoelemento> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdElemento;
            return null;
        }
    }
}
