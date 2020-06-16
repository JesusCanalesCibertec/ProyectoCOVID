export class ContratacionadendaentregablePk {
    idContratacion: number;
    idCodigo: number;
}
export class Contratacionadendaentregable extends ContratacionadendaentregablePk {
    dias:number;
    descripcion:string;
    fechainicio:Date;
    fechafin: Date;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

    //no mapeadas
    auxDeshabilitado: Boolean = true;
    minimo: number;
    maximo: number;
    fechaminima: Date;
}
