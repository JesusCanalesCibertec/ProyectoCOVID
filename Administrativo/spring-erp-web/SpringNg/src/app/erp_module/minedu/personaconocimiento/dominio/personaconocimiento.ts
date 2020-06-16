export class PersonaconocimientoPk {
    idConocimiento: number;
    idPersona: number;
}
export class Personaconocimiento extends PersonaconocimientoPk {
    tipo:string;
    area:string;
    nivel:string;
    conocimiento: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
}
