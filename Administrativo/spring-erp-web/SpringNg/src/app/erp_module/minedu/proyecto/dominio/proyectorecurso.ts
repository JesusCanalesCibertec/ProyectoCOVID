import { Proyectorecursoperiodo } from "./proyectorecursoperiodo";
import { SelectItem } from "primeng/api";

export class ProyectorecursoPk {
    idProyecto: number;
    idRecurso: number;

}

export class Proyectorecurso extends ProyectorecursoPk {

    constructor() {
        super();
        this.listaPeriodos = [];
    }

    listaPeriodos: Proyectorecursoperiodo[];

    idPersona: number;
    area: number;
    nombre: string;
    cargo: string;
    rol: string;
    horas: number;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    //atributos no mapeados

    nomArea: string;
    secuencia: number;
    auxConocimientos: string;
    idContratacion: number;

    personas: SelectItem[] = [];
    auxiliar: number;



}

