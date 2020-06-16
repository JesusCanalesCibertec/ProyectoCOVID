export class ProyectorecursoperiodoPk {
    idProyecto: number;
    idPersona: number;
    idPeriodo: number;
}

export class Proyectorecursoperiodo extends ProyectorecursoperiodoPk {
    idContratacion: number;
    fechafin: Date;
    fechainicio: Date;
    horas: number;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
}

