import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';

export class FiltroRas {
    idInstitucion: string;
    idBeneficiario: number;
    idDiagnostico: number;
    idPeriodo: string;
    desde: number;
    hasta: number;
    idSexo: string;
    idArea: string;
    nombreCompleto: string;
    cantidadRegistros: number;
    fechaAtencion: Date;
    idTipoAtencion: string;
    fechaAtencionFin: Date;
    fechaAtencionInicio: Date;
    idPrograma: string;
    idComponente: string;
    idUsuario: string;
    nombreDiagnostico: string;

    orden: number;
    sentido: number;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
}
