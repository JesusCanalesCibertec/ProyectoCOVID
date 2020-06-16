export class DtoSolicitudes {
    aplicacionCodigo: string;
    aplicacionNombre: string;

    procesoCodigo: string;
    procesoNombre: string;
    procesoNro: number;

    nivelActual: number;
    nivelAprobar: number;

    documentoReferencia: string;
    documentoFecha: Date;

    companiaCodigo: string;
    companiaNombre: string;

    unidadReplicacionCodigo: string;
    unidadReplicacionNombre: string;

    solicitanteId: number;
    solicitanteNombre: string;

    estadoCodigo: string;
    estadoNombre: string;

    procesoAprobar: number;
    procesoInternoAprobar: number;

    llave: string;
    observaciones: string;
    capacitaciones: string;

    fechaSolicitud: Date;
    uuid: string;

    flgSeleccionado: string;
    flgEsSuperUsuario: string;
    flgEsAdministrador: string;
    flgAdministradorApruebaTodo: string;
    flgTieneCorreos: string;
    flgEnviarSinCorreos: string;
    flgSolicitarObservaciones: string;
    flgSolicitarInformacionPrestamo: string;
    prestamoOtorgadoFlag: string;
    prestamoMonedaPago: string;
    prestamoTipoPago: string;
    prestamoNumeroRecibo: string;
    prestamoNumeroCheque: string;
    prestamoCuentaBancaria: string;
    observacionAccion: string;
    inicio: string;
    fin: string;
    concepto: string;
    desde: string;
    hasta: string;
    autorizado: string;
    aprobar: string;
    diasHabilies: number;
}
