export class PsInstitucionAreaTrabajoPk {
    idInstitucion: string;
    idArea: string;
}
export class PsInstitucionAreaTrabajo extends PsInstitucionAreaTrabajoPk {

    nombre: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
    idPrograma: string;

}
