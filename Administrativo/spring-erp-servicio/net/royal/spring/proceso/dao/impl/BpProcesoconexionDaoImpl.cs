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
using net.royal.spring.proceso.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.framework.core;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.proceso.dominio.dto;

namespace net.royal.spring.proceso.dao.impl
{
    public class BpProcesoconexionDaoImpl : GenericoDaoImpl<BpProcesoconexion>, BpProcesoconexionDao
    {
        private IServiceProvider servicioProveedor;

        public BpProcesoconexionDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "bpprocesoconexion")
        {
            servicioProveedor = _servicioProveedor;
        }

        public List<BpMaeprocesoelemento> listarElementoPorElementoEntrada(BpMaeprocesoelementoPk procesoElemento)
        {
            List<BpMaeprocesoelemento> resultado = new List<BpMaeprocesoelemento>();
            List<BpProcesoconexion> lst = listarPorElementoEntrada(procesoElemento);
            foreach (BpProcesoconexion bpProcesoConexion in lst)
            {
                BpMaeprocesoelemento e = servicioProveedor.GetService<BpMaeprocesoelementoDao>().obtenerPorId(new BpMaeprocesoelementoPk(
                        bpProcesoConexion.SalidaIdProceso, bpProcesoConexion.SalidaIdElemento).obtenerArreglo());
                resultado.Add(e);
            }
            return resultado;
        }

        public List<BpProcesoconexion> listarPorElementoEntrada(BpMaeprocesoelementoPk bpMaeprocesoelementoPk)
        {
            IQueryable<BpProcesoconexion> cri = this.getCriteria();
            cri = cri.Where(x => x.IdProceso == bpMaeprocesoelementoPk.IdProceso);
            cri = cri.Where(x => x.EntradaIdProceso == bpMaeprocesoelementoPk.IdProceso);
            cri = cri.Where(x => x.EntradaIdElemento == bpMaeprocesoelementoPk.IdElemento);
            return cri.ToList();
        }

        public List<BpProcesoconexion> listarPorProcesoVersionElementoEntradaUsuario(string idProceso, int idVersion, string idElementoEntrada)
        {
            List<BpProcesoconexion> lst;
            List<BpProcesoconexion> lst2 = new List<BpProcesoconexion>();

            IQueryable<BpProcesoconexion> cri = this.getCriteria();

            cri = cri.Where(x => x.IdProceso == idProceso);
            cri = cri.Where(x => x.IdVersion == idVersion);
            cri = cri.Where(x => x.EntradaIdProceso == idProceso);
            cri = cri.Where(x => x.EntradaIdElemento == idElementoEntrada);
            cri = cri.Where(x => x.FlagOcultarUsuario == null || x.FlagOcultarUsuario != "S");

            lst = cri.ToList();

            foreach (BpProcesoconexion bpProcesoConexion in lst)
            {
                bpProcesoConexion.auxElementoEntrada = servicioProveedor.GetService<BpMaeprocesoelementoDao>().obtenerPorId(new BpMaeprocesoelementoPk(bpProcesoConexion.EntradaIdProceso, bpProcesoConexion.EntradaIdElemento).obtenerArreglo());
                bpProcesoConexion.auxElementoSalida = servicioProveedor.GetService<BpMaeprocesoelementoDao>().obtenerPorId(new BpMaeprocesoelementoPk(bpProcesoConexion.SalidaIdProceso, bpProcesoConexion.SalidaIdElemento).obtenerArreglo());
                lst2.Add(bpProcesoConexion);
            }

            return lst2;
        }

        public List<BpProcesoconexion> listarPorProcesoVersionElementoEntradaUsuario(string idProceso, int idVersion, string idElementoEntrada, UsuarioActual usuarioActual, int idTransaccionSolicitante, int? idTransaccionResponsable, int? idTransaccionAprobador)
        {
            String transaccionSolicitanteCentroCostos = null;
            String transaccionSolicitanteSucursal = null;
            List<DtoProcesoConexion> lstResultado = new List<DtoProcesoConexion>();
            List<BpProcesoconexion> lst = new List<BpProcesoconexion>();

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_proceso", ConstanteUtil.TipoDato.String, idProceso));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_version", ConstanteUtil.TipoDato.Integer, idVersion));
            parametros.Add(new ParametroPersistenciaGenerico("p_id_elemento_entrada", ConstanteUtil.TipoDato.String, idElementoEntrada));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_usuario", ConstanteUtil.TipoDato.String, usuarioActual.UsuarioLogin));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_persona", ConstanteUtil.TipoDato.Integer, usuarioActual.PersonaId));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_puesto", ConstanteUtil.TipoDato.Integer, usuarioActual.PuestoEmpresaCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_centro_costo", ConstanteUtil.TipoDato.String, usuarioActual.CentroCostosCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_session_id_sucursal", ConstanteUtil.TipoDato.String, usuarioActual.SucursalCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_transaccion_id_solicitante", ConstanteUtil.TipoDato.Integer, idTransaccionSolicitante));
            parametros.Add(new ParametroPersistenciaGenerico("p_transaccion_id_responsable", ConstanteUtil.TipoDato.Integer, idTransaccionResponsable));
            parametros.Add(new ParametroPersistenciaGenerico("p_transaccion_id_aprobador", ConstanteUtil.TipoDato.Integer, idTransaccionAprobador));
            parametros.Add(new ParametroPersistenciaGenerico("p_transaccion_solicitante_id_centrocostos", ConstanteUtil.TipoDato.String, transaccionSolicitanteCentroCostos));
            parametros.Add(new ParametroPersistenciaGenerico("p_transaccion_solicitante_id_sucursal", ConstanteUtil.TipoDato.String, transaccionSolicitanteSucursal));

            _reader = this.listarPorQuery("bpprocesoconexion.listarPorProcesoVersionElementoEntradaUsuario", parametros);



            while (_reader.Read())
            {
                var bean = new DtoProcesoConexion();
                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.idVersion = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.idConexion = _reader.GetInt32(2);
                lstResultado.Add(bean);
            }

            this.dispose();

            for (int i = 0; i < lstResultado.Count; i++)
            {
                DtoProcesoConexion dto = (DtoProcesoConexion)lstResultado[i];
                BpProcesoconexion cone = obtenerPorId(new BpProcesoconexionPk(dto.idProceso, dto.idVersion, dto.idConexion).obtenerArreglo());

                BpMaeprocesoelemento aux = servicioProveedor.GetService<BpMaeprocesoelementoDao>().obtenerPorId(new BpMaeprocesoelementoPk(cone.IdProceso, cone.SalidaIdElemento).obtenerArreglo());

                if (!UString.estaVacio(cone.IconoHojaEstilo))
                    aux.AuxIconoHojaEstilo = cone.IconoHojaEstilo;
                else
                    aux.AuxIconoHojaEstilo = aux.IconoHojaEstilo;

                cone.auxElementoSalida = aux;

                BpMaeprocesoelemento aux1 = servicioProveedor.GetService<BpMaeprocesoelementoDao>().obtenerPorId(new BpMaeprocesoelementoPk(cone.IdProceso, cone.EntradaIdElemento).obtenerArreglo());
                cone.auxElementoEntrada = aux1;
                lst.Add(cone);
            }

            return lst;
        }

        public BpProcesoconexion obtener(string idProceso, int? idVersion, BpMaeprocesoelementoPk bpMaeElementoEntradaPk, BpMaeprocesoelementoPk bpMaeElementoSalidaPk)
        {
            List<BpProcesoconexion> resultado = null;


            // TODO Auto-generated method stub
            IQueryable<BpProcesoconexion> query = getCriteria();

            query = query.Where(x => x.IdProceso == idProceso);
            query = query.Where(x => x.IdVersion == idVersion);
            query = query.Where(x => x.EntradaIdProceso == bpMaeElementoEntradaPk.IdProceso);
            query = query.Where(x => x.EntradaIdElemento == bpMaeElementoEntradaPk.IdElemento);
            query = query.Where(x => x.SalidaIdProceso == bpMaeElementoSalidaPk.IdProceso);
            query = query.Where(x => x.SalidaIdElemento == bpMaeElementoSalidaPk.IdElemento);

            resultado = query.ToList();

            if (resultado == null)
                return null;

            if (resultado.Count == 1)
                return resultado[0];

            return null;
        }



        /*ERNESTO*/
        public List<DtoBpProcesoconexion> listado(string codigo)
        {
            List<ParametroPersistenciaGenerico> _parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoBpProcesoconexion> lst = new List<DtoBpProcesoconexion>();


            _parametros.Add(new ParametroPersistenciaGenerico("p_IdProceso", ConstanteUtil.TipoDato.String, codigo));


            _reader = this.listarPorQuery("bpprocesoconexion.filtro", _parametros);

            while (_reader.Read())
            {
                DtoBpProcesoconexion bean = new DtoBpProcesoconexion();

                if (!_reader.IsDBNull(0))
                    bean.idProceso = _reader.GetString(0);

                if (!_reader.IsDBNull(1))
                    bean.nomProceso = _reader.GetString(1);

                if (!_reader.IsDBNull(2))
                    bean.idConexion = _reader.GetInt32(2);

                if (!_reader.IsDBNull(3))
                    bean.idUnico = _reader.GetString(3);

                if (!_reader.IsDBNull(4))
                    bean.accion = _reader.GetString(4);

                if (!_reader.IsDBNull(5))
                    bean.idEntradaElemento = _reader.GetString(5);

                if (!_reader.IsDBNull(6))
                    bean.nomEntradaElemento = _reader.GetString(6);

                if (!_reader.IsDBNull(7))
                    bean.idSalidaElemento = _reader.GetString(7);

                if (!_reader.IsDBNull(8))
                    bean.nomSalidaElemento = _reader.GetString(8);

                if (!_reader.IsDBNull(9))
                    bean.idVersion = _reader.GetInt32(9);

                lst.Add(bean);
            }
            this.dispose();
            return lst;
        }



        public Int32 obtenercodigo(string pIdProceso, Int32 pIdConexion, Int32 pIdVersion)
        {
            IQueryable<BpProcesoconexion> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdConexion == pIdConexion && p.IdVersion == pIdVersion
                );

            List<BpProcesoconexion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].IdConexion;
            return 0;
        }

        public string obtenercadena(string pIdProceso, int pIdVersion, string pAccion)
        {
            IQueryable<BpProcesoconexion> query = this.getCriteria();
            query = query.Where(
                p => p.IdProceso == pIdProceso && p.IdVersion == pIdVersion && p.Accion == pAccion
                );

            List<BpProcesoconexion> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0].Accion;
            return null;
        }

        public int generarCodigo(string pIdProceso)
        {
            IQueryable<BpProcesoconexion> query = this.getCriteria();

            List<BpProcesoconexion> lst = query.ToList();
            query = query.Where(
                p => p.IdProceso == pIdProceso
                );
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdConexion));
            }

            return var + 1;
        }

        /*ERNESTO*/
    }
}
