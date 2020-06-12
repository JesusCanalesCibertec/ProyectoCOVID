using System;
using System.Text;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.rrhh.dominio.dto;
using System.Collections.Generic;
using net.royal.spring.programasocial.dominio.dtos;
using net.royal.spring.framework.core.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.programasocial.dominio.Constantes;
using net.royal.spring.sistema.dao;
using net.royal.spring.sistema.dominio;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsCapacidadDaoImpl : GenericoDaoImpl<PsCapacidad>, PsCapacidadDao
    {
        private IServiceProvider servicioProveedor;
        private PsCapacidadCursoDao psCapacidadCursoDao;
        private PsCapacidadTallerDao psCapacidadTallerDao;
        private PsBeneficiarioDao psBeneficiarioDao;
        private ParametrosmastDao parametrosmastDao;


        public PsCapacidadDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pscapacidad")
        {
            servicioProveedor = _servicioProveedor;
            psCapacidadCursoDao = servicioProveedor.GetService<PsCapacidadCursoDao>();
            psCapacidadTallerDao = servicioProveedor.GetService<PsCapacidadTallerDao>();
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
            parametrosmastDao = servicioProveedor.GetService<ParametrosmastDao>();
        }

        public DtoCapacidad obtenerPorId(PsCapacidadPk id)
        {

            PsCapacidad bean = new PsCapacidad();
            PsCapacidadTaller psCapacidadTaller = new PsCapacidadTaller();
            PsCapacidadCurso psCapacidadCurso = new PsCapacidadCurso();

            DtoCapacidad psCapacidad = new DtoCapacidad();

            bean = this.obtenerPorId(id.obtenerArreglo());

            psCapacidad.IdBeneficiario = bean.IdBeneficiario;
            psCapacidad.IdCapacidad = bean.IdCapacidad;
            psCapacidad.IdInstitucion = bean.IdInstitucion;
            psCapacidad.IdRiesgoCaidaDetalle = bean.IdRiesgoCaidaDetalle;
            psCapacidad.AnioRetraso = bean.AnioRetraso;
            psCapacidad.Barthel1 = bean.Barthel1;
            psCapacidad.Barthel10 = bean.Barthel10;
            psCapacidad.Barthel2 = bean.Barthel2;
            psCapacidad.Barthel3 = bean.Barthel3;
            psCapacidad.Barthel4 = bean.Barthel4;
            psCapacidad.Barthel5 = bean.Barthel4;
            psCapacidad.Barthel6 = bean.Barthel4;
            psCapacidad.Barthel7 = bean.Barthel4;
            psCapacidad.Barthel8 = bean.Barthel4;
            psCapacidad.Barthel9 = bean.Barthel4;
            psCapacidad.ComentarioCapacidad = bean.ComentarioCapacidad;
            psCapacidad.ComentarioRendimiento = bean.ComentarioRendimiento;
            psCapacidad.ComentarioRetraso = bean.ComentarioRetraso;
            psCapacidad.ComentarioTalleres = bean.ComentarioTalleres;
            psCapacidad.FechaInforme = bean.FechaInforme;
            psCapacidad.IdEvaluarOcupacional = bean.IdEvaluarOcupacional;
            psCapacidad.IdGradoEducativo = bean.IdGradoEducativo;
            psCapacidad.IdHabilidadOcupacional = bean.IdHabilidadOcupacional;
            psCapacidad.IdNivel = bean.IdNivel;
            psCapacidad.IdPeriodo = bean.IdPeriodo;
            psCapacidad.IdRiesgoCaida = bean.IdRiesgoCaida;
            psCapacidad.IdTipoComunicacion = bean.IdTipoComunicacion;
            psCapacidad.IdTipoInstitucion = bean.IdTipoInstitucion;
            psCapacidad.Katz1 = bean.Katz1;
            psCapacidad.Katz2 = bean.Katz2;
            psCapacidad.Katz3 = bean.Katz3;
            psCapacidad.Katz4 = bean.Katz4;
            psCapacidad.Katz5 = bean.Katz5;
            psCapacidad.Katz6 = bean.Katz6;
            psCapacidad.IdIletrado = bean.IdIletrado;
            psCapacidad.IdSaanee = bean.IdSaanee;
            psCapacidad.GradoDependenciaKatz = bean.GradoDependenciaKatz;
            psCapacidad.GradoDependenciaBarthel = bean.GradoDependenciaBarthel;
            psCapacidad.PorcentajeGradoBarthel = bean.PorcentajeGradoBarthel;
            psCapacidad.PorcentajeGradoKatz = bean.PorcentajeGradoKatz;
            psCapacidad.OcupacionAnterior = bean.OcupacionAnterior;
            return psCapacidad;


        }



        private void guardarCurso(DtoCapacidad bean)
        {
            PsCapacidadCurso psCapacidadCurso = new PsCapacidadCurso();
            psCapacidadCurso.IdBeneficiario = bean.IdBeneficiario;
            psCapacidadCurso.IdCapacidad = bean.IdCapacidad;
            psCapacidadCurso.IdCurso = bean.IdCurso;
            psCapacidadCurso.IdInstitucion = bean.IdInstitucion;
            psCapacidadCurso.IdNota = bean.IdNotaCurso;
            psCapacidadCurso.CreacionFecha = DateTime.Now;
            psCapacidadCurso.CreacionTerminal = "10.10.2.109";
            psCapacidadCurso.CreacionUsuario = bean.UsurioCodigo;

            psCapacidadCurso.ModificacionFecha = DateTime.Now;
            psCapacidadCurso.ModificacionTerminal = "10.10.2.109";
            psCapacidadCurso.ModificacionUsuario = bean.UsurioCodigo;
            psCapacidadCursoDao.registrar(psCapacidadCurso);
        }

        private void guardarTaller(DtoCapacidad bean)
        {
            PsCapacidadTaller psCapacidadTaller = new PsCapacidadTaller();
            psCapacidadTaller.IdBeneficiario = bean.IdBeneficiario;
            psCapacidadTaller.IdCapacidad = bean.IdCapacidad;
            psCapacidadTaller.IdTaller = bean.IdTaller;
            psCapacidadTaller.IdInstitucion = bean.IdInstitucion;
            psCapacidadTaller.IdNota = bean.IdNotaTaller;
            psCapacidadTaller.Cantidad = bean.CantidadTaller;
            psCapacidadTaller.CreacionFecha = DateTime.Now;
            psCapacidadTaller.CreacionTerminal = "10.10.2.109";
            psCapacidadTaller.CreacionUsuario = bean.UsurioCodigo;

            psCapacidadTaller.ModificacionFecha = DateTime.Now;
            psCapacidadTaller.ModificacionTerminal = "10.10.2.109";
            psCapacidadTaller.ModificacionUsuario = bean.UsurioCodigo;

            psCapacidadTallerDao.registrar(psCapacidadTaller);
        }

        private List<MensajeUsuario> validar(DtoCapacidad bean)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            if (bean.FechaInforme == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Fecha Informe es requerido"));
            }

            if (bean.EvaluadoBoolean)
            {
                if (String.IsNullOrEmpty(bean.ComentarioTalleres))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el Comentario"));
                }
                return lstRetorno;
            }

            if (bean.IdOrigen != "EVA")
            {
                if (bean.ValidarNinos)
                {
                    /*if (String.IsNullOrEmpty(bean.GradoDependenciaBarthel)) {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Dependencia Barthel es requerido"));
                    }*/

                    if (String.IsNullOrEmpty(bean.IdTipoInstitucion))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Tipo Institución es requerido"));
                    }

                    if (!String.IsNullOrEmpty(bean.IdTipoInstitucion))
                    {
                        if (bean.IdTipoInstitucion != "NIN")
                        {
                            if (String.IsNullOrEmpty(bean.IdNivel))
                            {
                                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Es Obligatorio ingresar el Nivel"));
                            }
                            else
                            {
                                if (bean.IdNivel != "APRE")
                                {
                                    if (String.IsNullOrEmpty(bean.IdGradoEducativo))
                                    {
                                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Es Obligatorio ingresar el Grado Educativo"));
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (String.IsNullOrEmpty(bean.GradoDependenciaKatz))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Dependencia Katz es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.IdRiesgoCaida))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Riesgo Caida es requerido"));
                    }

                }


            }
            else
            {
                if (bean.ValidarNinos)
                {
                    /*if (String.IsNullOrEmpty(bean.GradoDependenciaBarthel)) {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Dependencia Barthel es requerido"));
                    }*/

                    if (String.IsNullOrEmpty(bean.NotaPersonalSocial))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Nota Personal Social es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.NotaCienciaAmbiente))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Nota Ciencia y Ambiente  es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.NotaComunicacion))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Nota Comunicación es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.NotaComprensionLectora))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Nota Compresión Lectora es requerido"));
                    }


                    if (String.IsNullOrEmpty(bean.IdTipoInstitucion))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Tipo Institución es requerido"));
                    }
                    else
                    {
                        if (bean.IdTipoInstitucion != "NIN")
                        {
                            if (String.IsNullOrEmpty(bean.IdNivel))
                            {
                                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Es Obligatorio ingresar el Nivel"));
                            }
                            else
                            {
                                if (bean.IdNivel != "APRE")
                                {
                                    if (String.IsNullOrEmpty(bean.IdGradoEducativo))
                                    {
                                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Es Obligatorio ingresar el Grado Educativo"));
                                    }
                                }

                            }
                        }
                    }

                    if (String.IsNullOrEmpty(bean.NotaLogicoMatematico))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Nota Lógico Matemático es requerido"));
                    }
                }

                else
                {
                    if (String.IsNullOrEmpty(bean.GradoDependenciaKatz))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Dependencia Katz es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.IdRiesgoCaida))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Riesgo Caida es requerido"));
                    }


                    if (String.IsNullOrEmpty(bean.IdHabilidadOcupacional))
                    {

                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Habilidades Ocupacionales es requerido"));
                    }

                    if (String.IsNullOrEmpty(bean.IdEvaluarOcupacional))
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Evaluado en Hab. Ocupacional es requerido"));
                    }
                }
            }

            return lstRetorno;
        }

        public PsCapacidad coreInsertar(UsuarioActual usuarioActual, DtoCapacidad bean, String estado)
        {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);


            PsCapacidad psCapacidad = new PsCapacidad();
            psCapacidad.IdRiesgoCaidaDetalle = bean.IdRiesgoCaidaDetalle;
            psCapacidad.IdIletrado = bean.IdIletrado;
            psCapacidad.IdOrigen = bean.IdOrigen;
            psCapacidad.IdSaanee = bean.IdSaanee;
            psCapacidad.OcupacionAnterior = bean.OcupacionAnterior;
            psCapacidad.AnioRetraso = bean.AnioRetraso;
            psCapacidad.Barthel1 = bean.Barthel1;
            psCapacidad.Barthel10 = bean.Barthel10;
            psCapacidad.Barthel2 = bean.Barthel2;
            psCapacidad.Barthel3 = bean.Barthel3;
            psCapacidad.Barthel4 = bean.Barthel4;
            psCapacidad.Barthel5 = bean.Barthel4;
            psCapacidad.Barthel6 = bean.Barthel4;
            psCapacidad.Barthel7 = bean.Barthel4;
            psCapacidad.Barthel8 = bean.Barthel4;
            psCapacidad.Barthel9 = bean.Barthel4;
            psCapacidad.ComentarioCapacidad = bean.ComentarioCapacidad;
            psCapacidad.ComentarioRendimiento = bean.ComentarioRendimiento;
            psCapacidad.ComentarioRetraso = bean.ComentarioRetraso;
            psCapacidad.ComentarioTalleres = bean.ComentarioTalleres;
            psCapacidad.FechaInforme = bean.FechaInforme;
            psCapacidad.IdBeneficiario = bean.IdBeneficiario;
            psCapacidad.IdCapacidad = this.generarCodigo();
            bean.IdCapacidad = psCapacidad.IdCapacidad;
            bean.UsurioCodigo = usuarioActual.UsuarioLogin;
            psCapacidad.IdEvaluarOcupacional = bean.IdEvaluarOcupacional;
            psCapacidad.IdGradoEducativo = bean.IdGradoEducativo;
            psCapacidad.IdHabilidadOcupacional = bean.IdHabilidadOcupacional;
            psCapacidad.IdInstitucion = bean.IdInstitucion;
            psCapacidad.IdNivel = bean.IdNivel;
            psCapacidad.IdPeriodo = bean.IdPeriodo;
            psCapacidad.IdRiesgoCaida = bean.IdRiesgoCaida;
            psCapacidad.IdTipoComunicacion = bean.IdTipoComunicacion;
            psCapacidad.IdTipoInstitucion = bean.IdTipoInstitucion;
            psCapacidad.Katz1 = bean.Katz1;
            psCapacidad.Katz2 = bean.Katz2;
            psCapacidad.Katz3 = bean.Katz3;
            psCapacidad.Katz4 = bean.Katz4;
            psCapacidad.Katz5 = bean.Katz5;
            psCapacidad.Katz6 = bean.Katz6;

            psCapacidad.GradoDependenciaKatz = bean.GradoDependenciaKatz;
            psCapacidad.GradoDependenciaBarthel = bean.GradoDependenciaBarthel;
            psCapacidad.PorcentajeGradoBarthel = bean.PorcentajeGradoBarthel;
            psCapacidad.PorcentajeGradoKatz = bean.PorcentajeGradoKatz;
            psCapacidad.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            psCapacidad.CreacionFecha = DateTime.Now;
            psCapacidad.CreacionUsuario = usuarioActual.UsuarioLogin;

            if (bean.EvaluadoBoolean)
            {
                psCapacidad.Evaluado = "S";
            }
            else
            {
                psCapacidad.Evaluado = "";
            }

            this.registrar(psCapacidad);
            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion, bean.IdBeneficiario).obtenerArreglo());
            if (bean.IdOrigen != "EVA")
            {
                psBeneficiario.IdComponenteCapacidad = psCapacidad.IdCapacidad;
                psBeneficiarioDao.actualizar(psBeneficiario);
            }
            else
            {
                if (psBeneficiario.IdComponenteCapacidad == null)
                {
                    psBeneficiario.IdComponenteCapacidad = psCapacidad.IdCapacidad;
                    psBeneficiarioDao.actualizar(psBeneficiario);
                }
            }

            //************* CURSOS ****************//
            if (!String.IsNullOrEmpty(bean.NotaLogicoMatematico))
            {
                bean.IdCurso = "MATE";
                bean.IdNotaCurso = bean.NotaLogicoMatematico;
                this.guardarCurso(bean);
            }

            if (!String.IsNullOrEmpty(bean.NotaCienciaAmbiente))
            {
                bean.IdCurso = "AMBI";
                bean.IdNotaCurso = bean.NotaCienciaAmbiente;
                this.guardarCurso(bean);
            }
            if (!String.IsNullOrEmpty(bean.NotaComprensionLectora))
            {
                bean.IdCurso = "LECT";
                bean.IdNotaCurso = bean.NotaComprensionLectora;
                this.guardarCurso(bean);
            }
            if (!String.IsNullOrEmpty(bean.NotaComunicacion))
            {
                bean.IdCurso = "COMU";
                bean.IdNotaCurso = bean.NotaComunicacion;
                this.guardarCurso(bean);
            }
            if (!String.IsNullOrEmpty(bean.NotaPersonalSocial))
            {
                bean.IdCurso = "SOCI";
                bean.IdNotaCurso = bean.NotaPersonalSocial;
                this.guardarCurso(bean);
            }

            //************* TALLERES ****************//
            DtoCapacidad Talleres = new DtoCapacidad();
            Talleres = obtenerTalleres(bean);

            int TallerArtistico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_ARTISTICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerDeportivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_DEPORTIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFormativo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FORMATIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerCognitivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_COGNITIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFisico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FISICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);


            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerArtistico))
            {
                Talleres.IdTaller = TallerArtistico;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerArtistico;
                Talleres.CantidadTaller = Talleres.CantidadTallerArtistico;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerDeportivo))
            {
                Talleres.IdTaller = TallerDeportivo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerDeportivo;
                Talleres.CantidadTaller = Talleres.CantidadTallerDeportivo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerFormativo))
            {
                Talleres.IdTaller = TallerFormativo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerFormativo;
                Talleres.CantidadTaller = Talleres.CantidadTallerFormativo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerCognitivo))
            {
                Talleres.IdTaller = TallerCognitivo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerCognitivo;
                Talleres.CantidadTaller = Talleres.CantidadTallerCognitivo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerFisico))
            {
                Talleres.IdTaller = TallerFisico;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerFisico;
                Talleres.CantidadTaller = Talleres.CantidadTallerFisico;
                this.guardarTaller(Talleres);
            }

            return psCapacidad;
        }

        public int generarCodigo()
        {
            IQueryable<PsCapacidad> query = this.getCriteria();

            List<PsCapacidad> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdCapacidad));
            }

            return var + 1;
        }

        public PsCapacidad coreActualizar(UsuarioActual usuarioActual, DtoCapacidad bean, String estado)
        {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            PsCapacidad psCapacidad = new PsCapacidad();
            PsCapacidadTaller psCapacidadTaller = new PsCapacidadTaller();
            PsCapacidadCurso psCapacidadCurso = new PsCapacidadCurso();

            psCapacidad = this.obtenerPorId(new PsCapacidadPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad).obtenerArreglo());
            psCapacidad.AnioRetraso = bean.AnioRetraso;
            psCapacidad.Barthel1 = bean.Barthel1;
            psCapacidad.Barthel10 = bean.Barthel10;
            psCapacidad.Barthel2 = bean.Barthel2;
            psCapacidad.Barthel3 = bean.Barthel3;
            psCapacidad.Barthel4 = bean.Barthel4;
            psCapacidad.Barthel5 = bean.Barthel4;
            psCapacidad.Barthel6 = bean.Barthel4;
            psCapacidad.Barthel7 = bean.Barthel4;
            psCapacidad.Barthel8 = bean.Barthel4;
            psCapacidad.Barthel9 = bean.Barthel4;
            psCapacidad.IdRiesgoCaidaDetalle = bean.IdRiesgoCaidaDetalle;
            psCapacidad.ComentarioCapacidad = bean.ComentarioCapacidad;
            psCapacidad.ComentarioRendimiento = bean.ComentarioRendimiento;
            psCapacidad.ComentarioRetraso = bean.ComentarioRetraso;
            psCapacidad.ComentarioTalleres = bean.ComentarioTalleres;
            psCapacidad.FechaInforme = bean.FechaInforme;
            psCapacidad.IdEvaluarOcupacional = bean.IdEvaluarOcupacional;
            psCapacidad.IdGradoEducativo = bean.IdGradoEducativo;
            psCapacidad.IdHabilidadOcupacional = bean.IdHabilidadOcupacional;
            psCapacidad.IdNivel = bean.IdNivel;
            psCapacidad.IdPeriodo = bean.IdPeriodo;
            psCapacidad.IdRiesgoCaida = bean.IdRiesgoCaida;
            psCapacidad.IdTipoComunicacion = bean.IdTipoComunicacion;
            psCapacidad.IdTipoInstitucion = bean.IdTipoInstitucion;
            psCapacidad.Katz1 = bean.Katz1;
            psCapacidad.Katz2 = bean.Katz2;
            psCapacidad.Katz3 = bean.Katz3;
            psCapacidad.Katz4 = bean.Katz4;
            psCapacidad.Katz5 = bean.Katz5;
            psCapacidad.Katz6 = bean.Katz6;
            psCapacidad.IdIletrado = bean.IdIletrado;
            psCapacidad.IdSaanee = bean.IdSaanee;
            psCapacidad.GradoDependenciaKatz = bean.GradoDependenciaKatz;
            psCapacidad.GradoDependenciaBarthel = bean.GradoDependenciaBarthel;
            psCapacidad.PorcentajeGradoBarthel = bean.PorcentajeGradoBarthel;
            psCapacidad.PorcentajeGradoKatz = bean.PorcentajeGradoKatz;
            psCapacidad.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            psCapacidad.ModificacionFecha = DateTime.Now;
            psCapacidad.ModificacionUsuario = usuarioActual.UsuarioLogin;

            if (bean.EvaluadoBoolean)
            {
                bean.Evaluado = "S";
            }
            else
            {
                bean.Evaluado = "";
            }

            this.actualizar(psCapacidad);

            //************* CURSOS ****************//
            if (!String.IsNullOrEmpty(bean.NotaLogicoMatematico))
            {
                psCapacidadCurso = psCapacidadCursoDao.obtenerPorId(new PsCapacidadCursoPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad, bean.IdCursoMatematica).obtenerArreglo());

                if (psCapacidadCurso != null)
                {
                    psCapacidadCurso.IdNota = bean.NotaLogicoMatematico;
                    psCapacidadCursoDao.actualizar(psCapacidadCurso);
                }
                else
                {
                    this.guardarCurso(bean);
                }
            }

            if (!String.IsNullOrEmpty(bean.NotaCienciaAmbiente))
            {
                psCapacidadCurso = psCapacidadCursoDao.obtenerPorId(new PsCapacidadCursoPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad, bean.IdCursoAmbiente).obtenerArreglo());

                if (psCapacidadCurso != null)
                {
                    psCapacidadCurso.IdNota = bean.NotaCienciaAmbiente;
                    psCapacidadCursoDao.actualizar(psCapacidadCurso);
                }
                else
                {
                    this.guardarCurso(bean);
                }
            }
            if (!String.IsNullOrEmpty(bean.NotaComprensionLectora))
            {

                psCapacidadCurso = psCapacidadCursoDao.obtenerPorId(new PsCapacidadCursoPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad, bean.IdCursoCompresion).obtenerArreglo());

                if (psCapacidadCurso != null)
                {
                    psCapacidadCurso.IdNota = bean.NotaComprensionLectora;
                    psCapacidadCursoDao.actualizar(psCapacidadCurso);
                }
                else
                {
                    this.guardarCurso(bean);
                }
            }
            if (!String.IsNullOrEmpty(bean.NotaComunicacion))
            {
                psCapacidadCurso = psCapacidadCursoDao.obtenerPorId(new PsCapacidadCursoPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad, bean.IdCursoComunicacion).obtenerArreglo());

                if (psCapacidadCurso != null)
                {
                    psCapacidadCurso.IdNota = bean.NotaComunicacion;
                    psCapacidadCursoDao.actualizar(psCapacidadCurso);
                }
                else
                {
                    this.guardarCurso(bean);
                }
            }
            if (!String.IsNullOrEmpty(bean.NotaPersonalSocial))
            {
                psCapacidadCurso = psCapacidadCursoDao.obtenerPorId(new PsCapacidadCursoPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdCapacidad, bean.IdCursopersonal).obtenerArreglo());

                if (psCapacidadCurso != null)
                {
                    psCapacidadCurso.IdNota = bean.NotaPersonalSocial;
                    psCapacidadCursoDao.actualizar(psCapacidadCurso);
                }
                else
                {
                    this.guardarCurso(bean);
                }
            }


            psCapacidadTallerDao.eliminarPorCapacidad(bean.IdCapacidad);

            //************* TALLERES ****************//
            DtoCapacidad Talleres = new DtoCapacidad();
            Talleres = obtenerTalleres(bean);

            int TallerArtistico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_ARTISTICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerDeportivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_DEPORTIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFormativo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FORMATIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerCognitivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_COGNITIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFisico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FISICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);


            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerArtistico))
            {
                Talleres.IdTaller = TallerArtistico;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerArtistico;
                Talleres.CantidadTaller = Talleres.CantidadTallerArtistico;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerDeportivo))
            {
                Talleres.IdTaller = TallerDeportivo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerDeportivo;
                Talleres.CantidadTaller = Talleres.CantidadTallerDeportivo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerFormativo))
            {
                Talleres.IdTaller = TallerFormativo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerFormativo;
                Talleres.CantidadTaller = Talleres.CantidadTallerFormativo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerCognitivo))
            {
                Talleres.IdTaller = TallerCognitivo;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerCognitivo;
                Talleres.CantidadTaller = Talleres.CantidadTallerCognitivo;
                this.guardarTaller(Talleres);
            }

            if (!String.IsNullOrEmpty(Talleres.IdNotaTallerFisico))
            {
                Talleres.IdTaller = TallerFisico;
                Talleres.IdNotaTaller = Talleres.IdNotaTallerFisico;
                Talleres.CantidadTaller = Talleres.CantidadTallerFisico;
                this.guardarTaller(Talleres);
            }


            return psCapacidad;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad)
        {
            coreEliminar(new PsCapacidadPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad));
        }

        public void coreEliminar(PsCapacidadPk id)
        {
            PsCapacidad bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsCapacidad coreAnular(UsuarioActual usuarioActual, PsCapacidadPk id)
        {
            PsCapacidad bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return null;// coreActualizar(usuarioActual, bean, ConstanteEstadoGenerico.INACTIVO);
        }

        public PsCapacidad coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdCapacidad)
        {
            return coreAnular(usuarioActual, new PsCapacidadPk(pIdInstitucion, pIdBeneficiario, pIdCapacidad));
        }

        private DtoCapacidad obtenerTalleres(DtoCapacidad bean)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            DtoCapacidad dtoCapacidad = new DtoCapacidad();

            // String _Parametroclave, String _Aplicacioncodigo, String _Companiacodigo

            int TallerArtistico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_ARTISTICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerDeportivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_DEPORTIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFormativo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FORMATIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerCognitivo = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_COGNITIVO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);
            int TallerFisico = Convert.ToInt32(this.parametrosmastDao.obtenerPorId(
                new ParametrosmastPk(
                PsConstantes.TALLER_FISICO, PsConstantes.CODIGO_APLICACION, PsConstantes.CODIGO_COMPANIA).obtenerArreglo()).Numero);

            parametros.Add(new ParametroPersistenciaGenerico("pCursoCognitivo", ConstanteUtil.TipoDato.Integer, TallerCognitivo));
            parametros.Add(new ParametroPersistenciaGenerico("pCursoFisico", ConstanteUtil.TipoDato.Integer, TallerFisico));
            parametros.Add(new ParametroPersistenciaGenerico("pCursoDeportivo", ConstanteUtil.TipoDato.Integer, TallerDeportivo));
            parametros.Add(new ParametroPersistenciaGenerico("pCursoArtistico", ConstanteUtil.TipoDato.Integer, TallerArtistico));
            parametros.Add(new ParametroPersistenciaGenerico("pCursoFormativo", ConstanteUtil.TipoDato.Integer, TallerFormativo));
            parametros.Add(new ParametroPersistenciaGenerico("pFechaInicio", ConstanteUtil.TipoDato.Date, DateTime.Now));
            parametros.Add(new ParametroPersistenciaGenerico("pFechaFin", ConstanteUtil.TipoDato.Date, DateTime.Now));
            parametros.Add(new ParametroPersistenciaGenerico("pBeneficiario", ConstanteUtil.TipoDato.Integer, bean.IdBeneficiario));
            parametros.Add(new ParametroPersistenciaGenerico("pInstitucion", ConstanteUtil.TipoDato.String, bean.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("pPeriodo", ConstanteUtil.TipoDato.String, bean.IdPeriodo));

            _reader = this.listarPorQuery("pscapacidad.listarCapacidadesTalleres", parametros);

            while (_reader.Read())
            {

                if (!_reader.IsDBNull(0))
                    bean.CantidadTallerCognitivo = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.IdNotaTallerCognitivo = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CantidadTallerFisico = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.IdNotaTallerFisico = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.CantidadTallerDeportivo = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.IdNotaTallerDeportivo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.CantidadTallerArtistico = _reader.GetInt32(6);
                if (!_reader.IsDBNull(7))
                    bean.IdNotaTallerArtistico = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.CantidadTallerFormativo = _reader.GetInt32(8);
                if (!_reader.IsDBNull(9))
                    bean.IdNotaTallerFormativo = _reader.GetString(9);

            }

            this.dispose();


            return bean;
        }


        public ParametroPaginacionGenerico listarCapacidades(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCapacidad> lstResultado = new List<DtoCapacidad>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
            }

            if (String.IsNullOrEmpty(filtro.IdSexo))
            {
                filtro.IdSexo = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea))
            {
                filtro.IdArea = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            _reader = this.listarPorQuery("pscapacidad.listarPaginacion", parametros);

            while (_reader.Read())
            {
                DtoCapacidad bean = new DtoCapacidad();


                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdPeriodo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.NombreCompleto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.IdRiesgoCaida = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdTipoInstitucion = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdNivel = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdGradoEducativo = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.AnioRetraso = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.ComentarioRetraso = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdTipoComunicacion = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.ComentarioRendimiento = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.ComentarioTalleres = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.IdHabilidadOcupacional = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdEvaluarOcupacional = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.ComentarioCapacidad = _reader.GetString(17);

                if (!_reader.IsDBNull(18))
                    bean.IdCapacidad = _reader.GetInt32(18);
                if (!_reader.IsDBNull(19))
                    bean.Barthel1 = _reader.GetInt32(19);
                if (!_reader.IsDBNull(20))
                    bean.Barthel2 = _reader.GetInt32(20);
                if (!_reader.IsDBNull(21))
                    bean.Barthel3 = _reader.GetInt32(21);
                if (!_reader.IsDBNull(22))
                    bean.Barthel4 = _reader.GetInt32(22);
                if (!_reader.IsDBNull(23))
                    bean.Barthel5 = _reader.GetInt32(23);
                if (!_reader.IsDBNull(24))
                    bean.Barthel6 = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.Barthel7 = _reader.GetInt32(25);
                if (!_reader.IsDBNull(26))
                    bean.Barthel8 = _reader.GetInt32(26);
                if (!_reader.IsDBNull(27))
                    bean.Barthel9 = _reader.GetInt32(27);

                if (!_reader.IsDBNull(28))
                    bean.Barthel10 = _reader.GetInt32(28);
                if (!_reader.IsDBNull(29))
                    bean.Katz1 = _reader.GetInt32(29);
                if (!_reader.IsDBNull(30))
                    bean.Katz2 = _reader.GetInt32(30);
                if (!_reader.IsDBNull(31))
                    bean.Katz3 = _reader.GetInt32(31);
                if (!_reader.IsDBNull(32))
                    bean.Katz4 = _reader.GetInt32(32);
                if (!_reader.IsDBNull(33))
                    bean.Katz5 = _reader.GetInt32(33);
                if (!_reader.IsDBNull(34))
                    bean.Katz6 = _reader.GetInt32(34);

                if (!_reader.IsDBNull(35))
                    bean.NotaLogicoMatematico = _reader.GetString(35);
                if (!_reader.IsDBNull(36))
                    bean.NotaComunicacion = _reader.GetString(36);
                if (!_reader.IsDBNull(37))
                    bean.NotaComprensionLectora = _reader.GetString(37);

                if (!_reader.IsDBNull(38))
                    bean.NotaPersonalSocial = _reader.GetString(38);
                if (!_reader.IsDBNull(39))
                    bean.NotaCienciaAmbiente = _reader.GetString(39);
                if (!_reader.IsDBNull(40))
                    bean.IdNotaTallerArtistico = _reader.GetString(40);
                if (!_reader.IsDBNull(41))
                    bean.CantidadTallerArtistico = _reader.GetInt32(41);

                if (!_reader.IsDBNull(42))
                    bean.IdNotaTallerFormativo = _reader.GetString(42);
                if (!_reader.IsDBNull(43))
                    bean.CantidadTallerFormativo = _reader.GetInt32(43);
                if (!_reader.IsDBNull(44))
                    bean.IdNotaTallerDeportivo = _reader.GetString(44);
                if (!_reader.IsDBNull(45))
                    bean.CantidadTallerDeportivo = _reader.GetInt32(45);

                if (!_reader.IsDBNull(46))
                    bean.GradoDependenciaKatz = _reader.GetString(46);
                if (!_reader.IsDBNull(47))
                    bean.GradoDependenciaBarthel = _reader.GetString(47);

                if (!_reader.IsDBNull(48))
                    bean.PorcentajeGradoKatz = _reader.GetDecimal(48);
                if (!_reader.IsDBNull(49))
                    bean.PorcentajeGradoBarthel = _reader.GetDecimal(49);

                if (!_reader.IsDBNull(50))
                    bean.IdCursoMatematica = _reader.GetString(50);
                if (!_reader.IsDBNull(51))
                    bean.IdCursoComunicacion = _reader.GetString(51);
                if (!_reader.IsDBNull(52))
                    bean.IdCursoCompresion = _reader.GetString(52);
                if (!_reader.IsDBNull(53))
                    bean.IdCursopersonal = _reader.GetString(53);
                if (!_reader.IsDBNull(54))
                    bean.IdCursoAmbiente = _reader.GetString(54);

                if (!_reader.IsDBNull(55))
                    bean.IdTallerArtistico = _reader.GetString(55);
                if (!_reader.IsDBNull(56))
                    bean.IdTallerFormativo = _reader.GetString(56);
                if (!_reader.IsDBNull(57))
                    bean.IdTallerDeportivo = _reader.GetString(57);


                if (!_reader.IsDBNull(60))
                    bean.IdNotaTallerCognitivo = _reader.GetString(60);
                if (!_reader.IsDBNull(61))
                    bean.CantidadTallerCognitivo = _reader.GetInt32(61);

                if (!_reader.IsDBNull(62))
                    bean.IdNotaTallerFisico = _reader.GetString(62);
                if (!_reader.IsDBNull(63))
                    bean.CantidadTallerFisico = _reader.GetInt32(63);
                if (!_reader.IsDBNull(64))
                    bean.Evaluado = _reader.GetString(64);
                if (!_reader.IsDBNull(65))
                    bean.IdOrigen = _reader.GetString(65);

                if (!_reader.IsDBNull(66))
                    contador = _reader.GetInt32(66);
                if (!_reader.IsDBNull(67))
                    bean.IdRiesgoCaidaDetalle = _reader.GetString(67);
                if (!_reader.IsDBNull(68))
                    bean.Edad = _reader.GetInt32(68);
                if (!_reader.IsDBNull(69))
                    bean.sexo = _reader.GetString(69);
                if (!_reader.IsDBNull(70))
                    bean.secuencia = Convert.ToInt32(_reader.GetInt64(70));

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;

        }

        public DtoCapacidad calcularBarthel(DtoCapacidad bean)
        {

            Nullable<Decimal> Porcentaje = 0; ;
            DtoCapacidad retorno = new DtoCapacidad();

            if (bean.Barthel1 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel1;
            }
            if (bean.Barthel2 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel2;
            }
            if (bean.Barthel3 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel3;
            }
            if (bean.Barthel4 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel4;
            }
            if (bean.Barthel5 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel5;
            }
            if (bean.Barthel6 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel6;
            }
            if (bean.Barthel7 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel7;
            }
            if (bean.Barthel8 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel8;
            }
            if (bean.Barthel9 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel9;
            }
            if (bean.Barthel10 != null)
            {
                Porcentaje = Porcentaje + bean.Barthel10;
            }

            retorno.PorcentajeGradoBarthel = Porcentaje;

            if (retorno.PorcentajeGradoBarthel >= 0 && retorno.PorcentajeGradoBarthel <= 20)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Total";
            }

            if (retorno.PorcentajeGradoBarthel >= 20 && retorno.PorcentajeGradoBarthel <= 35)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Grave";
            }

            if (retorno.PorcentajeGradoBarthel >= 40 && retorno.PorcentajeGradoBarthel <= 55)
            {
                retorno.GradoDependenciaBarthel = "Dependiente Moderado";
            }

            if (retorno.PorcentajeGradoBarthel >= 60 && retorno.PorcentajeGradoBarthel <= 99)
            {
                retorno.GradoDependenciaBarthel = "Dependiente leve";
            }

            if (retorno.PorcentajeGradoBarthel >= 100)
            {
                retorno.GradoDependenciaBarthel = "Independiente";
            }


            return retorno;
        }

        public DtoCapacidad calcularKatz(DtoCapacidad bean)
        {
            Nullable<Decimal> Porcentaje = 0; ;
            DtoCapacidad retorno = new DtoCapacidad();

            if (bean.Katz1 != null)
            {
                Porcentaje = Porcentaje + bean.Katz1;
            }
            if (bean.Katz2 != null)
            {
                Porcentaje = Porcentaje + bean.Katz2;
            }
            if (bean.Katz3 != null)
            {
                Porcentaje = Porcentaje + bean.Katz3;
            }
            if (bean.Katz4 != null)
            {
                Porcentaje = Porcentaje + bean.Katz4;
            }
            if (bean.Katz5 != null)
            {
                Porcentaje = Porcentaje + bean.Katz5;
            }
            if (bean.Katz6 != null)
            {
                Porcentaje = Porcentaje + bean.Katz6;
            }

            retorno.PorcentajeGradoKatz = Porcentaje;

            if (retorno.PorcentajeGradoKatz == 0)
            {
                retorno.GradoDependenciaKatz = "Independiente";
            }
            else if (retorno.PorcentajeGradoKatz >= 1 && retorno.PorcentajeGradoKatz <= 5)
            {
                retorno.GradoDependenciaKatz = "Dependiente Parcial";
            }
            else if (retorno.PorcentajeGradoKatz == 6)
            {
                retorno.GradoDependenciaKatz = "Dependiente Total";
            }


            return retorno;
        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroCapacidad filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoCapacidad> lstResultado = new List<DtoCapacidad>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
            }

            if (String.IsNullOrEmpty(filtro.IdSexo))
            {
                filtro.IdSexo = null;
            }

            if (String.IsNullOrEmpty(filtro.IdArea))
            {
                filtro.IdArea = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            parametros.Add(new ParametroPersistenciaGenerico("p_orden", ConstanteUtil.TipoDato.Integer, filtro.orden));
            parametros.Add(new ParametroPersistenciaGenerico("p_sentido", ConstanteUtil.TipoDato.Integer, filtro.sentido));


            _reader = this.listarPorQuery("pscapacidad.listarPaginacionConsulta", parametros);

            while (_reader.Read())
            {
                DtoCapacidad bean = new DtoCapacidad();


                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdPeriodo = _reader.GetString(2);
                if (!_reader.IsDBNull(3))
                    bean.NombreCompleto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.Estado = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.IdRiesgoCaida = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.IdTipoInstitucion = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.IdNivel = _reader.GetString(8);
                if (!_reader.IsDBNull(9))
                    bean.IdGradoEducativo = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.AnioRetraso = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.ComentarioRetraso = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdTipoComunicacion = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.ComentarioRendimiento = _reader.GetString(13);
                if (!_reader.IsDBNull(14))
                    bean.ComentarioTalleres = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.IdHabilidadOcupacional = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdEvaluarOcupacional = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.ComentarioCapacidad = _reader.GetString(17);

                if (!_reader.IsDBNull(18))
                    bean.IdCapacidad = _reader.GetInt32(18);
                if (!_reader.IsDBNull(19))
                    bean.Barthel1 = _reader.GetInt32(19);
                if (!_reader.IsDBNull(20))
                    bean.Barthel2 = _reader.GetInt32(20);
                if (!_reader.IsDBNull(21))
                    bean.Barthel3 = _reader.GetInt32(21);
                if (!_reader.IsDBNull(22))
                    bean.Barthel4 = _reader.GetInt32(22);
                if (!_reader.IsDBNull(23))
                    bean.Barthel5 = _reader.GetInt32(23);
                if (!_reader.IsDBNull(24))
                    bean.Barthel6 = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.Barthel7 = _reader.GetInt32(25);
                if (!_reader.IsDBNull(26))
                    bean.Barthel8 = _reader.GetInt32(26);
                if (!_reader.IsDBNull(27))
                    bean.Barthel9 = _reader.GetInt32(27);

                if (!_reader.IsDBNull(28))
                    bean.Barthel10 = _reader.GetInt32(28);
                if (!_reader.IsDBNull(29))
                    bean.Katz1 = _reader.GetInt32(29);
                if (!_reader.IsDBNull(30))
                    bean.Katz2 = _reader.GetInt32(30);
                if (!_reader.IsDBNull(31))
                    bean.Katz3 = _reader.GetInt32(31);
                if (!_reader.IsDBNull(32))
                    bean.Katz4 = _reader.GetInt32(32);
                if (!_reader.IsDBNull(33))
                    bean.Katz5 = _reader.GetInt32(33);
                if (!_reader.IsDBNull(34))
                    bean.Katz6 = _reader.GetInt32(34);

                if (!_reader.IsDBNull(35))
                    bean.NotaLogicoMatematico = _reader.GetString(35);
                if (!_reader.IsDBNull(36))
                    bean.NotaComunicacion = _reader.GetString(36);
                if (!_reader.IsDBNull(37))
                    bean.NotaComprensionLectora = _reader.GetString(37);

                if (!_reader.IsDBNull(38))
                    bean.NotaPersonalSocial = _reader.GetString(38);
                if (!_reader.IsDBNull(39))
                    bean.NotaCienciaAmbiente = _reader.GetString(39);
                if (!_reader.IsDBNull(40))
                    bean.IdNotaTallerArtistico = _reader.GetString(40);
                if (!_reader.IsDBNull(41))
                    bean.CantidadTallerArtistico = _reader.GetInt32(41);

                if (!_reader.IsDBNull(42))
                    bean.IdNotaTallerFormativo = _reader.GetString(42);
                if (!_reader.IsDBNull(43))
                    bean.CantidadTallerFormativo = _reader.GetInt32(43);
                if (!_reader.IsDBNull(44))
                    bean.IdNotaTallerDeportivo = _reader.GetString(44);
                if (!_reader.IsDBNull(45))
                    bean.CantidadTallerDeportivo = _reader.GetInt32(45);

                if (!_reader.IsDBNull(46))
                    bean.GradoDependenciaKatz = _reader.GetString(46);
                if (!_reader.IsDBNull(47))
                    bean.GradoDependenciaBarthel = _reader.GetString(47);

                if (!_reader.IsDBNull(48))
                    bean.PorcentajeGradoKatz = _reader.GetDecimal(48);
                if (!_reader.IsDBNull(49))
                    bean.PorcentajeGradoBarthel = _reader.GetDecimal(49);

                if (!_reader.IsDBNull(50))
                    bean.IdCursoMatematica = _reader.GetString(50);
                if (!_reader.IsDBNull(51))
                    bean.IdCursoComunicacion = _reader.GetString(51);
                if (!_reader.IsDBNull(52))
                    bean.IdCursoCompresion = _reader.GetString(52);
                if (!_reader.IsDBNull(53))
                    bean.IdCursopersonal = _reader.GetString(53);
                if (!_reader.IsDBNull(54))
                    bean.IdCursoAmbiente = _reader.GetString(54);

                if (!_reader.IsDBNull(55))
                    bean.IdTallerArtistico = _reader.GetString(55);
                if (!_reader.IsDBNull(56))
                    bean.IdTallerFormativo = _reader.GetString(56);
                if (!_reader.IsDBNull(57))
                    bean.IdTallerDeportivo = _reader.GetString(57);

                if (!_reader.IsDBNull(58))
                    bean.IdTallerCognitivo = _reader.GetString(58);
                if (!_reader.IsDBNull(59))
                    bean.IdTallerFisico = _reader.GetString(59);
                if (!_reader.IsDBNull(60))
                    bean.IdNotaTallerCognitivo = _reader.GetString(60);
                if (!_reader.IsDBNull(61))
                    bean.CantidadTallerCognitivo = _reader.GetInt32(61);
                if (!_reader.IsDBNull(62))
                    bean.IdNotaTallerFisico = _reader.GetString(62);
                if (!_reader.IsDBNull(63))
                    bean.CantidadTallerFisico = _reader.GetInt32(63);

                if (!_reader.IsDBNull(64))
                    bean.NombreHabilidadOcupacional = _reader.GetString(64);
                if (!_reader.IsDBNull(65))
                    bean.Evaluado = _reader.GetString(65);

                if (!_reader.IsDBNull(66))
                    bean.NombreInstitucion = _reader.GetString(66);
                if (!_reader.IsDBNull(67))
                    bean.NombreNivel = _reader.GetString(67);
                if (!_reader.IsDBNull(68))
                    bean.NombreGrado = _reader.GetString(68);
                if (!_reader.IsDBNull(69))
                    bean.IdRiesgoCaidaDetalle = _reader.GetString(69);
                if (!_reader.IsDBNull(70))
                    contador = _reader.GetInt32(70);


                if (!_reader.IsDBNull(71))
                    bean.sexo = _reader.GetString(71);
                if (!_reader.IsDBNull(72))
                    bean.secuencia = _reader.GetInt32(72);
                if (!_reader.IsDBNull(73))
                    bean.Edad = _reader.GetInt32(73);

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();
            return paginacion;
        }

        public DtoCapacidad calcularAniosRetraso(DtoCapacidad bean)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            parametros.Add(new ParametroPersistenciaGenerico("pidGradoAcademico", ConstanteUtil.TipoDato.String, bean.IdGradoEducativo));
            parametros.Add(new ParametroPersistenciaGenerico("pFechaNacimiento", ConstanteUtil.TipoDato.Date, null));
            parametros.Add(new ParametroPersistenciaGenerico("pIdPersona", ConstanteUtil.TipoDato.Integer, bean.IdBeneficiario));
            _reader = this.listarPorQuery("pscapacidad.calcularAniosRetraso", parametros);
            while (_reader.Read())
            {
                if (!_reader.IsDBNull(0))
                    bean.AnioRetraso = _reader.GetString(0);
                else
                    bean.AnioRetraso = "";
            }
            this.dispose();
            return bean;
        }
    }
}
