import { PsBeneficiarioIngresoCasual } from "./psbeneficiarioingresocasual";
import { PsBeneficiarioIngresoDiagnostico } from "./psbeneficiarioingresodiagnostico";

export class PsBeneficiarioIngresoPk {
    idInstitucion: string;
    idBeneficiario: number;
    idIngreso:number;

}
export class PsBeneficiarioIngreso extends PsBeneficiarioIngresoPk {

    listaCausal: PsBeneficiarioIngresoCasual[];
    listaDiagnostico: PsBeneficiarioIngresoDiagnostico[];

    constructor() {
        super();
        this.listaCausal = [];
        this.listaDiagnostico = [];
    }


    fechaIngreso: Date;
    diasPermanencia: number;
    idInstitucionDeriva: string;
    idSituacionLegal: string;
    idArea: string;
    comentarios: string;
    fechaEgreso: Date;
    idMotivoEgreso: string;
    comentariosEgreso: string;
    estado: string;
    
}
