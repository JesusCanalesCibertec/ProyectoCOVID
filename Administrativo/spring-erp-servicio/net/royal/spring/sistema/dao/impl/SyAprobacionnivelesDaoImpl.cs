using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.sistema.dominio.dto;
using net.royal.spring.sistema.dominio.filtro;
using System.Collections.Generic;
using net.royal.spring.framework.core;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.rrhh.dominio;
using net.royal.spring.sistema.constante;
using net.royal.spring.contabilidad.dominio;
using net.royal.spring.contabilidad.dao;
using net.royal.spring.rrhh.dao;
using net.royal.spring.core.dao;
using net.royal.spring.core.dominio;
using net.royal.spring.core.dominio.filtro;

namespace net.royal.spring.sistema.dao.impl
{
    public class SyAprobacionnivelesDaoImpl : GenericoDaoImpl<SyAprobacionniveles>, SyAprobacionnivelesDao
    {
        private IServiceProvider servicioProveedor;
        private SyAprobacionnivelesDetDao syAprobacionnivelesDetDao;
        private ParametrosmastDao parametrosmastDao;
        private AcCostcentermstDao acCostcentermstDao;
        private HrUnidadoperativaDao hrUnidadoperativaDao;
        private DepartmentmstDao departmentmstDao;
        private PersonamastDao personamastDao;
        private MaMiscelaneosdetalleDao maMiscelaneosdetalleDao;
        private HrTipocontratoDao hrTipocontratoDao;
        private HrDepartamentoDao hrDepartamentoDao;

        public SyAprobacionnivelesDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "syaprobacionniveles")
        {
            servicioProveedor = _servicioProveedor;
            syAprobacionnivelesDetDao = _servicioProveedor.GetService<SyAprobacionnivelesDetDao>();
            parametrosmastDao = _servicioProveedor.GetService<ParametrosmastDao>();
            acCostcentermstDao = _servicioProveedor.GetService<AcCostcentermstDao>();
            hrUnidadoperativaDao = _servicioProveedor.GetService<HrUnidadoperativaDao>();
            departmentmstDao = _servicioProveedor.GetService<DepartmentmstDao>();
            hrTipocontratoDao = _servicioProveedor.GetService<HrTipocontratoDao>();
            hrDepartamentoDao = _servicioProveedor.GetService<HrDepartamentoDao>();
            personamastDao = _servicioProveedor.GetService<PersonamastDao>();
            maMiscelaneosdetalleDao = _servicioProveedor.GetService<MaMiscelaneosdetalleDao>();
        }

        public string esAdministrador(SyAprobacionnivelesPk pk, int? idPersona, String compania)
        {
            SyAprobacionniveles bean = this.obtenerPorCodigoProcesoCompania(pk.Codigo, compania);
            if (bean == null)
                return "N";
            if (!UBoolean.validarFlag(bean.Flagusuarioadministrador))
                return "N";
            if (UInteger.obtenerValorEnteroSinNulo(bean.Usuarioadministrador) == 0)
                return "N";
            if (UInteger.obtenerValorEnteroSinNulo(idPersona) == 0)
                return "N";
            if (bean.Usuarioadministrador == idPersona)
                return "S";
            return "N";
        }
        public string esSuperUsuario(SyAprobacionnivelesPk pk, int? idPersona, String compania)
        {
            SyAprobacionniveles bean = this.obtenerPorCodigoProcesoCompania(pk.Codigo, compania);

            if (bean == null)
                return "N";
            if (!UBoolean.validarFlag(bean.Flagusuariosuper))
                return "N";
            if (UInteger.obtenerValorEnteroSinNulo(bean.Usuario) == 0)
                return "N";
            if (UInteger.obtenerValorEnteroSinNulo(idPersona) == 0)
                return "N";
            if (bean.Usuario == idPersona)
                return "S";
            return "N";
        }
        public String esAdministrador(String codigoProceso, Int32? idPersona, String compania)
        {
            //ConstanteProceso.PROCESO_RRHH_CAPACITACION
            SyAprobacionnivelesPk pk = null;
            return esAdministrador(pk, idPersona, compania);
        }
        public String esSuperUsuario(String codigoProceso, Int32? idPersona, String compania)
        {
            SyAprobacionnivelesPk pk = null;
            return esSuperUsuario(pk, idPersona, compania);
        }

        public int? generarCodigo()
        {
            Int32? retorno = 0;
            _reader = this.listarPorSentenciaSQL("select isnull(max(codigo), 0)+1 from SY_APROBACIONNIVELES", null);
            while (_reader.Read())
            {
                DtoTabla bean = new DtoTabla();
                if (!_reader.IsDBNull(0))
                    retorno = _reader.GetInt32(0);
            }
            this.dispose();
            return retorno;
        }

        public SyAprobacionniveles obtenerPorCodigoProceso(Int32? codigoProceso)
        {
            IQueryable<SyAprobacionniveles> query = this.getCriteria();
            query = query.Where(p => p.Codigoproceso == codigoProceso);
            List<SyAprobacionniveles> lst = query.ToList();
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public SyAprobacionniveles obtenerPorCodigoProcesoCompania(Int32? codigoProceso, String compania)
        {
            List<SyAprobacionniveles> lst = obtenerPorCodigoProcesoCompaniaList(codigoProceso, compania);
            if (lst.Count == 1)
                return lst[0];
            return null;
        }

        public List<SyAprobacionniveles> obtenerPorCodigoProcesoCompaniaList(Int32? codigoProceso, String compania)
        {
            IQueryable<SyAprobacionniveles> query = this.getCriteria();
            query = query.Where(p => p.Codigoproceso == codigoProceso);
            query = query.Where(p => p.CompanyOwner == compania);
            List<SyAprobacionniveles> lst = query.ToList();
            return lst;
        }

        public List<SyAprobacionniveles> listar(FiltroNivelAprobacion filtro)
        {
            IQueryable<SyAprobacionniveles> query = this.getCriteria();
            if (!UString.estaVacio(filtro.IdAplicacion))
                query = query.Where(p => p.Aplicacion == filtro.IdAplicacion);
            if (!UString.estaVacio(filtro.Estado))
                query = query.Where(p => p.Estado == filtro.Estado);
            if (!UString.estaVacio(filtro.Compania))
                query = query.Where(p => p.CompanyOwner == filtro.Compania);
            query = query.OrderBy(p => p.Aplicacion);
            return query.ToList();
        }

        public List<DtoTabla> listarAplicacionPorUsuario(string idUsuario)
        {
            List<DtoTabla> lst = new List<DtoTabla>();
            lst.Add(new DtoTabla("HR", "Recursos Humanos"));
            lst.Add(new DtoTabla("PR", "Planillas y RRHH."));
            lst.Add(new DtoTabla("AS", "Asistencia"));
            return lst;
        }

        public HrDepartamento obtenerDepartamento(string idDepartamento)
        {
            String sentenciaSql;
            HrDepartamento bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_departamento", ConstanteUtil.TipoDato.String, idDepartamento));

            sentenciaSql = "SELECT cast(per.Descripcion as varchar) as 'nombre' ";
            sentenciaSql = sentenciaSql + " FROM HR_Departamento per";
            sentenciaSql = sentenciaSql + " WHERE per.Departamento = :p_id_departamento";
            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                bean = new HrDepartamento();
                if (!_reader.IsDBNull(0))
                    bean.Descripcion = _reader.GetString(0);
            }
            this.dispose();
            return bean;
        }

        public DtoEmpleado obtenerEmpleado(int? idPersona, string compania)
        {
            String sentenciaSql;
            DtoEmpleado bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, idPersona));
            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, compania));

            sentenciaSql = "select	cast(p.NombreCompleto as varchar) as 'nombreCompleto', ";
            sentenciaSql = sentenciaSql + " e.correointerno as 'correoInterno',";
            sentenciaSql = sentenciaSql + " e.UnidadOperativa as 'unidadOperativa',";
            sentenciaSql = sentenciaSql + " e.JefeResponsable as 'jefeResponsable',";
            sentenciaSql = sentenciaSql + " e.DepartamentoOperacional as 'departamentoOperacional',";
            sentenciaSql = sentenciaSql + " e.centrocostos as 'centroCostos',";
            sentenciaSql = sentenciaSql + " e.DeptoOrganizacion as 'deptoOrganizacion'";
            sentenciaSql = sentenciaSql + " from PersonaMast p inner join empleadomast e";
            sentenciaSql = sentenciaSql + " on p.persona=e.Empleado";
            sentenciaSql = sentenciaSql + " where e.empleado = :p_id_persona and e.CompaniaSocio = :p_compania";

            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleado();
                if (!_reader.IsDBNull(0))
                    bean.NombreCompleto = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CorreoInterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.UnidadOperativa = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.JefeResponsable = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.DepartamentoOperacional = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.CentroCostos = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.DeptoOrganizacion = _reader.GetString(6);

                bean.Compania = compania;
            }
            this.dispose();
            return bean;
        }

        public List<DtoEmpleado> obtenerEmpleados(int? idPersona)
        {
            String sentenciaSql;
            DtoEmpleado bean = null;
            List<DtoEmpleado> beans = new List<DtoEmpleado>(); ;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, idPersona));

            sentenciaSql = "select	cast(p.NombreCompleto as varchar) as 'nombreCompleto', ";
            sentenciaSql = sentenciaSql + " e.correointerno as 'correoInterno',";
            sentenciaSql = sentenciaSql + " e.UnidadOperativa as 'unidadOperativa',";
            sentenciaSql = sentenciaSql + " e.JefeResponsable as 'jefeResponsable',";
            sentenciaSql = sentenciaSql + " e.DepartamentoOperacional as 'departamentoOperacional',";
            sentenciaSql = sentenciaSql + " e.centrocostos as 'centroCostos',";
            sentenciaSql = sentenciaSql + " e.DeptoOrganizacion as 'deptoOrganizacion', e.companiasocio, e.empleado ";
            sentenciaSql = sentenciaSql + " from PersonaMast p inner join empleadomast e";
            sentenciaSql = sentenciaSql + " on p.persona=e.Empleado";
            sentenciaSql = sentenciaSql + " where e.empleado = :p_id_persona";

            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleado();
                if (!_reader.IsDBNull(0))
                    bean.NombreCompleto = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CorreoInterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.UnidadOperativa = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.JefeResponsable = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.DepartamentoOperacional = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.CentroCostos = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.DeptoOrganizacion = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Compania = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.empleado = _reader.GetInt32(8);

                beans.Add(bean);
            }
            this.dispose();
            return beans;
        }


        public DtoEmpleado obtenerEmpleado(int? idPersona)
        {
            String sentenciaSql;
            DtoEmpleado bean = null;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_id_persona", ConstanteUtil.TipoDato.Integer, idPersona));

            sentenciaSql = "select	cast(p.NombreCompleto as varchar) as 'nombreCompleto', ";
            sentenciaSql = sentenciaSql + " e.correointerno as 'correoInterno',";
            sentenciaSql = sentenciaSql + " e.UnidadOperativa as 'unidadOperativa',";
            sentenciaSql = sentenciaSql + " e.JefeResponsable as 'jefeResponsable',";
            sentenciaSql = sentenciaSql + " e.DepartamentoOperacional as 'departamentoOperacional',";
            sentenciaSql = sentenciaSql + " e.centrocostos as 'centroCostos',";
            sentenciaSql = sentenciaSql + " e.DeptoOrganizacion as 'deptoOrganizacion'";
            sentenciaSql = sentenciaSql + " from PersonaMast p inner join empleadomast e";
            sentenciaSql = sentenciaSql + " on p.persona=e.Empleado";
            sentenciaSql = sentenciaSql + " where e.empleado = :p_id_persona";
            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);
            while (_reader.Read())
            {
                bean = new DtoEmpleado();
                if (!_reader.IsDBNull(0))
                    bean.NombreCompleto = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CorreoInterno = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.UnidadOperativa = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.JefeResponsable = _reader.GetInt32(3);
                if (!_reader.IsDBNull(4))
                    bean.DepartamentoOperacional = _reader.GetString(4);
                if (!_reader.IsDBNull(5))
                    bean.CentroCostos = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.DeptoOrganizacion = _reader.GetString(6);
            }
            this.dispose();
            return bean;
        }

        public int? obtenerNroNiveles(SyAprobacionnivelesPk pk)
        {
            SyAprobacionniveles bean = this.obtenerPorId(pk);
            if (bean == null)
                return null;
            return bean.Nroniveles;
        }

        public SyAprobacionniveles obtenerPorId(SyAprobacionnivelesPk pk)
        {
            return base.obtenerPorId(pk.obtenerArreglo());
        }

        public SyAprobacionniveles obtenerPorIdCompleto(SyAprobacionnivelesPk syAprobacionnivelesPk)
        {
            SyAprobacionniveles bean = this.obtenerPorId(syAprobacionnivelesPk.obtenerArreglo());
            List<SyAprobacionnivelesDet> lst = syAprobacionnivelesDetDao.listarPorNivelAprobacion(syAprobacionnivelesPk);

            var parametro = parametrosmastDao.obtenerPorId(new ParametrosmastPk() { Aplicacioncodigo = "SY", Companiacodigo = "999999", Parametroclave = "TIPOBUSAPR" });
            if (parametro == null)
            {
                parametro = new Parametrosmast()
                {
                    Texto = ConstanteAprobacion.TIPO_BUSQUEDA_DEPARTAMENTO,
                    Explicacion = "Departamento"
                };
            }

            parametro.Texto = parametro.Texto.Trim();

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Usuario.Value == 99999)
                {

                    lst[i].AuxNombrePersona = "Aprobación Jefe Directo";
                }
                else
                {
                    //buscar el nonmbre de la persona sin importar la compania
                    string nombreSinCompania = personamastDao.obtenerNombrePorPk(new core.dominio.PersonamastPk(lst[i].Usuario.Value));

                    lst[i].AuxNombrePersona = nombreSinCompania;
                }

                var codigoArea = DtoTabla.obtenerLista(UString.split(lst[i].Area, ","));
                lst[i].ListaAreas = new List<HrDepartamento>();

                if (codigoArea != null)
                {
                    foreach (var item in codigoArea)
                    {

                        HrDepartamento pre = hrDepartamentoDao.obtenerPorId(item.Codigo);
                        
                        if (pre != null)
                        {
                            lst[i].ListaAreas.Add(pre);
                        }

                    }
                }


                var pre2 = new List<DtoTabla>();

                //si el valor es % obtenemos todos los codigo
                if (lst[i].Valor==null)
                {
                    lst[i].Valor = "%";
                }
                if (lst[i].Valor != null)
                {
                    if (lst[i].Valor == "%")
                    {
                        List<HrTipocontrato> lstContratoTemp;
                        if (bean.Codigo == 1)
                        {
                            FiltroMiscelaneosDetalle filtro = new FiltroMiscelaneosDetalle();
                            filtro.CodigoCompania = "999999";
                            filtro.CodigoAplicacion = "HR";
                            filtro.CodigoTabla = "MOTIVOREQ";
                            filtro.Estado = "A";
                            List<MaMiscelaneosdetalle> ls = maMiscelaneosdetalleDao.listar(filtro);
                            lstContratoTemp = new List<HrTipocontrato>();

                            foreach (var item in ls)
                            {
                                lstContratoTemp.Add(new HrTipocontrato()
                                {
                                    Tipocontrato = item.Codigoelemento,
                                    Descripcion = item.Descripcionlocal
                                });
                            }

                        }
                        else
                        {
                            lstContratoTemp = hrTipocontratoDao.listarActivos();
                        }


                        if (lstContratoTemp != null)
                        {
                            string valores = "";
                            foreach (var item in lstContratoTemp)
                            {
                                valores = valores + item.Tipocontrato + ",";
                            }

                            pre2 = DtoTabla.obtenerLista(UString.split(valores, ","));
                        }

                    }
                    else
                    {
                        pre2 = DtoTabla.obtenerLista(UString.split(lst[i].Valor, ","));
                    }
                }


                lst[i].ListaValores = new List<HrTipocontrato>();

                if (pre2 != null)
                {
                    foreach (var item in pre2)
                    {
                        lst[i].ListaValores.Add(new HrTipocontrato()
                        {
                            Tipocontrato = item.Codigo,
                            Descripcion = item.Nombre
                        });
                    }
                }

                lst[i].ListaCorreos = DtoTabla.obtenerLista(UString.split(lst[i].Correoelectronico, ";"));
                for (int iCorreo = 0; iCorreo < lst[i].ListaCorreos.Count; iCorreo++)
                {
                    String nombre = lst[i].ListaCorreos[iCorreo].Codigo;
                    lst[i].ListaCorreos[iCorreo].Nombre = nombre;
                }

                if (lst[i].ListaAreas != null)
                {
                    for (int iAreas = 0; iAreas < lst[i].ListaAreas.Count; iAreas++)
                    {
                        obtenerNombrePorAgrupacion(parametro.Texto, lst[i].ListaAreas[iAreas], lst[i].AuxSecuencia);
                    }
                }


                for (int iContratos = 0; iContratos < lst[i].ListaValores.Count; iContratos++)
                {
                    if (bean.Codigoproceso == 1)
                    {
                        MaMiscelaneosdetallePk pk = new MaMiscelaneosdetallePk();
                        pk.Aplicacioncodigo = "HR";
                        pk.Codigoelemento = lst[i].ListaValores[iContratos].Tipocontrato;
                        pk.Codigotabla = "MOTIVOREQ";
                        pk.Compania = "999999";
                        var f = maMiscelaneosdetalleDao.obtenerPorId(pk.obtenerArreglo());
                        lst[i].ListaValores[iContratos].Descripcion = f == null ? "" : f.Descripcionlocal;
                    }
                    else
                    {
                        lst[i].ListaValores[iContratos].Descripcion = hrTipocontratoDao.obtenerPorId(lst[i].ListaValores[iContratos].Tipocontrato).Descripcion;
                    }
                }
            }

            if (bean != null)
            {
                bean.ListaCorreos = DtoTabla.obtenerLista(UString.split(bean.Correoelectronico, ";"));
                for (int iCorreo = 0; iCorreo < bean.ListaCorreos.Count; iCorreo++)
                {
                    String nombre = bean.ListaCorreos[iCorreo].Codigo;
                    bean.ListaCorreos[iCorreo].Nombre = nombre;
                }

                bean.ListaCorreosConfirmacion = DtoTabla.obtenerLista(UString.split(bean.Nivel0Correoelectronico, ";"));
                for (int iCorreo = 0; iCorreo < bean.ListaCorreosConfirmacion.Count; iCorreo++)
                {
                    String nombre = bean.ListaCorreosConfirmacion[iCorreo].Codigo;
                    bean.ListaCorreosConfirmacion[iCorreo].Nombre = nombre;
                }

                bean.ListaNiveles = this.obtenerNiveles(lst);

                return bean;
            }

            return null;
        }

        private void obtenerNombrePorAgrupacion(String agrupacion, HrDepartamento hrDepartamento, int sec)
        {
            if (ConstanteAprobacion.TIPO_BUSQUEDA_CENTROCOSTO.Equals(agrupacion))
            {
                AcCostcentermst departamento = acCostcentermstDao.obtenerPorId(new AcCostcentermstPk() { Costcenter = hrDepartamento.Departamento }.obtenerArreglo());

                hrDepartamento.secuencia = sec;
                if (departamento != null)
                {
                    hrDepartamento.Descripcion = departamento.Localname;
                }


            }
            else if (ConstanteAprobacion.TIPO_BUSQUEDA_UNIDAD_OPERATIVA.Equals(agrupacion))
            {
                HrUnidadoperativa departamento = hrUnidadoperativaDao.obtenerPorId(new HrUnidadoperativaPk() { Unidadoperativa = hrDepartamento.Departamento }.obtenerArreglo());

                hrDepartamento.secuencia = sec;
                if (departamento != null)
                {
                    hrDepartamento.Descripcion = departamento.Descripcion;
                }
            }
            else if (ConstanteAprobacion.TIPO_BUSQUEDA_DEPARTAMENTO_ORGANIZACION.Equals(agrupacion))
            {
                Departmentmst departamento = departmentmstDao.obtenerPorId(new DepartmentmstPk() { Department = hrDepartamento.Departamento }.obtenerArreglo());

                hrDepartamento.secuencia = sec;
                if (departamento != null)
                {
                    hrDepartamento.Descripcion = departamento.Description;
                }
            }
            else
            {
                HrDepartamento departamento = this.obtenerDepartamento(hrDepartamento.Departamento);

                hrDepartamento.secuencia = sec;
                if (departamento != null)
                {
                    hrDepartamento.Descripcion = departamento.Descripcion;
                }

            }




        }

        private List<DtoNivelDetalle> obtenerNiveles(List<SyAprobacionnivelesDet> listaDetalle)
        {
            List<DtoNivelDetalle> listaNiveles = new List<DtoNivelDetalle>();

            if (listaDetalle == null)
                return listaNiveles;

            foreach (SyAprobacionnivelesDet detalle in listaDetalle)
            {
                Boolean flgEncontrado = false;
                foreach (DtoNivelDetalle nivel in listaNiveles)
                {
                    if (nivel.codigo.Equals(detalle.Nivel))
                    {
                        flgEncontrado = true;
                    }
                }
                if (!flgEncontrado)
                {
                    listaNiveles.Add(new DtoNivelDetalle(detalle.Nivel.Value, "NIVEL " + Convert.ToString(detalle.Nivel)));
                }
            }

            for (int i = 0; i < listaNiveles.Count; i++)
            {
                if (listaNiveles[i].ListaDetalle == null)
                    listaNiveles[i].ListaDetalle = new List<SyAprobacionnivelesDet>();

                foreach (SyAprobacionnivelesDet detalle in listaDetalle)
                {
                    if (detalle.Nivel == listaNiveles[i].codigo)
                    {
                        listaNiveles[i].ListaDetalle.Add(detalle);
                    }
                }
            }

            return listaNiveles;
        }

        public List<SyAprobacionniveles> listarPorAplicacionActivos(string idAplicacion)
        {
            FiltroNivelAprobacion filtro = new FiltroNivelAprobacion();
            filtro.IdAplicacion = idAplicacion;
            filtro.Estado = "A";
            return listar(filtro);
        }

        public SyAprobacionniveles registrar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles)
        {
            DateTime ahora = DateTime.Today;
            syAprobacionniveles.Codigo = this.generarCodigo();
            syAprobacionniveles.Ultimafechamodif = ahora;
            syAprobacionniveles.Ultimousuario = usuarioActual.UsuarioCodigo;
            syAprobacionniveles.CompanyOwner = usuarioActual.CompaniaSocioCodigo;
            this.registrar(syAprobacionniveles);

            if (syAprobacionniveles.ListaNiveles != null)
            {
                foreach (DtoNivelDetalle nivel in syAprobacionniveles.ListaNiveles)
                {
                    foreach (SyAprobacionnivelesDet det in nivel.ListaDetalle)
                    {
                        det.CompanyOwner = usuarioActual.CompaniaSocioCodigo;
                        det.Codigo = syAprobacionniveles.Codigo;
                        det.Flagunidadoperativa = UString.estaVacio(det.Flagarea) ? "" : det.Flagarea;
                        this.syAprobacionnivelesDetDao.registrar(det);
                    }
                }
            }


            return syAprobacionniveles;
        }

        public void actualizar(UsuarioActual usuarioActual, SyAprobacionniveles syAprobacionniveles)
        {
            DateTime ahora = DateTime.Today;
            syAprobacionniveles.Ultimafechamodif = ahora;
            syAprobacionniveles.Ultimousuario = usuarioActual.UsuarioCodigo;

            if (syAprobacionniveles.ListaNiveles!=null)
            {
                syAprobacionniveles.Nroniveles = syAprobacionniveles.ListaNiveles.Count;
            }

            this.actualizar(syAprobacionniveles);

            // elimino lo anterior
            SyAprobacionnivelesPk pk = new SyAprobacionnivelesPk();
            pk.Codigo = syAprobacionniveles.Codigo;
            pk.CompanyOwner = syAprobacionniveles.CompanyOwner;


            List<SyAprobacionnivelesDet> lstAnterior = syAprobacionnivelesDetDao
                    .listarPorNivelAprobacion(pk);
            if (lstAnterior != null)
            {
                foreach (SyAprobacionnivelesDet det in lstAnterior)
                {
                    this.syAprobacionnivelesDetDao.eliminar(det);
                }
            }

            //añado lo nuevo
            if (syAprobacionniveles.ListaNiveles != null)
            {
                foreach (DtoNivelDetalle nivel in syAprobacionniveles.ListaNiveles)
                {
                    foreach (SyAprobacionnivelesDet det in nivel.ListaDetalle)
                    {
                        det.Codigo = syAprobacionniveles.Codigo;
                        det.CompanyOwner = syAprobacionniveles.CompanyOwner;
                        det.Flagunidadoperativa = det.Flagarea;
                        this.syAprobacionnivelesDetDao.registrar(det);
                    }
                }
            }
        }

        public void eliminar(UsuarioActual usuarioActual, int? codigo, string compania)
        {
            SyAprobacionniveles nivel = obtenerPorIdCompleto(new SyAprobacionnivelesPk(codigo, compania));
            if (nivel == null)
            {
                return;
            }

            if (nivel.ListaNiveles != null)
            {
                foreach (DtoNivelDetalle n in nivel.ListaNiveles)
                {
                    foreach (SyAprobacionnivelesDet det in n.ListaDetalle)
                    {
                        this.syAprobacionnivelesDetDao.eliminar(det);
                    }
                }
            }


            this.eliminar(nivel);
        }

        public int? obtenerNroNivelesPorProceso(int? procesoAprobar)
        {

            IQueryable<SyAprobacionniveles> query = this.getCriteria();
            query = query.Where(p => p.Codigoproceso == procesoAprobar);
            List<SyAprobacionniveles> lst = query.ToList();

            if (lst.Count > 0)
            {
                return lst[0].Nroniveles;
            }
            return 0;
        }

        public void validarConfiguracionPorCompania(string proceso, string companiaSocioCodigo)
        {
            String sentenciaSql;
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();

            parametros.Add(new ParametroPersistenciaGenerico("p_compania", ConstanteUtil.TipoDato.String, companiaSocioCodigo));
            parametros.Add(new ParametroPersistenciaGenerico("p_proceso", ConstanteUtil.TipoDato.String, proceso));

            sentenciaSql = "select ISNULL(AN.FlagUsuarioSuper, 'N') ES_SUPER, isnull(Usuario, 0) SUPER, ISNULL(FLAGUSUARIOADMINISTRADOR, 'N') ES_ADMIN, isnull(USUARIOADMINISTRADOR, 0)ADMIN, (SELECT COUNT(1) FROM SY_AprobacionNiveles_Det WHERE CODIGO = AN.CODIGO) NIVELES from sy_Aprobacionniveles AN WHERE CodigoProceso IN( ";
            sentenciaSql = sentenciaSql + " select CodigoProceso from sy_Aprobacionproceso WHERE  ProcesoInterno IN ( ";
            sentenciaSql = sentenciaSql + " select DISTINCT CodigoElemento from MA_MiscelaneosDetalle where CodigoTabla = 'PROINTAPRO' and ValorCodigo2 = :p_proceso)) ";
            sentenciaSql = sentenciaSql + " AND companyowner = :p_compania ";
            _reader = this.listarPorSentenciaSQL(sentenciaSql, parametros);

            var mensaje = "El proceso de ";

            if (!_reader.HasRows)
            {
                if (ConstanteProceso.PROCESO_PLANILLA_PRESTAMO.Equals(proceso))
                {
                    mensaje = mensaje + ConstanteProceso.PROCESO_NOMBRE_PLANILLA_PRESTAMO;
                }
                else if (ConstanteProceso.PROCESO_PLANILLA_VACACION_WEB.Equals(proceso))
                {
                    mensaje = mensaje + ConstanteProceso.PROCESO_NOMBRE_PLANILLA_VACACION_WEB;
                }
                else if (ConstanteProceso.PROCESO_RRHH_FICHA_PERSONAL.Equals(proceso))
                {
                    mensaje = mensaje + ConstanteProceso.PROCESO_NOMBRE_RRHH_FICHA_PERSONAL;
                }

                this.dispose();

                throw new Exception(mensaje + " no tiene niveles configurados.");
            }


            if (_reader.Read())
            {
                var esSuper = "S".Equals(_reader.GetString(0));
                var usuarioSuper = _reader.GetInt32(1);

                var esAdmin = "S".Equals(_reader.GetString(2));
                var usuarioAdmin = _reader.GetInt32(3);

                var nroNiveles = _reader.GetInt32(4);

                var cumpleUsuarioSuper = esSuper && !UInteger.esCeroOrNulo(usuarioSuper);
                var cumpleUsuarioAdmin = esAdmin && !UInteger.esCeroOrNulo(usuarioAdmin);
                var cumpleNiveles = !UInteger.esCeroOrNulo(nroNiveles);

                if (!cumpleUsuarioSuper && !cumpleUsuarioAdmin && !cumpleNiveles)
                {
                    this.dispose();
                    throw new Exception("No se tiene configuración del proceso");
                }
            }


            this.dispose();
        }


    }
}
