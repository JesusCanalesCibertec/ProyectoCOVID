import { ParametroPaginacionGenerico } from '../../../../shared/dominio/ParametroPaginacionGenerico';
export class FiltroSolicitudes {

    constructor() {
        this.paginacion = new ParametroPaginacionGenerico();
    }
    Tipo: number;

    companiaSocio: string;
    aplicacion: string;
    proceso: number;
    numeroProceso: number;
    procesoInterno: number;
    unidadReplicacion: string;
    estado: string;
    documento: string;
    fechaDesde: Date;
    fechaHasta: Date;
    idPersonaSolicitante: number;
    idPersonaActual: number;
    nombreSolicitante: string;
    nivel: number;
    plan: number;
    paginacion: ParametroPaginacionGenerico;
    fechaSolicitud: Date;
    concepto: string;
    aprobar1: string;
    aprobar2: string;

}
