import { Proyectorecurso, ProyectorecursoPk } from "./proyectorecurso";

export class dtoProyecto {

    constructor() {
        this.listaEquipo = [];
    }

    listaEquipo: Proyectorecurso[];

    tipoproyecto: string;
    nomtipoproyecto: string;
    idproyecto: number;
    nombre: string;
    area: number;
    niveles: string;
    fechainicio: Date;
    fechafin: Date;
    estado: string;
    nomestado: string;
    desviacion: number;
    planificado: number;
    real: number;

    idSIS: string;
    nombreSIS: string;
    nomcortoSIS: string;
    gestor: string;
    descripcion: string;
    nomArea: string;
    observacion: string;
    secuencia: number;
    expediente: string;
    fechaexpediente: Date;
    asunto: string;
    proceso: string;

    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    //no mapeados
    auxfechaexpediente: string;
    auxfechafin: string;
    auxdesviacion: string;
    auxplanificado: string;
    auxreal: string;
}