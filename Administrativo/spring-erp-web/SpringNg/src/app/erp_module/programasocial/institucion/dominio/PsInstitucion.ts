export class PsInstitucionPk {
    idInstitucion: string;
}
export class PsInstitucion extends PsInstitucionPk {


    nombre: string;
    direccion: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

}
