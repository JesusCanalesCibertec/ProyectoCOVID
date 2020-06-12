export class DtoCapacidad {
    fechaInforme: Date;
    nombreCompleto: string;
    institucionNombre: string;
    idTipoInstitucion: string;
    idBeneficiario: number;
    idInstitucion: string;
    idPeriodo: string;
    idNivel: string;
    idGradoEducativo: string;
    anioRetraso: string;
    comentarioRetraso: string;
    comentarioTalleres: string;
    idRiesgoCaida: string;
    idRiesgoCaidaDetalle: string;

    idHabilidadOcupacional: string;
    idEvaluarOcupacional: string;
    comentarioCapacidad: string;
    idTipoComunicacion: string;
    comentarioRendimiento: string;
    idOrigen: string;
    ocupacionAnterior: string;
    idCapacidad: number;
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
    gradoDependenciaBarthel: string;
    gradoDependenciaKatz: string;

    porcentajeGradoBarthel: number;
    porcentajeGradoKatz: number;

    // ************* CURSOS ****************//
    notaLogicoMatematico: string;
    notaComunicacion: string;
    notaComprensionLectora: string;
    notaPersonalSocial: string;
    notaCienciaAmbiente: string;

    idCurso: string;
    idNotaCurso: string;

    idCursoMatematica: string;
    idCursoComunicacion: string;
    idCursoCompresion: string;
    idCursopersonal: string;
    idCursoAmbiente: string;


    // ************* TALLERES ****************//
    idTaller: string;
    idNotaTaller: string;
    cantidadTaller: number;
    idNotaTallerArtistico: string;
    cantidadTallerArtistico: number;
    idNotaTallerFormativo: string;
    cantidadTallerFormativo: number;
    idNotaTallerDeportivo: string;
    cantidadTallerDeportivo: number;

    idTallerArtistico: string;
    idTallerFormativo: string;
    idTallerDeportivo: string;
    validarNinos: boolean;
    idSaanee: string;
    idIletrado: string;

    evaluadoBoolean: Boolean;
    evaluado: string;


    edad: number;
}

