export class PsBeneficiarioIngresoCasualPk {
    idInstitucion: number;
    idBenificiario: number;
    idIngreso: number;
    idCausal: string;
}
export class PsBeneficiarioIngresoCasual extends PsBeneficiarioIngresoCasualPk {
    auxCausal: string;
}
