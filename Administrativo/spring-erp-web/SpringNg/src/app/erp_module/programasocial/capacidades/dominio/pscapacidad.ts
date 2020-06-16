export class PsCapacidadPk {
    idInstitucion: string;
    idBeneficiario: number;
    idCapacidad: number;
}

export class PsCapacidad extends PsCapacidadPk {
    fechaInforme: Date;
    idTipoInstitucion: string;
    idPeriodo: string;
    idNivel: string;
    idGradoEducativo: string;
    anioRetraso: string;
    comentarioRetraso: string;
    idOrigen: string;

    comentarioTalleres: string;
    idRiesgoCaida: string;
    idHabilidadOcupacional: string;
    idEvaluarOcupacional: string;
    comentarioCapacidad: string;
    comentarioRendimiento: string;

    porcentajeGradoBarthel: number;
    porcentajeGradoKatz: number;
    gradoDependenciaBarthel: string;
    gradoDependenciaKatz: string;
    ocupacionAnterior: string;
    otrasEnfermedades: string;

    katz1: number;
    katz2: number;
    katz3: number;
    katz4: number;
    katz5: number;
    katz6: number;
    barthel1: number;
    barthel2: number;
    barthel3: number;
    barthel4: number;
    barthel5: number;
    barthel6: number;
    barthel7: number;
    barthel8: number;
    barthel9: number;
    barthel10: number;
    estado: string;
    creacionUsuario: string;
    creacionFecha: Date;
    creacionTerminal: string;
    modificacionUsuario: string;
    modificacionFecha: Date;
    modificacionTerminal: string;
    idSaanee: string;
    idIletrado: string;
    ValidarNinos: Boolean;
    evaluadoBoolean: Boolean;
    evaluado: string;
}
