export class PersonatituloPk {
    idPersona: number;
    idCarrera: string;
    idCentro: string;
    idNivel: string;
}
export class Personatitulo extends PersonatituloPk {
    idGrado: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
}
