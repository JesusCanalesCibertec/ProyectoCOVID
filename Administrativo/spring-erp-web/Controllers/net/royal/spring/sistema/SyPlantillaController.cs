using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using net.royal.spring.sistema.servicio;
using net.royal.spring.framework.web.controller;
using net.royal.spring.sistema.dominio;
using net.royal.spring.framework.core.dominio;
using net.royal.spring.framework.core;
using static net.royal.spring.framework.core.dominio.MensajeUsuario;
using net.royal.spring.framework.core.dominio.dto;
using net.royal.spring.core.servicio;
using net.royal.spring.covid.servicio;

namespace net.royal.spring.sistema
{
    [Route("api/spring/sistema/[controller]")]
    public class SyPlantillaController : SecuredBaseController
    {
        private IServiceProvider servicioProveedor;
        private SyPlantillaServicio syPlantillaServicio;
        private PaisServicio paisServicio;

        public SyPlantillaController(IServiceProvider _servicioProveedor, IHttpContextAccessor httpContextAccessor) : base(_servicioProveedor, httpContextAccessor)
        {
            servicioProveedor = _servicioProveedor;
            syPlantillaServicio = servicioProveedor.GetService<SyPlantillaServicio>();
            paisServicio = servicioProveedor.GetService<PaisServicio>();
        }

        /*TEST*/
        [HttpGet("[action]")]
        public JsonResult obtenerFormulario(String codigo)
        {
            //el primer combo de los combos anidados si se carga
            var paises = paisServicio.listarTodos();
            var paisesItem = new List<SelectItemSpring>();

            foreach (var pais in paises)
            {
                paisesItem.Add(new SelectItemSpring()
                {
       
                });
            }

            var formulario = new Formulario()
            {
                nombre = "FORMULARIO ACCIDENTES",
                grupos = new List<Grupo>()
                {
                    new Grupo()
                    {
                        id=0,
                        isFieldset = true,
                        fieldsetTitle="Datos del solicitante",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Solicitante",
                            },
                             new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="TE",
                                defaultNuevoRegistro=_usuarioActual.PersonaId,
                                editable=false,
                            },
                             new Control()
                            {
                                id=2,
                                clase="ui-g-12 ui-md-6 ui-lg-3",
                                tipoControl="TE",
                                defaultNuevoRegistro=_usuarioActual.PersonaNombreCompleto,
                                editable=false,
                            },
                        }
                    },
                    new Grupo()
                    {
                        id=1,
                        isFieldset = true,
                        fieldsetTitle="Información general",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Empleado *",
                            },
                            new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-4",
                                tipoControl="SE",
                                tipoSelector="SELECTOR_EMPLEADO",
                                eliminar=true,
                                requerido=true,
                                descripcionAtributoRequerido=" EMPLEADO"
                            },
                            new Control()
                            {
                                id=2,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Lugar",
                            },
                            new Control()
                            {
                                id=3,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="CB",
                                selectItem = new List<SelectItemSpring>
                                {
                                    new SelectItemSpring()
                                    {
                                        value=null,
                                        label=" -- Seleccionar -- "
                                    },
                                    new SelectItemSpring()
                                    {
                                        value="01",
                                        label="ADMINISTRACIÓN",
                                        habilita=new List<int?[]>()
                                        {
                                            new int?[]{ 4, 1}
                                        }
                                    },
                                     new SelectItemSpring()
                                    {
                                        value="02",
                                        label="CONSULTORÍA",
                                        muestra=new List<int?[]>()
                                        {
                                            //cuando este en consultoria, muestra el control de las tablas
                                            //si se le pasa el segundo numero como null, todo el grupo sera afectado
                                            new int?[]{2, 0 },
                                            new int?[]{3, null }
                                        }
                                    },
                                     new SelectItemSpring()
                                    {
                                        value="03",
                                        label="LOGÍSTICA - COMPRAS"
                                    }
                                }
                            },
                             new Control()
                            {
                                id=4,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Correo",
                            },
                             new Control()
                            {
                                id=5,
                                clase="ui-g-12 ui-md-6 ui-lg-3",
                                tipoControl="TE",
                                maxLength=4,
                                expresionRegular="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*;$",
                                mensajeExpresionRegular="El correo debe contener el '@' y terminar con '; '"
                            },
                            new Control()
                            {
                                id=7,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Número",
                            },
                            new Control()
                            {
                                id=8,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="NU",
                                minimo = 0,
                                maximo = 5
                            },
                            new Control()
                            {
                                id=9,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="¿Pregunta? *",
                            },
                            new Control()
                            {
                                id=10,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="RA",
                                requerido=true,
                                //LA DESCRIPCION A MOSTRAR PARA EL MENSAJE DE REQUERIDO SIEMPRE SE COLOCA EN EL CONTROL DEL INPUT , NO EN LA ETIQUETA
                                descripcionAtributoRequerido="PREGUNTA",
                                selectItem=new List<SelectItemSpring>()
                                {
                                    new SelectItemSpring()
                                    {
                                        value="S",
                                        label="Si",
                                        habilita=new List<int?[]>()
                                        {
                                            new int?[]{ 4, 7}
                                        }
                                    },
                                    new SelectItemSpring()
                                    {
                                        value="N",
                                        label="No",
                                        muestra=new List<int?[]>()
                                        {
                                            new int?[]{ 4, 9}
                                        }
                                    }
                                }
                            },
                             new Control()
                            {
                                id=11,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Deportes *",
                            },
                             new Control()
                            {
                                id=12,
                                clase="ui-g-12 ui-md-6 ui-lg-3",
                                tipoControl="CH",
                                multiple=true,
                                descripcionAtributoRequerido="DEPORTES",
                                //especifica el rango de cuantos puede seleccionar
                                minimo =1,
                                maximo =2,
                                //oculto = true,
                                selectItem =new List<SelectItemSpring>
                                {
                                    new SelectItemSpring()
                                    {
                                        //ESTO NO ES EL VALOR QUE SE GUARDA, SINO UN DIFERENCIADOR PARA LUEGO SABER QUE CAMPO SE HA MARCADO CUANDO SON MULTIPLES
                                        value="F",
                                        label="Fútbol",
                                        muestra=new List<int?[]>()
                                        {
                                            new int?[]{ 5, null }
                                        }
                                    },
                                    new SelectItemSpring()
                                    {
                                        value="B",
                                        label="Basketball",
                                        habilita=new List<int?[]>()
                                        {
                                            new int?[]{ 6, 1}
                                        }
                                    },
                                    new SelectItemSpring()
                                    {
                                        value="G",
                                        label="Golf"
                                    }
                                }
                            },
                            new Control()
                            {
                                id=13,
                                clase="ui-g-12 ui-md-6 ui-lg-3",
                                tipoControl="LB",
                                texto="",
                            },
                             new Control()
                            {
                                id=14,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Fecha",
                            },
                             new Control()
                            {
                                id=14,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="FE",
                            },
                             new Control()
                            {
                                id=16,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Hora",
                            },
                              new Control()
                            {
                                id=17,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="HO",
                            },
                        }
                    },
                    new Grupo()
                    {
                        id=2,
                        isFieldset = true,
                        fieldsetTitle="Consecuencias",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles = new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl = "DT",
                                minimo=1,
                                maximo=5,
                                descripcionAtributoRequerido="CONSECUENCIAS",
                                columnas = new List<Columna>()
                                {
                                    new Columna(){
                                        field="id",
                                        header ="Código",
                                        align="center",
                                        tipoColumna="NU",
                                        ancho=10
                                    },
                                    new Columna(){
                                        field="descripcion",
                                        header ="Descripción *",
                                        tipoColumna="TE",
                                        editable=true,
                                        maxLength=100,
                                        requerido=true,
                                        //no se le pone un ancho par que tome el espacio sobrante
                                        expresionRegular="^[a-zA-Z_áéíóúñ\\s]*$",
                                        mensajeExpresionRegular="Solo ingresar letras en la descripción",
                                        defaultNuevoRegistro="VALOR DEFAULT EN NUEVO"
                                    },
                                    new Columna(){
                                        field="soles",
                                        header ="Precio soles",
                                        align="right",
                                        tipoColumna="MO",
                                        prefijo="S./",
                                        ancho=10,
                                        editable=true,
                                    },
                                    new Columna(){
                                        field="dolares",
                                        header ="Precio dólares",
                                        align="right",
                                        tipoColumna="MO",
                                        prefijo="$./",
                                        ancho=10,
                                        editable=true,
                                    },
                                    new Columna(){
                                        field="estado",
                                        header ="Estado",
                                        tipoColumna="CB",
                                        ancho=10,
                                        editable=true,
                                        defaultNuevoRegistro="A",
                                        opciones=new List<SelectItemSpring>()
                                        {
                                            new SelectItemSpring()
                                            {
                                                value=null,
                                                label=" -- Seleccionar -- "
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="A",
                                                label="Activo"
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="I",
                                                label="Inactivo"
                                            },
                                        }
                                    },
                                },
                                //EL EDITABLE SE DEFINIRA POR COLUMNA, POR DEFAULT SE COLOCA EN FALSE
                                //editable = true,
                                eliminar = true,
                            }
                        }
                    },
                    new Grupo()
                    {
                        id=3,
                        isFieldset = true,
                        fieldsetTitle="Cursos",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles = new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl = "DT",
                                minimo=1,
                                maximo=2,
                                descripcionAtributoRequerido="CURSO",
                                columnas = new List<Columna>()
                                {
                                    new Columna(){
                                        field="id",
                                        header ="Código",
                                        align="center",
                                        tipoColumna="NU",
                                        ancho=10
                                    },
                                    new Columna(){
                                        field="centro",
                                        header ="Centro de costos *",
                                        tipoColumna="SE",
                                        tipoSelector="SELECTOR_CENTROCOSTO",
                                        editable=true,
                                        requerido=true,
                                    },
                                    new Columna(){
                                        field="fecha",
                                        header ="Fecha",
                                        tipoColumna="FE",
                                        editable=true,
                                        ancho=10,
                                        formatoFechaInput="dd/mm/yy",
                                        formatoFechaOutput="dd/MM/yy"
                                    },
                                     new Columna(){
                                        field="fecha2",
                                        header ="Fecha2",
                                        tipoColumna="FE",
                                        editable=true,
                                        ancho=10,
                                        //este formato es de PrimeNG
                                        formatoFechaInput="dd-mm-yy",
                                        //este formato es de DatePipe de Angular
                                        formatoFechaOutput="dd-MM-yy"
                                    },
                                    new Columna(){
                                        field="nivel",
                                        header ="Nivel",
                                        tipoColumna="CB",
                                        ancho=10,
                                        editable=true,
                                        defaultNuevoRegistro="B",
                                        opciones=new List<SelectItemSpring>()
                                        {
                                            new SelectItemSpring()
                                            {
                                                value=null,
                                                label=" -- Seleccionar -- "
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="A",
                                                label="Alto"
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="M",
                                                label="Medio"
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="B",
                                                label="Bajo"
                                            },
                                        }
                                    },
                                    new Columna(){
                                        field="estado",
                                        header ="Estado",
                                        tipoColumna="CB",
                                        ancho=10,
                                        editable=true,
                                        defaultNuevoRegistro="A",
                                        opciones=new List<SelectItemSpring>()
                                        {
                                            new SelectItemSpring()
                                            {
                                                value=null,
                                                label=" -- Seleccionar -- "
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="A",
                                                label="Activo"
                                            },
                                            new SelectItemSpring()
                                            {
                                                value="I",
                                                label="Inactivo"
                                            },
                                        }
                                    },
                                },
                                eliminar = true,
                            }
                        }
                    },
                    new Grupo()
                    {
                        id = 4,
                        isFieldset=true,
                        fieldsetTitle="Preguntas",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="¿Pregunta 1?",
                            },
                            new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TA",
                                maxLength=500,
                            },
                            new Control()
                            {
                                id=2,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="¿Pregunta 2?",
                            },
                            new Control()
                            {
                                id=3,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TA",
                                maxLength=500,
                            },
                            new Control()
                            {
                                id=4,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="¿Pregunta 3?",
                            },
                            new Control()
                            {
                                id=5,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TA",
                                maxLength=500,
                                editable=false
                            },
                            new Control()
                            {
                                id=6,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="¿Pregunta 4?",
                            },
                            new Control()
                            {
                                id=7,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TA",
                                maxLength=500,
                            },
                            new Control()
                            {
                                id=8,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="¿Pregunta 5?",
                            },
                            new Control()
                            {
                                id=9,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TA",
                                maxLength=500,
                            }
                        }
                    },
                    new Grupo()
                     {
                        id = 5,
                        isFieldset=true,
                        fieldsetTitle="Futbol",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="Prueba sdasd",
                            },
                            new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TE",
                                maxLength=10,
                            },
                        }
                     },
                    new Grupo()
                     {
                        id = 6,
                        isFieldset=true,
                        fieldsetTitle="Basket",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="LB",
                                texto="Prueba sdasd",
                            },
                            new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl="TE",
                                maxLength=10,
                            },
                        }
                     },
                    new Grupo()
                     {
                        id = 7,
                        isFieldset=true,
                        fieldsetTitle="Empleados multiples",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-12",
                                tipoControl = "DT",
                                minimo=1,
                                maximo=5,
                                descripcionAtributoRequerido="EMPLEADOS MULTIPLES",
                                fieldSelector="empleado",
                                tipoSelector="SELECTOR_EMPLEADO_MULTIPLE",
                                multiple=true,
                                columnas = new List<Columna>()
                                {
                                    new Columna(){
                                        field="id",
                                        header ="Código",
                                        align="center",
                                        tipoColumna="NU",
                                        ancho=10
                                    },
                                    new Columna(){
                                        field="empleado",
                                        header ="Empleado",
                                        tipoColumna="C_SE",
                                    }
                                },
                                eliminar = true,
                            }
                        }
                     },
                    new Grupo()
                    {
                        id = 8,
                        isFieldset=true,
                        fieldsetTitle="Combos anidados",
                        clase="ui-g-12 ui-md-6 ui-lg-12",
                        controles=new List<Control>()
                        {
                            new Control()
                            {
                                id=0,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Pais",
                            },
                            new Control()
                            {
                                id=1,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="CB",
                                selectItem = paisesItem,
                                controla=new List<int?[]>(){
                                    new int?[]{ 8, 3}
                                },
                                nombreParametro="idPais"
                            },
                            new Control()
                            {
                                id=2,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Departamento",
                            },
                            new Control()
                            {
                                id=3,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="CB",
                                selectItem = new List<SelectItemSpring>(),
                                controla = new List<int?[]>(){
                                    new int?[]{ 8, 5}
                                },
                                padreControla = new List<int?[]>(){
                                    new int?[]{ 8, 1}
                                },
                                nombreParametro="idDepartamento",
                                buscarEn="api/spring/core/Departamento/listarActivosPorPais",

                                nombreValueFromRest ="departamento",
                                nombreLabelFromRest="descripcioncorta",
                            },
                            new Control()
                            {
                                id=4,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Provincia",
                            },
                            new Control()
                            {
                                id=5,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="CB",
                                selectItem = new List<SelectItemSpring>(),
                                controla = new List<int?[]>(){
                                    new int?[]{ 8, 7}
                                },
                                padreControla = new List<int?[]>(){
                                    new int?[]{ 8, 3}
                                },
                                nombreParametro="idProvincia",
                                buscarEn="api/spring/core/Provincia/listarActivosPorDepartamento",
                                nombreValueFromRest ="provincia",
                                nombreLabelFromRest="descripcioncorta",
                            },
                             new Control()
                            {
                                id=6,
                                clase="ui-g-12 ui-md-6 ui-lg-1",
                                tipoControl="LB",
                                texto="Distrito",
                            },
                             new Control()
                            {
                                id=7,
                                clase="ui-g-12 ui-md-6 ui-lg-2",
                                tipoControl="CB",
                                selectItem = new List<SelectItemSpring>(),
                                padreControla = new List<int?[]>(){
                                    new int?[]{ 8, 3},
                                    new int?[]{ 8, 5}
                                },
                                buscarEn="api/spring/core/Zonapostal/listarActivosPorProvincia",
                                nombreValueFromRest ="codigopostal",
                                nombreLabelFromRest="descripcioncorta",
                            },
                        }
                     }
                }
            };

            return Json(formulario);
        }

        [HttpPost("[action]")]
        public void grabarFormulario([FromBody]FormularioDatos datos)
        {
            throw new UException(new List<MensajeUsuario>() {
                    new MensajeUsuario(){
                           TipoMensaje=tipo_mensaje.ADVERTENCIA,
                           Mensaje="test 1"
                    },
                    new MensajeUsuario(){
                           TipoMensaje=tipo_mensaje.ERROR,
                           Mensaje="test 2"
                    },
                    new MensajeUsuario(){
                           TipoMensaje=tipo_mensaje.INFORMACION,
                           Mensaje="test 3"
                    },
                    new MensajeUsuario(){
                           TipoMensaje=tipo_mensaje.EXITO,
                           Mensaje="test 4"
                    }

            });
        }
        /*TEST*/


    }
    public class FormularioDatos
    {
        public IList<GrupoDato> grupos { get; set; }

        public FormularioDatos()
        {
            grupos = new List<GrupoDato>();
        }
    }

    public class GrupoDato
    {
        public int id { get; set; }
        public IList<Dato> datos { get; set; }
        public GrupoDato()
        {
            datos = new List<Dato>();
        }
    }
    public class Dato
    {
        public int id { get; set; }
        public string tipoDato { get; set; }
        public object valor { get; set; }
        public IList<object> valores { get; set; }

        public Dato()
        {
            valores = new List<object>();
        }
    }

    public class Formulario
    {
        public string nombre { get; set; }
        public IList<Grupo> grupos { get; set; }
    }

    public class Grupo
    {
        public int id { get; set; }
        public bool isFieldset { get; set; }
        public string fieldsetTitle { get; set; }
        public string clase { get; set; }
        public IList<Control> controles { get; set; }
    }

    public class Control
    {
        //campos comunes
        public Int32 id { get; set; }
        public String tipoControl { get; set; }
        public String clase { get; set; }
        //sino se le pone el ? , por default mandara un false
        public bool? editable { get; set; }
        public bool eliminar { get; set; }
        public int maxLength { get; set; }
        public bool requerido { get; set; }
        public string descripcionAtributoRequerido { get; set; }

        public string fieldSelector { get; set; }

        //en caso se oculte el control no se valida si es requerido
        public bool oculto { get; set; }

        //campos para el selector
        public String tipoSelector { get; set; }

        //campos para label
        public String texto { get; set; }

        //campos para una tabla
        public IList<Columna> columnas { get; set; }
        public IList<Object> columnasValores { get; set; }

        //campo para un combobox y radioButton
        public IList<SelectItemSpring> selectItem { get; set; }

        //campo para un numero
        public int minimo { get; set; }
        public int maximo { get; set; }

        public bool multiple { get; set; }

        public string expresionRegular { get; set; }
        public string mensajeExpresionRegular { get; set; }

        public Object defaultNuevoRegistro { get; set; }

        //datos para combo anidados
        //para llamar cuando hay un onchange
        public IList<int?[]> controla { get; set; }

        //para obtener el value del padre que lo controla
        public IList<int?[]> padreControla { get; set; }

        //para al enviar en el buscar, se coloque el nombre del parametro
        public string nombreParametro { get; set; }

        //para obtener la lista a partir de un servicio rest
        //convencion => 'api/spring/core/{contoller}/listarAnidado'
        public string buscarEn { get; set; }
        //
        public string nombreValueFromRest { get; set; }
        public string nombreLabelFromRest { get; set; }

    }

    public class SelectItemSpring
    {
        public string label { get; set; }
        public Object value { get; set; }
        //solo si un item lleva muestra = true, el ocultar funcionara cuando se cambie
        public IList<int?[]> muestra { get; set; }

        //solo si un item lleva habilita = true, el ocultar funcionara cuando se cambie
        public IList<int?[]> habilita { get; set; }
    }

    public class Columna
    {
        //nombre del campo del dto, igual que armar un datatable field="[campo]"
        public string field { get; set; }
        //cabecera
        public string header { get; set; }
        //alineamiento
        public string align { get; set; }

        //ancho de columna en %
        public int ancho { get; set; }

        //tipo de edicion
        public string tipoColumna { get; set; }

        //options en caso sea un combo
        public IList<SelectItemSpring> opciones { get; set; }

        //puede o no editar el contenido
        public bool editable { get; set; }

        //campos de tipo texto
        public int maxLength { get; set; }
        public string expresionRegular { get; set; }
        public string mensajeExpresionRegular { get; set; }

        //campos de tipo numero
        public int maximo { get; set; }
        public int minimo { get; set; }

        public bool requerido { get; set; }

        public Object defaultNuevoRegistro { get; set; }

        //campos para el selector
        public String tipoSelector { get; set; }

        //campo para componente tipo fecha
        public string formatoFechaInput { get; set; }
        public string formatoFechaOutput { get; set; }

        public string prefijo { get; set; }
    }
}

