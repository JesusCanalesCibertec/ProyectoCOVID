import { Contratacionadendaentregable } from "../../contratacionadendaentregable/dominio/contratacionadendaentregable";

export class ContratacionPk{
    idContratacion: number;

}

export class Contratacion extends ContratacionPk {
    idPersona: number;
    numeroplazo: number;
    fechainicio: Date;
    fechafin: Date;
    idModalidad: string;
    numeroorden: string;
    cargo: string;
    servicio: string;
    descripcionservicio: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
    fechacese: Date;
    motivocese: string;

    constructor() {
        super();
        this.listadetalle = [];
    }

    listadetalle: Contratacionadendaentregable[];
}

