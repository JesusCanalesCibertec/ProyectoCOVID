import { dtoFechasdisponibles } from "./dtoFechasdisponibles";
import { Proyectorecursoperiodo } from "../../proyecto/dominio/proyectorecursoperiodo";

export class dtoContratacion {
    idContratacion: number;
    persona: string;
    fechainicio: Date;
    fechafin: Date;
    idModalidad: string;
    modalidad: string;
    numeroorden: string;
    idCargo: string;
    cargo: string;
    estado: string;
    idPersona: number;

    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    conocimientos: string;
    secuencia: number;

    //parametors no mapeados
    diasocupados: Date[];
    rango: Date[];
    rol: string;
    horas: number;

    listaPeriodos: Proyectorecursoperiodo[];

    constructor() {
        this.listaPeriodos = [];
    }


}