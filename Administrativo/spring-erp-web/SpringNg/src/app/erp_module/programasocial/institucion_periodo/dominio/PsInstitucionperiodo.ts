export class PsInstitucionperiodoPk {
    idInstitucion: string;
    idAplicacion: string;
    idGrupo: string;
    idConcepto: string;
}
export class PsInstitucionperiodo extends PsInstitucionperiodoPk {


    idPeriodo: string;
    fechainicio: Date;
    fechafin: Date;
    fechainicioregistro: Date;
    fechafinregistro:Date;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

}
