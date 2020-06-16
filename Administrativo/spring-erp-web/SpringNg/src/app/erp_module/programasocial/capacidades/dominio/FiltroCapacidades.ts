import { ParametroPaginacionGenerico } from 'src/app/erp_module/shared/dominio/ParametroPaginacionGenerico';

export class FiltroCapacidades {
    estado: string;
    periodo: string;
    idInstitucion: string;
    cantidadRegistros: number;
    idArea: string;
    idSexo: string;
    nombreCompleto: string;
    idPrograma: string;
    idComponente: string;
    idUsuario: string;
    idBeneficiario: number;

    orden: number;
    sentido: number;

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    paginacion: ParametroPaginacionGenerico;
}
