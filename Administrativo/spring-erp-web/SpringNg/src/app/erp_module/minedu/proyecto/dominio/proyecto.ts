import { Proyectorecurso } from "./proyectorecurso";

export class ProyectoPk{
    idProyecto: number;

}

export class Proyecto extends ProyectoPk {


    listaRecursos: Proyectorecurso[] = [];
    listaRecursos1: Proyectorecurso[] = [];
    listaRecursos2: Proyectorecurso[] = [];

    constructor() {
        super();
        this.listaRecursos = new Array<Proyectorecurso>();
        this.listaRecursos1 = new Array<Proyectorecurso>();
        this.listaRecursos2 = new Array<Proyectorecurso>();
    }


    codigoproyecto: string;
    nombre: string;
    nombrecorto: string;
    idSistema: number;
    siglas: string;
    gestor: number;
    gestoranterior: string;
    expediente:string;
    fechaexpediente: Date;
    tipoproyecto:string;
    coordinacion: string;
    area: number;
    fechainicio: Date;
    fechafin:Date;
    asunto: string;
    estadoatencion: string;
    fasegestion: string;
    tipoanalisis:string;
    faseingenieria: string;
    avanceplan: number;
    avancereal: number;
    ruta: string;
    proceso: string;
    observacion: string;

    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    numparticipantes: number;

    public static readonly APLICACION_CODIGO = 'MP';

    //tab1
    public static readonly MISCELANEO_TIPO_PROYECTO = 'TIPOPROYEC';
    public static readonly MISCELANEO_COORDINACIONES_MINEDU = 'COORDINAMP';

    //tab2
    public static readonly MISCELANEO_ROLES_PROYECTO = 'ROLPROYECT';

    //tab3
    public static readonly MISCELANEO_ESTADO_PROYECTO = 'ESTPROYECT';
    public static readonly MISCELANEO_FASE_GESTIONES = 'FASEGEST';
    public static readonly MISCELANEO_FASE_INGENIERIAS = 'FASEINGE ';
    public static readonly MISCELANEO_TIPO_INGENIERIA = 'TIPOINGE'

}

