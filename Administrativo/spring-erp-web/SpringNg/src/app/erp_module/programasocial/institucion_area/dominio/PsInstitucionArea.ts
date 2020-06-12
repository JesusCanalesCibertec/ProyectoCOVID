export class PsInstitucionAreaPk {
    idInstitucion: string;
    idArea: string;
}
export class PsInstitucionArea extends PsInstitucionAreaPk {
    nombre: string;
    estado: string;
    idPrograma: string;
    idComponente: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
}
