export class PsBeneficiarioPk {
    idBeneficiario: number;
    idInstitucion: string;
}
export class PsBeneficiario extends PsBeneficiarioPk {

    idPrograma: string;
    idComponente: string;
    nombre: string;
    area: string;
    descripcion: string;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

}
