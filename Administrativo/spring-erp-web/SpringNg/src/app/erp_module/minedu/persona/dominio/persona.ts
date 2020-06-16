export class PersonaPk {
    idPersona: number;

}

export class Persona extends PersonaPk {
    idPersona: number;
    dni: string;
    nombre: string;
    apellido: string;
    anexo: string;
    celular: string;
    correo: string;
    estado: string;
    correoalterno: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
    usuario: string;

    //Campos no mapeados de contratacion
    auxIdContratacion: number;
    numeroplazo: number;
    fechainicio: Date;
    fechafin: Date;
    idModalidad: string;
    numeroorden: string;
    cargo: string;
    fechacese: Date;
    motivocese: string;
}

