using System;
using System.Text;
using System.Linq;
using System.Data.Common;

using Microsoft.Extensions.DependencyInjection;

using net.royal.spring.framework.web.dao;
using net.royal.spring.framework.web.dao.impl;

using net.royal.spring.programasocial.dao;
using net.royal.spring.programasocial.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.constante;
using net.royal.spring.framework.core;
using net.royal.spring.programasocial.dominio.dtos;
using System.Collections.Generic;
using net.royal.spring.rrhh.dominio.dto;
using net.royal.spring.framework.core.dominio.dto;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.salud.dao;
using net.royal.spring.salud.dominio;

namespace net.royal.spring.programasocial.dao.impl
{
    public class PsSaludDaoImpl : GenericoDaoImpl<PsSalud>, PsSaludDao
    {
        private IServiceProvider servicioProveedor;
        private PsSaludEstadoDao psSaludEstadoDao;
        private PsSaludBiomecanicaDao psSaludBiomecanicaDao;
        private PsSaludTerapiaDao psSaludTerapiaDao;
        private PsSaludExamenDao psSaludExamenDao;
        private PsSaludSubcondicionDao psSaludSubcondicionDao;
        private PsSaludDiscapacidadDao psSaludDiscapacidadDao;
        private PsSaludDiagnosticoDao psSaludDiagnosticoDao;
        private PsSaludTratamientoDao psSaludTratamientoDao;
        private PsBeneficiarioDao psBeneficiarioDao;
        private PsNutricionDao psNutricionDao;
        private PsSaludAyudaDao psSaludAyudaDao;
        private SsGediagnosticoDao ssGediagnosticoDao;



        public PsSaludDaoImpl(GenericoDbContext context, IServiceProvider _servicioProveedor) : base(context, "pssalud")
        {
            servicioProveedor = _servicioProveedor;
            psSaludEstadoDao = servicioProveedor.GetService<PsSaludEstadoDao>();
            psSaludBiomecanicaDao = servicioProveedor.GetService<PsSaludBiomecanicaDao>();
            psSaludTerapiaDao = servicioProveedor.GetService<PsSaludTerapiaDao>();
            psSaludExamenDao = servicioProveedor.GetService<PsSaludExamenDao>();
            psSaludSubcondicionDao = servicioProveedor.GetService<PsSaludSubcondicionDao>();
            psSaludDiscapacidadDao = servicioProveedor.GetService<PsSaludDiscapacidadDao>();
            psSaludDiagnosticoDao = servicioProveedor.GetService<PsSaludDiagnosticoDao>();
            psSaludTratamientoDao = servicioProveedor.GetService<PsSaludTratamientoDao>();
            psBeneficiarioDao = servicioProveedor.GetService<PsBeneficiarioDao>();
            psNutricionDao = servicioProveedor.GetService<PsNutricionDao>();
            psSaludAyudaDao = servicioProveedor.GetService<PsSaludAyudaDao>();
            ssGediagnosticoDao = servicioProveedor.GetService<SsGediagnosticoDao>();
        }

        public DtoPsSalud obtenerPorId(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud)
        {

            PsSalud bean = new PsSalud();
            DtoPsSalud psSalud = new DtoPsSalud();
            bean = this.obtenerPorId(new PsSaludPk(pIdInstitucion, pIdBeneficiario, pIdSalud).obtenerArreglo());

            psSalud.IdBeneficiario = bean.IdBeneficiario;
            psSalud.IdSalud = bean.IdSalud;
            psSalud.IdInstitucion = bean.IdInstitucion;
            psSalud.FechaInforme = bean.FechaInforme;
            psSalud.Estado = bean.Estado;
            psSalud.Hemoglobina = bean.Hemoglobina;
            psSalud.HemoglobinaResultado = bean.HemoglobinaResultado;
            psSalud.IdAyudaMedica = bean.IdAyudaMedica;
            psSalud.IdDescarteSerologico = bean.IdDescarteSerologico;
            psSalud.IdDescarteTbc = bean.IdDescarteTbc;
            psSalud.IdPeriodo = bean.IdPeriodo;
            psSalud.IdHta = bean.IdHta;
            psSalud.IdGrupoSanguineo = bean.IdGrupoSanguineo;
            psSalud.IdFactorRh = bean.IdFactorRh;
            psSalud.IdTbc = bean.IdTbc;
            psSalud.IdDiabetes = bean.IdDiabetes;
            psSalud.IdCognitivo = bean.IdCognitivo;
            psSalud.IdAfectivo = bean.IdAfectivo;
            psSalud.IdOsteoatrosis = bean.IdOsteoatrosis;
            psSalud.Comentarios = bean.Comentarios;
            psSalud.IdTratamientoAnemia = bean.IdTratamientoAnemia;
            psSalud.IdPruebaVIH = bean.IdPruebaVIH;
            psSalud.EdadMenarquia = bean.EdadMenarquia;
            psSalud.FechaUltimaMestruacion = bean.FechaUltimaMestruacion;
            psSalud.OtrasEnfermedades = bean.OtrasEnfermedades;
            psSalud.listaEstado = new List<DtoTabla>();
            psSalud.listaExamenes = new List<DtoTabla>();
            psSalud.listaSubCondicion = new List<DtoTabla>();
            psSalud.listaBioMecanica = new List<DtoTabla>();
            psSalud.listaTerapias = new List<DtoTabla>();
            psSalud.listaDiscapacidad = new List<DtoTabla>();
            psSalud.listaTratramiento = new List<DtoTabla>();
            psSalud.listaDiagnostico = new List<DtoTabla>();

            List<PsSaludDiscapacidad> listaDiscapacidad = psSaludDiscapacidadDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludEstado> listaPsSaludEstado = psSaludEstadoDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludBiomecanica> listaPsSaludBiomecanica = psSaludBiomecanicaDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludTerapia> listaPsSaludTerapia = psSaludTerapiaDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludExamen> listaPsSaludExamen = psSaludExamenDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludSubcondicion> listaPsSaludSubcondicion = psSaludSubcondicionDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludDiagnostico> listaPsSaludDiagnostico = psSaludDiagnosticoDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludTratamiento> listaPsSaludTratamiento = psSaludTratamientoDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);
            List<PsSaludAyuda> listaAyuda = psSaludAyudaDao.obtenerListado(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud);



            int est = 1;
            int exa = 1;
            int con = 1;
            int bio = 1;
            int ter = 1;
            int dis = 1;
            int tra = 1;
            int dia = 1;
            int ayu = 1;

            if (listaPsSaludEstado.Count > 0)
            {
                foreach (var estado in listaPsSaludEstado)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = estado.IdEstado;
                    dto.Secuencia = est;
                    dto.valorFlag = "M";
                    psSalud.listaEstado.Add(dto);
                    est++;
                }
            }

            if (listaAyuda.Count > 0)
            {
                foreach (var estado in listaAyuda)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = estado.IdAyuda;
                    dto.Secuencia = est;
                    dto.valorFlag = "M";
                    psSalud.listaAyuda.Add(dto);
                    ayu++;
                }
            }

            if (listaPsSaludBiomecanica.Count > 0)
            {
                foreach (var biomecanica in listaPsSaludBiomecanica)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = biomecanica.IdEstadoAyuda;
                    dto.IdTipoAyuda = biomecanica.IdTipoAyuda;
                    dto.Secuencia = bio;
                    dto.valorFlag = "M";
                    psSalud.listaBioMecanica.Add(dto);
                    bio++;
                }

            }
            if (listaPsSaludTerapia.Count > 0)
            {
                foreach (var terapia in listaPsSaludTerapia)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = terapia.IdTerapia;
                    dto.Secuencia = ter;
                    dto.valorFlag = "M";
                    psSalud.listaTerapias.Add(dto);
                    ter++;
                }

            }
            if (listaPsSaludExamen.Count > 0)
            {
                foreach (var examen in listaPsSaludExamen)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = examen.IdExamen;
                    dto.IdResultado = examen.IdResultado;
                    dto.Secuencia = exa;
                    dto.valorFlag = "M";
                    psSalud.listaExamenes.Add(dto);
                    exa++;
                }

            }
            if (listaPsSaludSubcondicion.Count > 0)
            {
                foreach (var condicion in listaPsSaludSubcondicion)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = condicion.IdCondicion;
                    dto.IdSubCondicion = condicion.IdSubcondicion;
                    dto.Secuencia = con;
                    dto.valorFlag = "M";
                    psSalud.listaSubCondicion.Add(dto);
                    con++;
                }

            }

            if (listaDiscapacidad.Count > 0)
            {
                foreach (var dispacidad in listaDiscapacidad)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = dispacidad.IdDiscapacidad;
                    dto.Secuencia = con;
                    dto.valorFlag = "M";
                    psSalud.listaDiscapacidad.Add(dto);
                    dis++;
                }

            }


            if (listaPsSaludDiagnostico.Count > 0)
            {
                foreach (var diagnostico in listaPsSaludDiagnostico)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = diagnostico.IdDiagnostico;
                    dto.Secuencia = con;
                    SsGediagnosticoPk pk = new SsGediagnosticoPk();
                    pk.IdDiagnostico = Convert.ToInt32(diagnostico.IdDiagnostico);
                    dto.Nombre = ssGediagnosticoDao.obtenerPorId(pk.obtenerArreglo()).Nombre;
                    dto.valorFlag = "M";
                    psSalud.listaDiagnostico.Add(dto);
                    dia++;
                }

            }

            if (listaPsSaludTratamiento.Count > 0)
            {
                foreach (var tratamiento in listaPsSaludTratamiento)
                {
                    DtoTabla dto = new DtoTabla();
                    dto.Codigo = tratamiento.IdTratamiento;
                    dto.Secuencia = con;
                    dto.valorFlag = "M";
                    psSalud.listaTratramiento.Add(dto);
                    tra++;
                }

            }

            return psSalud;
        }


        public PsSalud coreInsertar(UsuarioActual usuarioActual, DtoPsSalud bean)
        {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);


            if (UString.estaVacio(bean.Estado))
                bean.Estado = ConstanteEstadoGenerico.ACTIVO;

            PsSalud psSalud = new PsSalud();
            psSalud.FechaInforme = bean.FechaInforme;
            psSalud.Estado = bean.Estado;
            psSalud.Hemoglobina = bean.Hemoglobina;
            psSalud.HemoglobinaResultado = bean.HemoglobinaResultado;
            psSalud.IdAyudaMedica = bean.IdAyudaMedica;
            psSalud.IdBeneficiario = bean.IdBeneficiario;
            psSalud.IdDescarteSerologico = bean.IdDescarteSerologico;
            psSalud.IdDescarteTbc = bean.IdDescarteTbc;
            psSalud.IdInstitucion = bean.IdInstitucion;
            psSalud.IdPeriodo = bean.IdPeriodo;
            psSalud.IdSalud = this.generarCodigo();
            psSalud.IdOrigen = bean.IdOrigen;
            psSalud.IdGrupoSanguineo = bean.IdGrupoSanguineo;
            psSalud.IdFactorRh = bean.IdFactorRh;
            psSalud.IdHta = bean.IdHta;
            psSalud.IdTbc = bean.IdTbc;
            psSalud.IdDiabetes = bean.IdDiabetes;
            psSalud.IdCognitivo = bean.IdCognitivo;
            psSalud.IdAfectivo = bean.IdAfectivo;
            psSalud.IdOsteoatrosis = bean.IdOsteoatrosis;
            psSalud.Comentarios = bean.Comentarios;
            psSalud.IdPruebaVIH = bean.IdPruebaVIH;
            psSalud.EdadMenarquia = bean.EdadMenarquia;
            psSalud.FechaUltimaMestruacion = bean.FechaUltimaMestruacion;
            psSalud.OtrasEnfermedades = bean.OtrasEnfermedades;
            psSalud.IdTratamientoAnemia = bean.IdTratamientoAnemia;
            psSalud.CreacionTerminal = usuarioActual.UsuarioIpAddress;
            psSalud.CreacionFecha = DateTime.Now;
            psSalud.CreacionUsuario = usuarioActual.UsuarioLogin;

            if (bean.EvaluadoBoolean)
            {
                psSalud.Evaluado = "S";
            }
            else
            {
                psSalud.Evaluado = "";
            }

            this.registrar(psSalud);

            PsBeneficiario psBeneficiario = psBeneficiarioDao.obtenerPorId(new PsBeneficiarioPk(bean.IdInstitucion, bean.IdBeneficiario).obtenerArreglo());

            if (bean.IdOrigen != "EVA")
            {
                psBeneficiario.IdComponenteSalud = psSalud.IdSalud;
                psBeneficiarioDao.actualizar(psBeneficiario);
            }
            else
            {
                if (psBeneficiario.IdComponenteSalud == null)
                {
                    psBeneficiario.IdComponenteSalud = psSalud.IdSalud;
                    psBeneficiarioDao.actualizar(psBeneficiario);
                }
            }

            if (bean.listaEstado.Count > 0)
            {
                foreach (var estado in bean.listaEstado)
                {
                    if (estado.valorFlag != "E")
                    {
                        PsSaludEstado psSaludEstado = new PsSaludEstado();

                        psSaludEstado.IdBeneficiario = bean.IdBeneficiario;
                        psSaludEstado.IdInstitucion = bean.IdInstitucion;
                        psSaludEstado.IdSalud = psSalud.IdSalud;
                        psSaludEstado.IdEstado = estado.Codigo;
                        psSaludEstadoDao.registrar(psSaludEstado);
                    }
                }
            }

            if (bean.listaAyuda.Count > 0)
            {
                foreach (var estado in bean.listaAyuda)
                {
                    if (estado.valorFlag != "E")
                    {
                        if (!UString.estaVacio(estado.Codigo))
                        {
                            PsSaludAyuda psSaludEstado = new PsSaludAyuda();

                            psSaludEstado.IdBeneficiario = bean.IdBeneficiario;
                            psSaludEstado.IdInstitucion = bean.IdInstitucion;
                            psSaludEstado.IdSalud = psSalud.IdSalud;
                            psSaludEstado.IdAyuda = estado.Codigo;
                            psSaludAyudaDao.registrar(psSaludEstado);
                        }
                    }
                }
            }

            if (bean.listaDiscapacidad.Count > 0)
            {
                foreach (var discapacidad in bean.listaDiscapacidad)
                {
                    if (discapacidad.valorFlag != "E")
                    {
                        PsSaludDiscapacidad psSaludDiscapacidad = new PsSaludDiscapacidad();

                        psSaludDiscapacidad.IdBeneficiario = bean.IdBeneficiario;
                        psSaludDiscapacidad.IdInstitucion = bean.IdInstitucion;
                        psSaludDiscapacidad.IdSalud = psSalud.IdSalud;
                        psSaludDiscapacidad.IdDiscapacidad = discapacidad.Codigo;
                        psSaludDiscapacidadDao.registrar(psSaludDiscapacidad);
                    }
                }
            }


            if (bean.listaBioMecanica.Count > 0)
            {
                foreach (var biomecanica in bean.listaBioMecanica)
                {
                    if (biomecanica.valorFlag != "E")
                    {
                        PsSaludBiomecanica psSaludBiomecanica = new PsSaludBiomecanica();
                        psSaludBiomecanica.IdBeneficiario = bean.IdBeneficiario;
                        psSaludBiomecanica.IdInstitucion = bean.IdInstitucion;
                        psSaludBiomecanica.IdSalud = psSalud.IdSalud;

                        psSaludBiomecanica.IdEstadoAyuda = biomecanica.Codigo;
                        psSaludBiomecanica.IdTipoAyuda = biomecanica.IdTipoAyuda;

                        psSaludBiomecanicaDao.registrar(psSaludBiomecanica);
                    }
                }
            }

            if (bean.listaExamenes.Count > 0)
            {
                foreach (var examen in bean.listaExamenes)
                {
                    if (examen.valorFlag != "E")
                    {
                        PsSaludExamen psSaludExamen = new PsSaludExamen();
                        psSaludExamen.IdBeneficiario = bean.IdBeneficiario;
                        psSaludExamen.IdInstitucion = bean.IdInstitucion;
                        psSaludExamen.IdSalud = psSalud.IdSalud;

                        psSaludExamen.IdExamen = examen.Codigo;
                        psSaludExamen.IdResultado = "X";//examen.IdResultado;

                        psSaludExamenDao.registrar(psSaludExamen);
                    }
                }
            }

            if (bean.listaSubCondicion.Count > 0)
            {
                foreach (var subCondicion in bean.listaSubCondicion)
                {
                    if (subCondicion.valorFlag != "E")
                    {
                        PsSaludSubcondicion psSaludSubcondicion = new PsSaludSubcondicion();
                        psSaludSubcondicion.IdBeneficiario = bean.IdBeneficiario;
                        psSaludSubcondicion.IdInstitucion = bean.IdInstitucion;
                        psSaludSubcondicion.IdSalud = psSalud.IdSalud;

                        psSaludSubcondicion.IdSubcondicion = subCondicion.IdSubCondicion;
                        psSaludSubcondicion.IdCondicion = subCondicion.Codigo;

                        psSaludSubcondicionDao.registrar(psSaludSubcondicion);
                    }
                }
            }

            if (bean.listaTerapias.Count > 0)
            {
                foreach (var terapias in bean.listaTerapias)
                {
                    if (terapias.valorFlag != "E")
                    {
                        PsSaludTerapia psSaludTerapia = new PsSaludTerapia();
                        psSaludTerapia.IdBeneficiario = bean.IdBeneficiario;
                        psSaludTerapia.IdInstitucion = bean.IdInstitucion;
                        psSaludTerapia.IdSalud = psSalud.IdSalud;

                        psSaludTerapia.IdTerapia = terapias.Codigo;

                        psSaludTerapiaDao.registrar(psSaludTerapia);
                    }
                }
            }

            if (bean.listaTratramiento.Count > 0)
            {
                foreach (var tratamiento in bean.listaTratramiento)
                {
                    if (tratamiento.valorFlag != "E")
                    {
                        PsSaludTratamiento psSaludTratamiento = new PsSaludTratamiento();
                        psSaludTratamiento.IdBeneficiario = bean.IdBeneficiario;
                        psSaludTratamiento.IdInstitucion = bean.IdInstitucion;
                        psSaludTratamiento.IdSalud = psSalud.IdSalud;

                        psSaludTratamiento.IdTratamiento = tratamiento.Codigo;

                        psSaludTratamientoDao.registrar(psSaludTratamiento);
                    }
                }
            }

            if (bean.listaDiagnostico.Count > 0)
            {
                foreach (var diagnostico in bean.listaDiagnostico)
                {
                    if (diagnostico.valorFlag != "E")
                    {
                        PsSaludDiagnostico psSaludDiagnostico = new PsSaludDiagnostico();
                        psSaludDiagnostico.IdBeneficiario = bean.IdBeneficiario;
                        psSaludDiagnostico.IdInstitucion = bean.IdInstitucion;
                        psSaludDiagnostico.IdSalud = psSalud.IdSalud;

                        psSaludDiagnostico.IdDiagnostico = diagnostico.Codigo;

                        psSaludDiagnosticoDao.registrar(psSaludDiagnostico);
                    }
                }
            }

            return psSalud;
        }

        public int generarCodigo()
        {
            IQueryable<PsSalud> query = this.getCriteria();

            List<PsSalud> lst = query.ToList();
            int var = 0;
            if (lst.Count > 0)
            {
                var = (int)(lst.Max(bean => bean.IdSalud));
            }

            return var + 1;
        }

        private List<MensajeUsuario> validar(DtoPsSalud bean)
        {
            List<MensajeUsuario> lstRetorno = new List<MensajeUsuario>();

            if (bean.EvaluadoBoolean)
            {
                if (String.IsNullOrEmpty(bean.Comentarios))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el Comentario"));
                }
                return lstRetorno;
            }


            if (bean.IdOrigen != "EVA")
            {

                if (String.IsNullOrEmpty(bean.IdGrupoSanguineo))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El campo Grupo Sanguíneo es Obligatorio"));
                }

                if (String.IsNullOrEmpty(bean.IdFactorRh))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El campo Grupo factor RH es Obligatorio"));
                }


                if ("S" == bean.IdDiscapacidad)
                {
                    if (bean.listaDiscapacidad.Count == 0)
                    {
                        lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar un tipo de Discapacidad"));
                    }
                    else
                    {
                        for (int i = 0; i < bean.listaDiscapacidad.Count; i++)
                        {
                            if (bean.listaDiscapacidad[i].valorFlag != "E")
                            {
                                if (String.IsNullOrEmpty(bean.listaDiscapacidad[i].Codigo))
                                {
                                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar Dispacidades en la fila N° " + (i + 1)));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (bean.listaEstado.Count == 0)
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar estados de salud"));
                }

            }


            if (bean.FechaInforme == null)
            {
                lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "La Fecha del Informe es obligatoria"));
            }


            if (bean.listaSubCondicion.Count == 0)
            {
                //lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar Condiciones Sensoriales"));
            }
            else
            {
                for (int i = 0; i < bean.listaSubCondicion.Count; i++)
                {
                    if (bean.listaSubCondicion[i].valorFlag != "E")
                    {
                        if (String.IsNullOrEmpty(bean.listaSubCondicion[i].IdSubCondicion))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar SubCondicion Sensorial en la fila N° " + (i + 1)));
                        }

                        if (String.IsNullOrEmpty(bean.listaSubCondicion[i].Codigo))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar Condicion Sensorial en la fila N° " + (i + 1)));
                        }
                    }
                }
            }

            if (bean.listaBioMecanica.Count != 0)
            {
                for (int i = 0; i < bean.listaBioMecanica.Count; i++)
                {
                    if (bean.listaBioMecanica[i].valorFlag != "E")
                    {
                        if (String.IsNullOrEmpty(bean.listaBioMecanica[i].Codigo))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar El tipo de Ayuda en la fila N° " + (i + 1)));
                        }

                        if (String.IsNullOrEmpty(bean.listaBioMecanica[i].IdTipoAyuda))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el estado de la ayuda en la fila N° " + (i + 1)));
                        }
                    }
                }
            }

            if (bean.listaExamenes.Count != 0)
            {
                for (int i = 0; i < bean.listaExamenes.Count; i++)
                {
                    if (bean.listaExamenes[i].valorFlag != "E")
                    {
                        if (String.IsNullOrEmpty(bean.listaExamenes[i].Codigo))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el exámen auxiliar en la fila N° " + (i + 1)));
                        }

                        if (UInteger.esCeroOrNulo(bean.listaExamenes[i].IdResultadoNumerico))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Ingresar el resultado en la fila N° " + (i + 1)));
                        }
                    }
                }
            }


            if (bean.ValidarNinos)
            {
                /*if (bean.Hemoglobina == null) {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Hemoglobina es requerido"));
                }*/

                if (!String.IsNullOrEmpty(bean.HemoglobinaResultado))
                {
                    if (bean.HemoglobinaResultado != "Sin Anemia")
                    {
                        if (String.IsNullOrEmpty(bean.IdTratamientoAnemia))
                        {
                            lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "Debe Indicar si tiene Tratamiento de Anemia"));
                        }
                    }
                }


            }
            else
            {
                if (String.IsNullOrEmpty(bean.IdDescarteTbc))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Descarte TBC es requerido"));
                }
                if (String.IsNullOrEmpty(bean.IdDescarteSerologico))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Descarte Serologico es requerido"));
                }
                if (String.IsNullOrEmpty(bean.IdCognitivo))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Congnitivo es requerido"));
                }
                if (String.IsNullOrEmpty(bean.IdAfectivo))
                {
                    lstRetorno.Add(new MensajeUsuario(tipo_mensaje.ERROR, "El Campo Afectivo es requerido"));
                }
            }


            return lstRetorno;
        }

        public PsSalud coreActualizar(UsuarioActual usuarioActual, DtoPsSalud bean)
        {

            List<MensajeUsuario> lstRetorno = validar(bean);

            if (lstRetorno.Count > 0)
                throw new UException(lstRetorno);

            PsSalud psSalud = new PsSalud();
            psSalud = this.obtenerPorId(new PsSaludPk(bean.IdInstitucion, bean.IdBeneficiario, bean.IdSalud).obtenerArreglo());

            psSalud.FechaInforme = bean.FechaInforme;
            psSalud.Estado = bean.Estado;
            psSalud.Hemoglobina = bean.Hemoglobina;
            psSalud.HemoglobinaResultado = bean.HemoglobinaResultado;
            psSalud.IdAyudaMedica = bean.IdAyudaMedica;
            psSalud.IdDescarteSerologico = bean.IdDescarteSerologico;
            psSalud.IdDescarteTbc = bean.IdDescarteTbc;
            psSalud.IdPeriodo = bean.IdPeriodo;
            psSalud.IdHta = bean.IdHta;
            psSalud.IdGrupoSanguineo = bean.IdGrupoSanguineo;
            psSalud.IdFactorRh = bean.IdFactorRh;
            psSalud.IdTbc = bean.IdTbc;
            psSalud.IdDiabetes = bean.IdDiabetes;
            psSalud.IdCognitivo = bean.IdCognitivo;
            psSalud.IdAfectivo = bean.IdAfectivo;
            psSalud.IdOsteoatrosis = bean.IdOsteoatrosis;
            psSalud.Comentarios = bean.Comentarios;
            psSalud.IdPruebaVIH = bean.IdPruebaVIH;
            psSalud.EdadMenarquia = bean.EdadMenarquia;
            psSalud.FechaUltimaMestruacion = bean.FechaUltimaMestruacion;
            psSalud.OtrasEnfermedades = bean.OtrasEnfermedades;
            psSalud.IdTratamientoAnemia = bean.IdTratamientoAnemia;
            psSalud.ModificacionTerminal = usuarioActual.UsuarioIpAddress;
            psSalud.ModificacionFecha = DateTime.Now;
            psSalud.ModificacionUsuario = usuarioActual.UsuarioLogin;

            if (bean.EvaluadoBoolean)
            {
                psSalud.Evaluado = "S";
            }
            else
            {
                psSalud.Evaluado = "";
            }

            this.actualizar(psSalud);

            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPsSalud> lstResultado = new List<DtoPsSalud>();

            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, bean.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_idBeneficiario", ConstanteUtil.TipoDato.Integer, bean.IdBeneficiario));
            parametros.Add(new ParametroPersistenciaGenerico("p_Salud", ConstanteUtil.TipoDato.Integer, bean.IdSalud));

            this.ejecutarPorQuery("pssalud.eliminarDetalleSalud", parametros);

            if (bean.listaDiscapacidad.Count > 0)
            {

                foreach (var discapacidad in bean.listaDiscapacidad)
                {
                    if (discapacidad.valorFlag != "E")
                    {
                        PsSaludDiscapacidad psSaludDiscapacidad = new PsSaludDiscapacidad();

                        psSaludDiscapacidad.IdBeneficiario = bean.IdBeneficiario;
                        psSaludDiscapacidad.IdInstitucion = bean.IdInstitucion;
                        psSaludDiscapacidad.IdSalud = psSalud.IdSalud;
                        psSaludDiscapacidad.IdDiscapacidad = discapacidad.Codigo;
                        psSaludDiscapacidadDao.registrar(psSaludDiscapacidad);
                    }
                }
            }


            if (bean.listaEstado.Count > 0)
            {

                foreach (var estado in bean.listaEstado)
                {
                    if (estado.valorFlag != "E")
                    {
                        PsSaludEstado psSaludEstado = new PsSaludEstado();

                        psSaludEstado.IdBeneficiario = bean.IdBeneficiario;
                        psSaludEstado.IdInstitucion = bean.IdInstitucion;
                        psSaludEstado.IdSalud = psSalud.IdSalud;
                        psSaludEstado.IdEstado = estado.Codigo;
                        psSaludEstadoDao.registrar(psSaludEstado);
                    }
                }
            }

            if (bean.listaAyuda != null)
            {
                if (bean.listaAyuda.Count > 0)
                {

                    foreach (var estado in bean.listaAyuda)
                    {
                        if (estado.valorFlag != "E")
                        {
                            if (!UString.estaVacio(estado.Codigo))
                            {
                                PsSaludAyuda psSaludEstado = new PsSaludAyuda();

                                psSaludEstado.IdBeneficiario = bean.IdBeneficiario;
                                psSaludEstado.IdInstitucion = bean.IdInstitucion;
                                psSaludEstado.IdSalud = psSalud.IdSalud;
                                psSaludEstado.IdAyuda = estado.Codigo;
                                psSaludAyudaDao.registrar(psSaludEstado);
                            }
                        }
                    }
                }
            }

            if (bean.listaBioMecanica != null)
            {
                if (bean.listaBioMecanica.Count > 0)
                {

                    foreach (var biomecanica in bean.listaBioMecanica)
                    {
                        if (biomecanica.valorFlag != "E")
                        {
                            PsSaludBiomecanica psSaludBiomecanica = new PsSaludBiomecanica();
                            psSaludBiomecanica.IdBeneficiario = bean.IdBeneficiario;
                            psSaludBiomecanica.IdInstitucion = bean.IdInstitucion;
                            psSaludBiomecanica.IdSalud = psSalud.IdSalud;

                            psSaludBiomecanica.IdEstadoAyuda = biomecanica.Codigo;
                            psSaludBiomecanica.IdTipoAyuda = biomecanica.IdTipoAyuda;

                            psSaludBiomecanicaDao.registrar(psSaludBiomecanica);
                        }
                    }
                }
            }


            if (bean.listaExamenes != null)
            {
                if (bean.listaExamenes.Count > 0)
                {

                    foreach (var examen in bean.listaExamenes)
                    {
                        if (examen.valorFlag != "E")
                        {
                            PsSaludExamen psSaludExamen = new PsSaludExamen();
                            psSaludExamen.IdBeneficiario = bean.IdBeneficiario;
                            psSaludExamen.IdInstitucion = bean.IdInstitucion;
                            psSaludExamen.IdSalud = psSalud.IdSalud;

                            psSaludExamen.IdExamen = examen.Codigo;
                            psSaludExamen.IdResultado = examen.IdResultado;
                            psSaludExamen.IdResultadoNumerico = examen.IdResultadoNumerico;

                            psSaludExamenDao.registrar(psSaludExamen);
                        }
                    }
                }
            }

            if (bean.listaSubCondicion != null)
            {

                if (bean.listaSubCondicion.Count > 0)
                {

                    foreach (var subCondicion in bean.listaSubCondicion)
                    {
                        if (subCondicion.valorFlag != "E")
                        {
                            PsSaludSubcondicion psSaludSubcondicion = new PsSaludSubcondicion();
                            psSaludSubcondicion.IdBeneficiario = bean.IdBeneficiario;
                            psSaludSubcondicion.IdInstitucion = bean.IdInstitucion;
                            psSaludSubcondicion.IdSalud = psSalud.IdSalud;

                            psSaludSubcondicion.IdSubcondicion = subCondicion.IdSubCondicion;
                            psSaludSubcondicion.IdCondicion = subCondicion.Codigo;

                            psSaludSubcondicionDao.registrar(psSaludSubcondicion);
                        }
                    }
                }
            }

            if (bean.listaTerapias != null)
            {
                if (bean.listaTerapias.Count > 0)
                {

                    foreach (var terapias in bean.listaTerapias)
                    {
                        if (terapias.valorFlag != "E")
                        {
                            PsSaludTerapia psSaludTerapia = new PsSaludTerapia();
                            psSaludTerapia.IdBeneficiario = bean.IdBeneficiario;
                            psSaludTerapia.IdInstitucion = bean.IdInstitucion;
                            psSaludTerapia.IdSalud = psSalud.IdSalud;

                            psSaludTerapia.IdTerapia = terapias.Codigo;

                            psSaludTerapiaDao.registrar(psSaludTerapia);
                        }
                    }
                }
            }

            if (bean.listaTratramiento != null)
            {
                if (bean.listaTratramiento.Count > 0)
                {

                    foreach (var tratamiento in bean.listaTratramiento)
                    {
                        if (tratamiento.valorFlag != "E")
                        {
                            PsSaludTratamiento psSaludTratamiento = new PsSaludTratamiento();
                            psSaludTratamiento.IdBeneficiario = bean.IdBeneficiario;
                            psSaludTratamiento.IdInstitucion = bean.IdInstitucion;
                            psSaludTratamiento.IdSalud = psSalud.IdSalud;

                            psSaludTratamiento.IdTratamiento = tratamiento.Codigo;

                            psSaludTratamientoDao.registrar(psSaludTratamiento);
                        }
                    }
                }

            }


            if (bean.listaDiagnostico != null)
            {
                if (bean.listaDiagnostico.Count > 0)
                {

                    foreach (var diagnostico in bean.listaDiagnostico)
                    {
                        if (diagnostico.valorFlag != "E")
                        {
                            PsSaludDiagnostico psSaludDiagnostico = new PsSaludDiagnostico();
                            psSaludDiagnostico.IdBeneficiario = bean.IdBeneficiario;
                            psSaludDiagnostico.IdInstitucion = bean.IdInstitucion;
                            psSaludDiagnostico.IdSalud = psSalud.IdSalud;

                            psSaludDiagnostico.IdDiagnostico = diagnostico.Codigo;

                            psSaludDiagnosticoDao.registrar(psSaludDiagnostico);
                        }
                    }
                }
            }





            return psSalud;
        }

        public void coreEliminar(String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud)
        {
            coreEliminar(new PsSaludPk(pIdInstitucion, pIdBeneficiario, pIdSalud));
        }

        public void coreEliminar(PsSaludPk id)
        {
            PsSalud bean = obtenerPorId(id.obtenerArreglo());
            if (bean != null)
                this.eliminar(bean);
        }

        public PsSalud coreAnular(UsuarioActual usuarioActual, PsSaludPk id)
        {
            PsSalud bean = base.obtenerPorId(id.obtenerArreglo());
            if (bean == null)
                return null;
            return null;//coreActualizar(usuarioActual, bean);
        }

        public PsSalud coreAnular(UsuarioActual usuarioActual, String pIdInstitucion, Nullable<Int32> pIdBeneficiario, Nullable<Int32> pIdSalud)
        {
            return coreAnular(usuarioActual, new PsSaludPk(pIdInstitucion, pIdBeneficiario, pIdSalud));
        }

        public ParametroPaginacionGenerico listarSaludPaginacion(ParametroPaginacionGenerico paginacion, FiltroSalud filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPsSalud> lstResultado = new List<DtoPsSalud>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
            }


            parametros.Add(new ParametroPersistenciaGenerico("p_periodo", ConstanteUtil.TipoDato.String, filtro.Periodo));
            parametros.Add(new ParametroPersistenciaGenerico("p_institucion", ConstanteUtil.TipoDato.String, filtro.IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("p_area", ConstanteUtil.TipoDato.String, filtro.IdArea));
            parametros.Add(new ParametroPersistenciaGenerico("p_sexo", ConstanteUtil.TipoDato.String, filtro.IdSexo));
            parametros.Add(new ParametroPersistenciaGenerico("p_nombreCompleto", ConstanteUtil.TipoDato.String, filtro.NombreCompleto));
            parametros.Add(new ParametroPersistenciaGenerico("p_numpagina", ConstanteUtil.TipoDato.Integer, paginacion.RegistroInicio));
            parametros.Add(new ParametroPersistenciaGenerico("p_numregistros", ConstanteUtil.TipoDato.Integer, paginacion.CantidadRegistrosDevolver));
            parametros.Add(new ParametroPersistenciaGenerico("p_idPrograma", ConstanteUtil.TipoDato.String, filtro.IdPrograma));

            _reader = this.listarPorQuery("pssalud.listarPaginacion", parametros);




            while (_reader.Read())
            {
                DtoPsSalud bean = new DtoPsSalud();

                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdSalud = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.NombreCompleto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.IdPeriodo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Comentarios = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Estado = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Hemoglobina = _reader.GetDecimal(8);
                if (!_reader.IsDBNull(9))
                    bean.HemoglobinaResultado = _reader.GetString(9);



                if (!_reader.IsDBNull(10))
                    bean.IdAyudaMedica = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdDescarteSerologico = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdDescarteTbc = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.IdTratamientoAnemia = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.IdHta = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.IdTbc = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdDiabetes = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.IdCognitivo = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdAfectivo = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.IdOsteoatrosis = _reader.GetString(19);

                if (!_reader.IsDBNull(20))
                    bean.IdInstitucionNombre = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.IdPrograma = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.EsNuevo = _reader.GetString(22);

                if (!_reader.IsDBNull(23))
                    bean.IdPruebaVIH = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.EdadMenarquia = _reader.GetInt32(24);
                if (!_reader.IsDBNull(25))
                    bean.FechaUltimaMestruacion = _reader.GetDateTime(25);
                if (!_reader.IsDBNull(26))
                    bean.OtrasEnfermedades = _reader.GetString(26);

                if (!_reader.IsDBNull(27))
                    bean.AyudaMedicaNombre = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    bean.AfectivoNombre = _reader.GetString(28);
                if (!_reader.IsDBNull(29))
                    bean.CognitivoNombre = _reader.GetString(29);
                if (!_reader.IsDBNull(30))
                    contador = _reader.GetInt32(30);
                if (!_reader.IsDBNull(31))
                    bean.Evaluado = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.Edad = _reader.GetInt32(32);

                if (!_reader.IsDBNull(33))
                    bean.Secuencia = Convert.ToInt32(_reader.GetInt64(33));
                if (!_reader.IsDBNull(34))
                    bean.sexo = _reader.GetString(34);


                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;
        }

        public DtoPsSalud obtenerPorListados(string IdInstitucion, int? IdBeneficiario, int? IdSalud)
        {

            DtoPsSalud listas = new DtoPsSalud();

            if (IdSalud == null || IdSalud == 0)
            {
                listas.listaAyuda = new List<DtoTabla>();
                listas.listaEstado = new List<DtoTabla>();
                listas.listaExamenes = new List<DtoTabla>();
                listas.listaSubCondicion = new List<DtoTabla>();
                listas.listaBioMecanica = new List<DtoTabla>();
                listas.listaTerapias = new List<DtoTabla>();
                listas.listaDiscapacidad = new List<DtoTabla>();
                listas.listaTratramiento = new List<DtoTabla>();
                listas.listaDiagnostico = new List<DtoTabla>();
            }
            else
            {
                List<PsSaludAyuda> lista0 = psSaludAyudaDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludEstado> lista1 = psSaludEstadoDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludBiomecanica> lista2 = psSaludBiomecanicaDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludTerapia> lista3 = psSaludTerapiaDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludExamen> lista4 = psSaludExamenDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludSubcondicion> lista5 = psSaludSubcondicionDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludDiscapacidad> lista6 = psSaludDiscapacidadDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludDiagnostico> lista7 = psSaludDiagnosticoDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);
                List<PsSaludTratamiento> lista8 = psSaludTratamientoDao.obtenerListado(IdInstitucion, IdBeneficiario, IdSalud);

                listas.listaAyuda = new List<DtoTabla>();
                listas.listaEstado = new List<DtoTabla>();
                listas.listaExamenes = new List<DtoTabla>();
                listas.listaSubCondicion = new List<DtoTabla>();
                listas.listaBioMecanica = new List<DtoTabla>();
                listas.listaTerapias = new List<DtoTabla>();
                listas.listaDiscapacidad = new List<DtoTabla>();
                listas.listaTratramiento = new List<DtoTabla>();
                listas.listaDiagnostico = new List<DtoTabla>();


                if (lista0.Count > 0)
                {
                    foreach (var estado in lista0)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = estado.IdAyuda;
                        dto.valorFlag = "M";
                        listas.listaAyuda.Add(dto);
                    }
                }

                if (lista1.Count > 0)
                {
                    foreach (var estado in lista1)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = estado.IdEstado;
                        dto.valorFlag = "M";
                        listas.listaEstado.Add(dto);
                    }
                }
                if (lista2.Count > 0)
                {
                    foreach (var biomecanica in lista2)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = biomecanica.IdEstadoAyuda;
                        dto.IdTipoAyuda = biomecanica.IdTipoAyuda;
                        dto.valorFlag = "M";
                        listas.listaBioMecanica.Add(dto);
                    }

                }
                if (lista3.Count > 0)
                {
                    foreach (var terapia in lista3)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = terapia.IdTerapia;
                        dto.valorFlag = "M";
                        listas.listaTerapias.Add(dto);
                    }

                }
                if (lista4.Count > 0)
                {
                    foreach (var examen in lista4)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = examen.IdExamen;
                        dto.IdResultado = examen.IdResultado;
                        dto.IdResultadoNumerico = examen.IdResultadoNumerico;
                        dto.valorFlag = "M";
                        listas.listaExamenes.Add(dto);
                    }

                }
                if (lista5.Count > 0)
                {
                    foreach (var condicion in lista5)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = condicion.IdCondicion;
                        dto.IdSubCondicion = condicion.IdSubcondicion;
                        dto.valorFlag = "M";
                        listas.listaSubCondicion.Add(dto);
                    }

                }

                if (lista6.Count > 0)
                {
                    foreach (var dispacidad in lista6)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = dispacidad.IdDiscapacidad;
                        dto.valorFlag = "M";
                        listas.listaDiscapacidad.Add(dto);

                    }

                }


                if (lista7.Count > 0)
                {
                    foreach (var diagnostico in lista7)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = diagnostico.IdDiagnostico;
                        dto.valorFlag = "M";
                        listas.listaDiagnostico.Add(dto);

                    }

                }

                if (lista8.Count > 0)
                {
                    foreach (var tratamiento in lista8)
                    {
                        DtoTabla dto = new DtoTabla();
                        dto.Codigo = tratamiento.IdTratamiento;
                        dto.valorFlag = "M";
                        listas.listaTratramiento.Add(dto);

                    }

                }


            }



            return listas;
        }

        public DtoTabla calcularHemoglobina(DtoPsSalud bean)
        {

            DtoNutricion psNutricion = new DtoNutricion();
            psNutricion.IdBeneficiario = bean.IdBeneficiario;
            psNutricion.Peso = null;
            psNutricion.Talla = null;
            psNutricion.Hemoglobina = bean.Hemoglobina;

            DtoNutricion obj = psNutricionDao.obtenerCalculos(psNutricion);

            DtoTabla calculo = new DtoTabla();
            calculo.Descripcion = obj.HemoglobinaNombre;
            return calculo;

        }

        public List<DtoPsSalud> listarDiagnosticos(string IdInstitucion, int? IdBeneficiario, string IdTipoRas)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPsSalud> lstResultado = new List<DtoPsSalud>();

            if (String.IsNullOrEmpty(IdTipoRas))
            {
                IdTipoRas = null;
            }

            parametros.Add(new ParametroPersistenciaGenerico("pInstitucion", ConstanteUtil.TipoDato.String, IdInstitucion));
            parametros.Add(new ParametroPersistenciaGenerico("pIdBeneficiario", ConstanteUtil.TipoDato.Integer, IdBeneficiario));
            parametros.Add(new ParametroPersistenciaGenerico("TipoRas", ConstanteUtil.TipoDato.String, IdTipoRas));

            _reader = this.listarPorQuery("pssalud.listarDiagnosticos", parametros);

            while (_reader.Read())
            {
                DtoPsSalud bean = new DtoPsSalud();

                if (!_reader.IsDBNull(0))
                    bean.IdBeneficiario = _reader.GetInt32(0);
                if (!_reader.IsDBNull(1))
                    bean.IdInstitucion = _reader.GetString(1);
                if (!_reader.IsDBNull(2))
                    bean.CantDiagnosticos = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.DiagnosticosNombre = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.IdDiagnostico = _reader.GetInt32(4);
                if (!_reader.IsDBNull(5))
                    bean.TipoRas = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Secuencia = _reader.GetInt64(6);


                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;
        }

        public List<DtoPsSalud> listarTratamientos(string IdInstitucion, int? IdBeneficiario, int? IdDetalle, int? IdDiagnostico)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPsSalud> lstResultado = new List<DtoPsSalud>();

            parametros.Add(new ParametroPersistenciaGenerico("pIdDiagnostico", ConstanteUtil.TipoDato.Integer, IdDiagnostico));
            parametros.Add(new ParametroPersistenciaGenerico("p_Beneficiario", ConstanteUtil.TipoDato.Integer, IdBeneficiario));

            _reader = this.listarPorQuery("pssalud.listarTratamientos", parametros);

            while (_reader.Read())
            {
                DtoPsSalud bean = new DtoPsSalud();

                if (!_reader.IsDBNull(0))
                    bean.TratamientoNombre = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.CantTratamientos = _reader.GetInt32(1);

                lstResultado.Add(bean);
            }

            this.dispose();

            return lstResultado;

        }

        public ParametroPaginacionGenerico listarPaginacionConsulta(ParametroPaginacionGenerico paginacion, FiltroSalud filtro)
        {
            List<ParametroPersistenciaGenerico> parametros = new List<ParametroPersistenciaGenerico>();
            List<DtoPsSalud> lstResultado = new List<DtoPsSalud>();
            Int32 contador = 0;

            if (String.IsNullOrEmpty(filtro.NombreCompleto))
            {
                filtro.NombreCompleto = null;
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


            _reader = this.listarPorQuery("pssalud.listarPaginacionConsulta", parametros);

            while (_reader.Read())
            {
                DtoPsSalud bean = new DtoPsSalud();

                if (!_reader.IsDBNull(0))
                    bean.IdInstitucion = _reader.GetString(0);
                if (!_reader.IsDBNull(1))
                    bean.IdBeneficiario = _reader.GetInt32(1);
                if (!_reader.IsDBNull(2))
                    bean.IdSalud = _reader.GetInt32(2);
                if (!_reader.IsDBNull(3))
                    bean.NombreCompleto = _reader.GetString(3);
                if (!_reader.IsDBNull(4))
                    bean.FechaInforme = _reader.GetDateTime(4);
                if (!_reader.IsDBNull(5))
                    bean.IdPeriodo = _reader.GetString(5);
                if (!_reader.IsDBNull(6))
                    bean.Comentarios = _reader.GetString(6);
                if (!_reader.IsDBNull(7))
                    bean.Estado = _reader.GetString(7);
                if (!_reader.IsDBNull(8))
                    bean.Hemoglobina = _reader.GetDecimal(8);
                if (!_reader.IsDBNull(9))
                    bean.HemoglobinaResultado = _reader.GetString(9);
                if (!_reader.IsDBNull(10))
                    bean.IdAyudaMedica = _reader.GetString(10);
                if (!_reader.IsDBNull(11))
                    bean.IdDescarteSerologico = _reader.GetString(11);
                if (!_reader.IsDBNull(12))
                    bean.IdDescarteTbc = _reader.GetString(12);
                if (!_reader.IsDBNull(13))
                    bean.IdTratamientoAnemia = _reader.GetString(13);

                if (!_reader.IsDBNull(14))
                    bean.IdHta = _reader.GetString(14);
                if (!_reader.IsDBNull(15))
                    bean.IdTbc = _reader.GetString(15);
                if (!_reader.IsDBNull(16))
                    bean.IdDiabetes = _reader.GetString(16);
                if (!_reader.IsDBNull(17))
                    bean.IdCognitivo = _reader.GetString(17);
                if (!_reader.IsDBNull(18))
                    bean.IdAfectivo = _reader.GetString(18);
                if (!_reader.IsDBNull(19))
                    bean.IdOsteoatrosis = _reader.GetString(19);

                if (!_reader.IsDBNull(20))
                    bean.IdInstitucionNombre = _reader.GetString(20);
                if (!_reader.IsDBNull(21))
                    bean.IdPrograma = _reader.GetString(21);
                if (!_reader.IsDBNull(22))
                    bean.EsNuevo = _reader.GetString(22);
                if (!_reader.IsDBNull(23))
                    bean.NombreAyudaMedica = _reader.GetString(23);
                if (!_reader.IsDBNull(24))
                    bean.NombreCognitivo = _reader.GetString(24);
                if (!_reader.IsDBNull(25))
                    bean.NombreAfectivo = _reader.GetString(25);
                if (!_reader.IsDBNull(26))
                    bean.Evaluado = _reader.GetString(26);
                if (!_reader.IsDBNull(27))
                    bean.OtrasEnfermedades = _reader.GetString(27);
                if (!_reader.IsDBNull(28))
                    contador = _reader.GetInt32(28);









                if (!_reader.IsDBNull(29))
                    bean.Id_Nombre = _reader.GetString(29);
                if (!_reader.IsDBNull(30))
                    bean.Id_Nombre2 = _reader.GetString(30);
                if (!_reader.IsDBNull(31))
                    bean.Id_Nombre3 = _reader.GetString(31);
                if (!_reader.IsDBNull(32))
                    bean.Id_Nombre_ayuda_biomecanica = _reader.GetString(32);
                if (!_reader.IsDBNull(33))
                    bean.Id_Nombre_ayuda_biomecanica2 = _reader.GetString(33);
                if (!_reader.IsDBNull(34))
                    bean.Id_Nombre_ayuda_biomecanica3 = _reader.GetString(34);
                if (!_reader.IsDBNull(35))
                    bean.Id_Nombre_estado_ayuda_biomecanica = _reader.GetString(35);
                if (!_reader.IsDBNull(36))
                    bean.Id_Nombre_estado_ayuda_biomecanica2 = _reader.GetString(36);
                if (!_reader.IsDBNull(37))
                    bean.Id_Nombre_estado_ayuda_biomecanica3 = _reader.GetString(37);
                if (!_reader.IsDBNull(38))
                    bean.Id_Nombre_diagnostico = _reader.GetString(38);
                if (!_reader.IsDBNull(39))
                    bean.Id_Nombre_diagnostico2 = _reader.GetString(39);
                if (!_reader.IsDBNull(40))
                    bean.Id_Nombre_diagnostico3 = _reader.GetString(40);
                if (!_reader.IsDBNull(41))
                    bean.Id_Nombre_discapacidad = _reader.GetString(41);
                if (!_reader.IsDBNull(42))
                    bean.Id_Nombre_discapacidad2 = _reader.GetString(42);
                if (!_reader.IsDBNull(43))
                    bean.Id_Nombre_discapacidad3 = _reader.GetString(43);
                if (!_reader.IsDBNull(44))
                    bean.Id_Nombre_estado = _reader.GetString(44);
                if (!_reader.IsDBNull(45))
                    bean.Id_Nombre_estado2 = _reader.GetString(45);
                if (!_reader.IsDBNull(46))
                    bean.Id_Nombre_estado3 = _reader.GetString(46);
                if (!_reader.IsDBNull(47))
                    bean.Id_Nombre_examen = _reader.GetString(47);
                if (!_reader.IsDBNull(48))
                    bean.Id_Nombre_resultado = _reader.GetString(48);
                if (!_reader.IsDBNull(49))
                    bean.Id_Nombre_examen2 = _reader.GetString(49);
                if (!_reader.IsDBNull(50))
                    bean.Id_Nombre_resultado2 = _reader.GetString(50);
                if (!_reader.IsDBNull(51))
                    bean.Id_Nombre_examen3 = _reader.GetString(51);
                if (!_reader.IsDBNull(52))
                    bean.Id_Nombre_resultado3 = _reader.GetString(52);
                if (!_reader.IsDBNull(53))
                    bean.Id_Nombre_condicion = _reader.GetString(53);
                if (!_reader.IsDBNull(54))
                    bean.Id_Nombre_sub_condicion = _reader.GetString(54);
                if (!_reader.IsDBNull(55))
                    bean.Id_Nombre_condicion2 = _reader.GetString(55);
                if (!_reader.IsDBNull(56))
                    bean.Id_Nombre_sub_condicion2 = _reader.GetString(56);
                if (!_reader.IsDBNull(57))
                    bean.Id_Nombre_condicion3 = _reader.GetString(57);
                if (!_reader.IsDBNull(58))
                    bean.Id_Nombre_sub_condicion3 = _reader.GetString(58);

                if (!_reader.IsDBNull(59))
                    bean.Id_Nombre_terapia = _reader.GetString(59);
                if (!_reader.IsDBNull(60))
                    bean.Id_Nombre_terapia2 = _reader.GetString(60);
                if (!_reader.IsDBNull(61))
                    bean.Id_Nombre_terapia3 = _reader.GetString(61);
                if (!_reader.IsDBNull(62))
                    bean.Id_Nombre_tratamiento = _reader.GetString(62);
                if (!_reader.IsDBNull(63))
                    bean.Id_Nombre_tratamiento2 = _reader.GetString(63);
                if (!_reader.IsDBNull(64))
                    bean.Id_Nombre_tratamiento3 = _reader.GetString(64);


                if (!_reader.IsDBNull(65))
                    bean.Edad = _reader.GetInt32(65);
                if (!_reader.IsDBNull(66))
                    bean.sexo = _reader.GetString(66);
                if (!_reader.IsDBNull(67))
                    bean.Secuencia = _reader.GetInt32(67);

                lstResultado.Add(bean);
            }

            paginacion.RegistroEncontrados = contador;
            paginacion.ListaResultado = lstResultado;

            this.dispose();

            return paginacion;

        }


    }
}
