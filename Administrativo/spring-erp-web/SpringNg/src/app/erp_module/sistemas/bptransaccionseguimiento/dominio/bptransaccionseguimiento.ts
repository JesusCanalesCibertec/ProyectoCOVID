export class BpTransaccionSeguimientoPk {
    idTransaccion: number;
}
export class BpTransaccionSeguimiento extends BpTransaccionSeguimientoPk {

    entradaIdProceso: string;
    entradaIdElemento: number;
    entradaNombre: number;
    salidaIdProceso: string;
    salidaIdElemento: string;
    salidaNombre: string;
    fechaEvento: Date;
    idPersona: number;
    usuario: string;
    nombreCompleto: string;
    cargo: string;
    comentarios: number;
    estadoIdProceso: number;

    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;

}
