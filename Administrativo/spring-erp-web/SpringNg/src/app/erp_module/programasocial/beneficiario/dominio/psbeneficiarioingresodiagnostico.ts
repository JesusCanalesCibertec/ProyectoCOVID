export class PsBeneficiarioIngresoDiagnosticoPk {
    idInstitucion: number;
    idBenificiario: number;
    idIngreso: number;
    idDiagnostico: String;
}
export class PsBeneficiarioIngresoDiagnostico extends PsBeneficiarioIngresoDiagnosticoPk {
    auxDiagnostico: string;
}